using AgenteApp.Clases;
using AgenteApp.Modelos;
using AgenteApp.Repositorios;
using AgenteApp.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AgenteApp.Presentadores
{
    public class RecesoPresentador
    {
        private IReceso vista;
        public RecesoPresentador(IReceso vista)
        {
            this.vista = vista;
        }

        public async void ConsultaTipoRecesos()
        {
            RecesoRepositorio accesoDatos = new RecesoRepositorio();

            Resultado<List<TipoRecesos>> resultado = await accesoDatos.Consultar();
            if (resultado.mensajeError == string.Empty)
            {
                vista.TipoRecesos = resultado.valor;
            }
            else
                vista.MostrarMensajeAsync(resultado.mensajeError);
        }

        public async void solictarReceso()
        {
            RecesoRepositorio accesoDatos = new RecesoRepositorio();
            Resultado<String> resultado = await accesoDatos.SolictarReceso(vista.NombreUsuario, vista.idTipoSolicitud, vista.DescTipoSolicitud, vista.EstatusSolicitud, vista.LlamadaId);
            if (resultado.mensajeError == string.Empty)
            {
                vista.MostrarMensajeAsync(resultado.valor);
            }
               else
                 vista.MostrarMensajeAsync(resultado.mensajeError);
        }

        public async void autorizar()
        {
            RecesoRepositorio accesoDatos = new RecesoRepositorio();
            Resultado<String> resultado = await accesoDatos.autorizar(vista.NombreUsuario);
            if (resultado.mensajeError == string.Empty)
            {
                // vista.MostrarMensajeAsync(resultado.valor);
            }
            else { }
                //vista.MostrarMensajeAsync(resultado.mensajeError);
        }
        public async void ConsultaMovimientos()
        {
            RecesoRepositorio accesoDatos = new RecesoRepositorio();
            Resultado<List<MovimientoPersonal>> resultado = await accesoDatos.ConsultaMovimientos(vista.NombreUsuario);
            if (resultado.mensajeError == string.Empty)
            {
                vista.MovimientosPersonales = resultado.valor;
            }
            else
                vista.MostrarMensajeAsync(resultado.mensajeError);
        }
        public async void ConsultarStatus()
        {
            RecesoRepositorio accesoDatos = new RecesoRepositorio();
            Resultado<List<EstatusUsuario>> resultado = await accesoDatos.ConsultarStatus(vista.NombreUsuario);
            if (resultado.mensajeError == string.Empty)
            {
                vista.estatusUsuario = resultado.valor;
            }
            else
                vista.MostrarMensajeAsync(resultado.mensajeError);
        }
        public async void actualizarEstatus()
        {
            RecesoRepositorio accesoDatos = new RecesoRepositorio();
            Resultado<String> resultado = await accesoDatos.actualizarEstatus(vista.NombreUsuario,vista.EstatusSolicitud);
            if (resultado.mensajeError == string.Empty)
            {
                vista.actualizarEstatusResult(resultado.valor, vista.EstatusSolicitud);
            }
            else { }
            //vista.MostrarMensajeAsync(resultado.mensajeError);
        }
        public async void actualizarMovimientos()
        {
            RecesoRepositorio accesoDatos = new RecesoRepositorio();
            Resultado<String> resultado = await accesoDatos.actualizarMovimientos(vista.NombreUsuario,vista.fechaInicialGuardada,vista.horaInicialGuardada,vista.duracion,vista.duracionSeg);
            if (resultado.mensajeError == string.Empty)
            {                
                //vista.actualizarEstatusResult(resultado.valor, vista.EstatusSolicitud);
            }
            else { }
            //vista.MostrarMensajeAsync(resultado.mensajeError);
        }
        public async void insertarMovimiento()
        {
            RecesoRepositorio accesoDatos = new RecesoRepositorio();
            Resultado<List<MovimientoPersonal>> resultado = await accesoDatos.insertarMovimiento(vista.NombreUsuario,vista.idSolicutdGuardada,vista.DescTipoSolicitud);
            if (resultado.mensajeError == string.Empty)
            {
                vista.MovimientoPersonalGuarda = resultado.valor;
            }
            else
            { }
                //vista.MostrarMensajeAsync(resultado.mensajeError);
        }
    }
}