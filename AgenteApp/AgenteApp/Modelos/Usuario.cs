using System;
using System.Collections.Generic;
using System.Text;

namespace AgenteApp.Modelos
{
    public class Usuario
    {
        private string id;
        private string nombre;
        private string extension;
        private string puesto;
        private string tipoInicio;
        public string Id { get { return id; } set { id = value; } }
        public string Nombre { get { return nombre; } set { nombre = value; } }
        public string Extension { get { return extension; } set { extension = value; } }
        public string Puesto { get { return puesto; } set { puesto = value; } }
        public string TipoInicio { get { return tipoInicio; } set { tipoInicio = value; } }
    }
}
