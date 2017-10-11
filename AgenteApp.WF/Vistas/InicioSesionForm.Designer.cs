namespace AgenteApp.WF.Vistas
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
            this.topPanel = new System.Windows.Forms.Panel();
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.leftPanel = new System.Windows.Forms.Panel();
            this.rightPanel = new System.Windows.Forms.Panel();
            this.usuarioLabel = new System.Windows.Forms.Label();
            this.contrasenaLabel = new System.Windows.Forms.Label();
            this.barraSuperiorPanel = new System.Windows.Forms.Panel();
            this.tituloLabel = new System.Windows.Forms.Label();
            this.minimizeButton = new System.Windows.Forms.Button();
            this.cerrarButton = new System.Windows.Forms.Button();
            this.barraSuperiorPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // usuarioTextBox
            // 
            this.usuarioTextBox.Location = new System.Drawing.Point(163, 69);
            this.usuarioTextBox.Name = "usuarioTextBox";
            this.usuarioTextBox.Size = new System.Drawing.Size(161, 20);
            this.usuarioTextBox.TabIndex = 0;
            this.usuarioTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.contrasenaTextBox_KeyDown);
            // 
            // contrasenaTextBox
            // 
            this.contrasenaTextBox.Location = new System.Drawing.Point(163, 104);
            this.contrasenaTextBox.Name = "contrasenaTextBox";
            this.contrasenaTextBox.PasswordChar = '*';
            this.contrasenaTextBox.Size = new System.Drawing.Size(161, 20);
            this.contrasenaTextBox.TabIndex = 1;
            this.contrasenaTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.contrasenaTextBox_KeyDown);
            // 
            // inicarSesionButton
            // 
            this.inicarSesionButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.inicarSesionButton.FlatAppearance.BorderSize = 0;
            this.inicarSesionButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.inicarSesionButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.inicarSesionButton.ForeColor = System.Drawing.Color.White;
            this.inicarSesionButton.Location = new System.Drawing.Point(226, 140);
            this.inicarSesionButton.Name = "inicarSesionButton";
            this.inicarSesionButton.Size = new System.Drawing.Size(98, 23);
            this.inicarSesionButton.TabIndex = 2;
            this.inicarSesionButton.Text = "Iniciar sesión";
            this.inicarSesionButton.UseVisualStyleBackColor = false;
            this.inicarSesionButton.Click += new System.EventHandler(this.inicarSesionButton_Click);
            this.inicarSesionButton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.contrasenaTextBox_KeyDown);
            // 
            // topPanel
            // 
            this.topPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.ForeColor = System.Drawing.Color.Black;
            this.topPanel.Location = new System.Drawing.Point(1, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(406, 1);
            this.topPanel.TabIndex = 4;
            // 
            // bottomPanel
            // 
            this.bottomPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel.ForeColor = System.Drawing.Color.Black;
            this.bottomPanel.Location = new System.Drawing.Point(1, 200);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(406, 1);
            this.bottomPanel.TabIndex = 6;
            // 
            // leftPanel
            // 
            this.leftPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.leftPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.leftPanel.ForeColor = System.Drawing.Color.Black;
            this.leftPanel.Location = new System.Drawing.Point(0, 0);
            this.leftPanel.Name = "leftPanel";
            this.leftPanel.Size = new System.Drawing.Size(1, 201);
            this.leftPanel.TabIndex = 7;
            // 
            // rightPanel
            // 
            this.rightPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.rightPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.rightPanel.ForeColor = System.Drawing.Color.Black;
            this.rightPanel.Location = new System.Drawing.Point(407, 0);
            this.rightPanel.Name = "rightPanel";
            this.rightPanel.Size = new System.Drawing.Size(1, 201);
            this.rightPanel.TabIndex = 5;
            // 
            // usuarioLabel
            // 
            this.usuarioLabel.AutoSize = true;
            this.usuarioLabel.ForeColor = System.Drawing.Color.White;
            this.usuarioLabel.Location = new System.Drawing.Point(73, 72);
            this.usuarioLabel.Name = "usuarioLabel";
            this.usuarioLabel.Size = new System.Drawing.Size(43, 13);
            this.usuarioLabel.TabIndex = 8;
            this.usuarioLabel.Text = "Usuario";
            // 
            // contrasenaLabel
            // 
            this.contrasenaLabel.AutoSize = true;
            this.contrasenaLabel.ForeColor = System.Drawing.Color.White;
            this.contrasenaLabel.Location = new System.Drawing.Point(73, 107);
            this.contrasenaLabel.Name = "contrasenaLabel";
            this.contrasenaLabel.Size = new System.Drawing.Size(61, 13);
            this.contrasenaLabel.TabIndex = 9;
            this.contrasenaLabel.Text = "Contraseña";
            // 
            // barraSuperiorPanel
            // 
            this.barraSuperiorPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.barraSuperiorPanel.Controls.Add(this.tituloLabel);
            this.barraSuperiorPanel.Controls.Add(this.minimizeButton);
            this.barraSuperiorPanel.Controls.Add(this.cerrarButton);
            this.barraSuperiorPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.barraSuperiorPanel.Location = new System.Drawing.Point(1, 1);
            this.barraSuperiorPanel.Name = "barraSuperiorPanel";
            this.barraSuperiorPanel.Size = new System.Drawing.Size(406, 25);
            this.barraSuperiorPanel.TabIndex = 10;
            // 
            // tituloLabel
            // 
            this.tituloLabel.AutoSize = true;
            this.tituloLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tituloLabel.ForeColor = System.Drawing.Color.White;
            this.tituloLabel.Location = new System.Drawing.Point(5, 6);
            this.tituloLabel.Name = "tituloLabel";
            this.tituloLabel.Size = new System.Drawing.Size(97, 13);
            this.tituloLabel.TabIndex = 10;
            this.tituloLabel.Text = "Sesión de inicio";
            this.tituloLabel.Click += new System.EventHandler(this.tituloLabel_Click);
            // 
            // minimizeButton
            // 
            this.minimizeButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.minimizeButton.FlatAppearance.BorderSize = 0;
            this.minimizeButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.minimizeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.minimizeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.minimizeButton.Image = ((System.Drawing.Image)(resources.GetObject("minimizeButton.Image")));
            this.minimizeButton.Location = new System.Drawing.Point(356, 0);
            this.minimizeButton.Name = "minimizeButton";
            this.minimizeButton.Size = new System.Drawing.Size(25, 25);
            this.minimizeButton.TabIndex = 9;
            this.minimizeButton.UseVisualStyleBackColor = true;
            // 
            // cerrarButton
            // 
            this.cerrarButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.cerrarButton.FlatAppearance.BorderSize = 0;
            this.cerrarButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.cerrarButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.cerrarButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cerrarButton.Image = ((System.Drawing.Image)(resources.GetObject("cerrarButton.Image")));
            this.cerrarButton.Location = new System.Drawing.Point(381, 0);
            this.cerrarButton.Name = "cerrarButton";
            this.cerrarButton.Size = new System.Drawing.Size(25, 25);
            this.cerrarButton.TabIndex = 7;
            this.cerrarButton.UseVisualStyleBackColor = true;
            // 
            // InicioSesionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(408, 201);
            this.Controls.Add(this.barraSuperiorPanel);
            this.Controls.Add(this.contrasenaLabel);
            this.Controls.Add(this.usuarioLabel);
            this.Controls.Add(this.topPanel);
            this.Controls.Add(this.bottomPanel);
            this.Controls.Add(this.leftPanel);
            this.Controls.Add(this.rightPanel);
            this.Controls.Add(this.inicarSesionButton);
            this.Controls.Add(this.contrasenaTextBox);
            this.Controls.Add(this.usuarioTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "InicioSesionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inicio de sesión";
            this.barraSuperiorPanel.ResumeLayout(false);
            this.barraSuperiorPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox usuarioTextBox;
        private System.Windows.Forms.TextBox contrasenaTextBox;
        private System.Windows.Forms.Button inicarSesionButton;
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Panel bottomPanel;
        private System.Windows.Forms.Panel leftPanel;
        private System.Windows.Forms.Panel rightPanel;
        private System.Windows.Forms.Label usuarioLabel;
        private System.Windows.Forms.Label contrasenaLabel;
        private System.Windows.Forms.Panel barraSuperiorPanel;
        private System.Windows.Forms.Label tituloLabel;
        private System.Windows.Forms.Button minimizeButton;
        private System.Windows.Forms.Button cerrarButton;
    }
}

