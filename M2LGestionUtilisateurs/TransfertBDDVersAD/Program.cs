using System;
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

            // Connection base de données
            MySqlConnection bddConnection = new MySqlConnection("SERVER=" + configuration.lire("host", "localhost") +
                ";DATABASE=" + configuration.lire("database", "mrbs") + ";UID=" + configuration.lire("user", "mrbs") +
                ";PASSWORD=" + configuration.lire("password", "mrbs"));
            try
            {
                bddConnection.Open();

                bddConnection.Close();
            }
            catch (Exception erreur)
            {
                MessageBox.Show(erreur.Message, "Erreur lors de la connection à la base de données", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
