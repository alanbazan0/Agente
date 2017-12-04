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

namespace NavigationMenuSample.Views
{

    public sealed partial class CorreoPage : Page, ICorreo
    {
        Usuario usuario;
        CorreoPresentador correosPresentador;
        public CorreoPage()
        {
           this.InitializeComponent();
            webCorreo.NavigateToString("<html></html>");
            correosPresentador = new CorreoPresentador(this);

            indicadores.Visibility = Visibility.Collapsed;
            contactos.Visibility = Visibility.Collapsed;
            correosSalida.Visibility = Visibility.Collapsed;
        }

        public List<Correos> correos
        {
            set
            {
                correosListView.ItemsSource = value;
                if (!value[0].Contenido.Equals(""))
                {
                    byte[] datos = Convert.FromBase64String(value[0].Contenido);
                    string htmlCadena = Encoding.UTF8.GetString(datos);
                    webCorreo.NavigateToString(htmlCadena);
                }
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            usuario = (Usuario)e.Parameter;
            consultarCorreoEntrada();
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

    }
}