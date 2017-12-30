using AgenteApp.Clases;
using AgenteApp.Modelos;
using AgenteApp.Repositorios;
using AgenteApp.Vistas;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                foreach (Componente campoFromulario in campoFromulario)
                {
                    if (campoFromulario.presentacion == "1")
                        vista.CrearFormularioClientes(campoFromulario);
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

        private async void CrearFormularioAlta()
        {
            FormularioAltaRepositorio repositorio = new FormularioAltaRepositorio();
            Resultado<List<Componente>> resultado = await repositorio.ConsultarPorVersion(Constantes.VERSION);

            if (resultado.mensajeError == string.Empty)
            {
                campoFromulario = resultado.valor;
                foreach (Componente campoFromulario in campoFromulario)
                {
                    if (campoFromulario.presentacion == "1")
                        vista.CrearFormularioClientes(campoFromulario);
                }
            }
            else
                vista.MostrarMensaje("Error", resultado.mensajeError);
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
            Resultado <ClienteTelefono> resultado = await repositorio.ConsultarTelefonoIdCliente(idCliente);
            if (resultado.mensajeError == string.Empty)
            {
                vista.ClienteTelefono = resultado.valor;
            }
            else
                vista.MostrarMensaje("Error", resultado.mensajeError);
        }
        public async void ActualizarTelefonoCliente(ClienteTelefono clienteTelefono)
        {
            ClientesTelefonoRepositorio repositorio = new ClientesTelefonoRepositorio();
            Resultado<string> resultado = await repositorio.Actualizar(clienteTelefono);
            if (resultado.mensajeError == string.Empty)
            {
                vista.MostrarMensaje("Notificación", "Se actualizó el registro correctamente el cliente");
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
               if( resultado.valor.Count>0)
                    vista.ConsultarPortabilidad(resultado.valor[0].DescripcionPortabilidad, resultado.valor[0].IdMunicipio, resultado.valor[0].IdConsecutivo);
               else
                    vista.ConsultarPortabilidad(" "," "," ");
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
                vista.MostrarMensaje("Alta", "El cliente se guardo correctamente");
            }
            else
                vista.MostrarMensaje("Error", resultado.mensajeError);
        }

    }
}
