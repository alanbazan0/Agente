using System;
using System.Collections.Generic;
using System.Text;

namespace AgenteApp.Modelos
{
    public class Pausas
    {
        string idPausa;
        string idLlamada;
        string extencion;
        string telefono;
        string idAgente;
        string inicioPausa;
        string finalpausa;
        string duracion;
        public string IdPausa { get { return idPausa; } set { idPausa = value; } }
        public string IdLlamada { get { return idLlamada; } set { idLlamada = value; } }
        public string Extencion { get { return extencion; } set { extencion = value; } }
        public string Telefono { get { return telefono; } set { telefono = value; } }
        public string IdAgente { get { return idAgente; } set { idAgente = value; } }
        public string InicioPausa { get { return inicioPausa; } set { inicioPausa = value; } }
        public string Finalpausa { get { return finalpausa; } set { finalpausa = value; } }
        public string Duracion { get { return duracion; } set { duracion = value; } }
    }
}
