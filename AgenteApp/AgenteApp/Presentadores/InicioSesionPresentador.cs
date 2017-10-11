using System;
using System.Collections.Generic;
using System.Text;
using AgenteApp.Views;
using AgenteApp.Modelos;

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

        public async void IniciarSesion()
        {
            UsuarioServicioDatos accesoDatos = new UsuarioServicioDatos();
            usuario = await accesoDatos.Consultar(vista.NombreUsuario, vista.Contrasena);
         
            if(usuario!=null)
            {               
                vista.MostrarMenu(usuario);
            }
            else
                vista.MostrarMensaje("La combinación de usuario y contraseña es incorrecta.");


        }
    }
}
