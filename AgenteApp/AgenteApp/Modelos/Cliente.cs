using System;
using System.Collections.Generic;
using System.Text;

namespace AgenteApp.Modelos
{
    public class Cliente
    {
        public string id { get; set; }
        public string primerNombre { get; set; }
        public string segundoNombre { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public string rfc { get; set; }
        public string nss { get; set; }
        public string curp { get; set; }
        public string codigoPostal { get; set; }
        public string numeroExterior { get; set; }
        public string numeroInterior { get; set; }
        public string calle { get; set; }
        public string colonia { get; set; }
        public string estado { get; set; }
        public string pais { get; set; }
        public string correoElectronico { get; set; }

    }
}
