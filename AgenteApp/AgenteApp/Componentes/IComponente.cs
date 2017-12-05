using AgenteApp.Modelos;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgenteApp.Componentes
{
    public interface IComponente 
    {
        Componente Componente { get; set; }
        string Titulo { set; }
        Campo Filtro { get; }
        string Valor { set; }
    }
}
