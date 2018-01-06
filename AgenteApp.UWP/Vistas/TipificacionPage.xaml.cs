using AgenteApp.Presentadores;
using AgenteApp.Vistas;
using AgenteApp.Modelos;
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
using Windows.UI.Popups;
using System.Text;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace AgenteApp.UWP.Vistas
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class TipificacionPage : Page, ITipificacionVista
    {
        TipificacionPresentador presentador;
        List<Tipificacion> configuracion;
        public TipificacionPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
            this.Loaded += CommandBarPage_Loaded;
            presentador = new TipificacionPresentador(this);
            configuracion = new List<Tipificacion>();
            ConsultarConfiguracion();
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

        public void ConsultarConfiguracion()
        {
            presentador.ConsultarConfiguracion();
        }

        public List<Tipificacion> tipificacion
        {
            set
            {
                configuracion = value;
                CrearComponentes(configuracion);
            }
        }

        public void CrearComponentes(List<Tipificacion> conf)
        {
           
            StringBuilder xamlHeaderTemplate = new StringBuilder();
            xamlHeaderTemplate.AppendLine(@"<DataTemplate xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"" >");
            xamlHeaderTemplate.AppendLine(@"<Grid Padding = ""0"" Margin = ""0"" ScrollViewer.VerticalScrollBarVisibility=""Visible""   >");
            xamlHeaderTemplate.AppendLine(@"<Grid.RowDefinitions>");
            foreach (Tipificacion campo in conf)
            {
                xamlHeaderTemplate.AppendLine(@"<RowDefinition Height = ""50"" />");
            }
            xamlHeaderTemplate.AppendLine(@"</Grid.RowDefinitions >");
            xamlHeaderTemplate.AppendLine(@"<Grid.ColumnDefinitions>");
            xamlHeaderTemplate.AppendLine(@"<ColumnDefinition Width=""200""/>");
            xamlHeaderTemplate.AppendLine(@"<ColumnDefinition Width=""100""/>");
            xamlHeaderTemplate.AppendLine(@"<ColumnDefinition Width=""100""/>");
            xamlHeaderTemplate.AppendLine(@"<ColumnDefinition Width=""300""/>");
            xamlHeaderTemplate.AppendLine(@"<ColumnDefinition Width=""100""/>");
            xamlHeaderTemplate.AppendLine(@"</Grid.ColumnDefinitions>");                                 
            for (int i = 0; i < conf.Count; i++)
            {
                Tipificacion campo = conf[i];

                xamlHeaderTemplate.AppendLine(@"<TextBlock Grid.Column=""0"" Grid.Row="""+i+@""" Text=""" + campo.Descripcion + @""" Foreground=""Black"" MaxLines=""2"" TextWrapping=""WrapWholeWords""/>");
                xamlHeaderTemplate.AppendLine(@"<Button Grid.Column=""1""    Grid.Row="""+i+@"""  Background=""Blue"" Height=""100"" Width=""100""   >");
                xamlHeaderTemplate.AppendLine(@"<StackPanel>");
                xamlHeaderTemplate.AppendLine(@"<TextBlock Text=""A"" Foreground=""White"" HorizontalAlignment=""Center""/>");
                xamlHeaderTemplate.AppendLine(@"</StackPanel>");
                xamlHeaderTemplate.AppendLine(@"</Button>");
                xamlHeaderTemplate.AppendLine(@"<TextBox Grid.Column=""2"" Grid.Row="""+i+@"""  Grid.ColumnSpan=""1"" PlaceholderText="""" HorizontalAlignment=""Stretch"" Margin=""8"" />");
                xamlHeaderTemplate.AppendLine(@"<TextBox Grid.Column=""3"" Grid.Row="""+i+@"""  Grid.ColumnSpan=""1"" PlaceholderText="""" HorizontalAlignment=""Stretch"" Margin=""8"" />");
                xamlHeaderTemplate.AppendLine(@"<Button Grid.Column=""4""    Grid.Row="""+i+@""" Background=""Blue"" Height=""100"" Width=""100""   >");
                xamlHeaderTemplate.AppendLine(@"<StackPanel>");
                xamlHeaderTemplate.AppendLine(@"<TextBlock Text=""V"" Foreground=""White"" HorizontalAlignment=""Center""/>");
                xamlHeaderTemplate.AppendLine(@"</StackPanel>");
                xamlHeaderTemplate.AppendLine(@"</Button>");

            }
            xamlHeaderTemplate.AppendLine(@"</Grid>");
            xamlHeaderTemplate.AppendLine(@"</DataTemplate>");
            var headerTemplate = Windows.UI.Xaml.Markup.XamlReader.Load(xamlHeaderTemplate.ToString()) as DataTemplate;
            TipificacionListView.HeaderTemplate = headerTemplate;

            StringBuilder xamlItemTemplate = new StringBuilder();
            xamlItemTemplate.AppendLine(@"<DataTemplate xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"">");
            xamlItemTemplate.AppendLine(@"<Grid Padding = ""0"" Margin = ""0""  ScrollViewer.HorizontalScrollBarVisibility=""Visible""  ScrollViewer.VerticalScrollBarVisibility=""Visible"" >");
            xamlItemTemplate.AppendLine(@"<Grid.ColumnDefinitions>");
            xamlItemTemplate.AppendLine(@"<ColumnDefinition Width=""200""/>");
            xamlItemTemplate.AppendLine(@"</Grid.ColumnDefinitions>");          
            xamlItemTemplate.AppendLine(@"<Border  Grid.Column=""1"" CornerRadius=""0"" BorderBrush=""Black"" BorderThickness=""0 0 0 0"">");
            xamlItemTemplate.AppendLine(@"<TextBlock Text=""{Binding C}"" Foreground=""Black"" MaxLines=""2"" TextWrapping=""WrapWholeWords""/>");
            xamlItemTemplate.AppendLine(@"</Border>");
            xamlItemTemplate.AppendLine(@"</Grid>");
            xamlItemTemplate.AppendLine(@"</DataTemplate>");
            var itemTemplate = Windows.UI.Xaml.Markup.XamlReader.Load(xamlItemTemplate.ToString()) as DataTemplate;
            TipificacionListView.ItemTemplate = itemTemplate;
        }

        public async void MostrarMensajeAsync(string titulo, string mensaje)
        {
            if (mensaje != null)
            {
                var dialog = new MessageDialog(mensaje, titulo);
                await dialog.ShowAsync();
            }
        }
    }
}
