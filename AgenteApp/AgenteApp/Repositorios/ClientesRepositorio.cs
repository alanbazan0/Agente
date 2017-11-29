using AgenteApp.Clases;
using AgenteApp.DataAccess;
using AgenteApp.Modelos;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AgenteApp.Repositorios
{
    public class ClientesRepositorio : RepositorioBase
    {
        //public async Task<Resultado<List<Cliente>>> Consultar(List<Filtro> filtros, int version)
        public async Task<Resultado<List<Objeto>>> Consultar(List<Filtro> filtros, int version)
        {
            Resultado<List<Objeto>> datos = new Resultado<List<Objeto>>();
            //Resultado<List<Cliente>> datos = new Resultado<List<Cliente>>();
            DireccionBase = Constantes.DIRECCION_BASE;
            Url = "/BastiaanSoftwareCenter/php/repositorios/Clientes.php";         
            AgregarParametro("accion", "consultarDinamicamente");
            AgregarParametro("filtros", JsonConvert.SerializeObject(filtros));
            AgregarParametro("version", version.ToString());
            //AgregarParametro("campos", JsonConvert.SerializeObject(campos));
            try
            {
                using (var cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri(DireccionBase);
                    List<KeyValuePair<string, string>> parametros = GetParametros();
                    var contenido = new FormUrlEncodedContent(parametros);
                    var resultado = await cliente.PostAsync(Url, contenido);
                    string resultadoContenido = await resultado.Content.ReadAsStringAsync();
                    datos = JsonConvert.DeserializeObject<Resultado<List<Objeto>>>(resultadoContenido);
                    //datos = JsonConvert.DeserializeObject<Resultado<List<Cliente>>>(resultadoContenido);

                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);

            }
            return datos;
        }
        public async Task<Resultado<List<Objeto>>> ConsultarNumTel(string numero, int version)
        {
            Resultado<List<Objeto>> datos = new Resultado<List<Objeto>>();
            //Resultado<List<Cliente>> datos = new Resultado<List<Cliente>>();
            DireccionBase = Constantes.DIRECCION_BASE;
            Url = "/BastiaanSoftwareCenter/php/repositorios/Clientes.php";
            AgregarParametro("accion", "consultarDinamicamenteTelefono");
            AgregarParametro("numero", numero);
            AgregarParametro("version", version.ToString());
            //AgregarParametro("campos", JsonConvert.SerializeObject(campos));
            try
            {
                using (var cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri(DireccionBase);
                    List<KeyValuePair<string, string>> parametros = GetParametros();
                    var contenido = new FormUrlEncodedContent(parametros);
                    var resultado = await cliente.PostAsync(Url, contenido);
                    string resultadoContenido = await resultado.Content.ReadAsStringAsync();
                    datos = JsonConvert.DeserializeObject<Resultado<List<Objeto>>>(resultadoContenido);
                    //datos = JsonConvert.DeserializeObject<Resultado<List<Cliente>>>(resultadoContenido);

                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);

            }
            return datos;
        }

        public async Task<Resultado<Objeto>> ConsultarPorIdCliente(int idCliente, int version)
        {
            Resultado<Objeto> datos = new Resultado<Objeto>();
            //Resultado<List<Cliente>> datos = new Resultado<List<Cliente>>();
            DireccionBase = Constantes.DIRECCION_BASE;
            Url = "/BastiaanSoftwareCenter/php/repositorios/Clientes.php";
            AgregarParametro("accion", "consultarDinamicamenteIdCliente");
            AgregarParametro("idCliente", idCliente.ToString());
            AgregarParametro("version", version.ToString());
            //AgregarParametro("campos", JsonConvert.SerializeObject(campos));
            try
            {
                using (var cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri(DireccionBase);
                    List<KeyValuePair<string, string>> parametros = GetParametros();
                    var contenido = new FormUrlEncodedContent(parametros);
                    var resultado = await cliente.PostAsync(Url, contenido);
                    string resultadoContenido = await resultado.Content.ReadAsStringAsync();
                    datos = JsonConvert.DeserializeObject<Resultado<Objeto>>(resultadoContenido);
                    //datos = JsonConvert.DeserializeObject<Resultado<List<Cliente>>>(resultadoContenido);

                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);

            }
            return datos;
        }

        public async Task<Resultado<string>> Insertar(List<Campo> campos, int version)
        {
            Resultado<string> datos = new Resultado<string>();
            //Resultado<List<Cliente>> datos = new Resultado<List<Cliente>>();
            DireccionBase = Constantes.DIRECCION_BASE;
            Url = "/BastiaanSoftwareCenter/php/repositorios/Clientes.php";
            AgregarParametro("accion", "insertarDinamicamente");
            AgregarParametro("campos", JsonConvert.SerializeObject(campos));
            AgregarParametro("version", version.ToString());
            //AgregarParametro("campos", JsonConvert.SerializeObject(campos));
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
                    //datos = JsonConvert.DeserializeObject<Resultado<List<Cliente>>>(resultadoContenido);

                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);

            }
            return datos;
        }

        public async Task<Resultado<string>> Actualizar(List<Campo> campos, int version)
        {
            Resultado<string> datos = new Resultado<string>();
            //Resultado<List<Cliente>> datos = new Resultado<List<Cliente>>();
            DireccionBase = Constantes.DIRECCION_BASE;
            Url = "/BastiaanSoftwareCenter/php/repositorios/Clientes.php";
            AgregarParametro("accion", "actualizarDinamicamente");
            AgregarParametro("campos", JsonConvert.SerializeObject(campos));
            AgregarParametro("version", version.ToString());
            //AgregarParametro("campos", JsonConvert.SerializeObject(campos));
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
                    //datos = JsonConvert.DeserializeObject<Resultado<List<Cliente>>>(resultadoContenido);

                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);

            }
            return datos;
        }

    }
}
