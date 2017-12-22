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
        string horaInicia;
        string finalpausa;
        string horaFinal;
        string duracion;
        string acumulado;
        public string IdPausa { get { return idPausa; } set { idPausa = value; } }
        public string IdLlamada { get { return idLlamada; } set { idLlamada = value; } }
        public string Extencion { get { return extencion; } set { extencion = value; } }
        public string Telefono { get { return telefono; } set { telefono = value; } }
        public string IdAgente { get { return idAgente; } set { idAgente = value; } }
        public string InicioPausa { get { return inicioPausa; } set { inicioPausa = value; } }
        public string HoraInicia { get { return horaInicia; } set { horaInicia = value; } }
        public string Finalpausa { get { return finalpausa; } set { finalpausa = value; } }
        public string HoraFinal { get { return horaFinal; } set { horaFinal = value; } }
        public string Duracion { get { return duracion; } set { duracion = value; } }
        public string Acumulado { get { return acumulado; } set { acumulado = value; } }
    }
}
