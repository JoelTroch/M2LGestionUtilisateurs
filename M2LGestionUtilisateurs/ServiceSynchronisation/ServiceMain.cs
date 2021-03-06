﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices;
using System.IO;
using System.Reflection;
using System.ServiceProcess;
using System.Timers;
using MySql.Data.MySqlClient;

namespace ServiceSynchronisation
{
    public partial class ServiceMain : ServiceBase
    {
        private Config configuration = null;

        public ServiceMain()
        {
            InitializeComponent();
            this.AutoLog = false;
            logger = new EventLog();
            if (!EventLog.SourceExists("ServiceSynchronisation"))
                EventLog.CreateEventSource("ServiceSynchronisation", "ServiceSynchronisationLog");

            logger.Source = "ServiceSynchronisation";
            logger.Log = "ServiceSynchronisationLog";
        }

        protected override void OnStart(string[] args)
        {
            logger.WriteEntry("Démarrage, date UTC = " + DateTime.UtcNow.ToString(), EventLogEntryType.Information);
            base.OnStart(args);

            // Lecture de la configuration
            string repertoireActuel = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            configuration = new Config(repertoireActuel + "\\config.ini");

            // Prépare le timer
            Timer compteur = new Timer();
            compteur.Interval = double.Parse(configuration.lire("service", "intervalle", "30"));
            compteur.Elapsed += new ElapsedEventHandler(EffectueMAJ);
            compteur.Start();
        }

        public void EffectueMAJ(object sender, ElapsedEventArgs args)
        {
            if (configuration != null)
            {
                List<Utilisateur> listeUtilisateurs = new List<Utilisateur>();
                try
                {
                    // Connexion au LDAP
                    DirectoryEntry ldapServeur = new DirectoryEntry("LDAP://" + configuration.lire("ldap", "host", "169.254.36.173") + "/OU=usersM2L,DC=m2l,DC=fr", configuration.lire("ldap", "user", "Administrateur"), configuration.lire("ldap", "password", "Thoughtpolice2008"));

                    // Récupération de tous les utilisateurs dans le LDAP
                    DirectorySearcher ldapRecherche = new DirectorySearcher(ldapServeur);
                    ldapRecherche.Filter = "(objectClass=user)";
                    SearchResultCollection ldapResultat = ldapRecherche.FindAll();

                    // Pour chaque utilisateur dans le LDAP
                    foreach (SearchResult ldapUtilisateurActuel in ldapResultat)
                    {
                        DirectoryEntry ldapUtilisateur = ldapUtilisateurActuel.GetDirectoryEntry();
                        string userNom = ldapUtilisateur.Properties["SAMAccountName"].Value.ToString();
                        string userEmail = ldapUtilisateur.Properties["mail"].Value.ToString();
                        Utilisateur user = new Utilisateur(0, 0, userNom, "02975090013d1741f202efd1262ed14c", userEmail);
                        listeUtilisateurs.Add(user);
                    }
                    ldapServeur.Close();

                    if (listeUtilisateurs.Count > 0)
                    {
                        // Connection à la base de données
                        MySqlConnection bddConnection = new MySqlConnection("SERVER=" + configuration.lire("mysql", "host", "localhost") +
                            ";DATABASE=" + configuration.lire("mysql", "database", "mrbs") + ";UID=" + configuration.lire("mysql", "user", "mrbs") +
                            ";PASSWORD=" + configuration.lire("mysql", "password", "mrbs"));

                        // Nettoyer la BDD
                        bddConnection.Open();
                        MySqlCommand bddCommandeNettoyage = new MySqlCommand("DELETE * FROM mrbs_users", bddConnection);
                        bddCommandeNettoyage.ExecuteNonQuery();

                        // Préparation requête SQL
                        string requete = "INSERT INTO mrbs_users(name, password, email) VALUES";
                        for (int i = 0; i < listeUtilisateurs.Count; i++)
                        {
                            string caractereFinal = ",";
                            if (i == (listeUtilisateurs.Count - 1))
                                caractereFinal = ";";

                            requete += "('" + listeUtilisateurs[i].getNom() + "', '" + listeUtilisateurs[i].getMotDePasse() + "', '" +
                                listeUtilisateurs[i].getEmail() + "')" + caractereFinal;
                        }

                        // Effectue l'insertion
                        MySqlCommand bddCommandeInsertion = new MySqlCommand(requete, bddConnection);
                        bddCommandeInsertion.ExecuteNonQuery();

                        // Terminé
                        bddConnection.Close();
                    }
                    else
                        logger.WriteEntry("Aucune insertion doit être effectuée.", EventLogEntryType.Information);
                }
                catch (Exception erreur)
                {
                    logger.WriteEntry("Erreur, message = " + erreur.Message, EventLogEntryType.Error);
                }
            }
            else
                logger.WriteEntry("Pas de pointeur sur la classe configuration !", EventLogEntryType.Error);
        }

        protected override void OnStop()
        {
            logger.WriteEntry("Arrêt, date UTC = " + DateTime.UtcNow.ToString(), EventLogEntryType.Information);
            base.Stop();
        }
    }
}
