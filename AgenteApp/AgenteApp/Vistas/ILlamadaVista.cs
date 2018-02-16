﻿using System;
using System.Collections.Generic;
using System.Text;
using AgenteApp.Modelos;

namespace AgenteApp.Vistas
{
    public interface ILlamadaVista
    {
        void ConsultarClientesTel(string numero);
        List<ClienteTelefono> Clientes { set; }
        void ConsultarPortabilidad(string numero);
        List<Portabilidad> Portabilidad { set; }
        void ConsultarIdLlamada(string extension);
        string setIdLlamada { set; }
        string ultimoIdPausa { set; }
        void InsertarPausa();
        void ActualizarPausa();
        void ConsultarPausas();
        Pausas Pausa { get; set; }
        Parametros Parametro { get; set; }
        List<Pausas> Pausas { set; }
        List<Supervisores> Supervisores { set; }
        List<Usuario> usuarios { set; }
        List<Parametros> Parametros { set; }
        void MostrarMensajeAsync(string titulo, string mensaje);
    }
}
