using AgenteApp.Modelos;
using AgenteApp.Presentadores;
using AgenteApp.Vistas;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace AgenteApp.UWP.Vistas
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class DetalleCRMPage : Page, ITipificacionVista
    {
        TipificacionPresentador presentador;
        CRMPresentador presentadorcrm;
        List<Tipificacion> configuracion;
        List<Tipificacion> tipificaciones;
        List<DatosAsistente> DAsistentes;
        DatosAsistente DAsistente;
        string AsistenteSeleccionado;
        string folio = "",idCLinete="";
        public DetalleCRMPage()
        {
            this.InitializeComponent();
            //this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
            this.Loaded += CommandBarPage_Loaded;
            presentador = new TipificacionPresentador(this);
            presentadorcrm = new CRMPresentador(this);
            configuracion = new List<Tipificacion>();
            DAsistente = new DatosAsistente();
            DAsistentes = new List<DatosAsistente>();
            ConsultarConfiguracion();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            var parametros = e.Parameter;

            if (parametros != null)
            {
                folio = (string)parametros.GetType().GetProperty("folio").GetValue(parametros, null);
                idCLinete = (string)parametros.GetType().GetProperty("idCliente").GetValue(parametros, null);              
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
                presentadorcrm.ConsultarDatosTipificacion(folio, idCLinete);
            }
        }

        public List<Parametros> Parametros
        {
            set
            {
                
            }
        }

        public Parametros Parametro
        {
            set
            {

            }
        }

        public string IDCRM
        {
            set
            {
               
            }
          
        }

        public List<DatosAsistente> datosAsistente
        {
            set
            {
                DAsistentes = value;
                String DescripicionCampo = AsistenteSeleccionado;
                Grid gr = ContentContainer.Children[0] as Grid;
                var GriPrincipal = gr.Children.Where(a => (a as VariableSizedWrapGrid).Name == "Grid-" + DescripicionCampo)
                                                .Select(a => a)
                                                .First();
                VariableSizedWrapGrid VarSizeWrap = GriPrincipal as VariableSizedWrapGrid;
                var BtCancel = VarSizeWrap.Children.Where(a => (a as Button).Name == "Asis-" + DescripicionCampo)
                                                .Select(a => a)
                                                .First();
                Button bt = BtCancel as Button;
                Flyout f = bt.Flyout as Flyout;

                StackPanel sp = f.Content as StackPanel;

                var listv = sp.Children.Where(a => (a as ListView).Name == "ListView-" + DescripicionCampo)
                                                 .Select(a => a)
                                                 .First();
                ListView lv = listv as ListView;
                lv.ItemsSource = DAsistentes;
            }
        }

        public List<Tipificacion> valoresDatosAsistente
        {
            set
            {
                for (int i = 0; i < value.Count; i++)
                {
                    switch (value[i].Asistente)
                    {
                        case "1":
                            break;
                        case "2":
                            ValorbuscarIdPadre(value[i].Version, value[i].Secuencia, value[i].Campoid, value[i].Valorcampoid, value[i].Valorcampodsc);

                            break;
                        case "3":
                            break;
                        case "4":
                        //    presentador.CrearRadioButon(ref TipifiacionGrid, campo, i + 1);
                        //    break;
                        //case "5":
                        //    presentador.CrearFecha(ref TipifiacionGrid, campo, i + 1);
                        //    break;
                        case "6":
                            break;
                        case "7":
                            ValorbuscarIdPadreTexto(value[i].Version, value[i].Secuencia, value[i].Campoid, value[i].Valores);

                            break;
                    }
                }
            }
        }

        Parametros ITipificacionVista.Parametro { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        string ITipificacionVista.IDCRM { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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
                var GriPrincipal = gr.Children.Where(a => (a as VariableSizedWrapGrid).Name == "Grid-" + AsistenteSeleccionado)
                                                .Select(a => a)
                                                .First();
                VariableSizedWrapGrid VarSizeWrap = GriPrincipal as VariableSizedWrapGrid;
                var BtCancel = VarSizeWrap.Children.Where(a => (a as Button).Name == "Asis-" + AsistenteSeleccionado)
                                                .Select(a => a)
                                                .First();
                GriPrincipal = gr.Children.Where(a => (a as VariableSizedWrapGrid).Name == "Grid_id-" + AsistenteSeleccionado)
                                                .Select(a => a)
                                                .First();
                VarSizeWrap = GriPrincipal as VariableSizedWrapGrid;
                var textID = VarSizeWrap.Children.Where(a => (a as TextBox).Name == "ID-" + AsistenteSeleccionado)
                                                .Select(a => a)
                                                .First();

                TextBox txtId = textID as TextBox;
                txtId.Text = (string)usuarioConferencia.GetType().GetProperty("JerId1").GetValue(usuarioConferencia, null);

                GriPrincipal = gr.Children.Where(a => (a as VariableSizedWrapGrid).Name == "Grid_dsc-" + AsistenteSeleccionado)
                                                .Select(a => a)
                                                .First();
                VarSizeWrap = GriPrincipal as VariableSizedWrapGrid;
                var textDSC = VarSizeWrap.Children.Where(a => (a as TextBox).Name == "DSC-" + AsistenteSeleccionado)
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
            String DescripicionCampo = (sender as Button).Name.Split('-')[1];
            Grid gr = ContentContainer.Children[0] as Grid;
            var GriPrincipal = gr.Children.Where(a => (a as VariableSizedWrapGrid).Name == "Grid-" + DescripicionCampo)
                                            .Select(a => a)
                                            .First();
            VariableSizedWrapGrid VarSizeWrap = GriPrincipal as VariableSizedWrapGrid;
            var BtCancel = VarSizeWrap.Children.Where(a => (a as Button).Name == "Asis-" + DescripicionCampo)
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
            //aistente vacio porque es combo
            gridCombo = new VariableSizedWrapGrid();
            gridCombo.SetValue(Grid.RowProperty, i);
            gridCombo.SetValue(Grid.ColumnProperty, 1);
            gridCombo.Background = GetColor(campo.Colorfondo);
            gridCombo.Height = 50;
            gridCombo.VerticalAlignment = VerticalAlignment.Center;
            GridHeaderTemplate.Children.Add(gridCombo);
            // campo id
            gridCombo = new VariableSizedWrapGrid();
            gridCombo.SetValue(Grid.RowProperty, i);
            gridCombo.SetValue(Grid.ColumnProperty, 2);
            gridCombo.Background = GetColor(campo.Colorfondo);
            gridCombo.Height = 50;
            gridCombo.VerticalAlignment = VerticalAlignment.Center;
            TextBox text = new TextBox();
            text.Name = "ID-" + campo.Version + "." + campo.Secuencia + "." + campo.Campoid;
            gridCombo.Children.Add(text);
            GridHeaderTemplate.Children.Add(gridCombo);
            //campo descripcion
            gridCombo = new VariableSizedWrapGrid();
            gridCombo.SetValue(Grid.RowProperty, i);
            gridCombo.SetValue(Grid.ColumnProperty, 3);
            gridCombo.Background = GetColor(campo.Colorfondo);
            gridCombo.Height = 50;
            gridCombo.VerticalAlignment = VerticalAlignment.Center;
            text = new TextBox();
            text.Name = "DSC-" + campo.Version + "." + campo.Secuencia + "." + campo.Campoid;
            text.Width = 295;
            gridCombo.Children.Add(text);
            GridHeaderTemplate.Children.Add(gridCombo);
            //combo
            gridCombo = new VariableSizedWrapGrid();
            gridCombo.SetValue(Grid.RowProperty, i);
            gridCombo.SetValue(Grid.ColumnProperty, 4);
            gridCombo.Background = GetColor(campo.Colorfondo);
            gridCombo.Height = 50;
            gridCombo.VerticalAlignment = VerticalAlignment.Center;
            gridCombo.Name = "Grid-" + campo.Version + "." + campo.Secuencia + "." + campo.Campoid;
            Button BT = new Button();
            BT.Name = "Asis-" + campo.Version + "." + campo.Secuencia + "." + campo.Campoid;
            BT.Background = new SolidColorBrush(Colors.DodgerBlue);
            BT.Height = 50;
            BT.Width = 50;
            Flyout flyout = new Flyout();
            StackPanel SP = new StackPanel();
            ListView LV = new ListView();
            LV.SetValue(Grid.ColumnProperty, 0);
            LV.Name = "ListView-" + campo.Version + "." + campo.Secuencia + "." + campo.Campoid;
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
            BtCancel.Name = "ButtonCancel-" + campo.Version + "." + campo.Secuencia + "." + campo.Campoid;
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
            gridCombo.Name = "Grid-" + campo.Version + "." + campo.Secuencia + "." + campo.Campoid;
            AppBarButton BT = new AppBarButton();
            BT.Name = "Asis-" + campo.Version + "." + campo.Secuencia + "." + campo.Campoid;
            BT.Height = 50;
            BT.Width = 50;
            BT.Click += ConsultaAsistente;
            //BitmapImage bi = new BitmapImage();
            //bi.DecodePixelWidth = 100;
            //bi.DecodePixelHeight = 100;

            //bi.UriSource = new Uri("ms-appx:///Pages:,,,/Recursos/Imagenes/asistente.png");
            //ImageBrush ibh = new ImageBrush();
            //IconElement ie ;
            //ibh.ImageSource = bi;
            BT.Icon = new SymbolIcon(Symbol.Find);

            BT.Background = new SolidColorBrush(Colors.LightGray);
            Flyout flyout = new Flyout();
            StackPanel SP = new StackPanel();
            ListView LV = new ListView();
            LV.SetValue(Grid.ColumnProperty, 0);
            LV.Name = "ListView-" + campo.Version + "." + campo.Secuencia + "." + campo.Campoid;
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
            BtCancel.Name = "ButtonCancel-" + campo.Version + "." + campo.Secuencia + "." + campo.Campoid;
            BtCancel.Content = "Cancelar";
            BtCancel.HorizontalAlignment = HorizontalAlignment.Right;
            SP.Children.Add(BtCancel);
            flyout.Placement = FlyoutPlacementMode.Top;
            flyout.Content = SP;
            FlyoutBase.SetAttachedFlyout(BT, flyout);
            BT.Flyout = flyout;
            BtCancel.Click += new RoutedEventHandler(DeleteConfirmation_Click);
            //gridCombo.Children.Add(BT);
            GridHeaderTemplate.Children.Add(gridCombo);
            //campo id
            gridCombo = new VariableSizedWrapGrid();
            gridCombo.SetValue(Grid.RowProperty, i);
            gridCombo.SetValue(Grid.ColumnProperty, 2);
            gridCombo.Background = GetColor(campo.Colorfondo);
            gridCombo.Height = 50;
            gridCombo.VerticalAlignment = VerticalAlignment.Center;
            gridCombo.Name = "Grid_id-" + campo.Version + "." + campo.Secuencia + "." + campo.Campoid;
            TextBox text = new TextBox();
            text.Name = "ID-" + campo.Version + "." + campo.Secuencia + "." + campo.Campoid;
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
            gridCombo.Name = "Grid_dsc-" + campo.Version + "." + campo.Secuencia + "." + campo.Campoid;
            text = new TextBox();
            text.Name = "DSC-" + campo.Version + "." + campo.Secuencia + "." + campo.Campoid;
            text.Width = 290;
            gridCombo.Children.Add(text);
            GridHeaderTemplate.Children.Add(gridCombo);

        }
        private void GuardarTipi_Click(object sender, RoutedEventArgs e)
        {
            GuardarTipificacion();
        }

        public void GuardarTipificacion()
        {
            string valor = "";
            Tipificacion tipi;
            tipificaciones = new List<Tipificacion>();
            for (int i = 0; i < configuracion.Count; i++)
            {
                switch (configuracion[i].Asistente)
                {
                    case "1":
                        break;
                    case "2":
                        valor = buscarIdPadre(configuracion[i].Version, configuracion[i].Secuencia, configuracion[i].Campoid);
                        tipi = new Tipificacion();
                        tipi.Secuencia = configuracion[i].Secuencia;
                        tipi.Campoid = configuracion[i].Campoid;
                        tipi.Descripcion = configuracion[i].Descripcion;
                        tipi.Valores = valor;
                        tipificaciones.Add(tipi);
                        break;
                    case "3":
                        break;
                    case "4":
                    //    presentador.CrearRadioButon(ref TipifiacionGrid, campo, i + 1);
                    //    break;
                    //case "5":
                    //    presentador.CrearFecha(ref TipifiacionGrid, campo, i + 1);
                    //    break;
                    case "6":
                        break;
                    case "7":
                        valor = buscarIdPadreTexto(configuracion[i].Version, configuracion[i].Secuencia, configuracion[i].Campoid);
                        tipi = new Tipificacion();
                        tipi.Secuencia = configuracion[i].Secuencia;
                        tipi.Campoid = configuracion[i].Campoid;
                        tipi.Descripcion = configuracion[i].Descripcion;
                        tipi.Valores = valor;
                        tipificaciones.Add(tipi);
                        break;
                }

            }
            //presentador.GuardarTipificacion(tipificaciones);
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
            String DescripicionCampo = (sender as Button).Name.Split('-')[1];
            AsistenteSeleccionado = DescripicionCampo;
            string[] campoDatos = DescripicionCampo.Split('.');
            string campoHijo = campoDatos[campoDatos.Length - 1];
            string campoPadre = "";
            string Criterio = "";
            Tipificacion tip = new Tipificacion();
            for (int i = 0; i < configuracion.Count; i++)
            {
                tip = configuracion[i];
                if (tip.Campoid == campoHijo)
                {
                    if (tip.CampoPadre != "" && tip.CampoPadre != null)
                    {
                        foreach (Tipificacion jera in configuracion)
                        {
                            if (jera.Campoid.Equals(tip.CampoPadre))
                            {
                                Criterio = buscarIdPadre(jera.Version, jera.Secuencia, jera.Campoid);
                                break;
                            }
                        }
                    }

                    DAsistente.VErsion = tip.Version;
                    DAsistente.Secuencia = tip.Secuencia;
                    DAsistente.CampoCriterio = Criterio;
                }
            }
            presentador.ConsultarDatosAsistente(DAsistente);



        }
        private string buscarIdPadre(string version, string secuencia, String campoPadre)
        {
            Grid gr = ContentContainer.Children[0] as Grid;
            var GriPrincipal = gr.Children.Where(a => (a as VariableSizedWrapGrid).Name == "Grid_id-" + version + "." + secuencia + "." + campoPadre)
                                                .Select(a => a)
                                                .First();

            VariableSizedWrapGrid VarSizeWrap = GriPrincipal as VariableSizedWrapGrid;
            var textID = VarSizeWrap.Children.Where(a => (a as TextBox).Name == "ID-" + version + "." + secuencia + "." + campoPadre)
                                                .Select(a => a)
                                                .First();

            TextBox txtId = textID as TextBox;
            return txtId.Text;
        }

        private string buscarIdPadreTexto(string version, string secuencia, String campoPadre)
        {
            Grid gr = ContentContainer.Children[0] as Grid;
            var GriPrincipal = gr.Children.Where(a => (a as VariableSizedWrapGrid).Name == "Grid_texto-" + version + "." + secuencia + "." + campoPadre)
                                                .Select(a => a)
                                                .First();

            VariableSizedWrapGrid VarSizeWrap = GriPrincipal as VariableSizedWrapGrid;
            var textID = VarSizeWrap.Children.Where(a => (a as TextBox).Name == "Texto-" + version + "." + secuencia + "." + campoPadre)
                                                .Select(a => a)
                                                .First();

            TextBox txtId = textID as TextBox;
            return txtId.Text;
        }

        private void ValorbuscarIdPadre(string version, string secuencia, String campoPadre,string valorId,string valorDsc)
        {
            Grid gr = ContentContainer.Children[0] as Grid;
            var GriPrincipal = gr.Children.Where(a => (a as VariableSizedWrapGrid).Name == "Grid_id-" + version + "." + secuencia + "." + campoPadre)
                                                .Select(a => a)
                                                .First();

            VariableSizedWrapGrid VarSizeWrap = GriPrincipal as VariableSizedWrapGrid;
            var textID = VarSizeWrap.Children.Where(a => (a as TextBox).Name == "ID-" + version + "." + secuencia + "." + campoPadre)
                                                .Select(a => a)
                                                .First();

            var GridDsc = gr.Children.Where(a => (a as VariableSizedWrapGrid).Name == "Grid_dsc-" + version + "." + secuencia + "." + campoPadre)
                                                .Select(a => a)
                                                .First();
            VariableSizedWrapGrid VarSizeWrapDsc = GridDsc as VariableSizedWrapGrid;
            var textDCS = VarSizeWrapDsc.Children.Where(a => (a as TextBox).Name == "DSC-" + version + "." + secuencia + "." + campoPadre)
                                               .Select(a => a)
                                               .First();
            

            TextBox txtId = textID as TextBox;
            TextBox txtdsc = textDCS as TextBox;
            txtId.Text = valorId;
            txtdsc.Text = valorDsc;
        }

        private void ValorbuscarIdPadreTexto(string version, string secuencia, String campoPadre, string valorId)
        {
            Grid gr = ContentContainer.Children[0] as Grid;
            var GriPrincipal = gr.Children.Where(a => (a as VariableSizedWrapGrid).Name == "Grid_texto-" + version + "." + secuencia + "." + campoPadre)
                                                .Select(a => a)
                                                .First();

            VariableSizedWrapGrid VarSizeWrap = GriPrincipal as VariableSizedWrapGrid;
            var textID = VarSizeWrap.Children.Where(a => (a as TextBox).Name == "Texto-" + version + "." + secuencia + "." + campoPadre)
                                                .Select(a => a)
                                                .First();

            TextBox txtId = textID as TextBox;
            txtId.Text = valorId;
        }
    }
}

