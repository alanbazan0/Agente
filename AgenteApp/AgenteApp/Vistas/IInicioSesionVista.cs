using AgenteApp.Modelos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Windows.Storage;

namespace AgenteApp.Views
{
    public interface IInicioSesionVista
    {
        string NombreUsuario { get; set; }
        string Contrasena { get; set; }

        string MoveImage { get; set; }

        string Ip { get; set; }
        string Idhardware { get; set; }

        Usuario usuario { get; set; }
        
        Stream IMGStream { get; set; }
        StorageFile StoreFile { get; set; }

        void IniciarSesion();
        void MostrarMenu(Usuario usuario);
        void MostrarMensaje(string mensaje);
        void InsertarSesionTrabajo();
    }
}
