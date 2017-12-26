using System;
using System.Text;
using AgenteApp.Vistas;
using AgenteApp.Clases;
using AgenteApp.Modelos;
using AgenteApp.Repositorios;
using System.Collections.Generic;


namespace AgenteApp.Presentadores
{
    class TransferenciaPresentador
    {
        IParametrosVista vista;
        public async void ConsultarParametros()
        {
            ParametrosRepositorio repositorio = new ParametrosRepositorio();
            Resultado<List<Parametros>> resultado = await repositorio.Consultar(vista.Parametro);
            if (resultado.mensajeError == string.Empty)
            {
                vista.Parametros = resultado.valor;
            }
            else
            {
                vista.MostrarMensajeAsync("Error", resultado.mensajeError);
            }
        }
    }
}
