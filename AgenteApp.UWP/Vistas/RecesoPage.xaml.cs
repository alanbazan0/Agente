using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Graphics.Display;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using AgenteApp.Presentadores;
using AgenteApp.Vistas;
using AgenteApp.Modelos;
using Windows.UI.Popups;
using System.Diagnostics;
using System.Threading;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace AgenteApp.UWP.Vistas
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class RecesoPage : Page, IReceso
    {
        RecesoPresentador presentador;
        Usuario usuario;
        TipoRecesos item;

        int seg = 0;
        int segAcum = 0;
        int min = 0;
        int minAcum = 0;
        int hor = 0;
        int horAcum = 0;
        int segunds = 0;
        int segundos = 0;

        private DispatcherTimer dispatcherTimer;
        private DispatcherTimer acomuladoTimer;

        String estatusAgente="";
        String duracionReceso = "";
        String fechaGuardada = "";
        String horaGuarda = "";
        String idSeleccionado = "";


        public string NombreUsuario { get { return usuario.Id; } set { } }
        public string idTipoSolicitud { get { return item.Id; } set { } }
        public string DescTipoSolicitud { get { return item.RLargo; } set { } }
        public string EstatusSolicitud { get { return estatusAgente; } set { } }
        public string LlamadaId { get { return "2429847274.433434"; } set { } }

        public string idSolicutdGuardada { get { return idSeleccionado; } set { } }
        public string fechaInicialGuardada { get { return fechaGuardada; } set { } }
        public string horaInicialGuardada { get { return horaGuarda; } set { } }
        public string duracion { get { return duracionReceso; } set { } }
        public string duracionSeg { get { return ""+segundos; } set { } }

        public List<TipoRecesos> TipoRecesos
        {
            set
            {
                progressRing.IsActive = false;
                tipoRecesoComboBox.ItemsSource = value;
            }
        }

        public List<MovimientoPersonal> MovimientosPersonales
        {
            set
            {
                int tiempoAcomulado = 0;
                TimeSpan t;
                for (int i = 0; i < value.LongCount(); i++)
                {
                    if (value[i].DuracionSeg.Equals(""))
                    {
                        value[i].DuracionSeg = "0";
                    }
                    tiempoAcomulado +=int.Parse(value[i].DuracionSeg);
                    t = TimeSpan.FromSeconds(tiempoAcomulado);
                    string tiempo = string.Format("{0:D2}:{1:D2}:{2:D2}",t.Hours,t.Minutes,t.Seconds);
                    value[i].Orden = "" + (i + 1);
                    value[i].TiempoAcomulado = tiempo;

                    if (value[i].IdReceso.Equals("SBSC"))
                    {
                        horaInicio.Text=value[i].HoraInicial;
                    }
                }
                timerAcomulado.Text=string.Format("{0:D2}:{1:D2}:{2:D2}", t.Hours, t.Minutes, t.Seconds);
                segAcum = t.Seconds; minAcum = t.Minutes;  horAcum = t.Hours;
                clientesListView.ItemsSource = value;
            }
        }

        public List<MovimientoPersonal> MovimientoPersonalGuarda
        {
            set
            {
                List<MovimientoPersonal> a = value;
                fechaGuardada = a[0].FechaInicial;
                horaGuarda = a[0].HoraInicial;
                ConsultaMovimientos();

            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            usuario = (Usuario)e.Parameter;
            ConsultaTipoRecesos();
            ConsultaMovimientos();

        }
        public List<TipoRecesos> ParametrosIns
        {
            get
            {
                List<TipoRecesos> filtros = new List<TipoRecesos>();
                return filtros;
            }
        }        

        public List<EstatusUsuario> estatusUsuario
        {
            set
            {
                List<EstatusUsuario> estatus = new List<EstatusUsuario>();
                estatus = value;
                if (estatus[0].EstatusId.Equals("SOL"))
                {
                    ConsultarStatus();
                }
                else if(estatus[0].EstatusId.Equals("SOLAUT"))
                {
                    avisoAutorizado(estatus[0].DscReceso); 
                }
            }
        }

        private async void avisoAutorizado(String motivo)
        {
           var dialog = new MessageDialog("Solicitud de "+ motivo+" fue autorizado.");
           await dialog.ShowAsync();
            btnSolicitar.Visibility= Visibility.Collapsed;
            btnRegresarReceso.Visibility = Visibility.Collapsed;
            btnTomarReceso.Visibility = Visibility.Visible;

        }

        public RecesoPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;

            btnTomarReceso.Visibility = Visibility.Collapsed;
            btnRegresarReceso.Visibility = Visibility.Collapsed;
            presentador = new RecesoPresentador(this);            
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);


            acomuladoTimer = new DispatcherTimer();
            acomuladoTimer.Tick += dispatcherTimer_Tick2;
            acomuladoTimer.Interval = new TimeSpan(0, 0, 1);
        }
        void dispatcherTimer_Tick(object sender, object e)
        {
            ShowTime();
        }
        void dispatcherTimer_Tick2(object sender, object e)
        {
            mostrarTime();
        }
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            double? diagonal = DisplayInformation.GetForCurrentView().DiagonalSizeInInches;
            if (diagonal < 7)
            {
                topbar.Visibility = Visibility.Collapsed;
                pageTitleContainer.Visibility = Visibility.Visible;
            }
            else
            {
                topbar.Visibility = Visibility.Visible;
                pageTitleContainer.Visibility = Visibility.Collapsed;
            }
        }

        void clickSolicitar(object sender, RoutedEventArgs e)
        {
            solictarReceso();
        }

        
         void clickAutorizar(object sender, RoutedEventArgs e)
        {
            autorizar();           
        }
        void clickTomarReceso(object sender, RoutedEventArgs e)
        {
            actualizarEstatus();
        }
        void clickRegresarReceso(object sender, RoutedEventArgs e)
        {
            btnSolicitar.Visibility = Visibility.Collapsed;
            btnRegresarReceso.Visibility = Visibility.Collapsed;
            btnSolicitar.Visibility = Visibility.Visible;

            dispatcherTimer.Stop();
            acomuladoTimer.Stop();

            estatusAgente = "DIS";
            duracionReceso = timerReceso.Text;

            presentador.actualizarEstatus();
            actualizarMovimientos();
        }

        private void AppCerrarButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(NavigationMenuSample.Views.AgentePage));  //AgenteApp.UWP.Vistas.AgentePage));
        }

        public void ConsultaTipoRecesos()
        {
            progressRing.IsActive = true;
            presentador.ConsultaTipoRecesos();
        }

        public async void MostrarMensajeAsync(string mensaje)
        {
            progressRing.IsActive = false;            
            if (mensaje.Equals("true"))
            {
                 var dialog = new MessageDialog("Solicitud de receso enviada");
                await dialog.ShowAsync();
                ConsultarStatus();
            }
            else 
            {
                var dialog = new MessageDialog("Error al enviar solicitud de receso");
                await dialog.ShowAsync();
            }

        }
        
        private void mostrarTime()
        {
            if (minAcum == 60)
            {
                horAcum += 1;
                minAcum = 0;
            }
            if (segAcum == 60)
            {
                minAcum += 1;
                segAcum = 0;
            }
            segAcum += 1;
            String Sseg = "0";
            if (segAcum < 10)
            { Sseg += segAcum.ToString(); }
            else
            {
                Sseg = segAcum.ToString();
            }
            String Smin = "0";
            if (minAcum < 10)
            { Smin += minAcum.ToString(); }
            else
            {
                Smin = minAcum.ToString();
            }
            String Shor = "0";
            if (horAcum < 10)
            { Shor += horAcum.ToString(); }
            else
            {
                Shor = horAcum.ToString();
            }
            timerAcomulado.Text = Shor + ":" + Smin + ":" + Sseg;
        }

        private void ShowTime()
        {
            //timerReceso.Text = DateTime.Now.ToString("hh:mm:ss");
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
            timerReceso.Text = Shor + ":" + Smin + ":" + Sseg;
            segunds += 1;
            segundos += 1;
        }
        public void solictarReceso()
        {
            progressRing.IsActive = true;
            item = (TipoRecesos)tipoRecesoComboBox.SelectedItem;
            estatusAgente = "SOL";
            idSeleccionado = item.Id;
            presentador.solictarReceso();
        }

        public void autorizar()
        {            
            presentador.autorizar();
        }

        public void ConsultaMovimientos()
        {
            presentador.ConsultaMovimientos();
        }

        public void ConsultarStatus()
        {
            presentador.ConsultarStatus();
        }

        public void actualizarEstatus()
        {
            estatusAgente = "RES";
            presentador.actualizarEstatus();
            presentador.insertarMovimiento();
        }

        public void actualizarEstatusResult(String valor, String estatus)
        {
            if (estatus.Equals("RES"))
            {
                segundos = 0;
                dispatcherTimer.Start();
                acomuladoTimer.Start();
                btnTomarReceso.Visibility = Visibility.Collapsed;
                btnSolicitar.Visibility = Visibility.Collapsed;
                btnRegresarReceso.Visibility = Visibility.Visible;
            }
            else if(estatus.Equals("DIS"))
            {
                timerReceso.Text = "00:00:00";
                seg = 0;min = 0; hor = 0;
                btnTomarReceso.Visibility = Visibility.Collapsed;
                btnSolicitar.Visibility = Visibility.Visible;
                btnRegresarReceso.Visibility = Visibility.Collapsed;
            }

        }

        public void actualizarMovimientos()
        {
            presentador.actualizarMovimientos();
            ConsultaMovimientos();
        }
    }

    
}
