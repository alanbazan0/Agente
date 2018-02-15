using AgenteApp.Clases;
using AgenteApp.Modelos;
using AgenteApp.Repositorios;
using AgenteApp.Vistas;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgenteApp.Presentadores
{
    class LlamarPredictivoPresentador
    {
        ILlamarPredictivoVista vista;
        public LlamarPredictivoPresentador(ILlamarPredictivoVista vista)
        {
            this.vista = vista;
        }

        public async void ConsultarClientesTel(string numero)
        {
            //List<Filtro> filtros = vista.Filtros;
            ClientesTelefonoRepositorio repositorio = new ClientesTelefonoRepositorio();
            Resultado<List<ClienteTelefono>> resultado = await repositorio.ConsultarNumTel(numero);
            if (resultado.mensajeError == string.Empty)
            {
                vista.Clientes = resultado.valor;
            }
            else
                vista.MostrarMensajeAsync("Error", resultado.mensajeError);
        }

        public async void ConsultarSupervisores(string agenteid)
        {
            PausaRepositorio repositorio = new PausaRepositorio();
            Resultado<List<Supervisores>> resultado = await repositorio.ConsultarSupervisores(agenteid);
            if (resultado.mensajeError == string.Empty)
            {
                vista.Supervisores = resultado.valor;
            }
            else
                vista.MostrarMensajeAsync("Error", resultado.mensajeError);
        }
    }
}
