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
using Windows.Networking;
using Windows.Networking.Connectivity;
using Windows.System.Profile;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AgenteApp.UWP.Vistas
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ClientePage : Page, IFormulario
    {
        FormularioPresentador presentador;
        ComponenteFabrica componenteFabrica;

        List<ClienteTelefono>telefonosLis;
        List<Correos> correosList;
        List<CodigoPostal> direccionCP;
        Portabilidad parametroPortabilidad;
        ClienteTelefono clienteTelefono;
        private ModoVentana modo;
        string numeroSeleccionado, numeroSeleccionadoId;
        string correoSeleccionado, correoSeleccionadoId;
        string numTelefonico;
        string ip = "";
        string idhardware = "";
        public ClientePage()
        {
            this.InitializeComponent();
            this.Loaded += CommandBarPage_Loaded;
            //Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested +=            App_BackRequested;
            presentador = new FormularioPresentador(this);
            componenteFabrica = new ComponenteFabrica();
            //this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
            clienteTelefono = new ClienteTelefono();
            presentador.TipoTelefono();

            telefonosLis = new List<ClienteTelefono>();
            correosList = new List<Correos>();


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
                foreach (IComponente componente in formularioComponentes.Children)
                {
                    campos.Add(componente.Filtro);
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
                    this.HeaderTextBlock.Text = "Identificación del cliente / alta cliente";
                    numTelefonico = (string)parametros.GetType().GetProperty("telCliente").GetValue(parametros, null);
                    telefonoAgregar.Text = numTelefonico;
                    //razonSocialTextBox.Text = parametroPortabilidad.DescripcionPortabilidad;
                }
                else
                {
                    this.HeaderTextBlock.Text = "Identificación del cliente / actualizar cliente";
                    int idCliente = (int)parametros.GetType().GetProperty("idCliente").GetValue(parametros, null);


                    presentador.TraerDatosCliente(idCliente);
                    presentador.traerDatosTelefono(idCliente);
                    presentador.traerDatosCorreos(idCliente);

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

               /*clienteTelefono.Id = IdCliente;
                clienteTelefono.Nir = parametroPortabilidad.IdMunicipio;
                clienteTelefono.Serie = parametroPortabilidad.IdConsecutivo;
                clienteTelefono.Numeracion = numTelefonico;
                clienteTelefono.TelefonoCliente = "0";
                clienteTelefono.Compania = parametroPortabilidad.DescripcionPortabilidad;
                TipoTelefono itemboxTTelefono = (TipoTelefono)tipoTelefonoTextBox.SelectedItem;
                clienteTelefono.TipoTelefono = itemboxTTelefono.Id;
                telefonosLis.Add(clienteTelefono);
                */
                GuardarTelefonoCliente(IdCliente);
            }
            
        }

        public List<TipoTelefono> TipoTelefono
        {
            set
            {
                //tipoTelefonoTextBox.ItemsSource = value;
                cmbAgregarOri.ItemsSource = value;
                cmbAgregarOriCorre.ItemsSource = value;
            }
        }

        public List<ClienteTelefono> ClienteTelefono
        {
            set
            {
                if (!(value.Count() == 0))
                {
                    telefonoAgregar.Text = value[0].Numeracion;
                    //razonSocialTextBox.Text = value[0].Compania;
                    //tipoTelefonoTextBox.SelectedIndex = Convert.ToInt32(value[0].TipoTelefono) - 1;
                }
                telefonos.ItemsSource = value;
                telefonosLis = value;
            }
        }
                
        public List<Correos> ClienteCorreos
        {
            set
            {                
                correos.ItemsSource = value;
                correosList = value;
            }
        }

        public List<CodigoPostal> direccionesCodigo
        {
            set
            {
                var colonia = formularioComponentes.Children.Where(a => (a as IComponente).Componente.campoId == "BTCLIENTECOLONIA")
                                          .Select(a => a)
                                          .First();
                var ciudad = formularioComponentes.Children.Where(a => (a as IComponente).Componente.campoId == "BTCLIENTECIUDAD")
                                          .Select(a => a)
                                          .First();
                var estado = formularioComponentes.Children.Where(a => (a as IComponente).Componente.campoId == "BTCLIENTEESTADO")
                                          .Select(a => a)
                                          .First();

                if (value.Count() > 0)
                { 
                    (colonia as IComponente).Valor = value[0].colonia;
                    (ciudad as IComponente).Valor = value[0].ciudad;
                    (estado as IComponente).Valor = value[0].estado;
                }
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

        public void CrearFormularioClientes(Componente componente)
        {
            IComponente componenteVista = componenteFabrica.CrearComponente(componente); //criteriosSeleccionFabrica.CrearComponente(criterioSeleccion);
            
            /*if (componenteVista.Componente.campoId == "BTCLIENTEPNOMBRE")
            {
                (componenteVista as UIElement).KeyDown += ClientePage_KeyDownName;
            }*/
            if (componenteVista.Componente.campoId == "BTCLIENTEPNOMBRE" || componenteVista.Componente.campoId == "BTCLIENTESNOMBRE" || 
                 componenteVista.Componente.campoId == "BTCLIENTEAPATERNO" || componenteVista.Componente.campoId == "BTCLIENTEAMATERNO")
            {
                (componenteVista as UIElement).LostFocus += ClientePage_KeyDownName;
            }
            //le asiganamos evento al codigo postal si lo trea, para consultar
            else if (componenteVista.Componente.campoId == "BTCLIENTECPID")
            {
                (componenteVista as UIElement).LostFocus += ConsultarCP_LostFocus;
            }
            formularioComponentes.Children.Add(componenteVista as UIElement);
            
        }

        private void ConsultarCP_LostFocus(object sender, RoutedEventArgs e)
        {
            string codigoPostal = "";
            var CPOSTAL = formularioComponentes.Children.Where(a => (a as IComponente).Componente.campoId == "BTCLIENTECPID")
                                       .Select(a => a)
                                       .First();
            codigoPostal=((TextoComponente)CPOSTAL).Filtro.valor;

            presentador.consultarCP(codigoPostal);

        }

        private void ClientePage_KeyDownName(object sender, RoutedEventArgs e)
        {
            string nombre = "";
            string nombreS = "";
            string paterno = "";
            string materno = "";
            //hay que programar el evento onkeypress in the tap button 
            
                var primerNombre = formularioComponentes.Children.Where(a => (a as IComponente).Componente.campoId == "BTCLIENTEPNOMBRE")
                                           .Select(a => a)
                                           .First();
                var segundoNombre = formularioComponentes.Children.Where(a => (a as IComponente).Componente.campoId == "BTCLIENTESNOMBRE")
                                          .Select(a => a)
                                          .First();
                var apellidoPaterno = formularioComponentes.Children.Where(a => (a as IComponente).Componente.campoId == "BTCLIENTEAPATERNO")
                                          .Select(a => a)
                                          .First();
                var apellidoMaterno = formularioComponentes.Children.Where(a => (a as IComponente).Componente.campoId == "BTCLIENTEAMATERNO")
                                          .Select(a => a)
                                          .First();

                var nombreCompletoC = formularioComponentes.Children.Where(a => (a as IComponente).Componente.campoId == "BTCLIENTENCOMPLETO")
                                          .Select(a => a)
                                          .First();
                
                nombre = ((TextoComponente)primerNombre).Filtro.valor;
                nombreS = ((TextoComponente)segundoNombre).Filtro.valor;
                paterno = ((TextoComponente)apellidoPaterno).Filtro.valor;
                materno = ((TextoComponente)apellidoMaterno).Filtro.valor;

            nombreCompletoC.SetValue(Grid.ColumnSpanProperty,2);
            nombreCompletoC.SetValue(WidthProperty, 500);
            (nombreCompletoC as IComponente).Valor  = nombre + " " + nombreS + " " + paterno + " " + materno;
            
        }

        private void AppGuardarClienteButton_Click(object sender, RoutedEventArgs e)
        {
            GuardarClientes();
        }

        private void eliminarCorreo_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < correosList.Count; i++)
            {
                if (correosList[i].Correo.Equals(correoSeleccionado))
                {
                    correosList.RemoveRange(i, 1);
                }
            }
            presentador.eliminarCorreo(correoSeleccionado, correoSeleccionadoId);
            correos.ItemsSource = null;
            correos.ItemsSource = correosList;
        }

        private void eliminarNum_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < telefonosLis.Count; i++)
            {
                if (telefonosLis[i].Numeracion.Equals(numeroSeleccionado))
                {
                    telefonosLis.RemoveRange(i,1);
                }
            }
            if (!numeroSeleccionado.Equals(""))
                presentador.BorrarTelefonoCliente(numeroSeleccionado,numeroSeleccionadoId);
            telefonos.ItemsSource = null;
            telefonos.ItemsSource = telefonosLis;
        }

        private void telefonosListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var telefonoSel = e.ClickedItem;
            numeroSeleccionado =(string)telefonoSel.GetType().GetProperty("Numeracion").GetValue(telefonoSel, null);
            numeroSeleccionadoId = (string)telefonoSel.GetType().GetProperty("Id").GetValue(telefonoSel, null);
            EliminaNum.Visibility = Visibility.Visible;
            EliminaCorreo.Visibility = Visibility.Collapsed;
            //();
        }
        
        private void correosListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var correoSel = e.ClickedItem;
            correoSeleccionado = (string)correoSel.GetType().GetProperty("Correo").GetValue(correoSel, null);
            correoSeleccionadoId = (string)correoSel.GetType().GetProperty("Id").GetValue(correoSel, null);
            EliminaNum.Visibility = Visibility.Collapsed; 
            EliminaCorreo.Visibility = Visibility.Visible;
        }

        public void GuardarClientes()
        {
            progressRing.IsActive = true;
            presentador.GuardarClientes(modo);
        }

        public void GuardarTelefonoCliente(string IdCliente)
        {
            for (int i = 0; i < telefonosLis.Count; i++)
            {
                telefonosLis[i].Id = IdCliente;
            }
            GuardarCorreoCliente(IdCliente);
            presentador.guardarTelefonoCliente(telefonosLis);
            
        }

        public void GuardarCorreoCliente(string IdCliente)
        {
            presentador.guardarCorreoCliente(correosList, IdCliente);
        }

        public void AsignarValor(Componente campo, Objeto registro)
        {
    
               var componente = formularioComponentes.Children.Where(a => (a as IComponente).Componente.tablaId == campo.tablaId 
                                                                   && (a as IComponente).Componente.campoId == campo.campoId)
                                       .Select(a => a)
                                       .First();
            string alias = "C" + campo.orden.ToString();

            (componente as IComponente).Valor =  (string)registro.GetType().GetProperty(alias).GetValue(registro, null);


        }
        TipoTelefono itemboxTCorreo;
        private void clickAgregarCorreo(object sender, RoutedEventArgs e)
        {
            //string l = (string)sender.GetType().GetProperty("SelectedItem").GetValue(sender, null);

            itemboxTCorreo = (TipoTelefono)cmbAgregarOriCorre.SelectedItem;
            if (itemboxTCorreo != null)
            {
                correosList.Add(
                  new Correos()
                  {
                      Id = "",
                      Correo = correoAgregar.Text,
                      Origen = itemboxTCorreo.Id,
                      OrigenDsc = itemboxTCorreo.Descripcion,
                      EsNuevo = "S"

                  }
                  );
                correos.ItemsSource = null;
                // lista();
                correos.ItemsSource = correosList;
                correoAgregar.Text = "";
            }


        }
        TipoTelefono itemboxTTelefono;
        private void clickAgregarTel(object sender, RoutedEventArgs e)
        {
            itemboxTTelefono = (TipoTelefono)cmbAgregarOri.SelectedItem;
            presentador.ConsultarPortabilidad(telefonoAgregar.Text);
           
        }

        public void ActualizarTelefonoCliente(string idCliente)
        {
            /*clienteTelefono.Id = idCliente;
            clienteTelefono.Nir = parametroPortabilidad.IdMunicipio;
            clienteTelefono.Serie = parametroPortabilidad.IdConsecutivo;
            clienteTelefono.Numeracion = noTelefonicoClienteTextBox.Text;
            clienteTelefono.TelefonoCliente = "0";
            clienteTelefono.Compania =razonSocialTextBox.Text;
            TipoTelefono itemboxTTelefono = (TipoTelefono)tipoTelefonoTextBox.SelectedItem;
            clienteTelefono.TipoTelefono = itemboxTTelefono.Id;*/
            presentador.ActualizarTelefonoCliente(telefonosLis,idCliente);
        }
        public void ActualizarCorreoCliente(string idCliente)
        {
            /*clienteTelefono.Id = idCliente;
            clienteTelefono.Nir = parametroPortabilidad.IdMunicipio;
            clienteTelefono.Serie = parametroPortabilidad.IdConsecutivo;
            clienteTelefono.Numeracion = noTelefonicoClienteTextBox.Text;
            clienteTelefono.TelefonoCliente = "0";
            clienteTelefono.Compania =razonSocialTextBox.Text;
            TipoTelefono itemboxTTelefono = (TipoTelefono)tipoTelefonoTextBox.SelectedItem;
            clienteTelefono.TipoTelefono = itemboxTTelefono.Id;*/
            presentador.ActualizarCorreoCliente(correosList, idCliente);
        }
        public void ConsultarPortabilidad(string compania,string IdMunicipio, string IdConsecutivo)
        {
            telefonosLis.Add(
                 new ClienteTelefono()
                 {
                     Id = "",
                     TipoTelefonoDes = itemboxTTelefono.Descripcion,
                     TipoTelefono = itemboxTTelefono.Id,
                     TelefonoCliente = "0",
                     Compania = compania,
                     Nir = IdMunicipio,
                     Serie = IdConsecutivo,
                     Numeracion = telefonoAgregar.Text,
                     EsNuevo ="S"
        }
                 );
            telefonos.ItemsSource = null;
            telefonos.ItemsSource = telefonosLis;
            telefonoAgregar.Text = "";
        }
        public void obtenerInformacion()
        {
            //optenemos ip           
            foreach (HostName localHostName in NetworkInformation.GetHostNames())
            {
                if (localHostName.IPInformation != null)
                {
                    if (localHostName.Type == HostNameType.Ipv4)
                    {
                        ip = localHostName.ToString();
                        break;
                    }
                }
            }

            //optenemos id hardware
            var token = HardwareIdentification.GetPackageSpecificToken(null);
            var hardwareId = token.Id;
            var dataReader = Windows.Storage.Streams.DataReader.FromBuffer(hardwareId);
            byte[] bytes = new byte[hardwareId.Length];
            dataReader.ReadBytes(bytes);
            idhardware = BitConverter.ToString(bytes);
        }
    }
}
