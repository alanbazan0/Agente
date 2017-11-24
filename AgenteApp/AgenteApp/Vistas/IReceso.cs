using System;
using System.Collections.Generic;
using System.Text;
using AgenteApp.Modelos;

namespace AgenteApp.Vistas
{
    public interface IReceso
    {
        void ConsultaTipoRecesos();
        void ConsultaMovimientos();
        void ConsultarStatus();
        void actualizarEstatus();
        void solictarReceso();
        void actualizarMovimientos();
        void actualizarEstatusResult(String valor, String estatus);

        List<TipoRecesos> TipoRecesos { set; }
        List<MovimientoPersonal> MovimientosPersonales { set; }
        List<MovimientoPersonal> MovimientoPersonalGuarda { set; }
        List<TipoRecesos> ParametrosIns { get; }
        List<EstatusUsuario> estatusUsuario { set; }

        void MostrarMensajeAsync(string mensaje);
        string NombreUsuario { get; set; }
        string idTipoSolicitud { get; set; }
        string DescTipoSolicitud { get; set; }
        string EstatusSolicitud { get; set; }
        string LlamadaId { get; set; }

        string  idSolicutdGuardada { get; set; }
        string fechaInicialGuardada { get; set; }
        string horaInicialGuardada { get; set; }
        string duracion { get; set; }
        string duracionSeg { get; set; }
        void autorizar();
    }
}
