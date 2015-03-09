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
            try
            {

                DirectoryEntry Ldap = new DirectoryEntry("LDAP://votre-nom-AD", "Login", "Password");

            }

            catch (Exception Ex)
            {

                Console.WriteLine(Ex.Message);

            }
        }
    }
}
