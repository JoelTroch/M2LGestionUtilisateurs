using System;
using System.Diagnostics;
using System.DirectoryServices;
using System.IO;
using System.Reflection;
using System.ServiceProcess;
using System.Timers;

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
                // Connexion au LDAP
                DirectoryEntry ldapServeur = new DirectoryEntry("LDAP://" + configuration.lire("ldap", "host", "169.254.36.173") + "/OU=usersM2L,DC=m2l,DC=fr", configuration.lire("ldap", "user", "Administrateur"), configuration.lire("ldap", "password", "Thoughtpolice2008"));

                // Récupération de tous les utilisateurs dans le LDAP
                DirectorySearcher ldapRecherche = new DirectorySearcher(ldapServeur);
                ldapRecherche.Filter = "(objectClass=user)";
                SearchResultCollection ldapResultat = ldapRecherche.FindAll();
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
