using AgenteApp.Componentes;
using AgenteApp.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgenteApp.Fabricas
{
    public interface IFormularioFabrica
    {
        IFormularioComponente CrearComponente(CampoFormulario campoFormulario);
    }
}
