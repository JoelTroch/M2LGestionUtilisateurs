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
                    DirectoryEntry ldapServeur = new DirectoryEntry("LDAP://172.16.0.2/OU=usersM2L", "Administrateur", "Thoughtpolice2008");
                    DirectorySearcher ldapRecherche = new DirectorySearcher(ldapServeur);
                    ldapRecherche.Filter = "(objectClass=user)";
                    SearchResultCollection ldapResultat = ldapRecherche.FindAll();
                    if (ldapResultat.Count > 0)
                    {
                        foreach (SearchResult ldapUtilisateurActuel in ldapResultat)
                        {
                            // Récupération des infos utilisateur
                            DirectoryEntry ldapUtilisateur = ldapUtilisateurActuel.GetDirectoryEntry();
                            string ldapNom = ldapUtilisateur.Properties["sn"].Value.ToString();
                            string ldapPrenom = ldapUtilisateur.Properties["givenName"].Value.ToString();
                            string ldapEmail = ldapUtilisateur.Properties["mail"].Value.ToString();

                            // On va ordonner les utilisateurs qui ont aucune modification de ne pas les traiter
                            int i = 0;
                            bool ok = false;
                            while (i < listeUtilisateurs.Count && !ok)
                            {
                                string bddLogin = ldapNom.ToLower() + ldapPrenom.Substring(0, 1).ToLower();
                                if (listeUtilisateurs[i].getNom() == bddLogin && listeUtilisateurs[i].getEmail() == ldapEmail)
                                {
                                    listeUtilisateurs.RemoveAt(i);
                                    ok = true;
                                }
                            }
                        }
                    }
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
