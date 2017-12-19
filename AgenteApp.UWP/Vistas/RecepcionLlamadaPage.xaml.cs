using AgenteApp.Presentadores;
using AgenteApp.Vistas;
using AgenteApp.Clases;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Popups;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using AgenteApp.Modelos;
using Newtonsoft.Json.Linq;
using AgenteApp.UWP.Fabricas;
using AgenteApp.Componentes;
using System.Text;
//usings Linphone
//using Xamarin.Forms;
using Linphone;
using Windows.System.Threading;
using Windows.ApplicationModel.Core;
using System.Diagnostics;
using Windows.UI.Core;
using System.Reflection;
using Windows.ApplicationModel.Email;
using Windows.Storage;
using Windows.ApplicationModel.Activation;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace NavigationMenuSample.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RecepcionLlamadaPage : Page, IRecepcionLlamadaVista
    {
        Usuario usuario;
        RecepcionLlamadaPresentador presentador;
        private DispatcherTimer dispatcherTimer;
        Portabilidad portabilidadParametros;
        SoftphoneEmbebed telefono;
        public string numTelefonico;
        int seg = 0;
        int min = 0;
        int hor = 0;
        int segunds = 0;
        
        
        string extension;
        string IdLlamada;
        Boolean pausado = false;
        Call LlamadaPausada;

        private CoreListener Listener;

        public RecepcionLlamadaPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
            this.Loaded += CommandBarPage_Loaded;

            //correosPresentador = new CorreoPresentador(this);
            presentador = new RecepcionLlamadaPresentador(this);
            telefono = new SoftphoneEmbebed();
            usuario = null;
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            HeaderTextBlock.Text = "Recepción de llamada - Disponible";
            FechaLlamadaTextBox.Text = GetDateString();
            try
            {
                Listener = Factory.Instance.CreateCoreListener();
                Listener.OnRegistrationStateChanged = OnRegistration;
                Listener.OnCallStateChanged = OnCall;
                Listener.OnCallStatsUpdated = OnStats;
                telefono.LinphoneCore.AddListener(Listener);
            }
            catch { }

        }
       

       

        private void OnGlobal(Core lc, GlobalState gstate, string message)
        {
#if WINDOWS_UWP
            Debug.WriteLine("Global state changed: " + gstate);
#else
            Console.WriteLine("Global state changed: " + gstate);
#endif
        }

        

        

        private void OnRegistration(Core lc, ProxyConfig config, RegistrationState state, string message)
        {
#if WINDOWS_UWP
            Debug.WriteLine("Registration state changed: " + state);
#else
            Console.WriteLine("Registration state changed: " + state);
#endif
            Console.WriteLine("Registration state changed: " + state);
        }

        private void OnCall(Core lc, Call lcall, CallState state, string message)
        {
#if WINDOWS_UWP
            Debug.WriteLine("Call state changed: " + state);
#else
            Console.WriteLine("Call state changed: " + state);
#endif
            Console.WriteLine("Call state changed: " + state);

            if (lc.CallsNb > 0)
            {
                if (state == CallState.IncomingReceived)
                {
                    Contestar();
                }
                


            }
            else
            {
                HeaderTextBlock.Text = "Recepción de llamada - Disponible";               
                HoraLlamadaTextBox.Text = "00:00:00";
                dispatcherTimer.Stop();
                LimpiarDatos();
            }

           
        }

        private void Contestar()
        {
            if (telefono.LinphoneCore.CallsNb == 0)
            {
                
            }
            else
            {
             
                Call call = telefono.LinphoneCore.CurrentCall;
                if (call.State == CallState.IncomingReceived)
                {
                    telefono.LinphoneCore.AcceptCall(call);
                    HeaderTextBlock.Text = "Recepción de llamada - En llamada";
                    numTelefonico = NoTelTextBox.Text = call.RemoteAddress.DisplayName;
                    LimpiarDatos();
                    HoraLlamadaTextBox.Text = GetTimeString();
                    dispatcherTimer.Start();
                    ConsultarDatos(NoTelTextBox.Text);
                }

            }
        }

        private void Colgar(object sender, RoutedEventArgs e)
        {
            if (telefono.LinphoneCore.CallsNb == 0)
            {

            }
            else
            {
                Call call;
                if (telefono.LinphoneCore.CurrentCall != null)
                {
                    call = telefono.LinphoneCore.CurrentCall;
                    if (call.State == CallState.StreamsRunning)
                    {
                        telefono.LinphoneCore.TerminateAllCalls();
                        HeaderTextBlock.Text = "Recepción de llamada - Disponible";
                        HoraLlamadaTextBox.Text = "00:00:00";
                        dispatcherTimer.Stop();
                        LimpiarDatos();
                    }
                }
                else
                {
                    if (LlamadaPausada.State == CallState.Paused)
                    {
                        telefono.LinphoneCore.TerminateAllCalls();
                        HeaderTextBlock.Text = "Recepción de llamada - Disponible";
                        HoraLlamadaTextBox.Text = "00:00:00";
                        dispatcherTimer.Stop();
                        LimpiarDatos();
                    }

                }
                
            }
        }

        private void Pausar(object sender, RoutedEventArgs e)
        {
            if (telefono.LinphoneCore.CallsNb == 0)
            {

            }
            else
            {

                if (telefono.LinphoneCore.CurrentCall != null)
                    LlamadaPausada = telefono.LinphoneCore.CurrentCall;


                if (LlamadaPausada.State == CallState.StreamsRunning)
                {
                    telefono.LinphoneCore.PauseCall(LlamadaPausada);
                    HeaderTextBlock.Text = "Recepción de llamada - Pausa";
                    InsertarPausa();
                }
                else if (LlamadaPausada.State == CallState.Paused)
                {
                    telefono.LinphoneCore.ResumeCall(LlamadaPausada);
                    HeaderTextBlock.Text = "Recepción de llamada - En llamada";
                    LlamadaPausada = null;
                    ActualizarPausa();
                }
                ConsultarPausas();
            }
        }


        private void EntroCusor(object sender, RoutedEventArgs e)
        {
           // numTranfer.Visibility = Visibility.Visible;
            //numTranfer.Focus(FocusState.Keyboard);
        }

        private void SalioCursor(object sender, RoutedEventArgs e)
        {
            //numTranfer.Visibility = Visibility.Collapsed;
        }

        private void Transferir(object sender, RoutedEventArgs e)
        {
            
                if (telefono.LinphoneCore.CallsNb == 0)
                {

                }
                else
                {
                    Call call = telefono.LinphoneCore.CurrentCall;
                    if (call.State == CallState.StreamsRunning)
                    {
                        //telefono.LinphoneCore.TransferCall(call, numTranfer.Text);
                        //HeaderTextBlock.Text = "Recepción de llamada - Diponible";
                    }
                }
  

        }



        private void OnStats(Core lc, Call call, CallStats stats)
        {

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            onRegister(e);
            consultarCorreoEntrada();
        }

        public void onRegister(NavigationEventArgs e)
        {
            try
            {
                usuario = (Usuario)e.Parameter;
                extension = NoExtensionTextBox.Text = usuario.Extension;
                string password = Constantes.PASS_CALL + usuario.Extension;
                var authInfo = Factory.Instance.CreateAuthInfo(usuario.Extension, null, password, null, null, Constantes.DIRECCION_ELAXTIC);
                telefono.LinphoneCore.AddAuthInfo(authInfo);

                var proxyConfig = telefono.LinphoneCore.CreateProxyConfig();
                var identity = Factory.Instance.CreateAddress("sip:sample@domain.tld");
                identity.Username = usuario.Extension;
                identity.Domain = Constantes.DIRECCION_ELAXTIC;
                proxyConfig.Edit();
                proxyConfig.IdentityAddress = identity;
                proxyConfig.ServerAddr = Constantes.DIRECCION_ELAXTIC;
                proxyConfig.Route = Constantes.DIRECCION_ELAXTIC;
                proxyConfig.RegisterEnabled = true;
                proxyConfig.Done();
                telefono.LinphoneCore.AddProxyConfig(proxyConfig);
                telefono.LinphoneCore.DefaultProxyConfig = proxyConfig;
                telefono.LinphoneCore.RefreshRegisters();
                telefono.OnStart();
            }
            catch { }

        }

        public void consultarCorreoEntrada()
        {
           // correosPresentador.consultarCorreoEntrada(usuario.Id);
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

        void dispatcherTimer_Tick(object sender, object e)
        {
            ShowTime();
        }

        private void ShowTime()
        {
            //timerReceso.Text = DateTime.Now.ToString("hh:mm:ss");
            if (min == 60)
            {
                hor += 1;
                min = 0;
            }
            if (seg == 60)
            {
                min += 1;
                seg = 0;
            }
            seg += 1;
            String Sseg = "0";
            if (seg < 10)
            { Sseg += seg.ToString(); }
            else
            {
                Sseg = seg.ToString();
            }
            String Smin = "0";
            if (min < 10)
            { Smin += min.ToString(); }
            else
            {
                Smin = min.ToString();
            }
            String Shor = "0";
            if (hor < 10)
            { Shor += hor.ToString(); }
            else
            {
                Shor = hor.ToString();
            }
            timpoLlamadaTextBox.Text = Shor + ":" + Smin + ":" + Sseg;
            segunds += 1;
        }

        private string GetDateString()
        {
            DateTime dateTime = DateTime.Now;
            string text = dateTime.ToString("dd/MM/yyyy");// + "-" + dateTime.TimeOfDay.ToString();
            return text;
        }

        private void LimpiarDatos()
        {
            seg = 0;
            min = 0;
            hor = 0;
            segunds = 0;
            timpoLlamadaTextBox.Text = "00:00:00";
            EstadoTextBox.Text = "";
            ciudadTextBox.Text = "";
            tipoTelefonoTextBox.Text = "";
            tipoLlamadaTextBox.Text = "";
        }

        private string GetTimeString()
        {
            DateTime dateTime = DateTime.Now;
            string text = dateTime.TimeOfDay.ToString();
            text = text.Remove(text.IndexOf('.'));
            return text;
        }

        private void ConsultarDatos(string noTelefonico)
        {
            progressRing.IsActive = true;
            ConsultarIdLlamada(NoExtensionTextBox.Text);
            ConsultarPortabilidad(noTelefonico);
            ConsultarClientesTel(noTelefonico);
        }

        public void ConsultarPortabilidad(string numero)
        {
            presentador.ConsultarPortabilidad(numero);
        }

        public List<Portabilidad> Portabilidad
        {
            set
            {
                try
                {
                    EstadoTextBox.Text = value[0].EstadoPortabilidad;
                    ciudadTextBox.Text = value[0].MunicipioPortabilidad;
                    tipoTelefonoTextBox.Text = value[0].RedPortabilidad;
                    tipoLlamadaTextBox.Text = value[0].TipoLlamadaPortabilidad;
                    portabilidadParametros = value[0];
                }
                catch { }
            }
        }

        public Pausas Pausa
        {
            get
            {
                Pausas PausAux = new Pausas();
                PausAux.Extencion = extension;
                PausAux.IdLlamada = IdLlamada;
                PausAux.IdAgente = usuario.Id;
                PausAux.Telefono = numTelefonico;
                PausAux.IdPausa = "0";
                PausAux.InicioPausa = GetDateString();
                PausAux.Finalpausa = GetDateString();
                PausAux.Duracion = "0";
                lbCoincidencias.Foreground = new SolidColorBrush(Colors.White);
                return PausAux;

            }
            set
            {
                 

            }
        }


        public void ConsultarIdLlamada(string extension)
        {
            presentador.ConsultarIdLlamada(extension);
        }

        public string setIdLlamada
        {
            set
            {
                IdLlamada = idLlamadaTextBox.Text = value;
            }
        }

        public void ConsultarClientesTel(string numero)
        {           
            presentador.ConsultarClientesTel(numero);
        }

        public List<ClienteTelefono> Clientes
        {
            set
            {
                progressRing.IsActive = false;
                if (value.Count > 0)
                {
                    lbCoincidencias.Foreground = new SolidColorBrush(Colors.Green);
                    lbCoincidencias.Text = "Coincidencias encontradas con número telefónico: "+numTelefonico;
                }
                else
                {
                    lbCoincidencias.Foreground = new SolidColorBrush(Colors.Red);
                    lbCoincidencias.Text = "Coincidencias no encontradas con número telefónico: " + numTelefonico;
                }
            }
        }

        
             private void PruebaPausar(object sender, RoutedEventArgs e)
        {
            
                if (pausado==false)
                {

                    pausado = true;
                    InsertarPausa();
                }
                else
                {
                pausado = false;
                ActualizarPausa();
                }
            ConsultarPausas();
        }


        public void InsertarPausa()
        {
            presentador.InsertarPausa();
        }     

        public void ActualizarPausa()
        {
            presentador.ActulizarPausa();
        }

        public void ConsultarPausas()
        {
            presentador.ConsultarPausa();
        }

        public async void MostrarMensajeAsync(string titulo, string mensaje)
        {
            progressRing.IsActive = false;
            if (mensaje != null)
            {
                var dialog = new MessageDialog(mensaje, titulo);
                await dialog.ShowAsync();
            }
        }

        public List<Pausas> Pausas
        {
            set
            {
                TiempoEsperaListView.ItemsSource = value;
            }
        }

    }
}
