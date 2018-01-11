using AgenteApp.Modelos;
using AgenteApp.Presentadores;
using AgenteApp.Vistas;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking;
using Windows.Networking.Connectivity;
using Windows.System.Profile;
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
    public sealed partial class CRMPage : Page, ICRM
    {
        Usuario usuario;
        string idCliente;
        string nombreCompleto,ip, idhardware;
        CRMPresentador presentador;
        List<CampoGrid> camposGlobal;
        public CRMPage()
        {
            this.InitializeComponent();
            presentador = new CRMPresentador(this);
        }

        public List<Parametros> parametrosUsuario
        {
            set
            {
                for (int i = 0; i < value.Count; i++)
                {
                    if (value[i].PalabraReservada.Equals("@NOMCOMPLETO@"))
                    {
                        nombreCompleto = value[i].ValorParametro;
                        NombreCliente.Text = nombreCompleto;
                    }
                    if (value[i].PalabraReservada.Equals("@IDCLIENTE@"))
                    {
                        idCliente = value[i].ValorParametro;
                    }
                }
                cosultarCRM();
                cosultarCRMIndicadores();
                
            }
        }
        public string Ip { get { return ip; } set { ip = value; } }
        public string Idhardware { get { return idhardware; } set { idhardware = value; } }

        public List<Objeto> datosCRM
        {
            set
            {
                CRMListView.ItemsSource = value;
                Interacciones.Text = value.Count.ToString();
            }
        }

        public void consultarParametros()
        {
            presentador.consultarParametros(usuario.Id);           
        }

        public void MostrarMensaje(string mensaje)
        {
            if (mensaje != null)
            {
                var dialog = new MessageDialog(mensaje, "Error");
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            usuario = (Usuario)e.Parameter;
            obtenerInformacion();
            consultarParametros();
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

        public void cosultarCRM()
        {
            presentador.cosultarCRM(idCliente);
            presentador.CrearColumnasGridCRM();
        }
        public void cosultarCRMIndicadores()
        {
            presentador.cosultarCRMIndicadores(idCliente);
        }

        public void datosIndicadores(List<CRM> indicadoresCRM)
        {
            for (int i = 0; i < indicadoresCRM.Count; i++)
            {
                if (indicadoresCRM[i].CanalId.Equals("1"))
                    Inbound.Text = indicadoresCRM[i].Cantidad;
                else if (indicadoresCRM[i].CanalId.Equals("2"))
                    Outbound.Text = indicadoresCRM[i].Cantidad;
                else if (indicadoresCRM[i].CanalId.Equals("3"))
                    SMS.Text = indicadoresCRM[i].Cantidad;
                else if (indicadoresCRM[i].CanalId.Equals("4"))
                    Mailing.Text = indicadoresCRM[i].Cantidad;
                else if (indicadoresCRM[i].CanalId.Equals("5"))
                    Robot.Text = indicadoresCRM[i].Cantidad;

            }
        }

        public void CrearColumnasGrid1CRM(List<CampoGrid> campos)
        {
            camposGlobal = campos;
            StringBuilder xamlHeaderTemplate = new StringBuilder();
            xamlHeaderTemplate.AppendLine(@"<DataTemplate xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"">");
            xamlHeaderTemplate.AppendLine(@"<Grid Padding = ""0"" Margin = ""0"" ScrollViewer.HorizontalScrollBarVisibility=""Visible""  ScrollViewer.VerticalScrollBarVisibility=""Visible""   >");
            xamlHeaderTemplate.AppendLine(@"<Grid.ColumnDefinitions>");
            foreach (CampoGrid campo in campos)
            {
                xamlHeaderTemplate.AppendLine(@"<ColumnDefinition Width=""200""/>");
            }
            xamlHeaderTemplate.AppendLine(@"</Grid.ColumnDefinitions>");
            for (int i = 0; i < campos.Count; i++)
            {
                CampoGrid campo = campos[i];
                xamlHeaderTemplate.AppendLine(@"<Border  Grid.Column=""" + i.ToString() + @""" CornerRadius=""0"" BorderBrush=""Black"" Background=""#ff9900"" BorderThickness=""0 0 0 0"">");
                xamlHeaderTemplate.AppendLine(@"<TextBlock Text=""" + campo.titulo + @""" Foreground=""White"" MaxLines=""2"" TextWrapping=""WrapWholeWords""/>");
                xamlHeaderTemplate.AppendLine(@"</Border>");
            }
            xamlHeaderTemplate.AppendLine(@"</Grid>");
            xamlHeaderTemplate.AppendLine(@"</DataTemplate>");
            var headerTemplate = Windows.UI.Xaml.Markup.XamlReader.Load(xamlHeaderTemplate.ToString()) as DataTemplate;
            CRMListView.HeaderTemplate = headerTemplate;

            StringBuilder xamlItemTemplate = new StringBuilder();
            xamlItemTemplate.AppendLine(@"<DataTemplate xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"">");
            xamlItemTemplate.AppendLine(@"<Grid Padding = ""0"" Margin = ""0"" ScrollViewer.HorizontalScrollBarVisibility=""Visible""  ScrollViewer.VerticalScrollBarVisibility=""Visible""  >");
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
                xamlItemTemplate.AppendLine(@"<TextBlock Text=""{Binding C" + campo.orden + @"}"" Foreground=""Black"" MaxLines=""2"" TextWrapping=""WrapWholeWords""/>");
                xamlItemTemplate.AppendLine(@"</Border>");
            }
            xamlItemTemplate.AppendLine(@"</Grid>");
            xamlItemTemplate.AppendLine(@"</DataTemplate>");
            var itemTemplate = Windows.UI.Xaml.Markup.XamlReader.Load(xamlItemTemplate.ToString()) as DataTemplate;
            CRMListView.ItemTemplate = itemTemplate;
        }
    }
}
