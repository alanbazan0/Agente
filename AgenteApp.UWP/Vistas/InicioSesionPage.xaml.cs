﻿using AgenteApp.Views;
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
        public InicioSesionPage()
        {
            this.InitializeComponent();
            mensajeBlockText.Text = "";
            presentador = new InicioSesionPresentador(this);
        }

        public string NombreUsuario { get { return nombreUsuarioTextBox.Text; } set { nombreUsuarioTextBox.Text = value; } }
        public string Contrasena { get { return contrasenaPasswordBox.Password; } set { contrasenaPasswordBox.Password = value; } }
        
        public string Idhardware { get { return idhardware; } set { idhardware = value; } }
        public string Ip { get { return ip; } set { ip = value; } }

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
    }
}
