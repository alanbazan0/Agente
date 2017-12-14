﻿using AgenteApp.Clases;
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
            Resultado<string> resultado = await repositorio.Consultar(extension);
            if (resultado.mensajeError == string.Empty)
            {
                vista.setIdLlamada = resultado.valor;
            }
            else
                vista.MostrarMensajeAsync("Error", resultado.mensajeError);
        }
    }
}
