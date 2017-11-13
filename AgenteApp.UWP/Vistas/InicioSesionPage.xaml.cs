using AgenteApp.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using AgenteApp.Modelos;
using AgenteApp.Presenters;
using AgenteApp.UWP.Vistas;
using NavigationMenuSample;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AgenteApp.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class InicioSesionPage : Page, IInicioSesionVista
    {
        InicioSesionPresentador presentador;
        public InicioSesionPage()
        {
            this.InitializeComponent();
            mensajeBlockText.Text = "";
            presentador = new InicioSesionPresentador(this);
        }

        public string NombreUsuario { get { return nombreUsuarioTextBox.Text; } set { nombreUsuarioTextBox.Text = value; } }
        public string Contrasena { get { return contrasenaPasswordBox.Password; } set { contrasenaPasswordBox.Password = value; } }

        public void IniciarSesion()
        {
            progressRing.IsActive = true;
            presentador.IniciarSesion();
        }

        public void MostrarMensaje(string mensaje)
        {
            progressRing.IsActive = false;
            mensajeBlockText.Text = mensaje;
        }

        public void MostrarMenu(Usuario usuario)
        {
            progressRing.IsActive = false;
            Frame.Navigate(typeof(AppShell), usuario);
        }

        private void iniciarSesionButton_Click(object sender, RoutedEventArgs e)
        {
            mensajeBlockText.Text = "";
            IniciarSesion();
        }
    }
}
