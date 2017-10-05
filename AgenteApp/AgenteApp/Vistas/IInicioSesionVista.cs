using System;
using System.Collections.Generic;
using System.Text;

namespace AgenteApp.Views
{
    public interface IInicioSesionVista
    {
        string NombreUsuario { get; set; }
        string Contraseña { get; set; }
        void InicioSesion();
        void CargarMenu();
        void MostrarMensaje(string mensaje);
    }
}
