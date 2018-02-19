using AgenteApp.Clases;
using AgenteApp.Modelos;
using AgenteApp.Presentadores;
using AgenteApp.Vistas;
using Linphone;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace NavigationMenuSample.Views
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class LlamarPredictivoPage : Page, ILlamarPredictivoVista
    {
        Usuario usuario;
        List<Usuario> invitados;
        LlamarPredictivoPresentador presentador;
        private DispatcherTimer dispatcherTimer;
        private SoftphoneEmbebed telefonoOut;

        Call LlamadaPausada;
        List<Call> llamadaConferencia;
        string numTelefonico;
        Boolean pausado = false;
        Boolean enPausa = false;
        Boolean iniciado = false;
        int seg = 0;
        int min = 0;
        int hor = 0;
        int segunds = 0;
        int pseg = 0;
        int pmin = 0;
        int phor = 0;
        int psegunds = 0;
        string extension;

        public LlamarPredictivoPage()
        {
            this.InitializeComponent();
            // this.Loaded += CommandBarPage_Loaded;
            //this.Unloaded += App_Suspending;
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            HeaderTextBlock.Text = "Llamada entrante - Disponible";
            tFechaextBox.Text = txtTiempoLlamada.Text = GetDateString();
            presentador = new LlamarPredictivoPresentador(this);           
            llamadaConferencia = new List<Call>();
        }

        private void App_Suspending(Object sender, RoutedEventArgs e)
        {
            telefonoOut.LinphoneCore.RemoveListener(telefonoOut.listener);
        }


        private void OnRegistrationOut(Core lc, ProxyConfig config, RegistrationState state, string message)
        {
#if WINDOWS_UWP
            Debug.WriteLine("Registration state changed: " + state);
#else
            Console.WriteLine("Registration state changed: " + state);
#endif
            Console.WriteLine("Registration state changed: " + state);
        }

        private void OnCallOut(Core lc, Call lcall, CallState state, string message)
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
                else if (state == CallState.OutgoingInit)
                {
                    llamadaConferencia.Add(lcall);
                }



            }
            else
            {
                HeaderTextBlock.Text = "LLAMAR (PREDICTIVO-CALL BACK) - Disponible";
                ttxtTiempoLlamada.Text  = txtTiempoLlamada.Text = "00:00:00";
                dispatcherTimer.Stop();
                LimpiarDatos();
            }


        }

        private void OnStatsOut(Core lc, Call call, CallStats stats)
        {

        }

        private void Contestar()
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
                    HeaderTextBlock.Text = "LLAMAR (PREDICTIVO-CALL BACK) - En llamada";
                    LimpiarDatos();
                    numTelefonico = txtNoTelOrigen.Text = ttxtNumeroTelefono.Text = txtNumtelefonico.Text = call.RemoteAddress.DisplayName;
                    tHoratextBox.Text = GetTimeString();
                    dispatcherTimer.Start();
                    ConsultarDatos(numTelefonico);

                }

            }
        }

        private void ConsultarDatos(string noTelefonico)
        {
            //ConsultarIdLlamada(NoExtensionTextBox.Text);
            //ConsultarClientes(noTelefonico);
        }

        private void Colgar(object sender, RoutedEventArgs e)
        {
            if (telefonoOut.LinphoneCore.CallsNb == 0)
            {

            }
            else
            {
                Call call;
                if (telefonoOut.LinphoneCore.CurrentCall != null)
                {
                    call = telefonoOut.LinphoneCore.CurrentCall;
                    if (call.State == CallState.StreamsRunning)
                    {
                        telefonoOut.LinphoneCore.TerminateAllCalls();
                        HeaderTextBlock.Text = "LLAMAR (PREDICTIVO-CALL BACK) - Disponible";
                        ttxtTiempoLlamada.Text  = txtTiempoLlamada.Text = "00:00:00";
                        dispatcherTimer.Stop();
                        LimpiarDatos();
                    }
                }
                else if (LlamadaPausada != null)
                {

                    if (LlamadaPausada.State == CallState.Paused || LlamadaPausada.State == CallState.StreamsRunning)
                    {
                        telefonoOut.LinphoneCore.TerminateAllCalls();
                        HeaderTextBlock.Text = "LLAMAR (PREDICTIVO-CALL BACK) - Disponible";
                        ttxtTiempoLlamada.Text  = txtTiempoLlamada.Text = "00:00:00";
                        dispatcherTimer.Stop();
                        //LimpiarDatos();
                    }

                }
                else
                {
                    telefonoOut.LinphoneCore.TerminateAllCalls();
                    HeaderTextBlock.Text = "LLAMAR (PREDICTIVO-CALL BACK) - Disponible";
                    ttxtTiempoLlamada.Text  = txtTiempoLlamada.Text = "00:00:00";
                    dispatcherTimer.Stop();
                    LimpiarDatos();
                }

            }
        }


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

        private string GetTimeString()
        {
            DateTime dateTime = DateTime.Now;
            string text = dateTime.TimeOfDay.ToString();
            text = text.Remove(text.IndexOf('.'));
            return text;
        }

        private void LimpiarDatos()
        {
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
            ttxtTiempoLlamada.Text = txtTiempoLlamada.Text = "00:00:00";
            tEstadoTextBox.Text  = "";
            tCiudadTextBox.Text  = "";
            FlyInvitadosListView.ItemsSource = null;
            SupervisoresListView.ItemsSource = null;
        }

        void dispatcherTimer_Tick(object sender, object e)
        {
            ShowTime();
        }

        private void ShowTime()
        {
            Tiempollamada();
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
            ttxtTiempoLlamada.Text = txtTiempoLlamada.Text = Shor + ":" + Smin + ":" + Sseg;
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
            HeaderTextBlock.Text = "LLAMAR (PREDICTIVO-CALL  - Pausa - " + Shor + ":" + Smin + ":" + Sseg;
            psegunds += 1;
        }





        //funciones de transferencia


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
            if (telefonoOut.LinphoneCore.CallsNb == 0)
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
                        telefonoOut.LinphoneCore.Invite(txtNotelefonico.Text);
                    }
                }
            }
        }

        private void CrearConferencia()
        {
            if (telefonoOut.LinphoneCore.CallsNb == 0)
            {

            }
            else
            {
                Call call = telefonoOut.LinphoneCore.CurrentCall;
                if (call.State == CallState.StreamsRunning)
                {

                    ConferenceParams conf = telefonoOut.LinphoneCore.CreateConferenceParams();
                    telefonoOut.LinphoneCore.CreateConferenceWithParams(conf);
                    //Pausar("", null);
                    HeaderTextBlock.Text = "LLAMAR (PREDICTIVO-CALL BACK) - Conferencia";
                }
            }
        }

        private void Conferenciar(object sender, RoutedEventArgs e)
        {
            if (telefonoOut.LinphoneCore.CallsNb == 0)
            {

            }
            else
            {
                foreach (Call cl in llamadaConferencia)
                {
                    if (cl.State == CallState.Paused && cl != null)
                        telefonoOut.LinphoneCore.AddToConference(cl);
                }
                telefonoOut.LinphoneCore.AddAllToConference();
                enPausa = false;
                HeaderTextBlock.Text = "LLAMAR (PREDICTIVO-CALL BACK) - Conferencia";
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
            //presentador.ConsultarUsuarios();
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
                telefonoOut.LinphoneCore.Invite(txtNotelefonico.Text);
                Flyout f = this.Control1.Flyout as Flyout;
                if (f != null)
                {
                    f.Hide();
                }
            }

        }

        public List<ClienteTelefono> Clientes
        {
            set
            {
                
            }
        }

        public async void MostrarMensajeAsync(string titulo, string mensaje)
        {
            if (mensaje != null)
            {
                var dialog = new MessageDialog(mensaje, titulo);
                await dialog.ShowAsync();
            }
        }

        private void Transferir(object sender, RoutedEventArgs e)
        {

            if (telefonoOut.LinphoneCore.CallsNb == 0)
            {

            }
            else
            {
                Call call = telefonoOut.LinphoneCore.CurrentCall;
                if (call.State == CallState.StreamsRunning)
                {
                    telefonoOut.LinphoneCore.TransferCall(call, ttxtNumeroATranferir.Text);
                    HeaderTextBlock.Text = "LLAMAR (PREDICTIVO-CALL BACK) - Diponible";
                }
            }


        }

        private void Pausar(object sender, RoutedEventArgs e)
        {
            if (telefonoOut.LinphoneCore.CallsNb == 0)
            {

            }
            else
            {

                if (telefonoOut.LinphoneCore.CurrentCall != null)
                {
                    LlamadaPausada = telefonoOut.LinphoneCore.CurrentCall;
                    //telefonoOut.LinphoneCore.AddToConference(LlamadaPausada);
                    llamadaConferencia.Add(LlamadaPausada);
                }


                if (LlamadaPausada.State == CallState.StreamsRunning)
                {
                    telefonoOut.LinphoneCore.PauseCall(LlamadaPausada);
                    HeaderTextBlock.Text = "LLAMAR (PREDICTIVO-CALL BACK) - Pausa";
                    btPausa.Label = "Reanudar llamada";
                    enPausa = true;
                    pseg = 0;
                    pmin = 0;
                    pseg = 0;
                    psegunds = 0;
                    //InsertarPausa();
                }
                else if (LlamadaPausada.State == CallState.Paused)
                {
                    telefonoOut.LinphoneCore.ResumeCall(LlamadaPausada);
                    HeaderTextBlock.Text = "LLAMAR (PREDICTIVO-CALL BACK) - En llamada";
                    btPausa.Label = "Pausar llamada";
                    enPausa = false;
                    LlamadaPausada = null;
                    //ActualizarPausa();
                }
                //ConsultarPausas();
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            var parametros = e.Parameter;
            usuario = (Usuario)parametros.GetType().GetProperty("usuario").GetValue(parametros, null);
            //telefonoOut = (SoftphoneEmbebed)parametros.GetType().GetProperty("softphone").GetValue(parametros, null); ;
            //onRegister(e);
            ConsultarSupervisores();

            //ConsultarUsuarios();
        }

        public void onRegister(NavigationEventArgs e)
        {
            try
            {

                telefonoOut.listener = Factory.Instance.CreateCoreListener();
                telefonoOut.listener.OnRegistrationStateChanged = OnRegistrationOut;
                telefonoOut.listener.OnCallStateChanged = OnCallOut;
                telefonoOut.listener.OnCallStatsUpdated = OnStatsOut;
                telefonoOut.LinphoneCore.AddListener(telefonoOut.listener);

                iniciado = true;
                extension = txtExtesion.Text = tNoExttextBox.Text  = usuario.ExtensionOutbound;
                string password = Constantes.PASS_CALL + usuario.ExtensionOutbound;
                var authInfo = Factory.Instance.CreateAuthInfo(usuario.ExtensionOutbound, null, password, null, null, Constantes.DIRECCION_ELAXTIC);
                telefonoOut.LinphoneCore.AddAuthInfo(authInfo);

                var proxyConfig = telefonoOut.LinphoneCore.CreateProxyConfig();
                var identity = Factory.Instance.CreateAddress("sip:"+ usuario.ExtensionOutbound + "@domain.tld");
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
            catch(LinphoneException ex)
            {
                string error = ex.Message;
            }

        }

        private string GetDateString()
        {
            DateTime dateTime = DateTime.Now;
            string text = dateTime.ToString("dd/MM/yyyy");// + "-" + dateTime.TimeOfDay.ToString();
            return text;
        }
    }
}
