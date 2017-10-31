using AgenteApp.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgenteApp.Clases
{
    public class ResultadoCliente
    {
        public string mensajeError { get; set; }
        public List<Cliente> valor { get; set; }

        //public string MensajeError { get { return mensajeError; } }
        //public IEnumerable<Cliente> Valor { get { return valor; } }
    }
}
