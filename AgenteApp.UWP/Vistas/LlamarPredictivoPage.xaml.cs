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

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace NavigationMenuSample.Views
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class LlamarPredictivoPage : Page
    {
        public LlamarPredictivoPage()
        {
            this.InitializeComponent();
        }
        //funciones de transferencia
        private void digitarNumero(object sender, RoutedEventArgs e)
        {
            Button padnumeric = (Button)sender;
            switch (padnumeric.Name)
            {
                case "tbt7":
                    ttxtNumeroATranferir.Text = ttxtNumeroATranferir.Text + "7";
                    break;
                case "tbt8":
                    ttxtNumeroATranferir.Text = ttxtNumeroATranferir.Text + "8";
                    break;
                case "tbt9":
                    ttxtNumeroATranferir.Text = ttxtNumeroATranferir.Text + "9";
                    break;
                case "tbt4":
                    ttxtNumeroATranferir.Text = ttxtNumeroATranferir.Text + "4";
                    break;
                case "tbt5":
                    ttxtNumeroATranferir.Text = ttxtNumeroATranferir.Text + "5";
                    break;
                case "tbt6":
                    ttxtNumeroATranferir.Text = ttxtNumeroATranferir.Text + "6";
                    break;
                case "tbt3":
                    ttxtNumeroATranferir.Text = ttxtNumeroATranferir.Text + "3";
                    break;
                case "tbt2":
                    ttxtNumeroATranferir.Text = ttxtNumeroATranferir.Text + "2";
                    break;
                case "tbt1":
                    ttxtNumeroATranferir.Text = ttxtNumeroATranferir.Text + "1";
                    break;
                case "tbta":
                    ttxtNumeroATranferir.Text = ttxtNumeroATranferir.Text + "*";
                    break;
                case "tbt0":
                    ttxtNumeroATranferir.Text = ttxtNumeroATranferir.Text + "0";
                    break;
                case "tbtg":
                    ttxtNumeroATranferir.Text = ttxtNumeroATranferir.Text + "#";
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
    }
}
