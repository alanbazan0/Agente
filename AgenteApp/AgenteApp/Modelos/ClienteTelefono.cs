using System;
using System.Collections.Generic;
using System.Text;

namespace AgenteApp.Modelos
{
    public class ClienteTelefono
    {
        string id;
        //public string consecutivo { get; set; }
        string nir;
         string serie;
         string telefonoCliente;
         string numeracion;
         string compania;
         string tipoTelefono;

        public string Id { get { return id; } set {id= value; } }
        //public string consecutivo { get {; } set {= value; } }
        public string Nir { get { return nir; } set { nir= value; } }
        public string Serie { get { return serie; } set { serie= value; } }
        public string TelefonoCliente { get {return telefonoCliente; } set {telefonoCliente= value; } }
        public string Numeracion { get {return numeracion; } set { numeracion= value; } }
        public string Compania { get {return compania; } set { compania = value; } }
        public string TipoTelefono { get {return tipoTelefono; } set { tipoTelefono = value; } }
    }
}
