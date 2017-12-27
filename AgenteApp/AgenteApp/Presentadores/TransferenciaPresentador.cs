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

        public TransferenciaPresentador(IParametrosVista vista)
        {
            this.vista = vista;
        }

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

        public async void ConsultarPortabilidad(string numero)
        {
            //List<Filtro> filtros = vista.Filtros;
            ClientesPortabilidadRepositorio repositorio = new ClientesPortabilidadRepositorio();
            Resultado<List<Portabilidad>> resultado = await repositorio.Consultar(numero);
            if (resultado.mensajeError == string.Empty)
            {
                vista.Portabilidad = resultado.valor;
            }
            else
                vista.MostrarMensajeAsync("Error", resultado.mensajeError);
        }
    }
}
