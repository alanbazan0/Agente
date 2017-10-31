using AgenteApp.Modelos;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgenteApp.Vistas
{
    public interface IAgenteVista
    {
        void ConsultarClientes();
        void MostrarMensaje(string titulo, string mensaje);
        object CriteriosSeleccion { get;  }
        List<Cliente> Clientes { set; }
    }
}
