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
        List<ClienteTelefono> ClienteTelefono { set; }
        List<Correos> ClienteCorreos { set; }
        void CrearFormularioClientes(Componente componente);
        string idCliente { set;  }
        void ActualizarTelefonoCliente(string idCliente);
        void ConsultarPortabilidad(string compania,string municipio,string consecutivo);
        void AsignarValor(Componente componente, Objeto registro);
    }
}
