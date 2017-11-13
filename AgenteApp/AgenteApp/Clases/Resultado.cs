using System;
using System.Collections.Generic;
using System.Text;

namespace AgenteApp.Clases
{
    public class Resultado<T>
    {
        public string mensajeError { get; set; }
        public T valor { get; set; }

    }
}
