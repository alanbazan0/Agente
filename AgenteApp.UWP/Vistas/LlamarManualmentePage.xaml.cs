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
using System.Reflection;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace NavigationMenuSample.Views
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class LlamarManualmentePage : Page
    {
        public LlamarManualmentePage()
        {
            this.InitializeComponent();
        }


        //funciones de transferencia
        private void digitarNumero(object sender, RoutedEventArgs e)
        {
            Button padnumeric = (Button)sender;
            switch (padnumeric.Name)
            {
                case "tbt7Out":
                    ttxtNumeroATranferirOut.Text = ttxtNumeroATranferirOut.Text + "7";
                    break;
                case "tbt8Out":
                    ttxtNumeroATranferirOut.Text = ttxtNumeroATranferirOut.Text + "8";
                    break;
                case "tbt9Out":
                    ttxtNumeroATranferirOut.Text = ttxtNumeroATranferirOut.Text + "9";
                    break;
                case "tbt4Out":
                    ttxtNumeroATranferirOut.Text = ttxtNumeroATranferirOut.Text + "4";
                    break;
                case "tbt5Out":
                    ttxtNumeroATranferirOut.Text = ttxtNumeroATranferirOut.Text + "5";
                    break;
                case "tbt6Out":
                    ttxtNumeroATranferirOut.Text = ttxtNumeroATranferirOut.Text + "6";
                    break;
                case "tbt3":
                    ttxtNumeroATranferirOut.Text = ttxtNumeroATranferirOut.Text + "3";
                    break;
                case "tbt2Out":
                    ttxtNumeroATranferirOut.Text = ttxtNumeroATranferirOut.Text + "2";
                    break;
                case "tbt1Out":
                    ttxtNumeroATranferirOut.Text = ttxtNumeroATranferirOut.Text + "1";
                    break;
                case "tbtaOut":
                    ttxtNumeroATranferirOut.Text = ttxtNumeroATranferirOut.Text + "*";
                    break;
                case "tbt0":
                    ttxtNumeroATranferirOut.Text = ttxtNumeroATranferirOut.Text + "0";
                    break;
                case "tbtg":
                    ttxtNumeroATranferirOut.Text = ttxtNumeroATranferirOut.Text + "#";
                    break;
                case "cbt7":
                    txtNotelefonico.Text = txtNotelefonico.Text + "7";
                    break;
                case "cbt8":
                    txtNotelefonico.Text = txtNotelefonico.Text + "8";
                    break;
                case "cbt9":
                    txtNotelefonico.Text = txtNotelefonico.Text + "9";
                    break;
                case "cbt4":
                    txtNotelefonico.Text = txtNotelefonico.Text + "4";
                    break;
                case "cbt5":
                    txtNotelefonico.Text = txtNotelefonico.Text + "5";
                    break;
                case "cbt6":
                    txtNotelefonico.Text = txtNotelefonico.Text + "6";
                    break;
                case "cbt3":
                    txtNotelefonico.Text = txtNotelefonico.Text + "3";
                    break;
                case "cbt2":
                    txtNotelefonico.Text = txtNotelefonico.Text + "2";
                    break;
                case "cbt1":
                    txtNotelefonico.Text = txtNotelefonico.Text + "1";
                    break;
                case "cbta":
                    txtNotelefonico.Text = txtNotelefonico.Text + "*";
                    break;
                case "cbt0":
                    txtNotelefonico.Text = txtNotelefonico.Text + "0";
                    break;
                case "cbtg":
                    txtNotelefonico.Text = txtNotelefonico.Text + "#";
                    break;
            }
        }

        private void supervisoresListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var supervisor = e.ClickedItem;

            if (supervisor != null)
            {
                var contenido = (string)supervisor.GetType().GetProperty("ExtensionSupervisor").GetValue(supervisor, null);
                ttxtNumeroATranferirOut.Text = contenido;
            }

        }
    }
}
