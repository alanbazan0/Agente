using AgenteApp.Componentes;
using AgenteApp.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgenteApp.Fabricas
{
    public interface IComponenteFabrica
    {
        IComponente CrearComponente(Componente criterioSeleccion,int tamanoTitulo); 
    }
}
