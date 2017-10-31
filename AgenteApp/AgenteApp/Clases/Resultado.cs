using System;
using System.Collections.Generic;
using System.Text;

namespace AgenteApp.Clases
{
    public class Resultado<T>
    {
        public string mensajeError { get; set; }
        public T valor { get; set; }

        //public string MensajeError { get { return mensajeError; } }
        //public T Valor { get { return valor; } }
    }
}
