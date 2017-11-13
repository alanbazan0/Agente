using AgenteApp.Componentes;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Controls;
using AgenteApp.Modelos;

namespace AgenteApp.Componentes
{
    public class CriterioSeleccionTextBox : TextBox, ICriterioSeleccionComponente
    {
        protected CriterioSeleccion criterioSeleccion;
        public Filtro Filtro
        {
            get => new Filtro()
            {
                tablaId = criterioSeleccion.tablaId,
                campoId = criterioSeleccion.campoId,
                tipoDato = criterioSeleccion.tipoDato,
                valor = Text
            };
        }
        public CriterioSeleccion CriterioSeleccion { get { return criterioSeleccion; } set { criterioSeleccion=value; } }
    }
}
