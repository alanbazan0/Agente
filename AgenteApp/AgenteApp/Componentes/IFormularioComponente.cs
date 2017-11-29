using AgenteApp.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgenteApp.Componentes
{
    public interface IFormularioComponente
    {
        CampoFormulario CampoFormulario { get; set; }
        Campo Campo { get; }
        string Valor { get; set; }
    }
}
