using AgenteApp.Clases;
using AgenteApp.Modelos;
using AgenteApp.Repositorios;
using AgenteApp.Vistas;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgenteApp.Presentadores
{
    class CorreoPresentador
    {
        ICorreo  vista;
        List<CampoGrid> campos;
        List<Componente> criteriosSeleccion;

        public CorreoPresentador(ICorreo vista)
        {
            this.vista = vista;
        }

        public async void consultarCorreoEntrada(string nombre)
        {
            CorreoRepositorio repositorio = new CorreoRepositorio();
            Resultado<List<Correos>> resultado = await repositorio.consultarCorreoEntrada(nombre);
            if (resultado.mensajeError == string.Empty)
            {
                vista.correos = resultado.valor;
            }
            else
            { } //vista.MostrarMensaje("Error", resultado.mensajeError);
        }
        public async void consultarCorreoEntradaDia(string nombre)
        {
            CorreoRepositorio repositorio = new CorreoRepositorio();
            Resultado<List<Correos>> resultado = await repositorio.consultarCorreoEntradaDia(nombre);
            if (resultado.mensajeError == string.Empty)
            {
                vista.correos = resultado.valor;
            }
            else
            { } //vista.MostrarMensaje("Error", resultado.mensajeError);
        }
        public async void consultarCorreoEntradaMes(string nombre)
        {
            CorreoRepositorio repositorio = new CorreoRepositorio();
            Resultado<List<Correos>> resultado = await repositorio.consultarCorreoEntradaMes(nombre);
            if (resultado.mensajeError == string.Empty)
            {
                vista.correos = resultado.valor;
            }
            else
            { } //vista.MostrarMensaje("Error", resultado.mensajeError);
        }
        public async void consultarCorreoEntradaSemana(string nombre)
        {
            CorreoRepositorio repositorio = new CorreoRepositorio();
            Resultado<List<Correos>> resultado = await repositorio.consultarCorreoEntradaSemana(nombre);
            if (resultado.mensajeError == string.Empty)
            {
                vista.correos = resultado.valor;
            }
            else
            { } //vista.MostrarMensaje("Error", resultado.mensajeError);
        }
    }
}
