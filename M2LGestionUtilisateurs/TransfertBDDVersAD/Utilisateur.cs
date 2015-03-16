using System;

namespace TransfertBDDVersAD
{
    public class Utilisateur
    {
        // Attributs
        private int id;
        private int niveau;
        private string nom;
        private string motDePasse;
        private string email;

        // Constructeur
        public Utilisateur(int id, int niveau, string nom, string motDePasse, string email)
        {
            this.id = id;
            this.niveau = niveau;
            this.nom = nom;
            this.motDePasse = motDePasse;
            this.email = email;
        }

        // Accesseurs
        public int getId()
        {
            return this.id;
        }

        public int getNiveau()
        {
            return this.niveau;
        }

        public string getNom()
        {
            return this.nom;
        }

        public string getMotDePasse()
        {
            return this.motDePasse;
        }
        
        public string getEmail()
        {
            return this.email;
        }
    }
}
