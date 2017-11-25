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
        List<CampoFormulario> campoFromulario;
        List<TipoTelefono> tipoTelefono;

        public FormularioPresentador(IFormulario vista)
        {
            this.vista = vista;
        }
        public void CrearFormulario()
        {
            CrearFormularioAlta();
        }
        public async void GuardarClientes()
        {
            List<Campo> campos = vista.Campos;
            ClientesAltaRepositorio repositorio = new ClientesAltaRepositorio();
            Resultado<string> resultado = await repositorio.Insertar(campos, Constantes.VERSION);
            if (resultado.mensajeError == string.Empty)
            {
                vista.idCliente = resultado.valor;                
            }
            else
                vista.MostrarMensaje("Error", resultado.mensajeError);
        }
        public async void guardarTelefonoCliente(string idCliente, string numTel, Portabilidad portabilidad)
        {
            ClientesAltaTelefonoRepositorio repositorio = new ClientesAltaTelefonoRepositorio();
            Resultado<string> resultado = await repositorio.Insertar(idCliente, numTel, portabilidad);
            if (resultado.mensajeError == string.Empty)
            {
                vista.idCliente = resultado.valor;
            }
            else
                vista.MostrarMensaje("Error", resultado.mensajeError);
        }

        private async void CrearFormularioAlta()
        {
            FormularioAltaRepositorio repositorio = new FormularioAltaRepositorio();
            Resultado<List<CampoFormulario>> resultado = await repositorio.ConsultarPorVersion(Constantes.VERSION);

            if (resultado.mensajeError == string.Empty)
            {
                campoFromulario = resultado.valor;
                foreach (CampoFormulario campoFromulario in campoFromulario)
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
    }
}
