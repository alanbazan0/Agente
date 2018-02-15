using AgenteApp.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgenteApp.Vistas
{
    public interface ILlamarPredictivoVista
    {
        List<ClienteTelefono> Clientes { set; }
        List<Supervisores> Supervisores { set; }
        void MostrarMensajeAsync(string titulo, string mensaje);
    }
}
