namespace CreerUtilisateur
{
    partial class FrmAccueil
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnInserer = new System.Windows.Forms.Button();
            this.txtBoxLogin = new System.Windows.Forms.TextBox();
            this.txtBoxMotDePasse = new System.Windows.Forms.TextBox();
            this.txtBoxEmail = new System.Windows.Forms.TextBox();
            this.txtLogin = new System.Windows.Forms.Label();
            this.txtMotDePasse = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnInserer
            // 
            this.btnInserer.Location = new System.Drawing.Point(95, 198);
            this.btnInserer.Name = "btnInserer";
            this.btnInserer.Size = new System.Drawing.Size(250, 23);
            this.btnInserer.TabIndex = 0;
            this.btnInserer.Text = "Insérer";
            this.btnInserer.UseVisualStyleBackColor = true;
            this.btnInserer.Click += new System.EventHandler(this.btnInserer_Click);
            // 
            // txtBoxLogin
            // 
            this.txtBoxLogin.Location = new System.Drawing.Point(95, 70);
            this.txtBoxLogin.Name = "txtBoxLogin";
            this.txtBoxLogin.Size = new System.Drawing.Size(250, 20);
            this.txtBoxLogin.TabIndex = 3;
            this.txtBoxLogin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.boutonEntree);
            // 
            // txtBoxMotDePasse
            // 
            this.txtBoxMotDePasse.Location = new System.Drawing.Point(95, 96);
            this.txtBoxMotDePasse.Name = "txtBoxMotDePasse";
            this.txtBoxMotDePasse.PasswordChar = '*';
            this.txtBoxMotDePasse.Size = new System.Drawing.Size(250, 20);
            this.txtBoxMotDePasse.TabIndex = 4;
            this.txtBoxMotDePasse.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.boutonEntree);
            // 
            // txtBoxEmail
            // 
            this.txtBoxEmail.Location = new System.Drawing.Point(95, 122);
            this.txtBoxEmail.Name = "txtBoxEmail";
            this.txtBoxEmail.Size = new System.Drawing.Size(250, 20);
            this.txtBoxEmail.TabIndex = 5;
            this.txtBoxEmail.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.boutonEntree);
            // 
            // txtLogin
            // 
            this.txtLogin.AutoSize = true;
            this.txtLogin.Location = new System.Drawing.Point(13, 77);
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Size = new System.Drawing.Size(39, 13);
            this.txtLogin.TabIndex = 7;
            this.txtLogin.Text = "Login :";
            // 
            // txtMotDePasse
            // 
            this.txtMotDePasse.AutoSize = true;
            this.txtMotDePasse.Location = new System.Drawing.Point(13, 103);
            this.txtMotDePasse.Name = "txtMotDePasse";
            this.txtMotDePasse.Size = new System.Drawing.Size(77, 13);
            this.txtMotDePasse.TabIndex = 8;
            this.txtMotDePasse.Text = "Mot de passe :";
            // 
            // txtEmail
            // 
            this.txtEmail.AutoSize = true;
            this.txtEmail.Location = new System.Drawing.Point(13, 125);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(38, 13);
            this.txtEmail.TabIndex = 9;
            this.txtEmail.Text = "Email :";
            // 
            // FrmAccueil
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(357, 244);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtMotDePasse);
            this.Controls.Add(this.txtLogin);
            this.Controls.Add(this.txtBoxEmail);
            this.Controls.Add(this.txtBoxMotDePasse);
            this.Controls.Add(this.txtBoxLogin);
            this.Controls.Add(this.btnInserer);
            this.Name = "FrmAccueil";
            this.Text = "Gestion Utilisateur - Insertion";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnInserer;
        private System.Windows.Forms.TextBox txtBoxLogin;
        private System.Windows.Forms.TextBox txtBoxMotDePasse;
        private System.Windows.Forms.TextBox txtBoxEmail;
        private System.Windows.Forms.Label txtLogin;
        private System.Windows.Forms.Label txtMotDePasse;
        private System.Windows.Forms.Label txtEmail;
    }
}

