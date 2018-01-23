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
    public class ComponenteFabrica : IComponenteFabrica
    {
        public IComponente CrearComponente(Componente componente)
        {
         IComponente componenteVista = null;
            TipoComponente tipoComponente = TipoComponente.Texto;
            switch (tipoComponente)
            {
                case TipoComponente.Texto:
                    TextoComponente textBox;
                    if (componente.campoId.Equals("BTCLIENTENCOMPLETO"))
                        textBox = new TextoComponente(275);
                    else
                        textBox = new TextoComponente();
                    textBox.Titulo = componente.titulo;
                   // textBox.Width = 600;
                    //textBox.Height = 40;
                    componenteVista = textBox;
                    break;
                default:
                    break;
            }
            componenteVista.Componente = componente;
            return componenteVista;
        }

       
    }
}
