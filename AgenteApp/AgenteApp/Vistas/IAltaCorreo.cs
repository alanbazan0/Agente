using System;
using System.Collections.Generic;
using System.Text;

namespace AgenteApp.Vistas
{
    public interface IAltaCorreo
    {
        string inserto { set; }
        void insertarAltaClienteCorreo();
    }
}
