using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace TransfertBDDVersAD
{
    class Program
    {
        static void Main()
        {
            // Lecture de la configuration
            string repertoireActuel = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Config configuration = new Config(repertoireActuel + "\\config.ini");

            // Connection à la base de données
            MySqlConnection bddConnection = new MySqlConnection("SERVER=" + configuration.lire("mysql", "host", "localhost") +
                ";DATABASE=" + configuration.lire("mysql", "database", "mrbs") + ";UID=" + configuration.lire("mysql", "user", "mrbs") +
                ";PASSWORD=" + configuration.lire("mysql", "password", "mrbs"));
            List<Utilisateur> listeUtilisateurs = new List<Utilisateur>();
            try
            {
                bddConnection.Open();

                // Récupération de tous les utilisateurs
                string bddSQLRecupererUtilisateurs = "SELECT * FROM mrbs_users";
                MySqlCommand bddCmdRecupererUtilisateurs = new MySqlCommand(bddSQLRecupererUtilisateurs, bddConnection);
                MySqlDataReader bddDataReaderRecupererUtilisateurs = bddCmdRecupererUtilisateurs.ExecuteReader();
                while (bddDataReaderRecupererUtilisateurs.Read())
                    listeUtilisateurs.Add(new Utilisateur(Convert.ToInt32(bddDataReaderRecupererUtilisateurs["id"]),
                        Convert.ToInt32(bddDataReaderRecupererUtilisateurs["level"]),
                        bddDataReaderRecupererUtilisateurs["name"].ToString(),
                        bddDataReaderRecupererUtilisateurs["password"].ToString(),
                        bddDataReaderRecupererUtilisateurs["email"].ToString()));

                bddConnection.Close();
            }
            catch (Exception erreur)
            {
                MessageBox.Show(erreur.Message, "Erreur lors avec la base de données", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Il y a du traitement à faire
            if (listeUtilisateurs.Count > 0)
            {
                try
                {
                    // Connexion au LDAP
                    DirectoryEntry ldapServeur = new DirectoryEntry("LDAP://" + configuration.lire("ldap", "host", "169.254.36.173") + "/OU=usersM2L,DC=m2l,DC=fr", configuration.lire("ldap", "user", "Administrateur"), configuration.lire("ldap", "password", "Thoughtpolice2008"));

                    // Récupération de tous les utilisateurs dans le LDAP
                    DirectorySearcher ldapRecherche = new DirectorySearcher(ldapServeur);
                    ldapRecherche.Filter = "(objectClass=user)";
                    SearchResultCollection ldapResultat = ldapRecherche.FindAll();

                    // On supprime tous les utilisateurs dans le LDAP
                    foreach (SearchResult ldapUtilisateurActuel in ldapResultat)
                    {
                        DirectoryEntry ldapUtilisateur = ldapUtilisateurActuel.GetDirectoryEntry();
                        ldapUtilisateur.DeleteTree();
                        ldapUtilisateur.CommitChanges();
                    }

                    // Pour ensuite les rajouter dans le LDAP
                    foreach (Utilisateur unUtilisateur in listeUtilisateurs)
                    {
                        DirectoryEntry ldapUtilisateurActuel = ldapServeur.Children.Add("cn=" + unUtilisateur.getNom(), "user");
                        ldapUtilisateurActuel.Properties["SAMAccountName"].Add(unUtilisateur.getNom());
                        ldapUtilisateurActuel.Properties["mail"].Add(unUtilisateur.getEmail());
                        //ldapUtilisateurActuel.Properties["Description"].Add("ID BDD = " + unUtilisateur.getId());
                        ldapUtilisateurActuel.CommitChanges();
                        ldapUtilisateurActuel.Properties["userAccountControl"].Value = 0x0020;
                        ldapUtilisateurActuel.CommitChanges();
                    }

                    /*if (ldapResultat.Count > 0)
                    {
                        foreach (SearchResult ldapUtilisateurActuel in ldapResultat)
                        {
                            // Récupération des infos utilisateur
                            DirectoryEntry ldapUtilisateur = ldapUtilisateurActuel.GetDirectoryEntry();
                            string ldapNom = ldapUtilisateur.Properties["sn"].Value.ToString();
                            string ldapPrenom = ldapUtilisateur.Properties["givenName"].Value.ToString();
                            string ldapEmail = ldapUtilisateur.Properties["mail"].Value.ToString();

                            // Traitement des utilisateurs
                            int i = 0;
                            bool ok = false;
                            while (i < listeUtilisateurs.Count && !ok)
                            {
                                string bddLogin = ldapNom.ToLower() + ldapPrenom.Substring(0, 1).ToLower();
                                if (listeUtilisateurs[i].getNom() == bddLogin && listeUtilisateurs[i].getEmail() == ldapEmail) // Tout est identique, aucune MAJ à faire
                                {
                                    listeUtilisateurs.RemoveAt(i);
                                    ok = true;
                                }
                                else if (listeUtilisateurs[i].getNom() == bddLogin && listeUtilisateurs[i].getEmail() != ldapEmail) // Login identique mais e-mail identique
                                {
                                    ldapUtilisateur.Properties["mail"].Value = listeUtilisateurs[i].getEmail();
                                    ldapUtilisateur.CommitChanges();
                                    listeUtilisateurs.RemoveAt(i);
                                    ok = true;
                                }
                                else
                                    i++;
                            }
                        }
                    }*/
                    MessageBox.Show("Tous les utilisateurs de la BDD sont présents dans l'AD.", "Ajout avec succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception erreur)
                {
                    MessageBox.Show(erreur.Message, "Erreur avec l\'Active Directory", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("Aucun utilisateur à traiter.", "Aucun utilisateur à traiter", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
