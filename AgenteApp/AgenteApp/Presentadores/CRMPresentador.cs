﻿using AgenteApp.Clases;
using AgenteApp.Modelos;
using AgenteApp.Repositorios;
using AgenteApp.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AgenteApp.Presentadores
{
    class CRMPresentador
    {
        private ICRM vista;
        private ITipificacionVista vista2;

        private Usuario usuario;
        List<CampoGrid> campos;
        List<Componente> campoFromulario;
        public CRMPresentador(ICRM vista)
        {
            this.vista = vista;
        }
        public CRMPresentador(ITipificacionVista vista)
        {
            this.vista2 = vista;
        }
        public async void consultarParametros(string clienteId)
        {
            CRMRepositorio accesoDatos = new CRMRepositorio();
            Resultado<List<Parametros>> resultado = await accesoDatos.consultarParametros(clienteId,vista.Ip, vista.Idhardware);
            if (resultado.mensajeError == string.Empty)
            {
                vista.parametrosUsuario = resultado.valor;
            }
            else
                vista.MostrarMensaje(resultado.mensajeError);
        }

        public async void cosultarCRM(string clienteId)
        {
            CRMRepositorio accesoDatos = new CRMRepositorio();
            Resultado<List<Objeto>> resultado = await accesoDatos.cosultarDinamicaCRM(clienteId, Constantes.VERSION);
            if (resultado.mensajeError == string.Empty)
            {
                vista.datosCRM=resultado.valor;
            }
            else
                vista.MostrarMensaje(resultado.mensajeError);
        }


        public async void cosultarCRMIndicadores(string clienteId)
        {
            CRMRepositorio accesoDatos = new CRMRepositorio();
            Resultado<List<CRM>> resultado = await accesoDatos.cosultarCRMIndicadores(clienteId);
            if (resultado.mensajeError == string.Empty)
            {
                vista.datosIndicadores(resultado.valor);
            }
            else
                vista.MostrarMensaje(resultado.mensajeError);
        }

        public async void CrearColumnasGridCRM()
        {
            CRMRepositorio repositorio = new CRMRepositorio();
            Resultado<List<CampoGrid>> resultado = await repositorio.ConsultarPorVersionCRM(Constantes.VERSION);

            if (resultado.mensajeError == string.Empty)
            {
                campos = resultado.valor.Where(item => item.presentacion == "1").ToList();
                vista.CrearColumnasGrid1CRM(campos);

            }
            else
                vista.MostrarMensaje(resultado.mensajeError);
        }

        public async void CrearCamposCRM(string idCliente)
        {
            CRMRepositorio repositorio = new CRMRepositorio();
            Resultado<List<Componente>> resultado = await repositorio.ConsultarCamposClientePorVersion(Constantes.VERSION);

            if (resultado.mensajeError == string.Empty)
            {
                campoFromulario = resultado.valor;
                int tamanoTitulo = CalcularColumanMayor(campoFromulario);
                foreach (Componente campoFromulario in campoFromulario)
                {
                    if (campoFromulario.presentacion == "1")
                        vista.CrearFormularioClientes(campoFromulario, tamanoTitulo);
                }
                TraerDatosCliente(Convert.ToInt32(idCliente));
            }
            else
                vista.MostrarMensaje( resultado.mensajeError);
        }

        public async void TraerDatosCliente(int idCliente)
        {
            //List<Filtro> filtros = vista.Filtros;
            CRMRepositorio repositorio = new CRMRepositorio();
            Resultado<Objeto> resultado = await repositorio.ConsultarPorIdCliente(idCliente, Constantes.VERSION);
            if (resultado.mensajeError == string.Empty)
            {
                AsignarValores(resultado.valor);
            }
            else
                vista.MostrarMensaje(resultado.mensajeError);
        }

        private void AsignarValores(Objeto registro)
        {
            foreach (var campo in campoFromulario)
            {
                vista.AsignarValor(campo, registro);
            }
        }

        public async void ConsultarDatosTipificacion(string folio, string idCliente)
        {
            //List<Filtro> filtros = vista.Filtros;
            CRMRepositorio repositorio = new CRMRepositorio();
            Resultado<List<Tipificacion>> resultado = await repositorio.ConsultarDatosTipificacion(folio, idCliente, Constantes.VERSION);
            if (resultado.mensajeError == string.Empty)
            {
                vista2.valoresDatosAsistente = resultado.valor;
            }
            else
                vista2.MostrarMensajeAsync("Error", resultado.mensajeError);
        }
        
        public async void consultarConfIndicadores(string idCliente)
        {
            //List<Filtro> filtros = vista.Filtros;
            CRMRepositorio repositorio = new CRMRepositorio();
            Resultado<List<Indicadores>> resultado = await repositorio.consultarConfIndicadores(idCliente, Constantes.VERSION);
            if (resultado.mensajeError == string.Empty)
            {
                vista.datosConfIndicadores (resultado.valor);
            }
            else
                vista.MostrarMensajeAsync("Error", resultado.mensajeError);
        }
        public int CalcularColumanMayor(List<Componente> campoFromularios)
        {
            int tamanoTitulo = 0;
            foreach (Componente campoFromulario in campoFromularios)
            {
                    if (Convert.ToInt32(campoFromulario.titulo.Count()) > tamanoTitulo)
                    {
                        tamanoTitulo = Convert.ToInt32(campoFromulario.titulo.Count());
                    }
                
            }
            return tamanoTitulo;
        }

    }

}
