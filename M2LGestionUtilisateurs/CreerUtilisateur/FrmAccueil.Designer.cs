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
            this.txtBoxNiveau = new System.Windows.Forms.TextBox();
            this.txtBoxLogin = new System.Windows.Forms.TextBox();
            this.txtBoxMotDePasse = new System.Windows.Forms.TextBox();
            this.txtBoxEmail = new System.Windows.Forms.TextBox();
            this.txtNiveau = new System.Windows.Forms.Label();
            this.txtLogin = new System.Windows.Forms.Label();
            this.txtMotDePasse = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnInserer
            // 
            this.btnInserer.Location = new System.Drawing.Point(95, 226);
            this.btnInserer.Name = "btnInserer";
            this.btnInserer.Size = new System.Drawing.Size(75, 23);
            this.btnInserer.TabIndex = 0;
            this.btnInserer.Text = "Inserer";
            this.btnInserer.UseVisualStyleBackColor = true;
            // 
            // txtBoxNiveau
            // 
            this.txtBoxNiveau.Location = new System.Drawing.Point(95, 69);
            this.txtBoxNiveau.Name = "txtBoxNiveau";
            this.txtBoxNiveau.Size = new System.Drawing.Size(177, 20);
            this.txtBoxNiveau.TabIndex = 2;
            // 
            // txtBoxLogin
            // 
            this.txtBoxLogin.Location = new System.Drawing.Point(95, 96);
            this.txtBoxLogin.Name = "txtBoxLogin";
            this.txtBoxLogin.Size = new System.Drawing.Size(177, 20);
            this.txtBoxLogin.TabIndex = 3;
            // 
            // txtBoxMotDePasse
            // 
            this.txtBoxMotDePasse.Location = new System.Drawing.Point(95, 123);
            this.txtBoxMotDePasse.Name = "txtBoxMotDePasse";
            this.txtBoxMotDePasse.Size = new System.Drawing.Size(177, 20);
            this.txtBoxMotDePasse.TabIndex = 4;
            // 
            // txtBoxEmail
            // 
            this.txtBoxEmail.Location = new System.Drawing.Point(95, 150);
            this.txtBoxEmail.Name = "txtBoxEmail";
            this.txtBoxEmail.Size = new System.Drawing.Size(177, 20);
            this.txtBoxEmail.TabIndex = 5;
            // 
            // txtNiveau
            // 
            this.txtNiveau.AutoSize = true;
            this.txtNiveau.Location = new System.Drawing.Point(12, 76);
            this.txtNiveau.Name = "txtNiveau";
            this.txtNiveau.Size = new System.Drawing.Size(47, 13);
            this.txtNiveau.TabIndex = 6;
            this.txtNiveau.Text = "Niveau :";
            // 
            // txtLogin
            // 
            this.txtLogin.AutoSize = true;
            this.txtLogin.Location = new System.Drawing.Point(12, 103);
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Size = new System.Drawing.Size(39, 13);
            this.txtLogin.TabIndex = 7;
            this.txtLogin.Text = "Login :";
            // 
            // txtMotDePasse
            // 
            this.txtMotDePasse.AutoSize = true;
            this.txtMotDePasse.Location = new System.Drawing.Point(13, 129);
            this.txtMotDePasse.Name = "txtMotDePasse";
            this.txtMotDePasse.Size = new System.Drawing.Size(77, 13);
            this.txtMotDePasse.TabIndex = 8;
            this.txtMotDePasse.Text = "Mot de passe :";
            // 
            // txtEmail
            // 
            this.txtEmail.AutoSize = true;
            this.txtEmail.Location = new System.Drawing.Point(13, 156);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(38, 13);
            this.txtEmail.TabIndex = 9;
            this.txtEmail.Text = "Email :";
            // 
            // FrmAccueil
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtMotDePasse);
            this.Controls.Add(this.txtLogin);
            this.Controls.Add(this.txtNiveau);
            this.Controls.Add(this.txtBoxEmail);
            this.Controls.Add(this.txtBoxMotDePasse);
            this.Controls.Add(this.txtBoxLogin);
            this.Controls.Add(this.txtBoxNiveau);
            this.Controls.Add(this.btnInserer);
            this.Name = "FrmAccueil";
            this.Text = "Gestion Utilisateur - Insertion";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnInserer;
        private System.Windows.Forms.TextBox txtBoxNiveau;
        private System.Windows.Forms.TextBox txtBoxLogin;
        private System.Windows.Forms.TextBox txtBoxMotDePasse;
        private System.Windows.Forms.TextBox txtBoxEmail;
        private System.Windows.Forms.Label txtNiveau;
        private System.Windows.Forms.Label txtLogin;
        private System.Windows.Forms.Label txtMotDePasse;
        private System.Windows.Forms.Label txtEmail;
    }
}

