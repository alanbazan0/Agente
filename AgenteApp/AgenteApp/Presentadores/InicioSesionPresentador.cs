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
                    InsertarSesionTrabajo();
                    //vista.MostrarMenu(usuario);
                }
                else
                    vista.MostrarMensaje("La combinación de usuario y contraseña es incorrecta.");
            }
            else
                vista.MostrarMensaje(resultado.mensajeError);


        }

        public async void InsertarSesionTrabajo()
        {
            UsuarioRepositorio accesoDatos = new UsuarioRepositorio();
            Resultado<string> resultado = await accesoDatos.InsertarSesionTrabajo(vista.Ip, vista.Idhardware,vista.NombreUsuario);
            if (resultado.mensajeError == string.Empty)
            {
                string resultadoa = resultado.valor;
                if (resultadoa.Equals("true"))
                {
                    vista.MostrarMenu(usuario);
                }
                else
                    vista.MostrarMensaje("Sesion de trabajo, no cerro correctamente.\nFavor de liberar usuario para ingresar.");
            }
            else
                vista.MostrarMensaje(resultado.mensajeError);


        }
    }
}
