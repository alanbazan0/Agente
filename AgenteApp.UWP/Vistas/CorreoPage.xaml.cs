using AgenteApp.Vistas;
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
using AgenteApp.Presentadores;
using System.Text;
using System.Reflection;
using Windows.ApplicationModel.Email;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace NavigationMenuSample.Views
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class CorreoPage : Page, ICorreo
    {
        Usuario usuario;
        String correoCliente;
        CorreoPresentador correosPresentador;
        public CorreoPage()
        {
            this.InitializeComponent();
            webCorreo.NavigateToString("<html></html>");
            correosPresentador = new CorreoPresentador(this);

            calendario.Visibility = Visibility.Collapsed;
            contactos.Visibility = Visibility.Collapsed;
            correosSalida.Visibility = Visibility.Collapsed;
        }

        public List<Correos> correos
        {
            set
            {
                correosListView.ItemsSource = value;
                if (value == null)
                {
                    if (!value[0].Contenido.Equals(""))
                    {
                        byte[] datos = Convert.FromBase64String(value[0].Contenido);
                        string htmlCadena = Encoding.UTF8.GetString(datos);
                        webCorreo.NavigateToString(htmlCadena);
                    }
                }
            }
        }

        public string total
        {
            set { indiAcomuladoR.Text = value; }
        }
        public string dia
        {
            set { indiActualR.Text = value; }
        }
        public string semana
        {
            set { indiSemanaActualR.Text = value; }
        }
        public string mes
        {
            set { indiMesActualR.Text = value; }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            usuario = (Usuario)e.Parameter;
            consultarCorreoEntrada();
            ConsultarInfoIndicadores();
        }
        public void consultarCorreoEntrada()
        {
            correosPresentador.consultarCorreoEntrada(usuario.Id);
        }
        void clickCorreo(object sender, RoutedEventArgs e)
        {
            calendario.Visibility = Visibility.Visible;
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
        private void correosListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            //var cliente = clientesListView.SelectedItem;
            var correos = e.ClickedItem;

            if (correos != null)
            {
                var contenido = (string)correos.GetType().GetProperty("Contenido").GetValue(correos, null);
                string correo = (string)correos.GetType().GetProperty("Correo").GetValue(correos, null);
                string[] saludos = correo.Split('<');
                correoCliente = saludos[1].Replace(">", "");
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
        private void ResponderCorreo_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ResponCorreo), usuario);
        }
        private void acomulados(object sender, TappedRoutedEventArgs e)
        {
            consultarCorreoEntrada();
        }
        private void diaActual(object sender, TappedRoutedEventArgs e)
        {
            consultarCorreoEntradaDia();
        }
        private void semanaActual(object sender, TappedRoutedEventArgs e)
        {
            consultarCorreoEntradaSemana();
        }
        private void mesActual(object sender, TappedRoutedEventArgs e)
        {
            consultarCorreoEntradaMes();
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

        public void consultarCorreoEntradaDia()
        {
            correosPresentador.consultarCorreoEntradaDia(usuario.Id);
        }

        public void consultarCorreoEntradaMes()
        {
            correosPresentador.consultarCorreoEntradaMes(usuario.Id);
        }

        public void consultarCorreoEntradaSemana()
        {
            correosPresentador.consultarCorreoEntradaSemana(usuario.Id);
        }
        public void ConsultarInfoIndicadores()
        {
            consultarCorreoEntradaInfo();
            consultarCorreoEntradaDiaInfo();
            consultarCorreoEntradaMesInfo();
            consultarCorreoEntradaSemanaInfo();
        }

        public void consultarCorreoEntradaInfo()
        {
            correosPresentador.consultarCorreoEntradaInfo(usuario.Id);
        }

        public void consultarCorreoEntradaDiaInfo()
        {
            correosPresentador.consultarCorreoEntradaDiaInfo(usuario.Id);
        }

        public void consultarCorreoEntradaMesInfo()
        {
            correosPresentador.consultarCorreoEntradaMesInfo(usuario.Id);
        }

        public void consultarCorreoEntradaSemanaInfo()
        {
            correosPresentador.consultarCorreoEntradaSemanaInfo(usuario.Id);
        }
    }
}
