using AgenteApp.Modelos;
using Newtonsoft.Json.Linq;

using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;

namespace AgenteApp.Vistas
{
    public interface IFormulario
    {
        void MostrarMensaje(string titulo, string mensaje);
        List<Campo> Campos { get; }
        List<Objeto> Clientes { set; }
        List<TipoTelefono> TipoTelefono { set; }
        //List<CodigoPostal> direccionesCodigo { set; }
        List<ClienteTelefono> ClienteTelefono { set; }
        List<Correos> ClienteCorreos { set; }
        void CrearFormularioClientes(Componente componente,int tamanoTitulo);
        string idCliente { set;  }
        void ActualizarTelefonoCliente(string idCliente);
        void direccioneCodigo(List<CodigoPostal> direccionesCodigo, object sender, RoutedEventArgs e);
        void ActualizarCorreoCliente(string idCliente);
        void ConsultarPortabilidad(string compania,string municipio,string consecutivo);
        void AsignarValor(Componente componente, Objeto registro);
    }
}
