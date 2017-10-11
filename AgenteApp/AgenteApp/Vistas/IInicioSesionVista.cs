using AgenteApp.Modelos;
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
        void MostrarMenu(Usuario usuario);
        void MostrarMensaje(string mensaje);
    }
}
