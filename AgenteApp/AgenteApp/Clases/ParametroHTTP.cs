using System;
namespace AgenteApp.Clases
{
    public class ParametroHTTP
    {
        public ParametroHTTP(string nombre, string valor)
        {
            Nombre = nombre;
            Valor = valor;

        }
        public string Nombre
        {
            get;
            set;
        }
        public string Valor
        {
            get;
            set;
        }
    }
}
