using AgenteApp.Clases;
using AgenteApp.Modelos;
using AgenteApp.Repositorios;
using AgenteApp.Vistas;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgenteApp.Presentadores
{
    class CerrarSesionPresentador
    {
        ICerrarSesion vista;

        public CerrarSesionPresentador(ICerrarSesion vista)
        {
            this.vista = vista;
        }

        public async void CerrarSesion()
        {
            
            CerrarSesionRepositorio repositorio = new CerrarSesionRepositorio();
            Resultado<string> resultado = await repositorio.CerrarSesion(vista.NombreUsuario,vista.IP,vista.IdHardware);
            if (resultado.mensajeError == string.Empty)
            {
                vista.cerrarPRograma();
            }
            else
            { }// vista.MostrarMensajeAsync("Error", resultado.mensajeError);
        }

    }
}
