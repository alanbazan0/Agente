using AgenteApp.Clases;
using AgenteApp.Modelos;
using AgenteApp.Repositorios;
using AgenteApp.Presenters;
using AgenteApp.Vistas;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgenteApp.Presentadores
{

    class TipificacionPresentador
    {
        ITipificacionVista vista;
        public TipificacionPresentador(ITipificacionVista vista)
        {
            this.vista = vista;
        }

        public async void ConsultarConfiguracion()
        {
            //List<Filtro> filtros = vista.Filtros;
            TipificacionRepositorio repositorio = new TipificacionRepositorio();
            Resultado<List<Tipificacion>> resultado = await repositorio.ConsultarConfiguracion();
            if (resultado.mensajeError == string.Empty)
            {
                vista.tipificacion = resultado.valor;
            }
            else
                vista.MostrarMensajeAsync("Error", resultado.mensajeError);
        }
    }
}
