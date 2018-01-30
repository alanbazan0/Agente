using AgenteApp.Clases;
using AgenteApp.Modelos;
using AgenteApp.Repositorios;
using AgenteApp.Vistas;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.UI.Xaml;

namespace AgenteApp.Presentadores
{
    class FormularioPresentador
    {
        IFormulario vista;
        List<Componente> campoFromulario;
        List<TipoTelefono> tipoTelefono;

        public FormularioPresentador(IFormulario vista)
        {
            this.vista = vista;
        }
        public void CrearFormulario(ModoVentana modo)
        {
            if (modo == ModoVentana.ALTA)
                CrearFormularioAlta();
            else
                CrearForumularioActualizacion();
        }

        private async void CrearForumularioActualizacion()
        {
            FormularioActualizacionRepositorio repositorio = new FormularioActualizacionRepositorio();
            Resultado<List<Componente>> resultado = await repositorio.ConsultarPorVersion(Constantes.VERSION);

            if (resultado.mensajeError == string.Empty)
            {
                campoFromulario = resultado.valor;
                int tamanoTitulo = CalcularColumanMayor(campoFromulario);
                foreach (Componente campoFromulario in campoFromulario)
                {
                    if (campoFromulario.presentacion == "1")
                        vista.CrearFormularioClientes(campoFromulario, tamanoTitulo);
                }
            }
            else
                vista.MostrarMensaje("Error", resultado.mensajeError);
        }

        public async void GuardarClientes(ModoVentana modo)
        {

            List<Campo> campos = vista.Campos;
            ClientesRepositorio repositorio = new ClientesRepositorio();
            if (modo == ModoVentana.ALTA)
            {
                Resultado<string> resultado = await repositorio.Insertar(campos, Constantes.VERSION);
                if (resultado.mensajeError == string.Empty)
                {
                    vista.idCliente = resultado.valor;
                }
                else
                    vista.MostrarMensaje("Error", resultado.mensajeError);
            }
            else
            {
                Resultado<string> resultado = await repositorio.Actualizar(campos, Constantes.VERSION);
                if (resultado.mensajeError == string.Empty)
                {
                    vista.ActualizarCorreoCliente(resultado.valor);
                    vista.ActualizarTelefonoCliente(resultado.valor);

                }
                else
                    vista.MostrarMensaje("Error", resultado.mensajeError);
            }
        }
        public async void guardarTelefonoCliente(List<ClienteTelefono> clienteTelefono)
        {
            ClientesTelefonoRepositorio repositorio = new ClientesTelefonoRepositorio();
            Resultado<string> resultado = await repositorio.Insertar(clienteTelefono);
            if (resultado.mensajeError == string.Empty)
            {
                vista.MostrarMensaje("Alta", "El cliente se guardo correctamente");
            }
            else
                vista.MostrarMensaje("Error", resultado.mensajeError);
        }


        public async void BorrarTelefonoCliente(string numero, string idCliente)
        {
            ClientesTelefonoRepositorio repositorio = new ClientesTelefonoRepositorio();
            Resultado<string> resultado = await repositorio.BorrarTelefonoCliente(numero, idCliente);
            if (resultado.mensajeError == string.Empty)
            {
                vista.MostrarMensaje("Eliminar", "El numero del cliente se borro correctamente");
            }
            else
                vista.MostrarMensaje("Error", resultado.mensajeError);
        }

        public async void eliminarCorreo(string numero, string idCliente)
        {
            ClientesTelefonoRepositorio repositorio = new ClientesTelefonoRepositorio();
            Resultado<string> resultado = await repositorio.eliminarCorreo(numero, idCliente);
            if (resultado.mensajeError == string.Empty)
            {
                vista.MostrarMensaje("Alta", "El numero del cliente se borro correctamente");
            }
            else
                vista.MostrarMensaje("Error", resultado.mensajeError);
        }

        private async void CrearFormularioAlta()
        {
            FormularioAltaRepositorio repositorio = new FormularioAltaRepositorio();
            Resultado<List<Componente>> resultado = await repositorio.ConsultarPorVersion(Constantes.VERSION);

            if (resultado.mensajeError == string.Empty)
            {
                campoFromulario = resultado.valor;
                int tamanoTitulo= CalcularColumanMayor(campoFromulario);
                foreach (Componente campoFromulario in campoFromulario)
                {
                    if (campoFromulario.presentacion == "1")
                        vista.CrearFormularioClientes(campoFromulario, tamanoTitulo);
                }
            }
            else
                vista.MostrarMensaje("Error", resultado.mensajeError);
        }

        public int CalcularColumanMayor(List<Componente> campoFromularios)
        {
            int tamanoTitulo = 0;
            foreach (Componente campoFromulario in campoFromularios)
            {                  
                    if (Convert.ToInt32(campoFromulario.titulo.Count()) > tamanoTitulo)
                    {
                        tamanoTitulo = Convert.ToInt32(campoFromulario.titulo.Count());
                    }
                    else
                    {
                    }
                
            }
           return tamanoTitulo;
        }

        public async void TipoTelefono()
        {
            FormularioAltaRepositorio repositorio = new FormularioAltaRepositorio();
            Resultado<List<TipoTelefono>> resultado = await repositorio.ConsultarTiopTelefono();

            if (resultado.mensajeError == string.Empty)
            {
                vista.TipoTelefono = resultado.valor;
            }
            else
                vista.MostrarMensaje("Error", resultado.mensajeError);
        }


        public async void consultarCP(string cp, object sender, RoutedEventArgs e)
        {
            FormularioAltaRepositorio repositorio = new FormularioAltaRepositorio();
            Resultado<List<CodigoPostal>> resultado = await repositorio.consultarCP(cp);

            if (resultado.mensajeError == string.Empty)
            {
                vista.direccioneCodigo(resultado.valor,sender,e);
            }
            else
                vista.MostrarMensaje("Error", resultado.mensajeError);
        }
        
        public async void TraerDatosCliente(int idCliente)
        {
            //List<Filtro> filtros = vista.Filtros;
            ClientesRepositorio repositorio = new ClientesRepositorio();
            Resultado<Objeto> resultado = await repositorio.ConsultarPorIdCliente(idCliente, Constantes.VERSION);
            if (resultado.mensajeError == string.Empty)
            {
                AsignarValores(resultado.valor);
            }
            else
                vista.MostrarMensaje("Error", resultado.mensajeError);
        }

        public async void traerDatosTelefono(int idCliente)
        {
            ClientesTelefonoRepositorio repositorio = new ClientesTelefonoRepositorio();
            Resultado<List<ClienteTelefono>> resultado = await repositorio.ConsultarTelefonoIdCliente(idCliente);
            if (resultado.mensajeError == string.Empty)
            {
                vista.ClienteTelefono = resultado.valor;
            }
            else
                vista.MostrarMensaje("Error", resultado.mensajeError);
        }

        public async void traerDatosCorreos(int idCliente)
        {
            ClientesTelefonoRepositorio repositorio = new ClientesTelefonoRepositorio();
            Resultado<List<Correos>> resultado = await repositorio.ConsultarCorreoIdCliente(idCliente);
            if (resultado.mensajeError == string.Empty)
            {
                vista.ClienteCorreos = resultado.valor;
            }
            else
                vista.MostrarMensaje("Error", resultado.mensajeError);
        }
        
        public async void ActualizarTelefonoCliente(List<ClienteTelefono> clienteTelefono, string idCliente)
        {
            ClientesTelefonoRepositorio repositorio = new ClientesTelefonoRepositorio();
            Resultado<string> resultado = await repositorio.Actualizar(clienteTelefono, idCliente);
            if (resultado.mensajeError == string.Empty)
            {
                vista.MostrarMensaje("Notificación", "Se actualizó el registro correctamente el cliente");
            }
            else
                vista.MostrarMensaje("Error", resultado.mensajeError);
        }
        public async void ActualizarCorreoCliente(List<Correos> clienteCorreo, string idCliente)
        {
            ClientesTelefonoRepositorio repositorio = new ClientesTelefonoRepositorio();
            Resultado<string> resultado = await repositorio.ActualizarCorreo(clienteCorreo, idCliente);
            if (resultado.mensajeError == string.Empty)
            {
                //vista.MostrarMensaje("Notificación", "Se actualizó el registro correctamente el cliente");
            }
            else
                vista.MostrarMensaje("Error", resultado.mensajeError);
        }
        
        private void AsignarValores(Objeto registro)
        {
            foreach (var campo in campoFromulario)
            {
                vista.AsignarValor(campo, registro);
            }
        }
        public async void ConsultarPortabilidad(string numeroTelefono)
        {
            ClientesTelefonoRepositorio repositorio = new ClientesTelefonoRepositorio();
            Resultado<List<Portabilidad>> resultado = await repositorio.ConsultarPortabilidad(numeroTelefono);
            if (resultado.mensajeError == string.Empty)
            {
                if (resultado.valor.Count > 0)
                    vista.ConsultarPortabilidad(resultado.valor[0].DescripcionPortabilidad, resultado.valor[0].IdMunicipio, resultado.valor[0].IdConsecutivo);
                else
                {
                    resultado = await repositorio.ConsultarPortabilidadVacia(numeroTelefono);
                    vista.ConsultarPortabilidad(resultado.valor[0].DescripcionPortabilidad, resultado.valor[0].IdMunicipio, resultado.valor[0].IdConsecutivo);
                }
            }
            else
                vista.MostrarMensaje("Error", resultado.mensajeError);
        }
        public async void guardarCorreoCliente(List<Correos> clienteCorreo,string idCliente)
        {
            ClientesTelefonoRepositorio repositorio = new ClientesTelefonoRepositorio();
            Resultado<string> resultado = await repositorio.InsertarCorreo(clienteCorreo, idCliente);
            if (resultado.mensajeError == string.Empty)
            {
                //vista.MostrarMensaje("Alta", "El cliente se guardo correctamente");
            }
            else
                vista.MostrarMensaje("Error", resultado.mensajeError);
        }

    }
}
