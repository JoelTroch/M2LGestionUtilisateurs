using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.DirectoryServices;

namespace CreerUtilisateur
{
    public partial class FrmAccueil : Form
    {
        public FrmAccueil()
        {
            InitializeComponent();
        }

        private void btnInserer_Click(object sender, EventArgs e)
        {
            if (txtBoxLogin.Text == "" || txtBoxMotDePasse.Text == "" || txtBoxEmail.Text == "" )
            {
                MessageBox.Show(this, "Veuillez remplir les informations", "Veuillez remplir toutes les informations !", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    //Connexion au LDAP
                    DirectoryEntry Ldap = new DirectoryEntry("LDAP://169.254.36.173/OU=usersM2L,DC=m2l,DC=fr", "Administrateur", "Thoughtpolice2008");

                    string login = txtBoxLogin.Text;
                    string motDePasse = txtBoxMotDePasse.Text;
                    string email = txtBoxEmail.Text;

                    DirectoryEntry user = Ldap.Children.Add("cn=" + login, "user");
                    user.Properties["SAMAccountName"].Add(login); //Login
                    user.Properties["mail"].Add(email); //Email
                    user.CommitChanges();
                    //user.Invoke("SetPassword", new object[] { motDePasse }); //MotDePasse
                    user.Properties["userAccountControl"].Value = 0x0020;
                    user.CommitChanges();
                    MessageBox.Show("Ajout à l'AD avec succès.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception erreur)
                {
                    MessageBox.Show(erreur.Message, "Erreur lors de l'ajout à l'AD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
