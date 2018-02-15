using AgenteApp.Clases;
using AgenteApp.Modelos;
using AgenteApp.Repositorios;
using AgenteApp.Vistas;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgenteApp.Presentadores
{
    public class AppShellPresentador
    {

        IAppShell vista;

        public AppShellPresentador(IAppShell vista)
        {
            this.vista = vista;
        }

        public async void consultarFotoUsuario( string usuario)
        {
            AppShellRepositorio repositorio = new AppShellRepositorio();
            Resultado<string> resultado = await repositorio.consultarFotoUsuario(usuario);
            if (resultado.mensajeError == string.Empty)
            {
                vista.mostrarFotoUsuarioAsync (resultado.valor);
            }
            else { }
                //vista.MostrarMensajeAsync("Error", resultado.mensajeError);
        }
    }
}
