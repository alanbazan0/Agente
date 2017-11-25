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
    public class FormularioFabrica : IFormularioFabrica
    {
        public IFormularioComponente CrearComponente(CampoFormulario campoFormulario)
        {
            IFormularioComponente componente = null;
            TipoComponente tipoComponente = TipoComponente.Texto;
            switch (tipoComponente)
            {
                case TipoComponente.Texto:
                    FormularioTextBox textBox = new FormularioTextBox();
                    textBox.PlaceholderText = campoFormulario.titulo;
                    textBox.Width = 200;
                    textBox.Height = 30;
                    componente = textBox;
                    break;
                default:
                    break;
            }
            componente.CampoFormulario = campoFormulario;
            
            return componente;
        }
    }
}
