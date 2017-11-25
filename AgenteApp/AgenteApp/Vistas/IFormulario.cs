using AgenteApp.Modelos;
using Newtonsoft.Json.Linq;

using System;
using System.Collections.Generic;
using System.Text;

namespace AgenteApp.Vistas
{
    public interface IFormulario
    {
        void MostrarMensaje(string titulo, string mensaje);
        List<Campo> Campos { get; }
        List<Objeto> Clientes { set; }
        List<TipoTelefono> TipoTelefono { set; }
        void CrearFormularioClientes(CampoFormulario campoFormulario);
        string idCliente { set;  }
    }
}
