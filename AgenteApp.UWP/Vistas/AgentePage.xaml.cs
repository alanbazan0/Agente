using AgenteApp.Presentadores;
using AgenteApp.Vistas;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using AgenteApp.Modelos;
using Windows.UI.Popups;
using Newtonsoft.Json.Linq;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace NavigationMenuSample.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AgentePage : Page, IAgenteVista
    {
        AgentePresentador presentador;
        public AgentePage()
        {
            this.InitializeComponent();
            this.Loaded += CommandBarPage_Loaded;
            presentador = new AgentePresentador(this);
        }

        public object CriteriosSeleccion
        {
            get
            {
               return new { nombreCompleto = nombreCompletoCriterioTextBox.Text,
                            rfc = rfcCriterioTextBox.Text,
                           curp = curpCriterioTextBox.Text};               
            }
        }

        public List<Cliente> Clientes
        {
            set
            {
                //List<Cliente> lista = new List<Cliente>();
                //Cliente cliente = new Cliente();
                //cliente.primerNombre = "ALAn BAZAN";
                //lista.Add(cliente);
                clientesListView.ItemsSource = value;
            }
        }

        private void CommandBarPage_Loaded(object sender, RoutedEventArgs e)
        {
            double? diagonal = DisplayInformation.GetForCurrentView().DiagonalSizeInInches;

            //move commandbar to page bottom on small screens
            if (diagonal < 7)
            {
                topbar.Visibility = Visibility.Collapsed;
                pageTitleContainer.Visibility = Visibility.Visible;
                bottombar.Visibility = Visibility.Visible;
            }
            else
            {
                topbar.Visibility = Visibility.Visible;
                pageTitleContainer.Visibility = Visibility.Collapsed;
                bottombar.Visibility = Visibility.Collapsed;
            }
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            ConsultarClientes();
        }

        public void ConsultarClientes()
        {
            presentador.ConsultarClientes();
        }

        public async void MostrarMensaje(string titulo, string mensaje)
        {
            if (mensaje != null)
            {
                var dialog = new MessageDialog(mensaje, titulo);
                await dialog.ShowAsync();
            }
        }
    }
}
