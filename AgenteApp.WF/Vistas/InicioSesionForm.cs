﻿using AgenteApp.Modelos;
using AgenteApp.Presenters;
using AgenteApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace AgenteApp.WF.Vistas
{
    public partial class InicioSesionForm : Form, IInicioSesionVista
    {
        InicioSesionPresentador presentador;
        public InicioSesionForm()
        {
            InitializeComponent();
            presentador = new InicioSesionPresentador(this);
        }

        public string NombreUsuario
        {
            get { return usuarioTextBox.Text; }
            set { usuarioTextBox.Text = value; }
        }
        public string Contrasena
        {
            get { return contrasenaTextBox.Text; }
            set { contrasenaTextBox.Text = value; }
        }

    

        public void MostrarMenu(Usuario usuario)
        {
            MenuForm form = new MenuForm();
            form.Usuario = usuario;
            form.Show();
            Hide();
        }

        public void IniciarSesion()
        {
            presentador.IniciarSesion();
        }

        public void MostrarMensaje(string message)
        {
            MessageBox.Show(message);
        }

        private void inicarSesionButton_Click(object sender, EventArgs e)
        {
            IniciarSesion();
        }

        private void tituloLabel_Click(object sender, EventArgs e)
        {

        }

        private void contrasenaTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                IniciarSesion();
        }
    }
}
