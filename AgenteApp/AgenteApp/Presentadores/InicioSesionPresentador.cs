using System;
using System.Collections.Generic;
using System.Text;
using AgenteApp.Views;
using AgenteApp.Models;

namespace AgenteApp.Presenters
{
    public class InicioSesionPresentador
    {
        private IInicioSesionVista vista;
        private Usuario usuario;
        public InicioSesionPresentador(IInicioSesionVista vista)
        {
            this.vista = vista;
        }

        public async void Login()
        {
            UsuarioServicioDatos AccesoDatos = new UsuarioServicioDatos();
            usuario = await AccesoDatos.Encontrar(vista.NombreUsuario, vista.Contraseña);
         
            if(usuario!=null)
            {
                vista.MostrarMensaje("Access granted!");
                vista.CargarMenu();
            }
            else
                vista.MostrarMensaje("Access denied!");


        }
    }
}
