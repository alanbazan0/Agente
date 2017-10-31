using AgenteApp.Clases;
using AgenteApp.Modelos;
using AgenteApp.Repositorios;
using AgenteApp.Vistas;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgenteApp.Presentadores
{
    public class AgentePresentador
    {
        IAgenteVista vista;

        public AgentePresentador(IAgenteVista vista)
        {
            this.vista = vista;
        }

        public async void ConsultarClientes()
        {
            var criteriosSeleccion = vista.CriteriosSeleccion;
            ClientesRepositorio repositorio = new ClientesRepositorio();
            Resultado<List<Cliente>> resuldato = await repositorio.Consultar(criteriosSeleccion);
            if(resuldato.mensajeError=="")
            {
                vista.Clientes = resuldato.valor;
            }
            //if (resuldato.mensajeError == string.Empty)
            //{
            //    //List<Cliente> clientes = resuldato.valor;
            //    vista.Clientes = resuldato.valor;
            //}
            //else
            //    vista.MostrarMensaje("Error",resuldato.MensajeError);
           
        }
    }
}
