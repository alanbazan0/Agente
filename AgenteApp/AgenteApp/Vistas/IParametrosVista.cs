using System;
using System.Text;
using AgenteApp.Modelos;
using System.Collections.Generic;


namespace AgenteApp.Vistas
{
    interface IParametrosVista
    {
        Parametros Parametro { get; set; }
        List<Parametros> Parametros { set; }
        List<Portabilidad> Portabilidad { set; }
        void MostrarMensajeAsync(string titulo, string mensaje);
    }
}
