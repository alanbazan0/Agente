using AgenteApp.Componentes;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using AgenteApp.Modelos;

namespace AgenteApp.Componentes
{
    public class TextoComponente : Grid, IComponente
    {
        protected Componente criterioSeleccion;
        protected TextBox textBox;
        protected TextBlock textBlock;

        public TextoComponente()
        {
            textBox = new TextBox();
            textBlock = new TextBlock();
            
            this.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            this.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(150) });
            this.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(150) });
            textBlock.SetValue(Grid.ColumnProperty, 0);
            //textBlock.TextWrapping = TextWrapping.WrapWholeWords;
            textBox.SetValue(Grid.ColumnProperty, 1);
            textBlock.Margin = new Thickness(8);
            textBox.Margin = new Thickness(8);
            //this.Margin = new Thickness(8);
            this.Children.Add(textBlock);
            this.Children.Add(textBox);
        }

        public Campo Filtro
        {
            get => new Campo()
            {
                tablaId = criterioSeleccion.tablaId,
                campoId = criterioSeleccion.campoId,
                tipoDato = criterioSeleccion.tipoDato,
                valor = textBox.Text
            };
        }
        public Componente Componente { get { return criterioSeleccion; } set { criterioSeleccion=value; } }

        public string Titulo
        {
            set
            {
                textBlock.Text = value;
                //textBox.PlaceholderText = value;
            }
        }

        public string Valor { set { textBox.Text = value; }
            get { return textBox.Text; }
        }
    }
}
