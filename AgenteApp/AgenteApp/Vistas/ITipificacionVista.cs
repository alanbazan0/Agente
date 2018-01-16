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

        List<Tipificacion> valoresDatosAsistente { set; }
        void MostrarMensajeAsync(string titulo, string mensaje);
    }
}
