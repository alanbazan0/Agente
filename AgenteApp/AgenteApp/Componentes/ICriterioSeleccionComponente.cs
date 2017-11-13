using AgenteApp.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgenteApp.Componentes
{
    public interface ICriterioSeleccionComponente
    {
        CriterioSeleccion CriterioSeleccion { get; set; }
        Filtro Filtro { get; }
    }
}
