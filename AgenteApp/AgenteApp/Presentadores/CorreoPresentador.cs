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
        IAltaCorreo AVista;
        List<CampoGrid> campos;
        List<Componente> criteriosSeleccion;

        public CorreoPresentador(ICorreo vista)
        {
            this.vista = vista;
        }
        public CorreoPresentador(IAltaCorreo vista)
        {
            this.AVista = vista;
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

        public async void consultarCorreoEntradaInfo(string nombre)
        {
            CorreoRepositorio repositorio = new CorreoRepositorio();
            Resultado<Correos> resultado = await repositorio.consultarCorreoEntradaInfo(nombre);
            if (resultado.mensajeError == string.Empty)
            {
                vista.total = resultado.valor.Total;
            }
            else
            { } //vista.MostrarMensaje("Error", resultado.mensajeError);
        }
        public async void consultarCorreoEntradaDiaInfo(string nombre)
        {
            CorreoRepositorio repositorio = new CorreoRepositorio();
            Resultado<Correos> resultado = await repositorio.consultarCorreoEntradaDiaInfo(nombre);
            if (resultado.mensajeError == string.Empty)
            {
                vista.dia = resultado.valor.DIa;
            }
            else
            { } //vista.MostrarMensaje("Error", resultado.mensajeError);
        }
        public async void consultarCorreoEntradaMesInfo(string nombre)
        {
            CorreoRepositorio repositorio = new CorreoRepositorio();
            Resultado<Correos> resultado = await repositorio.consultarCorreoEntradaMesInfo(nombre);
            if (resultado.mensajeError == string.Empty)
            {
                vista.mes = resultado.valor.Mes;
            }
            else
            { } //vista.MostrarMensaje("Error", resultado.mensajeError);
        }
        public async void consultarCorreoEntradaSemanaInfo(string nombre)
        {
            CorreoRepositorio repositorio = new CorreoRepositorio();
            Resultado<Correos> resultado = await repositorio.consultarCorreoEntradaSemanaInfo(nombre);
            if (resultado.mensajeError == string.Empty)
            {
                vista.semana = resultado.valor.Semana;
            }
            else
            { } //vista.MostrarMensaje("Error", resultado.mensajeError);
        }
        public async void insertarAltaClienteCorreo(string nombre, string nombre2, string paterno, string materno, string correo)
        {
            CorreoRepositorio repositorio = new CorreoRepositorio();
            Random obj = new Random();    
            
            string posibles = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";            
            int longitud = posibles.Length;           
            char letra;           
            int longitudnuevacadenaRFC = 11;
            int longitudnuevacadenaCURP = 18;
            string nuevacadenaRFC = "";
            string nuevacadenaCURP = "";
            for (int i = 0; i < longitudnuevacadenaRFC; i++)                
            {
               letra = posibles[obj.Next(longitud)];
                nuevacadenaRFC += letra.ToString();                
            }

            for (int i = 0; i < longitudnuevacadenaCURP; i++)
            {
                letra = posibles[obj.Next(longitud)];
                nuevacadenaCURP += letra.ToString();
            }

            Resultado<string> resultado = await repositorio.insertarAltaClienteCorreo(nombre,nombre2,paterno,materno,correo, nuevacadenaRFC, nuevacadenaCURP);
            if (resultado.mensajeError == string.Empty)
            {
                AVista.inserto= resultado.valor;
            }
            else
            { } //vista.MostrarMensaje("Error", resultado.mensajeError);
        }
    }
}
