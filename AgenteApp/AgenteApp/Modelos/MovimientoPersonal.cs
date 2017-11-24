using System;
using System.Collections.Generic;
using System.Text;

namespace AgenteApp.Modelos
{
    public class MovimientoPersonal
    {

        string usuario, idReceso, fechaInicial, horaInicial, fechaFinal, horaFin;
        string duracion, duracionSeg, descTipoReceso;
        string tiempoAcomulado, orden;
        public string Usuario { get { return usuario; } set { usuario = value; } }
        public string IdReceso { get { return idReceso; } set { idReceso = value; } }
        public string FechaInicial { get { return fechaInicial; } set { fechaInicial = value; } }
        public string HoraInicial { get { return horaInicial; } set { horaInicial = value; } }
        public string FechaFinal { get { return fechaFinal; } set { fechaFinal = value; } }
        public string HoraFin { get { return horaFin; } set { horaFin = value; } }
        public string Duracion { get { return duracion; } set { duracion = value; } }
        public string DuracionSeg { get { return duracionSeg; } set { duracionSeg = value; } }
        public string DescTipoReceso { get { return descTipoReceso; } set { descTipoReceso = value; } }
        public string TiempoAcomulado { get { return tiempoAcomulado; } set { tiempoAcomulado = value; } }
        public string Orden { get { return orden; } set { orden = value; } }
        
    }
}
