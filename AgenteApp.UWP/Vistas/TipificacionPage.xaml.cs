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
using Windows.UI.Xaml.Media.Imaging;
using Windows.Networking;
using Windows.Networking.Connectivity;
using Windows.System.Profile;


// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace AgenteApp.UWP.Vistas
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class TipificacionPage : Page, ITipificacionVista
    {
        Usuario usuario;
        TipificacionPresentador presentador;
        List<Tipificacion> configuracion;
        List<Tipificacion> tipificaciones;
        List<DatosAsistente> DAsistentes;
        Parametros ParametroLocal;
        DatosAsistente DAsistente;
        string AsistenteSeleccionado;
        string ip;
        string idhardware;
        string noTelefonico;
        string IdLlamada;
        string IdCliente;
        string IdCrm="0";
        string ValorAnterior = "";
        List<string> ListaSecuencias;

        public TipificacionPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
            this.Loaded += CommandBarPage_Loaded;
            presentador = new TipificacionPresentador(this);
            configuracion = new List<Tipificacion>();
            DAsistente = new DatosAsistente();
            DAsistentes = new List<DatosAsistente>();
            ParametroLocal = new Parametros();
            ListaSecuencias = new List<string>();
            obtenerInformacion();
            
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
            ConsultarConfiguracion();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            usuario = (Usuario)e.Parameter;
            ConsultarParametros();
        }

        public Parametros Parametro { get => ParametroLocal; set { } }

        public List<Parametros> Parametros
        {
            set
            {
                for (int i = 0; i < value.Count; i++)
                {

                    switch (value[i].PalabraReservada)
                    {
                        case "@NUMEROTEL@":
                            noTelefonico =  value[i].ValorParametro;
                            break;
                        case "@IDLLAMADA@":
                            IdLlamada = value[i].ValorParametro;
                            break;
                        case "@IDCLIENTE@":
                             IdCliente = value[i].ValorParametro;
                            break;
                        case "@IDCRM@":
                            IdCrm = value[i].ValorParametro;
                            break;
                    }
                }
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
     
        public void ConsultarParametros()
        {
            IdCrm = "0";
            ParametroLocal.IdParamtro = usuario.Id;
            ParametroLocal.NumeroMaquina = idhardware;
            ParametroLocal.DireccionIp = ip;
            presentador.ConsultarParametros();
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

               var listv= sp.Children.Where(a => (a as ListView).Name == "ListView-" + DescripicionCampo)
                                                .Select(a => a)
                                                .First();
                ListView lv = listv as ListView;
                lv.ItemsSource = DAsistentes;
            }
        }

        public List<Tipificacion> valoresDatosAsistente { set => throw new NotImplementedException(); }

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
                int i = 0;
                for (; i < conf.Count; i++)
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

            presentador.CrearLineaDivisoriaPie(ref TipifiacionGrid, i + 1);
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

                string[] campoDatos = AsistenteSeleccionado.Split('.');
                string campoidPadre = campoDatos[campoDatos.Length - 1];
                string campoSecuencia = campoDatos[campoDatos.Length - 2];
                string version = campoDatos[campoDatos.Length - 3];
                BuscarDependenciaVisual(version, campoSecuencia, campoidPadre, txtId.Text);
               
                Button bt = BtCancel as Button;
                Flyout f = bt.Flyout as Flyout;
                if (f != null)
                {
                    f.Hide();
                }
            }
        }

        private void BuscarDependenciaVisual(string version, string secuencia, string campoIdPedre, string valorTrigger)
        {
            for (int j = 0; j < ListaSecuencias.Count; j++)
            { 
                if(ListaSecuencias[j]== secuencia)
                    OcultarNoPresentar(version, secuencia, j);
            }
            Tipificacion tip = new Tipificacion();
            for (int i = 0; i < configuracion.Count; i++)
            {
                tip = configuracion[i];
                if (tip.VersionDependenciaVisual == version && tip.SecuenciaDependenciaVisual == secuencia && tip.ValorTrigger == valorTrigger)
                {
                    Grid gr = ContentContainer.Children[0] as Grid;
                    switch (tip.Asistente)
                    {
                        case "2":
                            VisualizarAsistente(ref gr, tip.Version+"."+tip.Secuencia+"."+tip.Campoid,Visibility.Visible);
                            break;
                        case "7":
                            VisualizarTexto(ref gr, tip.Version + "." + tip.Secuencia + "." + tip.Campoid,Visibility.Visible);
                            break;
                    }
                    if(!ListaSecuencias.Contains(secuencia))
                        ListaSecuencias.Add(secuencia);
                }
            }
        }

        private void OcultarNoPresentar(string version, string secuencia, int CountDependent)
        {
            Tipificacion tip = new Tipificacion();
            for(int j = CountDependent;j<ListaSecuencias.Count;j++)
            {
                for (int i = 0; i < configuracion.Count; i++)
                {
                    tip = configuracion[i];
                    if (tip.VersionDependenciaVisual == version && tip.SecuenciaDependenciaVisual == ListaSecuencias[j])
                    {
                        Grid gr = ContentContainer.Children[0] as Grid;
                        switch (tip.Asistente)
                        {
                            case "2":
                                VisualizarAsistente(ref gr, tip.Version + "." + tip.Secuencia + "." + tip.Campoid, presentador.SeleccionarPresntacion(tip.Presentar));
                                break;
                            case "7":
                                VisualizarTexto(ref gr, tip.Version + "." + tip.Secuencia + "." + tip.Campoid, presentador.SeleccionarPresntacion(tip.Presentar));
                                break;
                        }
                    }
                }
            }
            
        }

        private void VisualizarAsistente(ref Grid contenedorPrincipal,string campohijo,Visibility visEnable)
       {
                var GriPrincipal = contenedorPrincipal.Children.Where(a => (a as VariableSizedWrapGrid).Name == "GridLabel-" + campohijo)
                                                    .Select(a => a)
                                                    .First();
                VariableSizedWrapGrid VarSizeWrap = GriPrincipal as VariableSizedWrapGrid;
                VarSizeWrap.Visibility = visEnable;

                GriPrincipal = contenedorPrincipal.Children.Where(a => (a as VariableSizedWrapGrid).Name == "Grid-" + campohijo)
                                                    .Select(a => a)
                                                    .First();
                VarSizeWrap = GriPrincipal as VariableSizedWrapGrid;
                VarSizeWrap.Visibility = visEnable;

                GriPrincipal = contenedorPrincipal.Children.Where(a => (a as VariableSizedWrapGrid).Name == "Grid_id-" + campohijo)
                                                .Select(a => a)
                                                .First();
                VarSizeWrap = GriPrincipal as VariableSizedWrapGrid;
                GriPrincipal.Visibility = visEnable;
                
                GriPrincipal = contenedorPrincipal.Children.Where(a => (a as VariableSizedWrapGrid).Name == "Grid_dsc-" + campohijo)
                                                .Select(a => a)
                                                .First();
                VarSizeWrap = GriPrincipal as VariableSizedWrapGrid;
                GriPrincipal.Visibility = visEnable;
                
       }
        
        private void VisualizarTexto(ref Grid contenedorPrincipal, string campohijo, Visibility visEnable)
        {
            Grid gr = ContentContainer.Children[0] as Grid;
            var GriPrincipal = gr.Children.Where(a => (a as VariableSizedWrapGrid).Name == "GridLabel-" + campohijo)
                                                .Select(a => a)
                                                .First();

            VariableSizedWrapGrid VarSizeWrap = GriPrincipal as VariableSizedWrapGrid;
            VarSizeWrap.Visibility = visEnable;

            GriPrincipal = contenedorPrincipal.Children.Where(a => (a as VariableSizedWrapGrid).Name == "Grid_texto-" + campohijo)
                                               .Select(a => a)
                                               .First();
            VarSizeWrap = GriPrincipal as VariableSizedWrapGrid;
            GriPrincipal.Visibility = visEnable;

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
            gridCombo.Background = presentador.GetColor(campo.Colorfondo);
            gridCombo.Height = 50;
            gridCombo.VerticalAlignment = VerticalAlignment.Center;
            TextBlock block = new TextBlock();
            block.Text = campo.Descripcion;
            block.Foreground = presentador.GetColor(campo.Colorletra);
            gridCombo.Children.Add(block);
            GridHeaderTemplate.Children.Add(gridCombo);
            //aistente vacio porque es combo
            gridCombo = new VariableSizedWrapGrid();
            gridCombo.SetValue(Grid.RowProperty, i);
            gridCombo.SetValue(Grid.ColumnProperty, 1);
            gridCombo.Background = presentador.GetColor(campo.Colorfondo);
            gridCombo.Height = 50;
            gridCombo.VerticalAlignment = VerticalAlignment.Center;
            GridHeaderTemplate.Children.Add(gridCombo);
            // campo id
            gridCombo = new VariableSizedWrapGrid();
            gridCombo.SetValue(Grid.RowProperty, i);
            gridCombo.SetValue(Grid.ColumnProperty, 2);
            gridCombo.Background = presentador.GetColor(campo.Colorfondo);
            gridCombo.Height = 50;
            gridCombo.VerticalAlignment = VerticalAlignment.Center;
            TextBox text = new TextBox();
            text.Name = "ID-"+campo.Version+"." +campo.Secuencia+"."+campo.Campoid ;
            gridCombo.Children.Add(text);
            GridHeaderTemplate.Children.Add(gridCombo);
            //campo descripcion
            gridCombo = new VariableSizedWrapGrid();
            gridCombo.SetValue(Grid.RowProperty, i);
            gridCombo.SetValue(Grid.ColumnProperty, 3);
            gridCombo.Background = presentador.GetColor(campo.Colorfondo);
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
            gridCombo.Background = presentador.GetColor(campo.Colorfondo);
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
            gridCombo.Background = presentador.GetColor(campo.Colorfondo);
            gridCombo.Height = 50;
            gridCombo.VerticalAlignment = VerticalAlignment.Center;
            gridCombo.Name = "GridLabel-" + campo.Version + "." + campo.Secuencia + "." + campo.Campoid;
            TextBlock block = new TextBlock();
            block.Text = campo.Descripcion;
            block.Foreground = presentador.GetColor(campo.Colorletra);
            gridCombo.Children.Add(block);
            gridCombo.Visibility = presentador.SeleccionarPresntacion(campo.Presentar);
            GridHeaderTemplate.Children.Add(gridCombo);

            //asistente
            gridCombo = new VariableSizedWrapGrid();
            gridCombo.SetValue(Grid.RowProperty, i);
            gridCombo.SetValue(Grid.ColumnProperty, 1);
            gridCombo.Background = presentador.GetColor(campo.Colorfondo);
            gridCombo.Height = 50;
            gridCombo.VerticalAlignment = VerticalAlignment.Center;
            gridCombo.Name = "Grid-" + campo.Version + "." + campo.Secuencia + "." + campo.Campoid;
            AppBarButton BT = new AppBarButton();
            BT.Name = "Asis-" + campo.Version + "." + campo.Secuencia + "." + campo.Campoid;
            BT.Height = 50;
            BT.Width = 50;
            BT.Click += ConsultaAsistente;
            BT.Icon = new SymbolIcon(Symbol.Find);          
            BT.Background = new SolidColorBrush(Colors.White);
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
            gridCombo.Children.Add(BT);
            gridCombo.Visibility = presentador.SeleccionarPresntacion(campo.Presentar);
            GridHeaderTemplate.Children.Add(gridCombo);

            //campo id
            gridCombo = new VariableSizedWrapGrid();
            gridCombo.SetValue(Grid.RowProperty, i);
            gridCombo.SetValue(Grid.ColumnProperty, 2);
            gridCombo.Background = presentador.GetColor(campo.Colorfondo);
            gridCombo.Height = 50;
            gridCombo.VerticalAlignment = VerticalAlignment.Center;
            gridCombo.Name = "Grid_id-" + campo.Version + "." + campo.Secuencia + "." + campo.Campoid;
            TextBox text = new TextBox();
            text.Name = "ID-"+campo.Version+"." +campo.Secuencia+"."+campo.Campoid ;
            gridCombo.Children.Add(text);
            gridCombo.Visibility = presentador.SeleccionarPresntacion(campo.Presentar);
            GridHeaderTemplate.Children.Add(gridCombo);

            // campo descripcion
            gridCombo = new VariableSizedWrapGrid();
            gridCombo.SetValue(Grid.RowProperty, i);
            gridCombo.SetValue(Grid.ColumnProperty, 3);
            gridCombo.SetValue(Grid.ColumnSpanProperty, 2);
            gridCombo.Background = presentador.GetColor(campo.Colorfondo);
            gridCombo.Height = 50;
            gridCombo.VerticalAlignment = VerticalAlignment.Center;
            gridCombo.Name = "Grid_dsc-" + campo.Version + "." + campo.Secuencia + "." + campo.Campoid;
            text = new TextBox();
            text.Name = "DSC-" + campo.Version + "." + campo.Secuencia + "." + campo.Campoid;
            text.Width = 290;
            gridCombo.Children.Add(text);
            gridCombo.Visibility = presentador.SeleccionarPresntacion(campo.Presentar);
            GridHeaderTemplate.Children.Add(gridCombo);

        }

        private void GuardarTipi_Click(object sender, RoutedEventArgs e)
        {
            GuardarTipificacion();
        }

        public void GuardarTipificacion()
        {
            string valor = "";
            CRM crm1 = new CRM();
            Tipificacion tipi;
            tipificaciones = new List<Tipificacion>();
            List<CRM> listCrm = new List<CRM>();
            crm1.CanalId = "1";
            crm1.IdLlamada       = IdLlamada;
            crm1.Extension       = usuario.Extension;
            crm1.TelefonoCliente = noTelefonico;
            crm1.IdCliente    = IdCliente;
            crm1.IdAgente     = usuario.Id;
            crm1.NombreAgente = usuario.Nombre;
            crm1.IdFolio = IdCrm;
            listCrm.Add(crm1);
            for (int i = 0; i < configuracion.Count; i++)
            {

                switch (configuracion[i].Asistente)
                {
                        case "1":                        
                        break;
                        case "2":
                            tipi = new Tipificacion();
                            tipi = buscarComponentePadre(configuracion[i].Version, configuracion[i].Secuencia, configuracion[i].Campoid);
                        if (tipi.Presentar == "SI")
                        {
                            tipi.Secuencia = configuracion[i].Secuencia;
                            tipi.Version = configuracion[i].Version;
                            tipi.Campoid = configuracion[i].Campoid;
                            tipi.Descripcion = configuracion[i].Descripcion;
                            tipificaciones.Add(tipi);
                        }
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
                            tipi = new Tipificacion();
                            tipi = buscarComponentePadreTexto(configuracion[i].Version, configuracion[i].Secuencia, configuracion[i].Campoid);

                        if (tipi.Presentar == "SI")
                        {
                            tipi.Secuencia = configuracion[i].Secuencia;
                            tipi.Version = configuracion[i].Version;
                            tipi.Campoid = configuracion[i].Campoid;
                            tipi.Descripcion = configuracion[i].Descripcion;
                            tipificaciones.Add(tipi);
                        }
                        break;
                }                
                
            }
            if(IdCrm == "0")
            {
                presentador.GuardarTipificacion(listCrm, tipificaciones);
            }
            else
            {
                presentador.ActulizarTipificacion(listCrm, tipificaciones);
            }
        }

        private void ConsultaAsistente(object sender, RoutedEventArgs e)
        {
            String DescripicionCampo = (sender as Button).Name.Split('-')[1];
            AsistenteSeleccionado = DescripicionCampo;
            string[] campoDatos = DescripicionCampo.Split('.');
            string campoHijo = campoDatos[campoDatos.Length-1];
            string campoPadre = "";
            string Criterio = "";
            Tipificacion tip = new Tipificacion();
            for (int i=0;i< configuracion.Count;i++)
            {
                tip = configuracion[i];
                if (tip.Campoid == campoHijo)
                {
                    if (tip.CampoPadre != ""&& tip.CampoPadre != null)
                    {
                        foreach (Tipificacion jera in configuracion)
                        {
                            if (jera.Campoid.Equals(tip.CampoPadre))
                            {
                                Criterio = buscarComponentePadre(jera.Version, jera.Secuencia, jera.Campoid).Valores;
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

        private Tipificacion buscarComponentePadre(string version, string secuencia, String campoPadre)
        {
            Tipificacion TipificaiconRecuperada = new Tipificacion();
            Grid gr = ContentContainer.Children[0] as Grid;
            var GriPrincipal = gr.Children.Where(a => (a as VariableSizedWrapGrid).Name == "Grid_id-" + version+"."+ secuencia+"."+ campoPadre)
                                                .Select(a => a)
                                                .First();
               
            VariableSizedWrapGrid VarSizeWrap = GriPrincipal as VariableSizedWrapGrid;
            var textID = VarSizeWrap.Children.Where(a => (a as TextBox).Name == "ID-" + version + "." + secuencia + "." + campoPadre)
                                                .Select(a => a)
                                                .First();

            TextBox txtId = textID as TextBox;
            TipificaiconRecuperada.Presentar = presentador.TomarPresntacion(GriPrincipal.Visibility);
            TipificaiconRecuperada.Valores = txtId.Text;
            return TipificaiconRecuperada;
        }

        private Tipificacion buscarComponentePadreTexto(string version, string secuencia, String campoPadre)
        {
            Tipificacion TipificaiconRecuperada = new Tipificacion();
            Grid gr = ContentContainer.Children[0] as Grid;
            var GriPrincipal = gr.Children.Where(a => (a as VariableSizedWrapGrid).Name == "Grid_texto-" + version + "." + secuencia + "." + campoPadre)
                                                .Select(a => a)
                                                .First();

            VariableSizedWrapGrid VarSizeWrap = GriPrincipal as VariableSizedWrapGrid;
            var textID = VarSizeWrap.Children.Where(a => (a as TextBox).Name == "Texto-" + version + "." + secuencia + "." + campoPadre)
                                                .Select(a => a)
                                                .First();

            TextBox txtId = textID as TextBox;
            TipificaiconRecuperada.Presentar = presentador.TomarPresntacion(GriPrincipal.Visibility);
            TipificaiconRecuperada.Valores = txtId.Text;
            return TipificaiconRecuperada;
        }

        public  string IDCRM
        {
            set
            {
                IdCrm = value;
            }
            get
            {
                return IdCrm;
            }
        }

        
    }
}
