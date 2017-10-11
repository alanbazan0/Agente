using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AgenteApp.Modelos;

namespace AgenteApp.WF.Vistas
{
    public partial class MenuForm : Form
    {
        private bool colapsado = false;

        public Usuario Usuario
        {
            set
            {
                nombreUsuarioLabel.Text = value.Nombre;
                nombreUsuarioAccesoLabel.Text = value.NombreUsuario;
            }
        }

        public MenuForm()
        {
            InitializeComponent();
            LlamadaEntranteUserControl llamadaEntranteUserControl = new LlamadaEntranteUserControl();
            llamadaEntranteUserControl.Dock = DockStyle.Fill;
            opcionPanel.Controls.Add(llamadaEntranteUserControl);
        }

        private void cerrarButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void maximizarButton_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void menuPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void menuButton_Click(object sender, EventArgs e)
        {
            menuPanel.Width = 476;
        }

        private void menuButton_Click_1(object sender, EventArgs e)
        {
            if(colapsado)
            {
                menuPanel.Width = 230;
                datosUsuarioPanel.Visible = true;
            }
            else
            {
                menuPanel.Width = 25;
                datosUsuarioPanel.Visible = false;
            }
            colapsado = !colapsado;
        }

        private void datosUsuarioPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
