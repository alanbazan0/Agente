using AgenteApp.Modelos;
using AgenteApp.Presentadores;
using AgenteApp.Vistas;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking;
using Windows.Networking.Connectivity;
using Windows.System.Profile;
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
    public sealed partial class CerrarSesionPage : Page,ICerrarSesion
    {
        Usuario usuario;
        string idhardware = "";
        string ip = "";

        CerrarSesionPresentador presentador;
        public CerrarSesionPage()
        {
            this.InitializeComponent();
            presentador = new CerrarSesionPresentador(this);
            
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            usuario = (Usuario)e.Parameter;
            obtenerInformacion();
            CerrarSesion();
        }
        public string NombreUsuario
        {
            get
            {
                return usuario.Id;
            }
            set
            {
                usuario.Id = value;
            }
        }

        public string IP { get { return ip; } set { ip = value; } }
        public string IdHardware { get { return idhardware; } set { idhardware = value; } }

        public void CerrarSesion()
        {
            presentador.CerrarSesion();
        }

        public void cerrarPRograma()
        {
            CoreApplication.Exit();
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
    }
}
