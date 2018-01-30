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

namespace AgenteApp.UWP.Vistas
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class ReconocimientoVozPage : Page
    {
        public ReconocimientoVozPage()
        {
            this.InitializeComponent();
        }

        public void escuchar(object sender, RoutedEventArgs e)
        {
            ////Inicia la escucha con el dispositivo de entrada de audio predeterminado 
            //reconocimiento_de_voz.SetInputToDefaultAudioDevice(); // Usaremos el microfono predeterminado del sistema 
            //reconocimiento_de_voz.LoadGrammar(new DictationGrammar()); //Carga la gramatica de Windows 
            //reconocimiento_de_voz.SpeechRecognized += te_escucho; // Controlador de eventos. Se ejecutara al reconocer 
            //reconocimiento_de_voz.RecognizeAsync(RecognizeMode.Multiple); //Iniciamos reconocimiento 
            //label1.Text = "Te estoy escuchando cuentame: ";
        }
    }
}
