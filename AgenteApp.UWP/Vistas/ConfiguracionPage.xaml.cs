using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using AgenteApp.Modelos;
using Xamarin.Forms.Internals;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace AgenteApp.UWP.Vistas
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class ConfiguracionPage : Page
    {
        List<Configuracion> conf = new List<Configuracion>();
        public ConfiguracionPage()
        {
            lista();
            this.InitializeComponent();
            list.ItemsSource = conf;
        }
        public void lista()
        {

            conf.Add(
                     new Configuracion()
                     {
                         Versión = "",
                         Secuencia = conf.Count(),
                         Descripcion = "",
                         Campo = "",
                         Asistente = "",
                         Tipo = "",
                         Tabla = "",
                         Campoid = "",
                         Campos = "",
                         Valores = ""
                     }
                     );

        }
        private void Window_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            string l = (string)sender.GetType().GetProperty("SelectedItem").GetValue(sender, null);
            if (e.Key == Windows.System.VirtualKey.Tab)
            {

                conf.Add(
                    new Configuracion()
                    {
                        Versión = "",
                        Secuencia = conf.Count() + 1,
                        Descripcion = "",
                        Campo = "",
                        Asistente = "",
                        Tipo = "",
                        Tabla = "",
                        Campoid = "",
                        Campos = "",
                        Valores = ""
                    }
                    );
                list.ItemsSource = null;
                // lista();
                list.ItemsSource = conf;
            }
        }
        private void datosListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            /* var k = list.GetType();
             //   var l = k.GetType().HasElementType.GetType().GetProperty("gridComponentes").GetValue(k,null);
               e.ClickedItem.ToString().ToList();
               list.ToString().ToList();
               e.ToString();
               sender.ToString().ToList();
             var r = Componentes.GetType();
             var ñ = Componentes.Children.GetType().HasElementType.GetType().GetProperty("list").GetValue(r,null);
             // var p = Componentes.Children.GetType().HasElementType.GetType().GetProperty("list").GetValue(Componentes, null);
          //   var u = list.GetType().GetProperty("gridComponentes");
             var componente = Componentes.Children.
             

   */
        }
    }
}
