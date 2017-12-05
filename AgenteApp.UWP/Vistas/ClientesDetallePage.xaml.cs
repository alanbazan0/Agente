﻿using AgenteApp.Clases;
using AgenteApp.Componentes;
using AgenteApp.Modelos;
using AgenteApp.Presentadores;
using AgenteApp.UWP.Fabricas;
using AgenteApp.Vistas;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Xamarin.Forms.Internals;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace NavigationMenuSample.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ClientesDetallePage : Page, IClientesVista
    {
        Usuario usuario;
        ClientesDetallePresentador presentador;
        FormularioFabrica formularioClienteFabrica;
        CriterioSeleccionFabrica criteriosSeleccionFabrica;
        List<CampoGrid> camposGlobal;
        Portabilidad portabilidadParametros;
        List<CampoGrid> camposGloba;
        private ModoVentana modo;
        string numTelefonico;
        public ClientesDetallePage()
        {

            this.InitializeComponent();
            presentador = new ClientesDetallePresentador(this);
            criteriosSeleccionFabrica = new CriterioSeleccionFabrica();
            this.Loaded += CommandBarPage_Loaded;
            formularioClienteFabrica = new FormularioFabrica();
            presentador.CrearCatalogoClientes();
        }

        public List<Filtro> Filtros
        {
            get
            {
                List<Filtro> filtros = new List<Filtro>();
                foreach (ICriterioSeleccionComponente componente in criteriosSeleccionDetalleCliente.Children)
                {
                    if (componente.Filtro.valor != string.Empty)
                        filtros.Add(componente.Filtro);
                }
                return filtros;
            }
        }
        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            ConsultarClientes();
        }
        private void AppBarAltaClienteButton_Click(object sender, RoutedEventArgs e)
        {
            var parametros = new
            {
                modo = this.modo,
                telCliente = this.numTelefonico,
                portabilidad = portabilidadParametros
            };
            this.Frame.Navigate(typeof(AgenteApp.UWP.Vistas.ClientePage), parametros);
           
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

                //EstadoTextBox.Text = value[0].EstadoPortabilidad;
                //ciudadTextBox.Text = value[0].MunicipioPortabilidad;
                //tipoTelefonoTextBox.Text = value[0].RedPortabilidad;
                //tipoLlamadaTextBox.Text = value[0].TipoLlamadaPortabilidad;

                portabilidadParametros = value[0];
            }
        }
        public string setIdLlamada { set => throw new NotImplementedException(); }

        public void CrearCriterioSeleccion(CriterioSeleccion criterioSeleccion)
        {
            ICriterioSeleccionComponente componente = criteriosSeleccionFabrica.CrearComponente(criterioSeleccion);
            criteriosSeleccionDetalleCliente.Children.Add(componente as UIElement);
        }
        private void clientesListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            //var cliente = clientesListView.SelectedItem;
            var cliente = e.ClickedItem;

            if (cliente != null)
            {
                var numero = camposGlobal.Where(a => a.campoId == "BTCLIENTENUMERO")
                                        .Select(a => a.id)
                                        .First();
                string alias = "C" + numero.ToString();
                int idCliente = Int32.Parse((string)cliente.GetType().GetProperty(alias).GetValue(cliente, null));

                var parametros = new { modo = ModoVentana.CAMBIOS, idCliente = idCliente, portabilidad = portabilidadParametros };
                this.Frame.Navigate(typeof(AgenteApp.UWP.Vistas.ClientePage), parametros);
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
                //bottombar.Visibility = Visibility.Visible;
            }
            else
            {
                topbar.Visibility = Visibility.Visible;
                pageTitleContainer.Visibility = Visibility.Collapsed;
                //bottombar.Visibility = Visibility.Collapsed;
            }


        }
        
        public void CrearColumnasGrid1(List<CampoGrid> campos)
        {
            camposGlobal = campos;
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
                xamlHeaderTemplate.AppendLine(@"<Border  Grid.Column=""" + i.ToString() + @""" CornerRadius=""0"" BorderBrush=""Black"" Background=""#ff9900"" BorderThickness=""0 0 0 0"">");
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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var parametros = e.Parameter;

            if (parametros != null)
            {

                modo = (ModoVentana)parametros.GetType().GetProperty("modo").GetValue(parametros, null);
                numTelefonico = (string)parametros.GetType().GetProperty("telCliente").GetValue(parametros, null);
                ConsultarPortabilidad(numTelefonico);
                //presentador.CrearFormulario(modo);
                //parametroPortabilidad = (Portabilidad)parametros.GetType().GetProperty("portabilidad").GetValue(parametros, null);
                if (modo == ModoVentana.ALTA)
                {
                    
                    ConsultarClientesTel(numTelefonico);
                    //numTelefonico = (string)parametros.GetType().GetProperty("telCliente").GetValue(parametros, null);
                    //noTelefonicoClienteTextBox.Text = numTelefonico;
                    //razonSocialTextBox.Text = parametroPortabilidad.DescripcionPortabilidad;
                }
                else
                {
                    ConsultarClientesTel(numTelefonico);
                    //int idCliente = (int)parametros.GetType().GetProperty("idCliente").GetValue(parametros, null);
                    //presentador.TraerDatosCliente(idCliente);
                    //presentador.traerDatosTelefono(idCliente);

                }

            }
        }

        public void ConsultarClientes()
        {
            progressRing.IsActive = true;
            presentador.ConsultarClientes();
        }

        public void ConsultarClientesTel(string numero)
        {
            progressRing.IsActive = true;
            presentador.ConsultarClientesTel(numero);
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

        public void ConsultarPortabilidad(string numero)
        {
            presentador.ConsultarPortabilidad(numero);
        }
    }
}