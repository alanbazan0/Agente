using AgenteApp.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.UI.Popups;
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
using AgenteApp.Presenters;
using AgenteApp.UWP.Vistas;
using NavigationMenuSample;
using System.Net;
using Windows.Networking.Connectivity;
using Windows.System.Profile;
using Windows.Networking;
using Windows.Storage.Streams;
using Windows.Media.Capture;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage;
using AgenteApp.Clases;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AgenteApp.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class InicioSesionPage : Page, IInicioSesionVista
    {
        InicioSesionPresentador presentador;
        string ip = "";
        string idhardware = "";
        private StorageFile storeFile;
        private IRandomAccessStream stream;
        private Stream imgStream;
        public Usuario usuario;
        private byte[] imageCompare;
        string moveImage = "";
        public InicioSesionPage()
        {
            this.InitializeComponent();
            mensajeBlockText.Text = "";
            presentador = new InicioSesionPresentador(this);
            textoBanerTextBlock.Text = "Bastiaan People & Software Center";
        }

        public string NombreUsuario { get { return nombreUsuarioTextBox.Text; } set { nombreUsuarioTextBox.Text = value; } }
        public string Contrasena { get { return contrasenaPasswordBox.Password; } set { contrasenaPasswordBox.Password = value; } }
        
        public string Idhardware { get { return idhardware; } set { idhardware = value; } }
        public string Ip { get { return ip; } set { ip = value; } }

        public string MoveImage { get { return this.moveImage; } set { this.moveImage = value; } }
        
        public Stream IMGStream { get { return imgStream; } set { imgStream = value; } }
        public StorageFile StoreFile { get { return storeFile; } set { this.storeFile = value; } }

        Usuario IInicioSesionVista.usuario
        {
            get { return usuario; }
            set
            {
                usuario = value;
                progressRing.IsActive = false;
                activarModoInicio();
            }
        }

        public void IniciarSesion()
        {
            obtenerInformacion();
            progressRing.IsActive = true;
            presentador.IniciarSesion();
        }

        public async void MostrarMensaje(string mensaje)
        {
            progressRing.IsActive = false;
            if (mensaje != null)
            {
                var dialog = new MessageDialog(mensaje, "Error");
                await dialog.ShowAsync();
            }
        }

        public void MostrarMenu(Usuario usuario)
        {
            progressRing.IsActive = false;
            //insertamos sesion de trabajo
           
           // InsertarSesionTrabajo();
            this.Frame.Navigate(typeof(AppShell), usuario);
        }

        private void iniciarSesionButton_Click(object sender, RoutedEventArgs e)
        {
            mensajeBlockText.Text = "";
            IniciarSesion();
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

        public void InsertarSesionTrabajo()
        {
            presentador.InsertarSesionTrabajo();
        }

        
        private async void captureBtn_Click(object sender, RoutedEventArgs e)
        {
            if (nombreUsuarioTextBox.Text != string.Empty)
            {
                CameraCaptureUI capture = new CameraCaptureUI();
                capture.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;
                capture.PhotoSettings.CroppedAspectRatio = new Size(3, 5);
                capture.PhotoSettings.MaxResolution = CameraCaptureUIMaxPhotoResolution.HighestAvailable;
                storeFile = await capture.CaptureFileAsync(CameraCaptureUIMode.Photo);
                if (storeFile != null)
                {
                    this.imageCompare = await Utilerias.GetBytesAsync(storeFile);
                    this.moveImage = Utilerias.asByteToString(this.imageCompare);
                    this.imgStream = await storeFile.OpenStreamForReadAsync();
                    BitmapImage bimage = new BitmapImage();
                    stream = await storeFile.OpenAsync(FileAccessMode.Read);


                    bimage.SetSource(stream);
                    captureImage.Source = bimage;
                    captureImage.Visibility = Visibility.Visible;
                    SubirFotoComparar();
                }
            }
        }
        public void SubirFotoComparar()
        {
            if (moveImage != string.Empty)
            {
                progressRing.IsActive = true;
                presentador.SubirFotoComparar();
            }
            else
                MostrarMensaje("Imagen no capturada.");
        }
        private void activarModoInicio()
        {
            if (usuario.TipoInicio.Equals("LF"))
                captureBtn.Visibility = Visibility.Visible;
        }
        private void nombreUsuarioTextBox_LosingFocus(object sender, RoutedEventArgs e)
        {
            mensajeBlockText.Text = "";
            if (nombreUsuarioTextBox.Text != string.Empty)
                DatosSesion();
        }
        private void DatosSesion()
        {
            obtenerInformacion();
            progressRing.IsActive = true;
            presentador.DatosSesion();
            
        }
        public void desactivarCarga()
        {
            if(progressRing.IsActive == true)
                progressRing.IsActive = false;
        }

        private void nombreUsuarioTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (captureBtn.Visibility == Visibility.Visible)
                captureBtn.Visibility = Visibility.Collapsed;
            contrasenaPasswordBox.Password = "";
            contrasenaPasswordBox.PlaceholderText = "Contraseña";
        }
    }
}
