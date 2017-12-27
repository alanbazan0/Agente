using System;
using System.IO;
using System.Linq;
using Windows.UI.Xaml;
using AgenteApp.Vistas;
using AgenteApp.Clases;
using AgenteApp.Modelos;
using Windows.Foundation;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using AgenteApp.Presentadores;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System.Collections.Generic;
using Windows.Foundation.Collections;
using Windows.UI.Xaml.Controls.Primitives;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Popups;
using Windows.Networking;
using Windows.Networking.Connectivity;
using Windows.System.Profile;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace NavigationMenuSample.Views
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class TransferenciaPage : Page, IParametrosVista
    {
        Usuario usuario;
        Parametros ParametroLocal;
        string noTelefonico;
        TransferenciaPresentador presentador;
        string ip;
        string idhardware;
        public TransferenciaPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
            this.Loaded += CommandBarPage_Loaded;
            presentador = new TransferenciaPresentador(this);
            ParametroLocal = new Parametros();
            usuario = null;
            FechaextBox.Text = GetDateString();
            obtenerInformacion();
        }

        public Parametros Parametro { get => ParametroLocal; set { } }

        public List<Parametros> Parametros
        {  set {
                for (int i = 0; i < value.Count; i++)
                {

                    switch(value[i].PalabraReservada)                      
                    {
                        case "@NUMEROTEL@":
                            noTelefonico =  txtNumeroTelefono.Text = value[i].ValorParametro;
                            break;
                        case "@IDLLAMADA@":
                            IdllamtextBox.Text = value[i].ValorParametro;
                            break;
                    }
                }
                ConsultarPortabilidad(noTelefonico);
            }
        }
        public string GetValorParametro(string palabraReservada)
        {
            string valor = "";
           
            return valor;
        }

        private void CommandBarPage_Loaded(object sender, RoutedEventArgs e)
        {
            double? diagonal = Windows.Graphics.Display.DisplayInformation.GetForCurrentView().DiagonalSizeInInches;

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

        public void ConsultarParametros()
        {
            NoExttextBox.Text = usuario.Extension;
            ParametroLocal.IdParamtro = usuario.Id;
            ParametroLocal.NumeroMaquina = idhardware;
            ParametroLocal.DireccionIp = ip;
            presentador.ConsultarParametros();
            
        }

        private void digitarNumero(object sender, RoutedEventArgs e)
        {
            Button padnumeric = (Button)sender;
            switch(padnumeric.Name)
            {
                case "bt7":
                    txtNumeroATranferir.Text = txtNumeroATranferir.Text+ "7";
                    break;
                case "bt8":
                    txtNumeroATranferir.Text = txtNumeroATranferir.Text + "8";
                    break;
                case "bt9":
                    txtNumeroATranferir.Text = txtNumeroATranferir.Text + "9";
                    break;
                case "bt4":
                    txtNumeroATranferir.Text = txtNumeroATranferir.Text + "4";
                    break;
                case "bt5":
                    txtNumeroATranferir.Text = txtNumeroATranferir.Text + "5";
                    break;
                case "bt6":
                    txtNumeroATranferir.Text = txtNumeroATranferir.Text + "6";
                    break;
                case "bt3":
                    txtNumeroATranferir.Text = txtNumeroATranferir.Text + "3";
                    break;
                case "bt2":
                    txtNumeroATranferir.Text = txtNumeroATranferir.Text + "2";
                    break;
                case "bt1":
                    txtNumeroATranferir.Text = txtNumeroATranferir.Text + "1";
                    break;
                case "bta":
                    txtNumeroATranferir.Text = txtNumeroATranferir.Text + "*";
                    break;
                case "bt0":
                    txtNumeroATranferir.Text = txtNumeroATranferir.Text + "0";
                    break;
                case "btg":
                    txtNumeroATranferir.Text = txtNumeroATranferir.Text + "#";
                    break;
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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            usuario = (Usuario)e.Parameter;
            ConsultarParametros();
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

        private string GetDateString()
        {
            DateTime dateTime = DateTime.Now;
            string text = dateTime.ToString("dd/MM/yyyy");// + "-" + dateTime.TimeOfDay.ToString();
            return text;
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
                    CiudadTextBox.Text = value[0].MunicipioPortabilidad;
                    EstatustextBox.Text = "Contestada";
                }
                catch { }
            }
        }

        private void Transferir(object sender, RoutedEventArgs e)
        {

            //if (telefono.LinphoneCore.CallsNb == 0)
            //{

            //}
            //else
            //{
            //    Call call = telefono.LinphoneCore.CurrentCall;
            //    if (call.State == CallState.StreamsRunning)
            //    {
            //        //telefono.LinphoneCore.TransferCall(call, numTranfer.Text);
            //        //HeaderTextBlock.Text = "Recepción de llamada - Diponible";
            //    }
            //}


        }

    }
}
