using AgenteApp.Clases;
using AgenteApp.Modelos;
using AgenteApp.Repositorios;
using AgenteApp.Vistas;

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgenteApp.Presentadores
{
    public class ClientesDetallePresentador
    {
        IClientesVista vista;
        List<CampoGrid> campos;
        List<CriterioSeleccion> criteriosSeleccion;

        public ClientesDetallePresentador(IClientesVista vista)
        {
            this.vista = vista;
        }

        public async void ConsultarClientes()
        {
            List<Filtro> filtros = vista.Filtros;
            ClientesRepositorio repositorio = new ClientesRepositorio();
            Resultado<List<Objeto>> resultado = await repositorio.Consultar(filtros,Constantes.VERSION);
            if(resultado.mensajeError==string.Empty)
            {
                vista.Clientes = resultado.valor;
            }
            else
                vista.MostrarMensajeAsync("Error", resultado.mensajeError);
        }

        public async void ConsultarClientesTel(string numero)
        {
            //List<Filtro> filtros = vista.Filtros;
            ClientesRepositorio repositorio = new ClientesRepositorio();
            Resultado<List<Objeto>> resultado = await repositorio.ConsultarNumTel(numero, Constantes.VERSION);
            if (resultado.mensajeError == string.Empty)
            {
                vista.Clientes = resultado.valor;
            }
            else
                vista.MostrarMensajeAsync("Error", resultado.mensajeError);
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

        public async void ConsultarIdLlamada(string extension)
        {
            //List<Filtro> filtros = vista.Filtros;
            AsteriskRepositorio repositorio = new AsteriskRepositorio();
            Resultado<string> resultado  = await repositorio.Consultar(extension);
            if (resultado.mensajeError == string.Empty)
            {
                vista.setIdLlamada = resultado.valor;
            }
            else
                vista.MostrarMensajeAsync("Error", resultado.mensajeError);
        }

        public void CrearCatalogoClientes()
        {
            CrearCriteriosSeleccion();
            CrearColumnasGrid();
        }

        private async void CrearColumnasGrid()
        {
            CamposGrid1Repositorio repositorio = new CamposGrid1Repositorio();
            Resultado<List<CampoGrid>> resultado = await repositorio.ConsultarPorVersion(Constantes.VERSION);

            if (resultado.mensajeError == string.Empty)
            {
                campos = resultado.valor.Where(item => item.presentacion == "1").ToList();
                vista.CrearColumnasGrid1(campos);

            }
            else
                vista.MostrarMensajeAsync("Error", resultado.mensajeError);
        }

        private async void CrearCriteriosSeleccion()
        {
            CriteriosSeleccionRepositorio repositorio = new CriteriosSeleccionRepositorio();
            Resultado<List<CriterioSeleccion>> resultado = await repositorio.ConsultarPorVersion(Constantes.VERSION);

            if (resultado.mensajeError == string.Empty)
            {
                criteriosSeleccion = resultado.valor;
                foreach (CriterioSeleccion criterioSeleccion in criteriosSeleccion)
                {
                    if (criterioSeleccion.presentacion == "1")
                        vista.CrearCriterioSeleccion(criterioSeleccion);
                }
            }
            else
                vista.MostrarMensajeAsync("Error", resultado.mensajeError);
        }
    }
}
