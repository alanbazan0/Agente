using AgenteApp.Vistas;
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
using AgenteApp.Presentadores;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AgenteApp.UWP.Vistas
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MenuPage : Page, IMenuVista
    {
        MenuPresentador presentador;
        public MenuPage()
        {
            this.InitializeComponent();
            presentador = new MenuPresentador(this);
            Loaded += MenuPage_Loaded;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            Usuario usuario = (Usuario)e.Parameter;

        }

        private void MenuPage_Loaded(object sender, RoutedEventArgs e)
        {
            presentador.ConsultarOpcionesMenu();
        }

        public void AbrirOpcionMenu(OpcionMenu opcionMenu)
        {
            throw new NotImplementedException();
        }

        public void CrearMenu(List<OpcionMenu> opcionesMenu)
        {
           
        }
    }
}
