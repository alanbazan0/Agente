using AgenteApp.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgenteApp.Vistas
{
    public interface ICorreo
    {
        List<Correos> correos { set; }
        string total { set; }
        string dia { set; }
        string semana { set; }
        string mes { set; }


        void consultarCorreoEntrada();
        void consultarCorreoEntradaDia();
        void consultarCorreoEntradaMes();
        void consultarCorreoEntradaSemana();

        void consultarCorreoEntradaInfo();
        void consultarCorreoEntradaDiaInfo();
        void consultarCorreoEntradaMesInfo();
        void consultarCorreoEntradaSemanaInfo();
    }
}
