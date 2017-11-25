using System;
using System.Collections.Generic;
using System.Text;

namespace AgenteApp.Modelos
{
    public class TipoTelefono
    {
        private string id;
        private string descripcion;
        
        public string Id { get { return id; } set { id = value; } }
        public string Descripcion { get { return descripcion; } set { descripcion = value; } }
    }
}
