using AgenteApp.Clases;
using AgenteApp.DataAccess;
using AgenteApp.Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.Storage;

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
                datos.mensajeError = "Error al iniciar sesion. \r\nDetalle Error:\r\n" + ex.Message;

            }
            return datos;
        }

        public async Task<Resultado<string>> InsertarSesionTrabajo(string ip, string idHardware, string nombre)
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

        public async Task<Resultado<Usuario>> ConsultarDatosSesion(string id)
        {
            Resultado<Usuario> datos = new Resultado<Usuario>();

            DireccionBase = Constantes.DIRECCION_BASE;
            Url = "/BastiaanSoftwareCenter/php/repositorios/Usuarios.php";
            AgregarParametro("accion", "ConsultarDatosSesion");
            AgregarParametro("id", id);

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
                datos.mensajeError = "Error al iniciar sesion. \r\nDetalle Error:\r\n" + ex.Message;

            }
            return datos;
        }

        public async Task<Resultado<string>> SubirFotoComparar(string id, StorageFile file, string estatus, string IMG64bits)
        {
            Resultado<string> datos = new Resultado<string>();

            DireccionBase = Constantes.DIRECCION_BASE;
            Url = "/BastiaanSoftwareCenter/php/repositorios/Usuarios.php";
            //AgregarParametro("accion", "subirImagen");
            //AgregarParametro("id", id);
            //AgregarParametro("image", archivo);
            //AgregarParametro("estatus", estatus);
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(DireccionBase);
                MultipartFormDataContent form = new MultipartFormDataContent();
                HttpContent content = new StringContent("subirImagen");
                form.Add(content, "accion");
                form.Add(new StringContent(id), "id");
                form.Add(new StringContent(estatus), "estatus");
                form.Add(new StringContent(IMG64bits), "img64");
                //var stream = await file.OpenStreamForReadAsync();
                //content = new StreamContent(stream);
                //content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                //{
                //    Name = "fileToUpload",
                //    FileName = file.Name,
                //};
                //form.Add(content);
                var response = await client.PostAsync(Url, form);
                datos = JsonConvert.DeserializeObject<Resultado<string>>(response.Content.ReadAsStringAsync().Result);

            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);
                datos.mensajeError = "Error al iniciar sesion. \r\nDetalle Error:\r\n" + ex.Message;

            }
            return datos;
        }
        public async Task<Resultado<string>> ChecarEstatus(string idUsuario, string idTabla)
        {
            Resultado<string> datos = new Resultado<string>();

            DireccionBase = Constantes.DIRECCION_BASE;
            Url = "/BastiaanSoftwareCenter/php/repositorios/Usuarios.php";
            AgregarParametro("accion", "consultarEstatus");
            AgregarParametro("idUsuario", idUsuario);
            AgregarParametro("idTabla", idTabla);
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
        public async void ElimitarFaceTemp(string idUsuario, string idTabla)
        {
            Resultado<bool> datos = new Resultado<bool>();

            DireccionBase = Constantes.DIRECCION_BASE;
            Url = "/BastiaanSoftwareCenter/php/repositorios/Usuarios.php";
            AgregarParametro("accion", "eliminarFaceTemp");
            AgregarParametro("idUsuario", idUsuario);
            AgregarParametro("idTabla", idTabla);
            try
            {
                using (var cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri(DireccionBase);
                    List<KeyValuePair<string, string>> parametros = GetParametros();
                    var contenido = new FormUrlEncodedContent(parametros);
                    var resultado = await cliente.PostAsync(Url, contenido);
                    string resultadoContenido = await resultado.Content.ReadAsStringAsync();
                    datos = JsonConvert.DeserializeObject<Resultado<bool>>(resultadoContenido);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);
                datos.mensajeError = "Error al iniciar sesion. \r\nDetalle Error:\r\n" + ex.Message;

            }
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
            Resultado<List<Usuario>> datos = new Resultado<List<Usuario>>();

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