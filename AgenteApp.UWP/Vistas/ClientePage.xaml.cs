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
using System.Reflection;


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
        private ModoVentana modo;
        string numTelefonico;
        public ClientePage()
        {
            this.InitializeComponent();
            this.Loaded += CommandBarPage_Loaded;
            //Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested +=            App_BackRequested;
            presentador = new FormularioPresentador(this);
            formularioClienteFabrica = new FormularioFabrica();
            //this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
            clienteTelefono = new ClienteTelefono();
            presentador.TipoTelefono();

        }
        /*private void App_BackRequested(object sender, Windows.UI.Core.BackRequestedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame == null)
                return;

            // Navigate back if possible, and if the event has not 
            // already been handled .
            if (rootFrame.CanGoBack && e.Handled == false)
            {
                e.Handled = true;
                rootFrame.GoBack();
            }
        }*/

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
                    campos.Add(componente.Campo);
                }
                return campos;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var parametros =  e.Parameter;

            if (parametros != null)
            {
                modo = (ModoVentana)parametros.GetType().GetProperty("modo").GetValue(parametros, null);
                presentador.CrearFormulario(modo);
                parametroPortabilidad = (Portabilidad)parametros.GetType().GetProperty("portabilidad").GetValue(parametros, null);
                if (modo == ModoVentana.ALTA)
                {
                    numTelefonico = (string)parametros.GetType().GetProperty("telCliente").GetValue(parametros, null); 
                    noTelefonicoClienteTextBox.Text = numTelefonico;
                    razonSocialTextBox.Text = parametroPortabilidad.DescripcionPortabilidad;
                }
                else
                {
                    int idCliente = (int)parametros.GetType().GetProperty("idCliente").GetValue(parametros, null);
                    presentador.TraerDatosCliente(idCliente);
                    presentador.traerDatosTelefono(idCliente);
                    
                }

            }
        }

        public List<Objeto> Clientes
        {
            set
            {
                progressRing.IsActive = false;
                clientesListView.ItemsSource = value;

                //Y AQUI HAY QUE MANDAR EL LLENADO DE LOS NUMEROS TELEFONICOS  /////////////////////////////////////////////////////////////////////////
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

        public ClienteTelefono ClienteTelefono
        {
            set
            {
                noTelefonicoClienteTextBox.Text = value.Numeracion;
                razonSocialTextBox.Text = value.Compania;
                tipoTelefonoTextBox.SelectedIndex = Convert.ToInt32(value.TipoTelefono);
            }
        }

        //private void AppCerrarButton_Click(object sender, RoutedEventArgs e)
        //{
            
        //    this.Frame.Navigate(typeof(NavigationMenuSample.Views.AgentePage));  //AgenteApp.UWP.Vistas.AgentePage));
            
        //}

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
            //if (campoFormulario.campoId == "BTCLIENTENUMERO")
            //{
            //    (componente as UIElement).IsEnabled = false;
            //}
        }

        private void AppGuardarClienteButton_Click(object sender, RoutedEventArgs e)
        {
            GuardarClientes();
        }

        public void GuardarClientes()
        {
            progressRing.IsActive = true;
            presentador.GuardarClientes(modo);
        }

        public void GuardarTelefonoCliente(string IdCliente)
        {
            clienteTelefono.Id = IdCliente;
            clienteTelefono.Nir = parametroPortabilidad.IdMunicipio;
            clienteTelefono.Serie = parametroPortabilidad.IdConsecutivo;
            clienteTelefono.Numeracion = numTelefonico;
            clienteTelefono.TelefonoCliente = "0";
            clienteTelefono.Compania = parametroPortabilidad.DescripcionPortabilidad;
            TipoTelefono itemboxTTelefono = (TipoTelefono)tipoTelefonoTextBox.SelectedItem;
            clienteTelefono.TipoTelefono = itemboxTTelefono.Id;
            presentador.guardarTelefonoCliente(clienteTelefono);
        }

        public void AsignarValor(CampoFormulario campo, Objeto registro)
        {
            var componente = formularioComponentes.Children.Where(a => (a as IFormularioComponente).CampoFormulario.tablaId == campo.tablaId 
                                                                   && (a as IFormularioComponente).CampoFormulario.campoId == campo.campoId)
                                       .Select(a => a)
                                       .First();
            string alias = "C" + campo.id.ToString();

            (componente as IFormularioComponente).Valor =  (string)registro.GetType().GetProperty(alias).GetValue(registro, null);


        }

        public void ActualizarTelefonoCliente(string idCliente)
        {
            clienteTelefono.Id = idCliente;
            clienteTelefono.Nir = parametroPortabilidad.IdMunicipio;
            clienteTelefono.Serie = parametroPortabilidad.IdConsecutivo;
            clienteTelefono.Numeracion = noTelefonicoClienteTextBox.Text;
            clienteTelefono.TelefonoCliente = "0";
            clienteTelefono.Compania =razonSocialTextBox.Text;
            TipoTelefono itemboxTTelefono = (TipoTelefono)tipoTelefonoTextBox.SelectedItem;
            clienteTelefono.TipoTelefono = itemboxTTelefono.Id;
            presentador.ActualizarTelefonoCliente(clienteTelefono);
        }
    }
}
