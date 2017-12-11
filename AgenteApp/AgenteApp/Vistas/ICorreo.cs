using AgenteApp.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgenteApp.Vistas
{
    public interface ICorreo
    {
        List<Correos> correos { set; }
        void consultarCorreoEntrada();
        void consultarCorreoEntradaDia();
        void consultarCorreoEntradaMes();
        void consultarCorreoEntradaSemana();
    }
}
