using AgenteApp.Componentes;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Controls;
using AgenteApp.Modelos;

namespace AgenteApp.Componentes
{
    public class FormularioTextBox :TextBox, IFormularioComponente
    {
        protected CampoFormulario campoFormulario;
        public Campo Campo
        {
            get => new Campo()
            {
                tablaId = campoFormulario.tablaId,
                campoId = campoFormulario.campoId,
                tipoDato = campoFormulario.tipoDato,
                valor = Text
            };
        }
        public CampoFormulario CampoFormulario { get { return campoFormulario; } set { campoFormulario = value; } }
    }
}
