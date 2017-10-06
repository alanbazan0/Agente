using System;
using System.Collections.Generic;
using System.Text;

namespace AgenteApp.Views
{
    public interface IInicioSesionVista
    {
        string NombreUsuario { get; set; }
        string Contrasena { get; set; }
        void IniciarSesion();
        void CargarMenu();
        void MostrarMensaje(string mensaje);
    }
}
