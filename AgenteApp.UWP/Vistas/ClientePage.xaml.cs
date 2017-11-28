using AgenteApp.Presentadores;
using AgenteApp.Vistas;
using AgenteApp.UWP.Fabricas;
using AgenteApp.Modelos;
using Windows.UI.Popups;
using AgenteApp.Clases;
using AgenteApp.Componentes;
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


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AgenteApp.UWP.Vistas
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ClientePage : Page, IFormulario
    {
        FormularioPresentador presentador;
        FormularioFabrica formularioClienteFabrica;
        Portabilidad parametroPortabilidad;
        ClienteTelefono clienteTelefono;
        string numTelefonico;
        public ClientePage()
        {
            this.InitializeComponent();
            this.Loaded += CommandBarPage_Loaded;
            presentador = new FormularioPresentador(this);
            formularioClienteFabrica = new FormularioFabrica();
            //this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
            clienteTelefono = new ClienteTelefono();
            presentador.TipoTelefono();

        }
        private void CommandBarPage_Loaded(object sender, RoutedEventArgs e)
        {
            double? diagonal = DisplayInformation.GetForCurrentView().DiagonalSizeInInches;
            
            //move commandbar to page bottom on small screens
            if (diagonal < 7)
            {
                topbar.Visibility = Visibility.Collapsed;
                pageTitleContainer.Visibility = Visibility.Visible;
               // bottombar.Visibility = Visibility.Visible;
            }
            else
            {
                topbar.Visibility = Visibility.Visible;
                pageTitleContainer.Visibility = Visibility.Collapsed;
            //bottombar.Visibility = Visibility.Collapsed;
            }

            
        }

        public List<Campo> Campos
        {
            get
            {
                List<Campo> campos = new List<Campo>();
                foreach (IFormularioComponente componente in formularioComponentes.Children)
                {
                    if (componente.Campo.valor != string.Empty)
                        campos.Add(componente.Campo);
                    else
                    {
                        componente.Campo.valor = " ";
                        campos.Add(componente.Campo);
                    }
                }
                return campos;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Object[] person = (object[]) e.Parameter;

            if (person != null)
            {
                if ((ModoVentana)person[0] == ModoVentana.ALTA)
                {
                    presentador.CrearFormulario();
                    numTelefonico = (string)person[1];
                    parametroPortabilidad = (Portabilidad)person[2];
                    noTelefonicoClienteTextBox.Text = numTelefonico;
                    razonSocialTextBox.Text = parametroPortabilidad.DescripcionPortabilidad;
                }
                else
                {
                    presentador.CrearFormulario();
                    //entra en modo cambio
                }

            }
        }

        public List<Objeto> Clientes
        {
            set
            {
                progressRing.IsActive = false;
                clientesListView.ItemsSource = value;
            }
        }

        public string idCliente
        {
            set
            {
               string IdCliente = value;
                GuardarTelefonoCliente(IdCliente);
            }
            
        }

        public List<TipoTelefono> TipoTelefono
        {
            set
            {
                tipoTelefonoTextBox.ItemsSource = value;
            }
        }

        private void AppCerrarButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(NavigationMenuSample.Views.AgentePage));  //AgenteApp.UWP.Vistas.AgentePage));
        }
        public async void MostrarMensaje(string titulo, string mensaje)
        {
            progressRing.IsActive = false;
            if (mensaje != null)
            {
                var dialog = new MessageDialog(mensaje, titulo);
                await dialog.ShowAsync();
            }
        }

        public void CrearFormularioClientes(CampoFormulario campoFormulario)
        {
            IFormularioComponente componente = formularioClienteFabrica.CrearComponente(campoFormulario); //criteriosSeleccionFabrica.CrearComponente(criterioSeleccion);
            formularioComponentes.Children.Add(componente as UIElement);
        }

        private void AppGuardarClienteButton_Click(object sender, RoutedEventArgs e)
        {
            GuardarClientes();
        }

        public void GuardarClientes()
        {
            progressRing.IsActive = true;
            presentador.GuardarClientes();
        }
        public void GuardarTelefonoCliente(string IdCliente)
        {
            clienteTelefono.id = IdCliente;
            clienteTelefono.nir = parametroPortabilidad.IdMunicipio;
            clienteTelefono.serie = parametroPortabilidad.IdConsecutivo;
            clienteTelefono.telefonoCliente = numTelefonico;
            clienteTelefono.numeracion = "0";
            clienteTelefono.compania = parametroPortabilidad.DescripcionPortabilidad;
            TipoTelefono itemboxTTelefono = (TipoTelefono)tipoTelefonoTextBox.SelectedItem;
            clienteTelefono.tipoTelefono = itemboxTTelefono.Id;

            presentador.guardarTelefonoCliente(clienteTelefono);
        }
        

    }
}
