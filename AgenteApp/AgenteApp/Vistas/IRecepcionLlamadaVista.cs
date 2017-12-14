using AgenteApp.Modelos;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using Linphone;

namespace AgenteApp.Vistas
{
    public interface IRecepcionLlamadaVista
    {
        void ConsultarClientesTel(string numero);
        void MostrarMensajeAsync(string titulo, string mensaje);
        List<Objeto> Clientes { set; }
        List<Portabilidad> Portabilidad { set; }
        void ConsultarPortabilidad(string numero);
        string setIdLlamada { set; }

    }
}
