using AgenteApp.Clases;
using AgenteApp.Modelos;
using AgenteApp.Repositorios;
using AgenteApp.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgenteApp.Presentadores
{
    class CRMPresentador
    {
        private ICRM vista;
        private Usuario usuario;
        List<CampoGrid> campos;

        public CRMPresentador(ICRM vista)
        {
            this.vista = vista;
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

    }
}
