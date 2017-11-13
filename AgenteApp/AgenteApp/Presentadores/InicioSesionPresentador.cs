using System;
using System.Collections.Generic;
using System.Text;
using AgenteApp.Views;
using AgenteApp.Modelos;
using AgenteApp.Clases;

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
            UsuarioRepositorio accesoDatos = new UsuarioRepositorio();
            Resultado<Usuario> resultado = await accesoDatos.Consultar(vista.NombreUsuario, vista.Contrasena);
            if(resultado.mensajeError==string.Empty)
            {
                usuario = resultado.valor;
                if (usuario != null)
                {
                    vista.MostrarMenu(usuario);
                }
                else
                    vista.MostrarMensaje("La combinación de usuario y contraseña es incorrecta.");
            }
            else
                vista.MostrarMensaje(resultado.mensajeError);


        }
    }
}
