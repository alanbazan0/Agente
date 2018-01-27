using AgenteApp.Modelos;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgenteApp.Vistas
{
    public interface IAgenteVista
    {
        void ConsultarClientes();
        void ConsultarClientesTel(string numero);
        void MostrarMensajeAsync(string titulo, string mensaje);
        List<Campo> Filtros { get;  }
        List<Objeto> Clientes { set; }
        List<Portabilidad> Portabilidad {  set; }
        void CrearCriterioSeleccion(Componente criterioSeleccion,int tamanoTitulo);
        void CrearColumnasGrid1(List<CampoGrid> campos);
        void ConsultarPortabilidad(string numero);
        string setIdLlamada { set; }
    }
}
