using System;
namespace AgenteApp.Models
{
    public class HTTPParametros
    {
        public HTTPParametros(string nombre, string valor)
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
