using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Media.Imaging;

namespace AgenteApp.Modelos
{
    public class Usuario
    {
        private string id;
        private string nombre;
        private string extension;
        private string puesto;
        private string tipoInicio;
        private string extensionOutbound;
        private BitmapImage image;
        public string Id { get { return id; } set { id = value; } }
        public string Nombre { get { return nombre; } set { nombre = value; } }
        public string Extension { get { return extension; } set { extension = value; } }
        public string Puesto { get { return puesto; } set { puesto = value; } }
        public string TipoInicio { get { return tipoInicio; } set { tipoInicio = value; } }
        public string ExtensionOutbound { get { return extensionOutbound; } set { extensionOutbound = value; } }
        public BitmapImage Image { get { return image; } set { image = value; } }
    }
}
