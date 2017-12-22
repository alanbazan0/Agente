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
        List<ClienteTelefono> Clientes { set; }
        void ConsultarPortabilidad(string numero);
        List<Portabilidad> Portabilidad { set; }
        void ConsultarIdLlamada(string extension);
        string setIdLlamada { set; }
        string ultimoIdPausa { set; }
        void InsertarPausa();
        void ActualizarPausa();
        void ConsultarPausas();
        Pausas Pausa { get; set; }
        Parametros Parametro { get; set; }
        List<Pausas> Pausas { set; }
        
        void MostrarMensajeAsync(string titulo, string mensaje);

    }
}
