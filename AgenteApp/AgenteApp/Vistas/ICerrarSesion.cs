using AgenteApp.Modelos;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgenteApp.Vistas
{
    public interface ICerrarSesion
    {
        void CerrarSesion();
        string NombreUsuario { get; set; }
        string IP { get; set; }
        string IdHardware { get; set; }
        void cerrarPRograma();
    }
}
