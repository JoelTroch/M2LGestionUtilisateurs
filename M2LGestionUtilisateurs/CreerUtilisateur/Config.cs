using System;
using System.Runtime.InteropServices;
using System.Text;

namespace CreerUtilisateur
{
    class Config
    {
        // Imports DLL
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string cle, string valeur, string fichier);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string cle, string defaut, StringBuilder valeurRetour, int taille, string fichier);
        
        // Attributs
        private string fichier;

        public Config(string fichier)
        {
            this.fichier = fichier;
        }

        // Méthodes
        public void ecrire(string section, string cle, string valeur)
        {
            WritePrivateProfileString(section, cle, valeur, this.fichier);
        }

        public string lire(string section, string cle, string defaut)
        {
            StringBuilder constructeur = new StringBuilder(255);
            int i = GetPrivateProfileString(section, cle, defaut, constructeur, 255, this.fichier);
            return constructeur.ToString();
        }
    }
}
