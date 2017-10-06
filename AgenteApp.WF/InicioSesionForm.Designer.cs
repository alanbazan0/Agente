namespace AgenteApp.WF
{
    partial class InicioSesionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InicioSesionForm));
            this.usuarioTextBox = new System.Windows.Forms.TextBox();
            this.contrasenaTextBox = new System.Windows.Forms.TextBox();
            this.inicarSesionButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // usuarioTextBox
            // 
            this.usuarioTextBox.Location = new System.Drawing.Point(155, 126);
            this.usuarioTextBox.Name = "usuarioTextBox";
            this.usuarioTextBox.Size = new System.Drawing.Size(100, 20);
            this.usuarioTextBox.TabIndex = 0;
            // 
            // contrasenaTextBox
            // 
            this.contrasenaTextBox.Location = new System.Drawing.Point(155, 177);
            this.contrasenaTextBox.Name = "contrasenaTextBox";
            this.contrasenaTextBox.Size = new System.Drawing.Size(100, 20);
            this.contrasenaTextBox.TabIndex = 1;
            // 
            // inicarSesionButton
            // 
            this.inicarSesionButton.Location = new System.Drawing.Point(155, 240);
            this.inicarSesionButton.Name = "inicarSesionButton";
            this.inicarSesionButton.Size = new System.Drawing.Size(75, 23);
            this.inicarSesionButton.TabIndex = 2;
            this.inicarSesionButton.Text = "Iniciar sesión";
            this.inicarSesionButton.UseVisualStyleBackColor = true;
            this.inicarSesionButton.Click += new System.EventHandler(this.inicarSesionButton_Click);
            // 
            // InicioSesionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 367);
            this.Controls.Add(this.inicarSesionButton);
            this.Controls.Add(this.contrasenaTextBox);
            this.Controls.Add(this.usuarioTextBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "InicioSesionForm";
            this.Text = "Inicio de sesión";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox usuarioTextBox;
        private System.Windows.Forms.TextBox contrasenaTextBox;
        private System.Windows.Forms.Button inicarSesionButton;
    }
}

