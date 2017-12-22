using System;
using System.Collections.Generic;
using System.Text;

namespace AgenteApp.Modelos
{
    public class Parametros
    {
        string idParamtro;
        string direccionIp;
        string numeroMaquina;
        string descripcionValor;
        string palabraReservada;
        string valorParametro;
        public string IdParamtro { get { return idParamtro; } set { idParamtro = value; } }
        public string DireccionIp { get { return direccionIp; } set { direccionIp = value; } }
        public string NumeroMaquina { get { return numeroMaquina; } set { numeroMaquina = value; } }
        public string DescripcionValor { get { return descripcionValor; } set { descripcionValor = value; } }
        public string PalabraReservada { get { return palabraReservada; } set { palabraReservada = value; } }
        public string ValorParametro { get { return valorParametro; } set { valorParametro = value; } }
    }
}
