using System;
using System.Collections.Generic;
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
            MySqlConnection bddConnection = new MySqlConnection("SERVER=" + configuration.lire("host", "localhost") +
                ";DATABASE=" + configuration.lire("database", "mrbs") + ";UID=" + configuration.lire("user", "mrbs") +
                ";PASSWORD=" + configuration.lire("password", "mrbs"));
            try
            {
                bddConnection.Open();

                // Récupération de tous les utilisateurs
                List<Utilisateur> listeUtilisateurs = new List<Utilisateur>();
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
                MessageBox.Show(erreur.Message, "Erreur lors de la connection à la base de données", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
