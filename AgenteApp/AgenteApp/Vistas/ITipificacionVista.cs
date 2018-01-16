using System;
using AgenteApp.Modelos;
using System.Collections.Generic;
using System.Text;

namespace AgenteApp.Vistas
{
    interface ITipificacionVista
    {
        List<Tipificacion> tipificacion { set; }
        List<DatosAsistente> datosAsistente { set; }
        List<Parametros> Parametros { set; }
        Parametros Parametro { get; set; }
        string IDCRM { get; set; }

        List<Tipificacion> valoresDatosAsistente { set; }
        void MostrarMensajeAsync(string titulo, string mensaje);
    }
}
