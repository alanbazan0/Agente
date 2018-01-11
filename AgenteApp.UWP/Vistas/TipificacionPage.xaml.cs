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
using Windows.UI;
using System.Reflection;

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
        List<DatosAsistente> DAsistentes;
        DatosAsistente DAsistente;
        string AsistenteSeleccionado;
        public TipificacionPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
            this.Loaded += CommandBarPage_Loaded;
            presentador = new TipificacionPresentador(this);
            configuracion = new List<Tipificacion>();
            DAsistente = new DatosAsistente();
            DAsistentes = new List<DatosAsistente>();
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

        public List<DatosAsistente> datosAsistente
        {
            set
            {
                DAsistentes = value;
                String DescripicionCampo = AsistenteSeleccionado;
                Grid gr = ContentContainer.Children[0] as Grid;
                var GriPrincipal = gr.Children.Where(a => (a as VariableSizedWrapGrid).Name == "Grid_" + DescripicionCampo)
                                                .Select(a => a)
                                                .First();
                VariableSizedWrapGrid VarSizeWrap = GriPrincipal as VariableSizedWrapGrid;
                var BtCancel = VarSizeWrap.Children.Where(a => (a as Button).Name == "Asis_" + DescripicionCampo)
                                                .Select(a => a)
                                                .First();
                Button bt = BtCancel as Button;
                Flyout f = bt.Flyout as Flyout;

                StackPanel sp = f.Content as StackPanel;

               var listv= sp.Children.Where(a => (a as ListView).Name == "ListView_" + DescripicionCampo)
                                                .Select(a => a)
                                                .First();
                ListView lv = listv as ListView;
                lv.ItemsSource = DAsistentes;
            }
        }

        public void CrearComponentes(List<Tipificacion> conf)
        {
            Grid TipifiacionGrid = new Grid();
            TipifiacionGrid.Padding = new Thickness(0);
            TipifiacionGrid.Margin = new Thickness(0);
            RowDefinition r = new RowDefinition();
            foreach (Tipificacion ren in conf)
            {
                r = new RowDefinition();
                r.Height = GridLength.Auto;
                TipifiacionGrid.RowDefinitions.Add(r);
            }
            r = new RowDefinition();
            r.Height = GridLength.Auto;
            TipifiacionGrid.RowDefinitions.Add(r);
            ColumnDefinition column = new ColumnDefinition();
            column.Width = new GridLength(200);
            TipifiacionGrid.ColumnDefinitions.Add(column);
            column = new ColumnDefinition();
            column.Width = new GridLength(100);
            TipifiacionGrid.ColumnDefinitions.Add(column);
            column = new ColumnDefinition();
            column.Width = new GridLength(100);
            TipifiacionGrid.ColumnDefinitions.Add(column);
            column = new ColumnDefinition();
            column.Width = new GridLength(300);
            TipifiacionGrid.ColumnDefinitions.Add(column);
            column = new ColumnDefinition();
            column.Width = new GridLength(100);
            TipifiacionGrid.ColumnDefinitions.Add(column);
            for (int i = 0; i < conf.Count; i++)
            {
                Tipificacion campo = conf[i];

                switch (campo.Asistente)
                {
                    case "1":
                        CrearCombo(ref TipifiacionGrid, campo, i + 1);
                        break;
                    case "2":
                        CrearAsistente(ref TipifiacionGrid, campo, i + 1);
                        break;
                    case "3":
                        presentador.CrearCheckBox(ref TipifiacionGrid, campo, i + 1);
                        break;
                    case "4":
                    //    presentador.CrearRadioButon(ref TipifiacionGrid, campo, i + 1);
                    //    break;
                    //case "5":
                    //    presentador.CrearFecha(ref TipifiacionGrid, campo, i + 1);
                    //    break;
                    case "6":
                        presentador.CrearLineaDivisoria(ref TipifiacionGrid, campo, i + 1);
                        break;
                    case "7":
                        presentador.CrearTexto(ref TipifiacionGrid, campo, i + 1);
                        break;

                }



            }
            ContentContainer.Children.Add(TipifiacionGrid);

        }

        public async void MostrarMensajeAsync(string titulo, string mensaje)
        {
            if (mensaje != null)
            {
                var dialog = new MessageDialog(mensaje, titulo);
                await dialog.ShowAsync();
            }
        }


        public void FlyAsisListView_ItemClick(object sender, ItemClickEventArgs e)
        {

            ListView LV = sender as ListView;
            var usuarioConferencia = e.ClickedItem;
            if (usuarioConferencia != null)
            {
                int secuencia = LV.Items.Count;

                Grid gr = ContentContainer.Children[0] as Grid;
                var GriPrincipal = gr.Children.Where(a => (a as VariableSizedWrapGrid).Name == "Grid_" + AsistenteSeleccionado)
                                                .Select(a => a)
                                                .First();
                VariableSizedWrapGrid VarSizeWrap = GriPrincipal as VariableSizedWrapGrid;
                var BtCancel = VarSizeWrap.Children.Where(a => (a as Button).Name == "Asis_" + AsistenteSeleccionado)
                                                .Select(a => a)
                                                .First();
                GriPrincipal = gr.Children.Where(a => (a as VariableSizedWrapGrid).Name == "Grid_id_" + AsistenteSeleccionado)
                                                .Select(a => a)
                                                .First();
                VarSizeWrap = GriPrincipal as VariableSizedWrapGrid;
                var textID = VarSizeWrap.Children.Where(a => (a as TextBox).Name == "ID_" + AsistenteSeleccionado)
                                                .Select(a => a)
                                                .First();

                TextBox txtId = textID as TextBox;
                txtId.Text = (string)usuarioConferencia.GetType().GetProperty("JerId1").GetValue(usuarioConferencia, null);

                GriPrincipal = gr.Children.Where(a => (a as VariableSizedWrapGrid).Name == "Grid_dsc_" + AsistenteSeleccionado)
                                                .Select(a => a)
                                                .First();
                VarSizeWrap = GriPrincipal as VariableSizedWrapGrid;
                var textDSC = VarSizeWrap.Children.Where(a => (a as TextBox).Name == "DSC_" + AsistenteSeleccionado)
                                                .Select(a => a)
                                                .First();

                TextBox txtDsc = textDSC as TextBox;
                txtDsc.Text = (string)usuarioConferencia.GetType().GetProperty("CampoDescripcion").GetValue(usuarioConferencia, null);

               
                Button bt = BtCancel as Button;
                Flyout f = bt.Flyout as Flyout;
                if (f != null)
                {
                    f.Hide();
                }
            }
        }

        private void DeleteConfirmation_Click(object sender, RoutedEventArgs e)
        {
            String DescripicionCampo = (sender as Button).Name.Split('_')[1];
            Grid gr = ContentContainer.Children[0] as Grid;
            var GriPrincipal = gr.Children.Where(a => (a as VariableSizedWrapGrid).Name == "Grid_" + DescripicionCampo)
                                            .Select(a => a)
                                            .First();
            VariableSizedWrapGrid VarSizeWrap = GriPrincipal as VariableSizedWrapGrid;
            var BtCancel = VarSizeWrap.Children.Where(a => (a as Button).Name == "Asis_" + DescripicionCampo)
                                            .Select(a => a)
                                            .First();
            Button bt = BtCancel as Button;
            Flyout f = bt.Flyout as Flyout;
            if (f != null)
            {
                f.Hide();
            }
        }

        public void CrearCombo(ref Grid GridHeaderTemplate, Tipificacion campo, int i)
        {
            VariableSizedWrapGrid gridCombo = new VariableSizedWrapGrid();
            gridCombo.SetValue(Grid.RowProperty, i);
            gridCombo.SetValue(Grid.ColumnProperty, 0);
            gridCombo.Background = GetColor(campo.Colorfondo);
            gridCombo.Height = 50;
            gridCombo.VerticalAlignment = VerticalAlignment.Center;
            TextBlock block = new TextBlock();
            block.Text = campo.Descripcion;
            block.Foreground = GetColor(campo.Colorletra);
            gridCombo.Children.Add(block);
            GridHeaderTemplate.Children.Add(gridCombo);

            gridCombo = new VariableSizedWrapGrid();
            gridCombo.SetValue(Grid.RowProperty, i);
            gridCombo.SetValue(Grid.ColumnProperty, 1);
            gridCombo.Background = GetColor(campo.Colorfondo);
            gridCombo.Height = 50;
            gridCombo.VerticalAlignment = VerticalAlignment.Center;
            GridHeaderTemplate.Children.Add(gridCombo);

            gridCombo = new VariableSizedWrapGrid();
            gridCombo.SetValue(Grid.RowProperty, i);
            gridCombo.SetValue(Grid.ColumnProperty, 2);
            gridCombo.Background = GetColor(campo.Colorfondo);
            gridCombo.Height = 50;
            gridCombo.VerticalAlignment = VerticalAlignment.Center;
            TextBox text = new TextBox();
            text.Name = campo.Descripcion + "ID";
            gridCombo.Children.Add(text);
            GridHeaderTemplate.Children.Add(gridCombo);

            gridCombo = new VariableSizedWrapGrid();
            gridCombo.SetValue(Grid.RowProperty, i);
            gridCombo.SetValue(Grid.ColumnProperty, 3);
            gridCombo.Background = GetColor(campo.Colorfondo);
            gridCombo.Height = 50;
            gridCombo.VerticalAlignment = VerticalAlignment.Center;
            text = new TextBox();
            text.Name = campo.Descripcion + "DSC";
            text.Width = 295;
            gridCombo.Children.Add(text);
            GridHeaderTemplate.Children.Add(gridCombo);

            gridCombo = new VariableSizedWrapGrid();
            gridCombo.SetValue(Grid.RowProperty, i);
            gridCombo.SetValue(Grid.ColumnProperty, 4);
            gridCombo.Background = GetColor(campo.Colorfondo);
            gridCombo.Height = 50;
            gridCombo.VerticalAlignment = VerticalAlignment.Center;
            gridCombo.Name = "Grid_" + campo.Descripcion;
            Button BT = new Button();
            BT.Name = "Asis_" + campo.Descripcion;
            BT.Background = new SolidColorBrush(Colors.DodgerBlue);
            BT.Height = 50;
            BT.Width = 50;


            Flyout flyout = new Flyout();
            StackPanel SP = new StackPanel();
            ListView LV = new ListView();
            LV.SetValue(Grid.ColumnProperty, 0);
            LV.Name = "ListView_" + campo.Descripcion;
            LV.IsItemClickEnabled = true;
            LV.MaxHeight = 400;
            StringBuilder xamlHeaderTemplate = new StringBuilder();
            xamlHeaderTemplate.AppendLine(@"<DataTemplate xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"">");
            xamlHeaderTemplate.AppendLine(@"<Grid  Padding = ""0"" Margin = ""15,0,0,0"" >");
            xamlHeaderTemplate.AppendLine(@"<Grid.ColumnDefinitions >");
            xamlHeaderTemplate.AppendLine(@"<ColumnDefinition Width = ""100"" />");
            xamlHeaderTemplate.AppendLine(@"<ColumnDefinition Width = ""250"" />");
            xamlHeaderTemplate.AppendLine(@"</Grid.ColumnDefinitions >");
            xamlHeaderTemplate.AppendLine(@"<Border  Grid.Column = ""0"" CornerRadius = ""0"" BorderBrush = ""Black"" Background = ""#6b6b6b"" BorderThickness = ""0 0 0 0""  Grid.ColumnSpan = ""2"" Margin = ""0,0,-124,0"" >");
            xamlHeaderTemplate.AppendLine(@"<TextBlock Text = ""ID""  FontSize = ""15"" Foreground = ""White"" MaxLines = ""2"" TextWrapping = ""WrapWholeWords"" />");
            xamlHeaderTemplate.AppendLine(@"</Border >");
            xamlHeaderTemplate.AppendLine(@"<Border  Grid.Column = ""2"" CornerRadius = ""0"" BorderBrush = ""Black"" Background = ""#6b6b6b"" BorderThickness = ""0 0 0 0"" Margin = ""0,0,-124,0"" > ");
            xamlHeaderTemplate.AppendLine(@"<TextBlock Text = """ + campo.Descripcion + @"""  FontSize = ""15"" Foreground = ""White"" MaxLines = ""2"" TextWrapping = ""WrapWholeWords"" />");
            xamlHeaderTemplate.AppendLine(@"</Border >");
            xamlHeaderTemplate.AppendLine(@"</Grid > ");
            xamlHeaderTemplate.AppendLine(@"</DataTemplate>");
            var itemTemplate = Windows.UI.Xaml.Markup.XamlReader.Load(xamlHeaderTemplate.ToString()) as DataTemplate;

            LV.HeaderTemplate = itemTemplate;

            StringBuilder xamlItemTemplate = new StringBuilder();
            xamlItemTemplate.AppendLine(@"<DataTemplate xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"">");
            xamlItemTemplate.AppendLine(@"<Grid Padding = ""0"" Margin = ""15,0,0,0"" >");
            xamlItemTemplate.AppendLine(@"<Grid.ColumnDefinitions >");
            xamlItemTemplate.AppendLine(@"<ColumnDefinition Width = ""100"" /> ");
            xamlItemTemplate.AppendLine(@"<ColumnDefinition Width = ""250"" />");
            xamlItemTemplate.AppendLine(@"</Grid.ColumnDefinitions >");
            xamlItemTemplate.AppendLine(@"<Grid.RowDefinitions > ");
            xamlItemTemplate.AppendLine(@"<RowDefinition Height = ""Auto"" />");
            xamlItemTemplate.AppendLine(@"</Grid.RowDefinitions >");
            xamlItemTemplate.AppendLine(@"<TextBlock Grid.Column = ""0"" Text = ""{Binding Id}"" MaxLines = ""2"" TextWrapping = ""WrapWholeWords"" /> ");
            xamlItemTemplate.AppendLine(@"<TextBlock Grid.Column = ""3"" Text = ""{Binding Dsc}"" MaxLines = ""2"" TextWrapping = ""WrapWholeWords"" />");
            xamlItemTemplate.AppendLine(@"</Grid >");
            xamlItemTemplate.AppendLine(@"</DataTemplate>");
            itemTemplate = Windows.UI.Xaml.Markup.XamlReader.Load(xamlItemTemplate.ToString()) as DataTemplate;
            LV.ItemTemplate = itemTemplate;
            SP.Children.Add(LV);
            Button BtCancel = new Button();
            BtCancel.Name = "ButtonCancel_" + campo.Descripcion;
            BtCancel.Content = "Cancelar";
            BtCancel.HorizontalAlignment = HorizontalAlignment.Right;
            SP.Children.Add(BtCancel);
            flyout.Placement = FlyoutPlacementMode.Top;
            flyout.Content = SP;
            FlyoutBase.SetAttachedFlyout(BT, flyout);
            BT.Flyout = flyout;
            BtCancel.Click += new RoutedEventHandler(DeleteConfirmation_Click);
            gridCombo.Children.Add(BT);
            GridHeaderTemplate.Children.Add(gridCombo);
        }


        public void CrearAsistente(ref Grid GridHeaderTemplate, Tipificacion campo, int i)
        {
            //label
            VariableSizedWrapGrid gridCombo = new VariableSizedWrapGrid();
            gridCombo.SetValue(Grid.RowProperty, i);
            gridCombo.SetValue(Grid.ColumnProperty, 0);
            gridCombo.Background = GetColor(campo.Colorfondo);
            gridCombo.Height = 50;
            gridCombo.VerticalAlignment = VerticalAlignment.Center;
            TextBlock block = new TextBlock();
            block.Text = campo.Descripcion;
            block.Foreground = GetColor(campo.Colorletra);
            gridCombo.Children.Add(block);
            GridHeaderTemplate.Children.Add(gridCombo);

            //asistente
            gridCombo = new VariableSizedWrapGrid();
            gridCombo.SetValue(Grid.RowProperty, i);
            gridCombo.SetValue(Grid.ColumnProperty, 1);
            gridCombo.Background = GetColor(campo.Colorfondo);
            gridCombo.Height = 50;
            gridCombo.VerticalAlignment = VerticalAlignment.Center;
            gridCombo.Name = "Grid_" + campo.Descripcion;
            Button BT = new Button();
            BT.Name = "Asis_" + campo.Descripcion;
            BT.Background = new SolidColorBrush(Colors.DodgerBlue);
            BT.Height = 50;
            BT.Width = 50;
            BT.Click += ConsultaAsistente;

            Flyout flyout = new Flyout();
            StackPanel SP = new StackPanel();
            ListView LV = new ListView();
            LV.SetValue(Grid.ColumnProperty, 0);
            LV.Name = "ListView_" + campo.Descripcion;
            LV.IsItemClickEnabled = true;
            LV.ItemClick += FlyAsisListView_ItemClick;
            LV.MaxHeight = 400;
            StringBuilder xamlHeaderTemplate = new StringBuilder();
            xamlHeaderTemplate.AppendLine(@"<DataTemplate xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"">");
            xamlHeaderTemplate.AppendLine(@"<Grid  Padding = ""0"" Margin = ""15,0,0,0"" >");
            xamlHeaderTemplate.AppendLine(@"<Grid.ColumnDefinitions >");
            xamlHeaderTemplate.AppendLine(@"<ColumnDefinition Width = ""100"" />");
            xamlHeaderTemplate.AppendLine(@"<ColumnDefinition Width = ""250"" />");
            xamlHeaderTemplate.AppendLine(@"</Grid.ColumnDefinitions >");
            xamlHeaderTemplate.AppendLine(@"<Border  Grid.Column = ""0"" CornerRadius = ""0"" BorderBrush = ""Black"" Background = ""#6b6b6b"" BorderThickness = ""0 0 0 0""  Grid.ColumnSpan = ""2"" Margin = ""0,0,-124,0"" >");
            xamlHeaderTemplate.AppendLine(@"<TextBlock Text = ""ID""  FontSize = ""15"" Foreground = ""White"" MaxLines = ""2"" TextWrapping = ""WrapWholeWords"" />");
            xamlHeaderTemplate.AppendLine(@"</Border >");
            xamlHeaderTemplate.AppendLine(@"<Border  Grid.Column = ""2"" CornerRadius = ""0"" BorderBrush = ""Black"" Background = ""#6b6b6b"" BorderThickness = ""0 0 0 0"" Margin = ""0,0,-124,0"" > ");
            xamlHeaderTemplate.AppendLine(@"<TextBlock Text = """ + campo.Descripcion + @"""  FontSize = ""15"" Foreground = ""White"" MaxLines = ""2"" TextWrapping = ""WrapWholeWords"" />");
            xamlHeaderTemplate.AppendLine(@"</Border >");
            xamlHeaderTemplate.AppendLine(@"</Grid > ");
            xamlHeaderTemplate.AppendLine(@"</DataTemplate>");
            var itemTemplate = Windows.UI.Xaml.Markup.XamlReader.Load(xamlHeaderTemplate.ToString()) as DataTemplate;

            LV.HeaderTemplate = itemTemplate;

            StringBuilder xamlItemTemplate = new StringBuilder();
            xamlItemTemplate.AppendLine(@"<DataTemplate xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"">");
            xamlItemTemplate.AppendLine(@"<Grid Padding = ""0"" Margin = ""15,0,0,0"" >");
            xamlItemTemplate.AppendLine(@"<Grid.ColumnDefinitions >");
            xamlItemTemplate.AppendLine(@"<ColumnDefinition Width = ""100"" /> ");
            xamlItemTemplate.AppendLine(@"<ColumnDefinition Width = ""250"" />");
            xamlItemTemplate.AppendLine(@"</Grid.ColumnDefinitions >");
            xamlItemTemplate.AppendLine(@"<Grid.RowDefinitions > ");
            xamlItemTemplate.AppendLine(@"<RowDefinition Height = ""Auto"" />");
            xamlItemTemplate.AppendLine(@"</Grid.RowDefinitions >");
            xamlItemTemplate.AppendLine(@"<TextBlock Grid.Column = ""0"" Text = ""{Binding JerId1}"" MaxLines = ""2"" TextWrapping = ""WrapWholeWords"" /> ");
            xamlItemTemplate.AppendLine(@"<TextBlock Grid.Column = ""3"" Text = ""{Binding CampoDescripcion}"" MaxLines = ""2"" TextWrapping = ""WrapWholeWords"" />");
            xamlItemTemplate.AppendLine(@"</Grid >");
            xamlItemTemplate.AppendLine(@"</DataTemplate>");
            itemTemplate = Windows.UI.Xaml.Markup.XamlReader.Load(xamlItemTemplate.ToString()) as DataTemplate;
            LV.ItemTemplate = itemTemplate;
            SP.Children.Add(LV);
            Button BtCancel = new Button();
            BtCancel.Name = "ButtonCancel_" + campo.Descripcion;
            BtCancel.Content = "Cancelar";
            BtCancel.HorizontalAlignment = HorizontalAlignment.Right;
            SP.Children.Add(BtCancel);
            flyout.Placement = FlyoutPlacementMode.Top;
            flyout.Content = SP;
            FlyoutBase.SetAttachedFlyout(BT, flyout);
            BT.Flyout = flyout;
            BtCancel.Click += new RoutedEventHandler(DeleteConfirmation_Click);
            gridCombo.Children.Add(BT);
            GridHeaderTemplate.Children.Add(gridCombo);
            //campo id
            gridCombo = new VariableSizedWrapGrid();
            gridCombo.SetValue(Grid.RowProperty, i);
            gridCombo.SetValue(Grid.ColumnProperty, 2);
            gridCombo.Background = GetColor(campo.Colorfondo);
            gridCombo.Height = 50;
            gridCombo.VerticalAlignment = VerticalAlignment.Center;
            gridCombo.Name = "Grid_id_" + campo.Descripcion;
            TextBox text = new TextBox();
            text.Name = "ID_"+ campo.Descripcion;
            gridCombo.Children.Add(text);
            GridHeaderTemplate.Children.Add(gridCombo);
            // campo descripcion
            gridCombo = new VariableSizedWrapGrid();
            gridCombo.SetValue(Grid.RowProperty, i);
            gridCombo.SetValue(Grid.ColumnProperty, 3);
            gridCombo.SetValue(Grid.ColumnSpanProperty, 2);
            gridCombo.Background = GetColor(campo.Colorfondo);
            gridCombo.Height = 50;
            gridCombo.VerticalAlignment = VerticalAlignment.Center;
            gridCombo.Name = "Grid_dsc_" + campo.Descripcion;
            text = new TextBox();
            text.Name = "DSC_"+ campo.Descripcion;
            text.Width = 290;
            gridCombo.Children.Add(text);
            GridHeaderTemplate.Children.Add(gridCombo);

            // combo

        }

        private SolidColorBrush GetColor(string color)
        {
            SolidColorBrush colorBrush = new SolidColorBrush(Colors.White);
            switch (color)
            {
                case "Black":
                    colorBrush = new SolidColorBrush(Colors.Black);
                    break;
                case "White":
                    colorBrush = new SolidColorBrush(Colors.White);
                    break;
                case "Blue":
                    colorBrush = new SolidColorBrush(Colors.Blue);
                    break;

            }

            return colorBrush;
        }


        private void ConsultaAsistente(object sender, RoutedEventArgs e)
        {
            String DescripicionCampo = (sender as Button).Name.Split('_')[1];
            AsistenteSeleccionado = DescripicionCampo;
            foreach (Tipificacion tip in configuracion)
            {
                if (tip.Descripcion == DescripicionCampo)
                {
                    DAsistente.VErsion = tip.Version;
                    DAsistente.Secuencia = tip.Secuencia;
                }
            }
            presentador.ConsultarDatosAsistente(DAsistente);


        }
    }
}
