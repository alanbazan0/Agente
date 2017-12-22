using AgenteApp.Clases;
using AgenteApp.Modelos;
using AgenteApp.Repositorios;
using AgenteApp.Vistas;

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace AgenteApp.Presentadores
{
    class RecepcionLlamadaPresentador
    {
        IRecepcionLlamadaVista vista;
        List<CampoGrid> campos;
        List<Componente> criteriosSeleccion;

        public RecepcionLlamadaPresentador(IRecepcionLlamadaVista vista)
        {
            this.vista = vista;
        }

        public async void ConsultarClientesTel(string numero)
        {
            //List<Filtro> filtros = vista.Filtros;
            ClientesTelefonoRepositorio repositorio = new ClientesTelefonoRepositorio();
            Resultado<List<ClienteTelefono>> resultado = await repositorio.ConsultarNumTel(numero);
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
            Resultado<string> resultado = await repositorio.Consultar(extension);
            if (resultado.mensajeError == string.Empty)
            {
                vista.setIdLlamada = resultado.valor;
            }
            else
                vista.MostrarMensajeAsync("Error", resultado.mensajeError);
        }

        public async void InsertarPausa()
        {
            PausaRepositorio repositorio = new PausaRepositorio();
            Resultado<object> resultado = await repositorio.InsertarPausa(vista.Pausa);
            if (resultado.mensajeError != string.Empty)
            {
                vista.ultimoIdPausa = resultado.mensajeError;
                vista.Pausas = JsonConvert.DeserializeObject<List<Pausas>>(resultado.valor.ToString());
            }

        }

        public async void ActulizarPausa()
        {
            PausaRepositorio repositorio = new PausaRepositorio();
            Resultado<List<Pausas>> resultado = await repositorio.ActulizarPausa(vista.Pausa);
            if (resultado.mensajeError == string.Empty)
            {
                vista.Pausas = resultado.valor;
            }
            else
                vista.MostrarMensajeAsync("Error", resultado.mensajeError);

        }

        public async void ConsultarPausa()
        {
            PausaRepositorio repositorio = new PausaRepositorio();
            Resultado<List<Pausas>> resultado = await repositorio.ConsultarPausa(vista.Pausa);
            if (resultado.mensajeError == string.Empty)
            {
                vista.Pausas = resultado.valor;
            }
            else
                vista.MostrarMensajeAsync("Error", resultado.mensajeError);
        }

        
        public async void InsertarParametros()
        {
            //List<Filtro> filtros = vista.Filtros;
            ParametrosRepositorio repositorio = new ParametrosRepositorio();
            Resultado<string> resultado = await repositorio.Insertar(vista.Parametro);
            //if (resultado.mensajeError != string.Empty)
            //{
            //    vista.MostrarMensajeAsync("Error", resultado.mensajeError);
            //}  
        }

        public async void BorrarParametros()
        {
            ParametrosRepositorio repositorio = new ParametrosRepositorio();
            Resultado<string> resultado = await repositorio.Borrar(vista.Parametro);
            //if (resultado.mensajeError != string.Empty)
            //{
            //    vista.MostrarMensajeAsync("Error", resultado.mensajeError);
            //}  
        }
        
    }
}
