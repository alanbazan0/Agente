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


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace NavigationMenuSample.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AgentePage : Page, IAgenteVista
    {
        Usuario usuario;
        AgentePresentador presentador;
        CorreoPresentador correosPresentador;
        ComponenteFabrica criteriosSeleccionFabrica;
        private DispatcherTimer dispatcherTimer;
        Portabilidad portabilidadParametros;
        public string numTelefonico;
        int seg = 0;
        int min = 0;
        int hor = 0;
        int segunds = 0;
        List<CampoGrid> camposGloba;
        public Core LinphoneCore { get; set; }
       
        private CoreListener Listener;

        public AgentePage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
            this.Loaded += CommandBarPage_Loaded;
            webCorreo.NavigateToString("<html></html>");

            presentador = new AgentePresentador(this);
            //correosPresentador = new CorreoPresentador(this);

            criteriosSeleccionFabrica = new ComponenteFabrica();
            usuario = null;
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            presentador.CrearCatalogoClientes();

            indicadores.Visibility = Visibility.Collapsed;
            contactos.Visibility = Visibility.Collapsed;
            correosSalida.Visibility = Visibility.Collapsed;
            try
            {
                string rc_path = null;
                LinphoneWrapper.setNativeLogHandler();

                Core.SetLogLevelMask(0xFF);
                CoreListener listener = Factory.Instance.CreateCoreListener();
                listener.OnGlobalStateChanged = OnGlobal;
                LinphoneCore = Factory.Instance.CreateCore(listener, rc_path, null);
                LinphoneCore.NetworkReachable = true;
                //LinphoneCore.MicEnabled = true;
                //LinphoneCore.MicGainDb = 90.0F;
                //LinphoneCore.MicGainDb = 90.0;


                Listener = Factory.Instance.CreateCoreListener();
                Listener.OnRegistrationStateChanged = OnRegistration;
                Listener.OnCallStateChanged = OnCall;
                Listener.OnCallStatsUpdated = OnStats;
                
                
                LinphoneCore.AddListener(Listener);
                
            }
            catch (Exception ex)
            { }
            HeaderTextBlock.Text += " - En linea";
            FechaLlamadaTextBox.Text = GetDateString();

            //numTelefonico = NoTelTextBox.Text = "8711897006";
            //ConsultarPortabilidad(NoTelTextBox.Text);
            //ConsultarClientesTel(NoTelTextBox.Text);

           // SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;

        }
        private void LinphoneCoreIterate(ThreadPoolTimer timer)
        {
            while (true)
            {
#if WINDOWS_UWP
                CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.High,
                () => {
                    LinphoneCore.Iterate();
                });
#else
                Device.BeginInvokeOnMainThread(() =>
                {
                    LinphoneCore.Iterate();
                });
                Thread.Sleep(50);
#endif
            }
        }

        private void OnGlobal(Core lc, GlobalState gstate, string message)
        {
#if WINDOWS_UWP
            Debug.WriteLine("Global state changed: " + gstate);
#else
            Console.WriteLine("Global state changed: " + gstate);
#endif
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);            
            onRegister(e);
            consultarCorreoEntrada();
        }
        public void onRegister(NavigationEventArgs e)
        {
            usuario = (Usuario)e.Parameter;
            try
            {
                NoExtensionTextBox.Text = usuario.Extension;
                string password = Constantes.PASS_CALL + usuario.Extension;
                var authInfo = Factory.Instance.CreateAuthInfo(usuario.Extension, null, password, null, null, Constantes.DIRECCION_ELAXTIC);
                LinphoneCore.AddAuthInfo(authInfo);

                var proxyConfig = LinphoneCore.CreateProxyConfig();
                var identity = Factory.Instance.CreateAddress("sip:sample@domain.tld");
                identity.Username = usuario.Extension;
                identity.Domain = Constantes.DIRECCION_ELAXTIC;
                proxyConfig.Edit();
                proxyConfig.IdentityAddress = identity;
                proxyConfig.ServerAddr = Constantes.DIRECCION_ELAXTIC;
                proxyConfig.Route = Constantes.DIRECCION_ELAXTIC;
                proxyConfig.RegisterEnabled = true;
                proxyConfig.Done();
                LinphoneCore.AddProxyConfig(proxyConfig);
                LinphoneCore.DefaultProxyConfig = proxyConfig;

                LinphoneCore.RefreshRegisters();
                
                OnStart();
                

            }
            catch (Exception ex)
            { }
        }

        protected  void OnStart()
        {
            // Handle when your app starts
#if WINDOWS_UWP
            TimeSpan period = TimeSpan.FromSeconds(1);
            ThreadPoolTimer PeriodicTimer = ThreadPoolTimer.CreatePeriodicTimer(LinphoneCoreIterate, period);
#else
            Thread iterate = new Thread(LinphoneCoreIterate);
            iterate.IsBackground = false;
            iterate.Start();
#endif
        }

        protected  void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected  void OnResume()
        {
            // Handle when your app resumes
        }
        
        private void OnRegistration(Core lc, ProxyConfig config, RegistrationState state, string message)
        {
#if WINDOWS_UWP
            Debug.WriteLine("Registration state changed: " + state);
#else
            Console.WriteLine("Registration state changed: " + state);
#endif
            //registration_status.Text = "Registration state changed: " + state;

            if (state == RegistrationState.Ok)
            {
                
                //register.IsEnabled = false;
            }
        }

        private void OnCall(Core lc, Call lcall, CallState state, string message)
        {
#if WINDOWS_UWP
            Debug.WriteLine("Call state changed: " + state);
#else
            Console.WriteLine("Call state changed: " + state);
#endif
            //HeaderTextBlock.Text = "Llamada - " + state;

                if (lc.CallsNb > 0)
            {
                if (state == CallState.IncomingReceived)
                {
                    
                     OnCallClicked();
                    numTelefonico = NoTelTextBox.Text = lcall.RemoteAddress.DisplayName;
                    ConsultarDatos(NoTelTextBox.Text);
                    //PRIMERO ME TRAIGO LA PORTABILIDAD Y LUEGO CONSULTO A BTCLIENTESTEL PARA VER SI YA EXISTE EN LA BASE DE DATOS
                    //consultar la tabla de BTCLIENTESTEL
                    // call.Text = "Answer Call (" + lcall.RemoteAddressAsString + ")";
                    // linphonecore.AcceptCall(call);
                    //presentador.ConsultarClientes();
                }
                else
                {
                    HeaderTextBlock.Text = "Llamada - Finalizada";
                    // call.Text = "Terminate Call";
                }
            }
            else
            {
                //call.Text = "Start Call";
                HeaderTextBlock.Text = "Llamada - Disponible";
                dispatcherTimer.Stop();
                //call_stats.Text = "";
            }
        }

        private void ConsultarDatos(string noTelefonico)
        {
           // ConsultarIdLlamada(noTelefonico);
            ConsultarPortabilidad(noTelefonico);
            ConsultarClientesTel(noTelefonico);
        }

        private void OnCallClicked()
        {
            if (LinphoneCore.CallsNb == 0)
            {
                var addr = LinphoneCore.InterpretUrl(Constantes.DIRECCION_ELAXTIC);
                LinphoneCore.InviteAddress(addr);
            }
            else
            {
                Call call = LinphoneCore.CurrentCall;
                if (call.State == CallState.IncomingReceived)
                {
                    LinphoneCore.AcceptCall(call);
                    LimpiarDatos();
                    
                    HoraLlamadaTextBox.Text = GetTimeString();
                    dispatcherTimer.Start();
                }
                else
                {
                    LinphoneCore.TerminateAllCalls();
                    HeaderTextBlock.Text = "Llamada - Disponible";
                    dispatcherTimer.Stop();
                }
            }
        }

        private void AppBarCallEndButton_Click(object sender, RoutedEventArgs e)
        {
            OnCallClicked();
        }
        
        private void OnStats(Core lc, Call call, CallStats stats)
        {
#if WINDOWS_UWP
            Debug.WriteLine("Call stats: " + stats.DownloadBandwidth + " kbits/s / " + stats.UploadBandwidth + " kbits/s");
#else
            Console.WriteLine("Call stats: " + stats.DownloadBandwidth + " kbits/s / " + stats.UploadBandwidth + " kbits/s");
#endif
            //call_stats.Text = "Call stats: " + stats.DownloadBandwidth + " kbits/s / " + stats.UploadBandwidth + " kbits/s";

        }
        
        public List<Campo> Filtros
        {
            get
            {
                List<Campo> filtros = new List<Campo>();
                foreach (IComponente componente in criteriosSeleccionStackPanel.Children)
                {
                    if(componente.Filtro.valor != string.Empty)
                        filtros.Add(componente.Filtro);
                }                    
                return filtros;
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

        public List<Portabilidad> Portabilidad
        {
            set
            {

                EstadoTextBox.Text = value[0].EstadoPortabilidad;
                ciudadTextBox.Text = value[0].MunicipioPortabilidad;
                tipoTelefonoTextBox.Text = value[0].RedPortabilidad;
                tipoLlamadaTextBox.Text = value[0].TipoLlamadaPortabilidad;

                portabilidadParametros = value[0];
            }
        }

        public List<Correos> correos
        {
            set
            {
                correosListView.ItemsSource = value;
                //if(!value[0].Contenido.Equals(""))
                //{ 
                //    byte[] datos = Convert.FromBase64String(value[0].Contenido);
                //    string htmlCadena = Encoding.UTF8.GetString(datos);
                //    webCorreo.NavigateToString(htmlCadena);
                //}
            }
        }

        public string setIdLlamada
        {
            set
            {
                idLlamadaTextBox.Text = value;
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
       
        private void ApprRecesoButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(RecesoPage), usuario);
        }

        public void ConsultarClientes()
        {
            progressRing.IsActive = true;
            presentador.ConsultarClientes();
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
        
        public void CrearCriterioSeleccion(Componente criterioSeleccion)
        {
            IComponente componente = criteriosSeleccionFabrica.CrearComponente(criterioSeleccion);
            criteriosSeleccionStackPanel.Children.Add(componente as UIElement);
        }

        private void clientesListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            //var cliente = clientesListView.SelectedItem;
            var cliente = e.ClickedItem;
            
            if (cliente != null)
            {
                var numero = camposGloba.Where(a => a.campoId == "BTCLIENTENUMERO")
                                        .Select(a => a.id)
                                        .First();
                string alias = "C" + numero.ToString();
                int idCliente = Int32.Parse((string)cliente.GetType().GetProperty(alias).GetValue(cliente, null));
                
                var parametros = new { modo = ModoVentana.CAMBIOS, idCliente = idCliente, portabilidad = portabilidadParametros };
                this.Frame.Navigate(typeof(AgenteApp.UWP.Vistas.ClientePage), parametros);
            }

        }
        
        private void correosListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            //var cliente = clientesListView.SelectedItem;
            var correos = e.ClickedItem;

            if (correos != null)
            {
                var contenido= (string)correos.GetType().GetProperty("Contenido").GetValue(correos, null);
                byte[] datos = Convert.FromBase64String(contenido);
                string htmlCadena = Encoding.UTF8.GetString(datos);
                webCorreo.NavigateToString(htmlCadena);
                /* var numero = camposGloba.Where(a => a.c == "BTCLIENTENUMERO").Select(a => a.id).First();
                 string alias = "C" + numero.ToString();
                 int idCliente = Int32.Parse((string)correos.GetType().GetProperty(alias).GetValue(correos, null));

                 var parametros = new { modo = ModoVentana.CAMBIOS, idCliente = idCliente };
                 this.Frame.Navigate(typeof(AgenteApp.UWP.Vistas.ClientePage), parametros);*/
            }

        }
        public void CrearColumnasGrid1(List<CampoGrid> campos)
        {
            camposGloba = campos;
            StringBuilder xamlHeaderTemplate = new StringBuilder();
            xamlHeaderTemplate.AppendLine(@"<DataTemplate xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"">");
            xamlHeaderTemplate.AppendLine(@"<Grid Padding = ""0"" Margin = ""0"">");           
            xamlHeaderTemplate.AppendLine(@"<Grid.ColumnDefinitions>");
            foreach (CampoGrid campo in campos)
            {
                xamlHeaderTemplate.AppendLine(@"<ColumnDefinition Width=""200""/>");
            }
            xamlHeaderTemplate.AppendLine(@"</Grid.ColumnDefinitions>");
            for (int i = 0; i < campos.Count; i++)           
            {
                CampoGrid campo = campos[i];
                xamlHeaderTemplate.AppendLine(@"<Border  Grid.Column="""+ i.ToString()+ @""" CornerRadius=""0"" BorderBrush=""Black"" Background=""#ff9900"" BorderThickness=""0 0 0 0"">");                                                  
                xamlHeaderTemplate.AppendLine(@"<TextBlock Text=""" + campo.titulo + @""" Foreground=""White"" MaxLines=""2"" TextWrapping=""WrapWholeWords""/>");
                xamlHeaderTemplate.AppendLine(@"</Border>");
            }
            xamlHeaderTemplate.AppendLine(@"</Grid>");           
            xamlHeaderTemplate.AppendLine(@"</DataTemplate>");            
            var headerTemplate = Windows.UI.Xaml.Markup.XamlReader.Load(xamlHeaderTemplate.ToString()) as DataTemplate;          
            clientesListView.HeaderTemplate = headerTemplate;

            StringBuilder xamlItemTemplate = new StringBuilder();
            xamlItemTemplate.AppendLine(@"<DataTemplate xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"">");
            xamlItemTemplate.AppendLine(@"<Grid Padding = ""0"" Margin = ""0"">");
            xamlItemTemplate.AppendLine(@"<Grid.ColumnDefinitions>");
            foreach (CampoGrid campo in campos)
            {
                xamlItemTemplate.AppendLine(@"<ColumnDefinition Width=""200""/>");
            }
            xamlItemTemplate.AppendLine(@"</Grid.ColumnDefinitions>");
            //xamlItemTemplate.AppendLine(@"<Grid.RowDefinitions>");
            //xamlItemTemplate.AppendLine(@"<RowDefinition Height = ""Auto""></RowDefinition>");
            //xamlItemTemplate.AppendLine(@"</Grid.RowDefinitions>");                                    
            for (int i = 0; i < campos.Count; i++)
            {
                CampoGrid campo = campos[i];
                xamlItemTemplate.AppendLine(@"<Border  Grid.Column=""" + i.ToString() + @""" CornerRadius=""0"" BorderBrush=""Black"" BorderThickness=""0 0 0 0"">");
                xamlItemTemplate.AppendLine(@"<TextBlock Text=""{Binding C" + campo.id + @"}"" Foreground=""Black"" MaxLines=""2"" TextWrapping=""WrapWholeWords""/>");
                xamlItemTemplate.AppendLine(@"</Border>");
            }
            xamlItemTemplate.AppendLine(@"</Grid>");
            xamlItemTemplate.AppendLine(@"</DataTemplate>");
            var itemTemplate = Windows.UI.Xaml.Markup.XamlReader.Load(xamlItemTemplate.ToString()) as DataTemplate;
            clientesListView.ItemTemplate = itemTemplate;
        }

        private void AppBarAltaClienteButton_Click(object sender, RoutedEventArgs e)
        {
            if (!NoTelTextBox.Text.Equals(string.Empty))
            {
                var parametros = new { modo = ModoVentana.ALTA,
                                       telCliente = NoTelTextBox.Text,
                                       portabilidad = portabilidadParametros  };
                this.Frame.Navigate(typeof(AgenteApp.UWP.Vistas.ClientePage), parametros);
            }
            else
                MostrarMensajeAsync("Error", "Es necesario el numero telefonico para dar de alta al usuario");
        
        }

        private string GetDateString()
        {
            DateTime dateTime = DateTime.Now;
            string text = dateTime.ToString("dd/MM/yyyy");// + "-" + dateTime.TimeOfDay.ToString();
            return text;
        }

        private string GetTimeString()
        {
            DateTime dateTime = DateTime.Now;
            string text =  dateTime.TimeOfDay.ToString();
            text = text.Remove(text.IndexOf('.'));
            return text;
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

        public void ConsultarPortabilidad(string numero)
        {
            presentador.ConsultarPortabilidad(numero);
        }
        public void ConsultarIdLlamada(string extension)
        {
            presentador.ConsultarIdLlamada(extension);
        }

        public void ConsultarClientesTel(string numero)
        {
            progressRing.IsActive = true;
            presentador.ConsultarClientesTel(numero);
        }

        void clickCorreo (object sender, RoutedEventArgs e)
        {
            calendario.Visibility= Visibility.Visible;
            indicadores.Visibility = Visibility.Collapsed;
            contactos.Visibility = Visibility.Collapsed;
        }
        void clickContacto(object sender, RoutedEventArgs e)
        {
            calendario.Visibility = Visibility.Collapsed;
            indicadores.Visibility = Visibility.Collapsed;
            contactos.Visibility = Visibility.Visible;
        }
        void clickIndicadores(object sender, RoutedEventArgs e)
        {
            calendario.Visibility = Visibility.Collapsed;
            indicadores.Visibility = Visibility.Visible;
            contactos.Visibility = Visibility.Collapsed;
        }
        void clickCorreoE(object sender, RoutedEventArgs e)
        {
            correosEntrada.Visibility = Visibility.Visible;
            correosSalida.Visibility = Visibility.Collapsed;
        }
        void clickCorreoS(object sender, RoutedEventArgs e)
        {
            correosSalida.Visibility = Visibility.Visible;
            correosEntrada.Visibility = Visibility.Collapsed;
            SendEmailButton();
        }
        private void webCorreo_Loaded(object sender, EventArgs e)
        {
            webCorreo.NavigateToString("<html><body><h1>test</h1></body></html>");
        }

        public void consultarCorreoEntrada()
        {
            //correosPresentador.consultarCorreoEntrada(usuario.Id);
        }
        private async void SendEmailButton()
        {
            EmailMessage emailMessage = new EmailMessage();
            emailMessage.To.Add(new EmailRecipient("***@***.com"));
            string messageBody = "Hello World";
            emailMessage.Body = messageBody;
            /*StorageFolder MyFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            StorageFile attachmentFile = await MyFolder.GetFileAsync("MyTestFile.txt");
            if (attachmentFile != null)
            {
                var stream = Windows.Storage.Streams.RandomAccessStreamReference.CreateFromFile(attachmentFile);
                var attachment = new Windows.ApplicationModel.Email.EmailAttachment(
                         attachmentFile.Name,
                         stream);
                emailMessage.Attachments.Add(attachment);
            }*/
            await EmailManager.ShowComposeNewEmailAsync(emailMessage);
        }

        private void LimpiarDatos()
        {
             seg = 0;
             min = 0;
             hor = 0;
             segunds = 0;
            HeaderTextBlock.Text = "Llamada - Iniciada";
            timpoLlamadaTextBox.Text = "00:00:00";
        }
   

        
    }
}
