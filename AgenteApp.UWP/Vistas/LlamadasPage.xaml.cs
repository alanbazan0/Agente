using System;
using Linphone;
using Windows.UI;
using Windows.UI.Xaml;
using AgenteApp.Vistas;
using AgenteApp.Clases;
using Windows.UI.Popups;
using AgenteApp.Modelos;
using System.Reflection;
using System.Diagnostics;
using Windows.Networking;
using Windows.UI.Xaml.Media;
using Windows.System.Profile;
using AgenteApp.Presentadores;
using Windows.UI.Xaml.Controls;
using Windows.Graphics.Display;
using Windows.UI.Xaml.Navigation;
using System.Collections.Generic;
using Windows.Networking.Connectivity;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace AgenteApp.UWP.Vistas
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class LlamadasPage : Page, ILlamadaVista
    {

        string ip;
        int seg = 0;
        int min = 0;
        int hor = 0;
        int pseg = 0;
        int pmin = 0;
        int phor = 0;
        int segunds = 0;
        Usuario usuario;
        int psegunds = 0;
        string extension;
        string IdLlamada;
        string idhardware;
        string IdCrm = "";
        string extensionOut;      
        Call LlamadaPausada;
        string IdCliente = "";
        Boolean enPausa = false;
        List<Usuario> invitados;
        Boolean pausado = false;
        Boolean iniciado = false;
        string noTelefonico = "";
        string NombreCliente = "";
        Parametros ParametroLocal;
        public string numTelefonico;
        List<Call> llamadaConferencia;
        string ultimoIdPausaLocal = "0";
        LlamadasPresentador presentador;
        string extensionConLlamada = "";
        public SoftphoneEmbebed telefono;
        public SoftphoneEmbebed telefonoOut;
        Portabilidad portabilidadParametros;
        private DispatcherTimer dispatcherTimer;
        public Parametros Parametro { get => ParametroLocal; set { } }

        public LlamadasPage()
        {
            this.InitializeComponent();
            //this.Unloaded += App_Suspending;
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
            this.Loaded += CommandBarPage_Loaded;

            //correosPresentador = new CorreoPresentador(this);
            presentador = new LlamadasPresentador(this);
            ParametroLocal = new Parametros();
            invitados = new List<Usuario>();

            llamadaConferencia = new List<Call>();
            usuario = null;
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            HeaderTextBlock.Text = "Llamadas(Entrada/Salida) - Disponible";
            tFechaextBox.Text = FechaLlamadaTextBox.Text = llFechaextBoxOut.Text = txtFechaSalida.Text=  GetDateString();
            obtenerInformacion();
        }

        private void App_Suspending(Object sender, RoutedEventArgs e)
        {
            telefono.LinphoneCore.RemoveListener(telefono.listener);
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
                    extensionConLlamada = lcall.ToAddress.Username;
                    if (extensionConLlamada == extension)
                        Contestar();
                    else if(extensionConLlamada == extensionOut)
                        EntradaDatos();
                }
                else if (state == CallState.OutgoingInit)
                {
                    llamadaConferencia.Add(lcall);
                }



            }
            else
            {
                HeaderTextBlock.Text = "Llamadas(Entrada/Salida) - Disponible";
                ttxtTiempoLlamada.Text = HoraLlamadaTextBox.Text = txtFechaSalida.Text = llHoratextBoxOut.Text = "00:00:00";
                dispatcherTimer.Stop();
                LimpiarDatosEntrada();
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
                    HeaderTextBlock.Text = "Llamadas(Entrada/Salida) - En llamada";
                    LimpiarDatosEntrada();
                    numTelefonico = txtNoTelOrigen.Text = ttxtNumeroTelefono.Text = NoTelEntrada.Text = call.RemoteAddress.DisplayName;
                    ttEstatustextBox.Text = estatusTextBox.Text = "Contestada";
                    tHoratextBox.Text = HoraLlamadaTextBox.Text =  GetTimeString();
                    dispatcherTimer.Start();
                    ConsultarDatos(NoTelEntrada.Text);

                }

            }
        }

        
            private void EntradaDatos()
        {
            if (telefonoOut.LinphoneCore.CallsNb == 0)
            {

            }
            else
            {

                Call call = telefonoOut.LinphoneCore.CurrentCall;
                if (call.State == CallState.IncomingReceived)
                {
                    telefonoOut.LinphoneCore.AcceptCall(call);
                    HeaderTextBlock.Text = "Llamadas(Entrada/Salida) - En llamada";
                    LimpiarDatosSalida();
                    numTelefonico = txtNoTelOrigen.Text = ttxtNumeroTelefono.Text = txtNumtelefonicoSalida.Text = call.RemoteAddress.DisplayName;
                    tHoratextBox.Text = txtHoraSalida.Text= GetTimeString();
                    dispatcherTimer.Start();
                    ConsultarDatos(numTelefonico);

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
                        HeaderTextBlock.Text = "Llamadas(Entrada/Salida) - Disponible";                        
                        dispatcherTimer.Stop();
                        if (extensionConLlamada == extension)
                        {
                            ttxtTiempoLlamada.Text = HoraLlamadaTextBox.Text = "00:00:00";
                            LimpiarDatosEntrada();
                        }
                        else if (extensionConLlamada == extensionOut)
                        {
                            ttxtTiempoLlamada.Text = txtHoraSalida.Text = "00:00:00";
                            LimpiarDatosSalida();
                        }
                    }
                }
                else if (LlamadaPausada != null)
                {

                    if (LlamadaPausada.State == CallState.Paused || LlamadaPausada.State == CallState.StreamsRunning)
                    {
                        telefono.LinphoneCore.TerminateAllCalls();
                        HeaderTextBlock.Text = "Llamadas(Entrada/Salida) - Disponible";
                        ttxtTiempoLlamada.Text = HoraLlamadaTextBox.Text = "00:00:00";
                        dispatcherTimer.Stop();
                        //LimpiarDatos();
                    }

                }
                else
                {
                    telefono.LinphoneCore.TerminateAllCalls();
                    HeaderTextBlock.Text = "Llamadas(Entrada/Salida) - Disponible";
                    ttxtTiempoLlamada.Text = HoraLlamadaTextBox.Text = "00:00:00";
                    dispatcherTimer.Stop();
                    LimpiarDatosEntrada();
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
                {
                    LlamadaPausada = telefono.LinphoneCore.CurrentCall;
                    //telefono.LinphoneCore.AddToConference(LlamadaPausada);
                    llamadaConferencia.Add(LlamadaPausada);
                }


                if (LlamadaPausada.State == CallState.StreamsRunning)
                {
                    telefono.LinphoneCore.PauseCall(LlamadaPausada);
                    HeaderTextBlock.Text = "Llamadas(Entrada/Salida) - Pausa";
                    btPausa.Label = "Reanudar llamada";
                    enPausa = true;
                    pseg = 0;
                    pmin = 0;
                    pseg = 0;
                    psegunds = 0;
                    if(extensionConLlamada==extension)
                        InsertarPausa();
                }
                else if (LlamadaPausada.State == CallState.Paused)
                {
                    telefono.LinphoneCore.ResumeCall(LlamadaPausada);
                    HeaderTextBlock.Text = "Llamadas(Entrada/Salida) - En llamada";
                    btPausa.Label = "Pausar llamada";
                    enPausa = false;
                    LlamadaPausada = null;
                    if(extensionConLlamada== extension)
                        ActualizarPausa();
                }
                //ConsultarPausas();
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
                    telefono.LinphoneCore.TransferCall(call, ttxtNumeroATranferir.Text);
                    HeaderTextBlock.Text = "Llamadas(Entrada/Salida) - Diponible";
                }
            }


        }

        private void CambioTap(object sender, SelectionChangedEventArgs e)
        {
            ConsultarParametros();
            Pivot PV = (Pivot)sender; 
            if (((FrameworkElement)PV.SelectedItem).Name == "tapEntrada")
            {
                btTrasnferir.Visibility = Visibility.Collapsed;
                btConferencia.Visibility = Visibility.Collapsed;
            }
            else if (((FrameworkElement)PV.SelectedItem).Name == "tapSalida")
            {
                btTrasnferir.Visibility = Visibility.Collapsed;
                btConferencia.Visibility = Visibility.Collapsed;
            }
            else if (((FrameworkElement)PV.SelectedItem).Name == "tapLamadaManual")
            {
                btTrasnferir.Visibility = Visibility.Collapsed;
                btConferencia.Visibility = Visibility.Collapsed;
            }       
            else if (((FrameworkElement)PV.SelectedItem).Name == "tapTransfer")
            {
                tNoExttextBox.Text = extensionConLlamada;
                btTrasnferir.Visibility = Visibility.Visible;
                btConferencia.Visibility = Visibility.Collapsed;             
            }
            else if (((FrameworkElement)PV.SelectedItem).Name == "tapConferencia")
            {
                btTrasnferir.Visibility = Visibility.Collapsed;
                btConferencia.Visibility = Visibility.Visible;
                CrearConferencia();
            }
        }

        public void ConsultarParametros()
        {
            ParametroLocal.IdParamtro = usuario.Id;
            ParametroLocal.NumeroMaquina = idhardware;
            ParametroLocal.DireccionIp = ip;
            presentador.ConsultarParametros();
        }

        public List<Parametros> Parametros
        {
            set
            {
                for (int i = 0; i < value.Count; i++)
                {

                    switch (value[i].PalabraReservada)
                    {
                        case "@NUMEROTEL@":
                            noTelefonico = value[i].ValorParametro;
                            break;
                        case "@IDLLAMADA@":
                            IdLlamada = value[i].ValorParametro;
                            break;
                        case "@IDCLIENTE@":
                            IdCliente = value[i].ValorParametro;
                            break;
                        case "@IDCRM@":
                            IdCrm = value[i].ValorParametro;
                            break;
                        case "@NOMCOMPLETO@":
                            ttxtNombreCompletoconf.Text = ttxtNombreCompleto.Text = NombreCliente = value[i].ValorParametro;
                            break;
                    }
                }
            }
        }

        private void OnStats(Core lc, Call call, CallStats stats)
        {

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            var parametros = e.Parameter;
            usuario = (Usuario)parametros.GetType().GetProperty("usuario").GetValue(parametros, null);
            telefono = (SoftphoneEmbebed)parametros.GetType().GetProperty("softphone").GetValue(parametros, null);
            telefonoOut = (SoftphoneEmbebed)parametros.GetType().GetProperty("softphone").GetValue(parametros, null);
            telefono.listener = Factory.Instance.CreateCoreListener();
            telefono.listener.OnRegistrationStateChanged = OnRegistration;
            telefono.listener.OnCallStateChanged = OnCall;
            telefono.listener.OnCallStatsUpdated = OnStats;
            telefono.LinphoneCore.AddListener(telefono.listener);
            onRegister();
            OnregisterSalida();
            ConsultarSupervisores();

            //ConsultarUsuarios();
        }

        public void onRegister()
        {
            try
            {

               
                iniciado = true;
                ParametroLocal.IdParamtro = usuario.Id;
                ParametroLocal.DireccionIp = ip;
                ParametroLocal.NumeroMaquina = idhardware;
                extension = NoExtensionEntrada.Text = usuario.Extension;
                string password = Constantes.PASS_CALL + usuario.Extension;
                var authInfo = Factory.Instance.CreateAuthInfo(usuario.Extension, null, password, null, null, Constantes.DIRECCION_ELAXTIC);
                telefono.LinphoneCore.AddAuthInfo(authInfo);

                var proxyConfig = telefono.LinphoneCore.CreateProxyConfig();
                var identity = Factory.Instance.CreateAddress("sip:" + usuario.Extension + "sample@domain.tld");
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
        
        public void OnregisterSalida()
        {
            try
            {
                iniciado = true;
                ParametroLocal.IdParamtro = usuario.Id;
                ParametroLocal.DireccionIp = ip;
                ParametroLocal.NumeroMaquina = idhardware;
                extensionOut = txtExtesionSalida.Text= usuario.ExtensionOutbound;
                string password = Constantes.PASS_CALL + usuario.ExtensionOutbound;
                var authInfo = Factory.Instance.CreateAuthInfo(usuario.ExtensionOutbound, null, password, null, null, Constantes.DIRECCION_ELAXTIC);
                telefonoOut.LinphoneCore.AddAuthInfo(authInfo);

                var proxyConfig = telefonoOut.LinphoneCore.CreateProxyConfig();
                var identity = Factory.Instance.CreateAddress("sip:" + usuario.ExtensionOutbound + "sample@domain.tld");
                identity.Username = usuario.ExtensionOutbound;
                identity.Domain = Constantes.DIRECCION_ELAXTIC;
                proxyConfig.Edit();
                proxyConfig.IdentityAddress = identity;
                proxyConfig.ServerAddr = Constantes.DIRECCION_ELAXTIC;
                proxyConfig.Route = Constantes.DIRECCION_ELAXTIC;
                proxyConfig.RegisterEnabled = true;
                proxyConfig.Done();
                telefonoOut.LinphoneCore.AddProxyConfig(proxyConfig);
                telefonoOut.LinphoneCore.DefaultProxyConfig = proxyConfig;
                telefonoOut.LinphoneCore.RefreshRegisters();
                telefonoOut.OnStart();
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
            if (extensionConLlamada == extension)
                Tiempollamada();
            else if (extensionConLlamada == extensionOut)
                TiempollamadaSalida();
            else
                tiempollamadaSalidaManual();


            if (enPausa == true)
            {
                TiempoPausa();
            }

        }

        private void Tiempollamada()
        {
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
            ttxtTiempoLlamada.Text = txtEntradaTiempoLlamada.Text = Shor + ":" + Smin + ":" + Sseg;
            segunds += 1;
        }

        private void TiempollamadaSalida()
        {
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
            ttxtTiempoLlamada.Text = txtTiempoLlamadaSalida.Text = Shor + ":" + Smin + ":" + Sseg;
            segunds += 1;
        }
     
        private void tiempollamadaSalidaManual()
        {
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
            ttxtTiempoLlamada.Text = lltxtTiempoLlamadaOut.Text = Shor + ":" + Smin + ":" + Sseg;
            segunds += 1;
        }

        private void TiempoPausa()
        {
            if (pmin == 60)
            {
                phor += 1;
                pmin = 0;
            }
            if (pseg == 60)
            {
                pmin += 1;
                pseg = 0;
            }
            pseg += 1;
            String Sseg = "0";
            if (pseg < 10)
            { Sseg += pseg.ToString(); }
            else
            {
                Sseg = pseg.ToString();
            }
            String Smin = "0";
            if (pmin < 10)
            { Smin += pmin.ToString(); }
            else
            {
                Smin = pmin.ToString();
            }
            String Shor = "0";
            if (phor < 10)
            { Shor += phor.ToString(); }
            else
            {
                Shor = phor.ToString();
            }
            HeaderTextBlock.Text = "Llamadas(Entrada/Salida) - Pausa - " + Shor + ":" + Smin + ":" + Sseg;
            psegunds += 1;
        }

        private string GetDateString()
        {
            DateTime dateTime = DateTime.Now;
            string text = dateTime.ToString("dd/MM/yyyy");// + "-" + dateTime.TimeOfDay.ToString();
            return text;
        }

        private void LimpiarDatosEntrada()
        {
            BorrarParametros();
            llamadaConferencia.Clear();
            seg = 0;
            min = 0;
            hor = 0;
            segunds = 0;
            pseg = 0;
            pmin = 0;
            phor = 0;
            psegunds = 0;
            enPausa = false;
            ttxtTiempoLlamada.Text = txtEntradaTiempoLlamada.Text = "00:00:00";
            tEstadoTextBox.Text = EstadoTextBox.Text = "";
            tCiudadTextBox.Text = ciudadTextBox.Text = "";
            tipoTelefonoTextBox.Text = "";
            tipoLlamadaTextBox.Text = "";
            estatusTextBox.Text = "";
            TiempoEsperaListView.ItemsSource = null;
            FlyInvitadosListView.ItemsSource = null;
            SupervisoresListView.ItemsSource = null;
            lbCoincidencias.Foreground = new SolidColorBrush(Colors.White);

        }

        private void LimpiarDatosSalida()
        {
            BorrarParametros();
            llamadaConferencia.Clear();
            seg = 0;
            min = 0;
            hor = 0;
            segunds = 0;
            pseg = 0;
            pmin = 0;
            phor = 0;
            psegunds = 0;
            enPausa = false;
            txtTiempoLlamadaSalida.Text = "00:00:00";
            txtIdllamadaSalida.Text = "";
           
            TiempoEsperaListView.ItemsSource = null;
            FlyInvitadosListView.ItemsSource = null;
            SupervisoresListView.ItemsSource = null;
            lbCoincidencias.Foreground = new SolidColorBrush(Colors.White);

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
            if (extensionConLlamada == extension)
            {
                ConsultarIdLlamada(NoExtensionEntrada.Text);
                ConsultarPortabilidad(noTelefonico);
                ConsultarClientesTel(noTelefonico);
            }
            else if (extensionConLlamada == extensionOut)
            {
                ConsultarIdLlamada(txtExtesionSalida.Text);
            }

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
                    tEstadoTextBox.Text = EstadoTextBox.Text = value[0].EstadoPortabilidad;
                    tCiudadTextBox.Text = ciudadTextBox.Text = value[0].MunicipioPortabilidad;
                    tipoTelefonoTextBox.Text = value[0].RedPortabilidad;
                    tipoLlamadaTextBox.Text = value[0].TipoLlamadaPortabilidad;
                    portabilidadParametros = value[0];
                }
                catch { }
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
                try
                {
                    if(extensionConLlamada == extension)
                        IdLlamada = tIdllamtextBox.Text = txtidLlamadaEntrada.Text  = value;
                    else if(extensionConLlamada == extensionOut)
                        IdLlamada = tIdllamtextBox.Text = txtIdllamadaSalida.Text  = value;
                    ParametroLocal.DescripcionValor = "Llamada id";
                    ParametroLocal.PalabraReservada = "@IDLLAMADA@";
                    ParametroLocal.ValorParametro = IdLlamada;
                    InsertarParametros();
                }
                catch { }
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
                    lbCoincidencias.Text = "Coincidencias encontradas con número telefónico: " + numTelefonico;
                    ParametroLocal.ValorParametro = "S";
                }
                else
                {
                    lbCoincidencias.Foreground = new SolidColorBrush(Colors.Red);
                    lbCoincidencias.Text = "Coincidencias no encontradas con número telefónico: " + numTelefonico;
                    ParametroLocal.ValorParametro = "N";
                }
                ParametroLocal.DescripcionValor = "coincidencia";
                ParametroLocal.PalabraReservada = "@COINCIDENCIA@";
                InsertarParametros();

                ParametroLocal.DescripcionValor = "numero entrante";
                ParametroLocal.PalabraReservada = "@NUMEROTEL@";
                ParametroLocal.ValorParametro = numTelefonico;
                InsertarParametros();
            }
        }

        public void BorrarParametros()
        {
            presentador.BorrarParametros();
        }

        public void InsertarParametros()
        {
            presentador.InsertarParametros();
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
                PausAux.IdPausa = ultimoIdPausaLocal;
                PausAux.InicioPausa = GetDateString();
                PausAux.Finalpausa = GetDateString();
                PausAux.Duracion = "0";
                return PausAux;

            }
            set
            {


            }
        }

        private void PruebaPausar(object sender, RoutedEventArgs e)
        {

            if (pausado == false)
            {

                pausado = true;
                InsertarPausa();
            }
            else
            {
                pausado = false;
                ActualizarPausa();
            }
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

        public List<Pausas> Pausas
        {
            set
            {
                TiempoEsperaListView.ItemsSource = value;
            }
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

        public string ultimoIdPausa
        {
            set
            {
                try
                {
                    ultimoIdPausaLocal = value;
                }
                catch { }
            }
        }
        //funciones de transferencia
        private void digitarNumero(object sender, RoutedEventArgs e)
        {
            Button padnumeric = (Button)sender;
            switch (padnumeric.Name)
            {
                case "tbt7":
                    ttxtNumeroATranferir.Text = ttxtNumeroATranferir.Text + "7";
                    break;
                case "tbt8":
                    ttxtNumeroATranferir.Text = ttxtNumeroATranferir.Text + "8";
                    break;
                case "tbt9":
                    ttxtNumeroATranferir.Text = ttxtNumeroATranferir.Text + "9";
                    break;
                case "tbt4":
                    ttxtNumeroATranferir.Text = ttxtNumeroATranferir.Text + "4";
                    break;
                case "tbt5":
                    ttxtNumeroATranferir.Text = ttxtNumeroATranferir.Text + "5";
                    break;
                case "tbt6":
                    ttxtNumeroATranferir.Text = ttxtNumeroATranferir.Text + "6";
                    break;
                case "tbt3":
                    ttxtNumeroATranferir.Text = ttxtNumeroATranferir.Text + "3";
                    break;
                case "tbt2":
                    ttxtNumeroATranferir.Text = ttxtNumeroATranferir.Text + "2";
                    break;
                case "tbt1":
                    ttxtNumeroATranferir.Text = ttxtNumeroATranferir.Text + "1";
                    break;
                case "tbta":
                    ttxtNumeroATranferir.Text = ttxtNumeroATranferir.Text + "*";
                    break;
                case "tbt0":
                    ttxtNumeroATranferir.Text = ttxtNumeroATranferir.Text + "0";
                    break;
                case "tbtg":
                    ttxtNumeroATranferir.Text = ttxtNumeroATranferir.Text + "#";
                    break;
                case "cbt7":
                    txtNotelefonico.Text = txtNotelefonico.Text + "7";
                    break;
                case "cbt8":
                    txtNotelefonico.Text = txtNotelefonico.Text + "8";
                    break;
                case "cbt9":
                    txtNotelefonico.Text = txtNotelefonico.Text + "9";
                    break;
                case "cbt4":
                    txtNotelefonico.Text = txtNotelefonico.Text + "4";
                    break;
                case "cbt5":
                    txtNotelefonico.Text = txtNotelefonico.Text + "5";
                    break;
                case "cbt6":
                    txtNotelefonico.Text = txtNotelefonico.Text + "6";
                    break;
                case "cbt3":
                    txtNotelefonico.Text = txtNotelefonico.Text + "3";
                    break;
                case "cbt2":
                    txtNotelefonico.Text = txtNotelefonico.Text + "2";
                    break;
                case "cbt1":
                    txtNotelefonico.Text = txtNotelefonico.Text + "1";
                    break;
                case "cbta":
                    txtNotelefonico.Text = txtNotelefonico.Text + "*";
                    break;
                case "cbt0":
                    txtNotelefonico.Text = txtNotelefonico.Text + "0";
                    break;
                case "cbtg":
                    txtNotelefonico.Text = txtNotelefonico.Text + "#";
                    break;
            }
        }

        public void ConsultarSupervisores()
        {
            presentador.ConsultarSupervisores(usuario.Id);
        }

        public List<Supervisores> Supervisores
        {
            set
            {
                SupervisoresListView.ItemsSource = value;
            }
        }

        private void supervisoresListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var supervisor = e.ClickedItem;

            if (supervisor != null)
            {
                var contenido = (string)supervisor.GetType().GetProperty("ExtensionSupervisor").GetValue(supervisor, null);
                ttxtNumeroATranferir.Text = contenido;
            }

        }
        //funciones de conferencia
        private void agregarInvitados(object sender, RoutedEventArgs e)
        {
            if (telefono.LinphoneCore.CallsNb == 0)
            {

            }
            else
            {
                int secuencia = InvitadosListView.Items.Count;
                if (txtParticipantes.Text != "")
                {
                    if (txtNotelefonico.Text != "")
                    {
                        invitados.Add(
                          new Usuario()
                          {
                              Nombre = txtParticipantes.Text,
                              Puesto = "Externo",
                              Extension = txtNotelefonico.Text,
                              Id = Convert.ToString(secuencia + 1)
                          }
                          );
                        InvitadosListView.ItemsSource = null;
                        InvitadosListView.ItemsSource = invitados;
                        telefono.LinphoneCore.Invite(txtNotelefonico.Text);
                    }
                }
            }
        }

        private void CrearConferencia()
        {
            if (telefono.LinphoneCore.CallsNb == 0)
            {

            }
            else
            {
                Call call = telefono.LinphoneCore.CurrentCall;
                if (call.State == CallState.StreamsRunning)
                {

                    ConferenceParams conf = telefono.LinphoneCore.CreateConferenceParams();
                    telefono.LinphoneCore.CreateConferenceWithParams(conf);
                    //Pausar("", null);
                    HeaderTextBlock.Text = "Llamadas(Entrada/Salida) - Conferencia";
                }
            }
        }

        private void Conferenciar(object sender, RoutedEventArgs e)
        {
            if (telefono.LinphoneCore.CallsNb == 0)
            {

            }
            else
            {
                foreach (Call cl in llamadaConferencia)
                {
                    if (cl.State == CallState.Paused && cl != null)
                        telefono.LinphoneCore.AddToConference(cl);
                }
                telefono.LinphoneCore.AddAllToConference();
                enPausa = false;
                HeaderTextBlock.Text = "Llamadas(Entrada/Salida) - Conferencia";
            }
        }

        private void DeleteConfirmation_Click(object sender, RoutedEventArgs e)
        {
            Flyout f = this.Control1.Flyout as Flyout;
            if (f != null)
            {
                f.Hide();
            }
        }

        public void ConsultarUsuarios(object sender, RoutedEventArgs e)
        {
            presentador.ConsultarUsuarios();
        }

        public List<Usuario> usuarios
        {
            set
            {
                FlyInvitadosListView.ItemsSource = value;
            }
        }

        private void flyListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var usuarioConferencia = e.ClickedItem;

            if (usuarioConferencia != null)
            {
                int secuencia = InvitadosListView.Items.Count;

                invitados.Add(
                  new Usuario()
                  {
                      Nombre = (string)usuarioConferencia.GetType().GetProperty("Nombre").GetValue(usuarioConferencia, null),
                      Puesto = (string)usuarioConferencia.GetType().GetProperty("Puesto").GetValue(usuarioConferencia, null),
                      Extension = (string)usuarioConferencia.GetType().GetProperty("Extension").GetValue(usuarioConferencia, null),
                      Id = Convert.ToString(secuencia + 1)
                  }
                  );
                InvitadosListView.ItemsSource = null;
                InvitadosListView.ItemsSource = invitados;
                telefono.LinphoneCore.Invite(txtNotelefonico.Text);
                Flyout f = this.Control1.Flyout as Flyout;
                if (f != null)
                {
                    f.Hide();
                }
                
            }

        }

    }

}
