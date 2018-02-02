using AgenteApp.Modelos;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgenteApp.Vistas
{
    public interface IClientesVista
    {
        string IP { get; set; }
        string IdHardware { get; set; }
        void ConsultarClientes();
        void ConsultarParametros();
        void Consultar(string numeroTel);
        void AgragarValoraFiltro(string valor);
        void ConsultarClientesTel(string numero);
        void MostrarMensajeAsync(string titulo, string mensaje);
        List<Campo> Filtros { get;  }
        List<Objeto> Clientes { set; }
        List<Objeto> ClientesCriterio { set; }
        List<Portabilidad> Portabilidad {  set; }
        void CrearCriterioSeleccion(Componente criterioSeleccion,int tamano);
        void CrearColumnasGrid1(List<CampoGrid> campos);
        void CrearColumnasGrid2(List<CampoGrid> campos);
        void ConsultarPortabilidad(string numero);
        string setIdLlamada { set; }
    }
}
