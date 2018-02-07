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
            if (resultado.mensajeError == string.Empty)
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

        public async void DatosSesion()
        {
            UsuarioRepositorio accesoDatos = new UsuarioRepositorio();
            Resultado<Usuario> resultado = await accesoDatos.ConsultarDatosSesion(vista.NombreUsuario);
            if (resultado != null)
            {
                if (resultado.mensajeError == string.Empty)
                {
                    usuario = resultado.valor;
                    if (usuario != null)
                    {
                        vista.usuario = usuario;
                    }
                    else
                        vista.MostrarMensaje("El usuario no existe, favor de verificar.");
                }
                else
                    vista.desactivarCarga();
            }
            else
                vista.desactivarCarga();
        }
        public async void SubirFotoComparar()
        {
            UsuarioRepositorio accesoDatos = new UsuarioRepositorio();
            Resultado<string> resultado = await accesoDatos.SubirFotoComparar(vista.NombreUsuario, vista.StoreFile, "CM", vista.MoveImage);
            if (resultado.mensajeError == string.Empty)
            {
                string res = resultado.valor;
                if (res != null)
                {
                    ChecarEstatus(res);
                }
                else
                    vista.MostrarMensaje("El usuario no existe, favor de verificar.");
            }
            else
                vista.MostrarMensaje(resultado.mensajeError);
        }

        public async void InsertarSesionTrabajo()
        {
            UsuarioRepositorio accesoDatos = new UsuarioRepositorio();
            Resultado<string> resultado = await accesoDatos.InsertarSesionTrabajo(vista.Ip, vista.Idhardware, vista.NombreUsuario);
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

        public async void ChecarEstatus(string idTabla)
        {
            UsuarioRepositorio accesoDatos = new UsuarioRepositorio();
            Resultado<string> resultado = await accesoDatos.ChecarEstatus(vista.NombreUsuario, idTabla);
            if (resultado.mensajeError == string.Empty)
            {
                string res = resultado.valor;
                if (res != null)
                {
                    switch (res)
                    {
                        case "TR":
                            //unica opcion que tiene acceso concedido
                            //aviso();
                            //vista.MostrarMensaje("Acceso concedido.");
                            vista.MostrarMenu(vista.usuario);
                            ElimitarFaceTemp(idTabla);
                            break;
                        //case "SI":
                        //    //Thread.sleep(3000);
                        //    aviso2();
                        //    break;
                        case "NO":
                            //Error Intrusion de seguridad compra no autorizada
                            //aviso3();
                            vista.MostrarMensaje("Intrusion de seguridad.");
                            ElimitarFaceTemp(idTabla);
                            break;
                        case "AL":
                            ChecarEstatus(idTabla);
                            break;
                        case "CM":
                            ChecarEstatus(idTabla);
                            break;
                        case "NON":
                            //Error: Template no obtenido, le falta nitidez a la foto. Favor de tomar la foto nuevamente.
                            //aviso4();
                            vista.MostrarMensaje("Falta nitidez a la foto, favor de intentar nuevamente");
                            ElimitarFaceTemp(idTabla);
                            break;
                        case "NOI":
                            //Error: Rostro no identificado. Favor de intentar nuevamente.
                            //aviso7();
                            vista.MostrarMensaje("Rostro no identificado. Favor de intentar nuevamente.");
                            ElimitarFaceTemp(idTabla);
                            break;
                        default:
                            break;
                    }
                }
                else
                    vista.MostrarMensaje("El usuario no existe, favor de verificar.");
            }
            else
                vista.MostrarMensaje(resultado.mensajeError);
        }

        private void ElimitarFaceTemp(string idTabla)
        {
            UsuarioRepositorio accesoDatos = new UsuarioRepositorio();
            accesoDatos.ElimitarFaceTemp(vista.NombreUsuario, idTabla);
        }
    }
}
