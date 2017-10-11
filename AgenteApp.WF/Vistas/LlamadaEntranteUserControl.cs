using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AgenteApp.WF.Temas;

namespace AgenteApp.WF.Vistas
{
    public partial class LlamadaEntranteUserControl : UserControl
    {
        //example at 
        //https://www.codeproject.com/Articles/364272/Easily-Add-a-Ribbon-into-a-WinForms-Application-Cs
        public LlamadaEntranteUserControl()
        {
            InitializeComponent();
            CargarTema();
        }

        private void CargarTema()
        {
            Theme.ColorTable = new DarkSkin();
            menuRibbon.Refresh();
            this.Refresh();
        }
    }
}
