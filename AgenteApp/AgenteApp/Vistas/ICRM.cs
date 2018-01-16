using AgenteApp.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgenteApp.Vistas
{
    public interface ICRM
    {
        List<Parametros> parametrosUsuario { set; }
        List<Objeto> datosCRM { set; }
        string Ip { get; set; }
        string Idhardware { get; set; }        
        void MostrarMensaje(string mensaje);
        void consultarParametros();
        void cosultarCRM();
        void datosIndicadores(List<CRM> indicadoresCRM);
        void CrearColumnasGrid1CRM(List<CampoGrid> campos);
        void CrearFormularioClientes(Componente componente);
        void AsignarValor(Componente componente, Objeto registro);

    }
}
