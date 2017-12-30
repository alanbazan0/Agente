using AgenteApp.Clases;
using AgenteApp.DataAccess;
using AgenteApp.Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AgenteApp.Presenters
{
    public class UsuarioRepositorio : RepositorioBase
    {
        public UsuarioRepositorio()
        {
        }

        public async Task<Resultado<Usuario>> Consultar(string id, string contrasena) 
        {
            Resultado<Usuario> datos = new Resultado<Usuario>();
           
            DireccionBase = Constantes.DIRECCION_BASE;
            Url = "/BastiaanSoftwareCenter/php/repositorios/Usuarios.php";
            AgregarParametro("accion", "consultarPorIdContrasena");
            AgregarParametro("id", id);
            AgregarParametro("contrasena", contrasena);
           
            try
            {
                using (var cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri(DireccionBase);
                    List<KeyValuePair<string, string>> parametros = GetParametros();
                    var contenido = new FormUrlEncodedContent(parametros);
                    var resultado = await cliente.PostAsync(Url, contenido);
                    string resultadoContenido = await resultado.Content.ReadAsStringAsync();
                    datos = JsonConvert.DeserializeObject<Resultado<Usuario>>(resultadoContenido);
                   

                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);
                datos.mensajeError="Error al iniciar sesion. \r\nDetalle Error:\r\n"+ex.Message;

            }
            return datos;
        }

        public async Task<Resultado<string>> InsertarSesionTrabajo(string ip, string idHardware,string nombre)
        {
            Resultado<string> datos = new Resultado<string>();

            DireccionBase = Constantes.DIRECCION_BASE;
            Url = "/BastiaanSoftwareCenter/php/repositorios/Usuarios.php";
            AgregarParametro("accion", "InsertarSesionTrabajo");
            AgregarParametro("ip", ip);
            AgregarParametro("idHardware", idHardware);
            AgregarParametro("nombre", nombre);

            try
            {
                using (var cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri(DireccionBase);
                    List<KeyValuePair<string, string>> parametros = GetParametros();
                    var contenido = new FormUrlEncodedContent(parametros);
                    var resultado = await cliente.PostAsync(Url, contenido);
                    string resultadoContenido = await resultado.Content.ReadAsStringAsync();
                    datos = JsonConvert.DeserializeObject<Resultado<string>>(resultadoContenido);


                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);
                datos.mensajeError = "Error al iniciar sesion. \r\nDetalle Error:\r\n" + ex.Message;

            }
            return datos;
        }
        //public async Task<Usuario> Consultar(string id, string contrasena)
        //{
        //    Repositorio<Usuario> servicioDatos = new Repositorio<Usuario>(Constantes.DIRECCION_BASE, "/BastiaanSoftwareCenter/php/repositorios/Usuarios.php");
        //    servicioDatos.AgregarParametro("accion", "consultarPorIdContrasena");
        //    servicioDatos.AgregarParametro("id", id);
        //    servicioDatos.AgregarParametro("contrasena", contrasena);
        //    servicioDatos.Metodo = MetodoHTTP.POST;
        //    Usuario usuario = await servicioDatos.Ejecutar();
        //    return usuario;

        //}

        public async Task<Resultado<List<Usuario>>> ConsultarUsuarios()
        {
            Resultado<List< Usuario >> datos = new Resultado<List<Usuario>>();

            DireccionBase = Constantes.DIRECCION_BASE;
            Url = "/BastiaanSoftwareCenter/php/repositorios/Usuarios.php";
            AgregarParametro("accion", "consultarUsuarios");

            try
            {
                using (var cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri(DireccionBase);
                    List<KeyValuePair<string, string>> parametros = GetParametros();
                    var contenido = new FormUrlEncodedContent(parametros);
                    var resultado = await cliente.PostAsync(Url, contenido);
                    string resultadoContenido = await resultado.Content.ReadAsStringAsync();
                    datos = JsonConvert.DeserializeObject<Resultado<List<Usuario>>>(resultadoContenido);


                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);
                datos.mensajeError = "Error al iniciar sesion. \r\nDetalle Error:\r\n" + ex.Message;

            }
            return datos;
        }
    }
}