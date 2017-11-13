using AgenteApp.Componentes;
using AgenteApp.Fabricas;
using AgenteApp.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgenteApp.UWP.Fabricas
{
    public class CriterioSeleccionFabrica : ICriterioSeleccionFabrica
    {
        public ICriterioSeleccionComponente CrearComponente(CriterioSeleccion criterioSeleccion)
        {
            ICriterioSeleccionComponente componente = null;
            TipoComponente tipoComponente = TipoComponente.Texto;
            switch (tipoComponente)
            {
                case TipoComponente.Texto:
                    CriterioSeleccionTextBox textBox = new CriterioSeleccionTextBox();
                    textBox.PlaceholderText = criterioSeleccion.titulo;
                    textBox.Width = 200;
                    textBox.Height = 30;
                    componente = textBox;                 
                    break;
                default:
                    break;
            }
            componente.CriterioSeleccion = criterioSeleccion;
            return componente;
        }
    }
}
