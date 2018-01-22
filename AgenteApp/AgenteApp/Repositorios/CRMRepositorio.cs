using AgenteApp.Clases;
using AgenteApp.DataAccess;
using AgenteApp.Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AgenteApp.Repositorios
{
    class CRMRepositorio: RepositorioBase
    {
        public string DireccionBase { get; private set; }
        public string Url { get; private set; }

        public async Task<Resultado<List<Parametros>>> consultarParametros(string idUsuario,string ip, string idhardware)
        {
            Resultado<List<Parametros>> datos = new Resultado<List<Parametros>>();
            //Resultado<List<Cliente>> datos = new Resultado<List<Cliente>>();
            DireccionBase = Constantes.DIRECCION_BASE;
            Url = "/BastiaanSoftwareCenter/php/repositorios/Parametros.php";
            AgregarParametro("accion", "consultarParametros");
            AgregarParametro("IdParametro",idUsuario);
            AgregarParametro("DireccionIp", ip);
            AgregarParametro("NumeroMaquina", idhardware);
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
                    datos = JsonConvert.DeserializeObject<Resultado<List<Parametros>>>(resultadoContenido);
                    //datos = JsonConvert.DeserializeObject<Resultado<List<Cliente>>>(resultadoContenido);

                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);

            }
            return datos;
        }


        public async Task<Resultado<List<CRM>>> cosultarCRM(string idCliente)
        {
            Resultado<List<CRM>> datos = new Resultado<List<CRM>>();
            //Resultado<List<Cliente>> datos = new Resultado<List<Cliente>>();
            DireccionBase = Constantes.DIRECCION_BASE;
            Url = "/BastiaanSoftwareCenter/php/repositorios/CRM.php";
            AgregarParametro("accion", "cosultarCRM");
            AgregarParametro("idCliente", idCliente);
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
                    datos = JsonConvert.DeserializeObject<Resultado<List<CRM>>>(resultadoContenido);
                    //datos = JsonConvert.DeserializeObject<Resultado<List<Cliente>>>(resultadoContenido);

                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);

            }
            return datos;
        }

        public async Task<Resultado<List<CRM>>> cosultarCRMIndicadores(string idCliente)
        {
            Resultado<List<CRM>> datos = new Resultado<List<CRM>>();
            //Resultado<List<Cliente>> datos = new Resultado<List<Cliente>>();
            DireccionBase = Constantes.DIRECCION_BASE;
            Url = "/BastiaanSoftwareCenter/php/repositorios/CRM.php";
            AgregarParametro("accion", "cosultarCRMIndicadores");
            AgregarParametro("idCliente", idCliente);
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
                    datos = JsonConvert.DeserializeObject<Resultado<List<CRM>>>(resultadoContenido);
                    //datos = JsonConvert.DeserializeObject<Resultado<List<Cliente>>>(resultadoContenido);

                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);

            }
            return datos;
        }

        public async Task<Resultado<List<CampoGrid>>> ConsultarPorVersionCRM(int version)
        {
            Resultado<List<CampoGrid>> datos = null;
            DireccionBase = Constantes.DIRECCION_BASE;
            Url = "/BastiaanSoftwareCenter/php/repositorios/CRM.php";
            AgregarParametro("accion", "consultarPorVersionCRM");
            AgregarParametro("version", version.ToString());
            try
            {
                using (var cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri(DireccionBase);
                    List<KeyValuePair<string, string>> parametros = GetParametros();
                    var contenido = new FormUrlEncodedContent(parametros);
                    var resultado = await cliente.PostAsync(Url, contenido);
                    string resultadoContenido = await resultado.Content.ReadAsStringAsync();
                    datos = JsonConvert.DeserializeObject<Resultado<List<CampoGrid>>>(resultadoContenido);
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            return datos;
        }

        public async Task<Resultado<List<Objeto>>> cosultarDinamicaCRM(string numero, int version)
        {
            Resultado<List<Objeto>> datos = new Resultado<List<Objeto>>();
            //Resultado<List<Cliente>> datos = new Resultado<List<Cliente>>();
            DireccionBase = Constantes.DIRECCION_BASE;
            Url = "/BastiaanSoftwareCenter/php/repositorios/CRM.php";
            AgregarParametro("accion", "cosultarDinamicaCRM");
            AgregarParametro("idCliente", numero);
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

        public async Task<Resultado<List<Componente>>> ConsultarCamposClientePorVersion(int version)
        {
            Resultado<List<Componente>> datos = null;
            DireccionBase = Constantes.DIRECCION_BASE;
            Url = "/BastiaanSoftwareCenter/php/repositorios/CRM.php";
            AgregarParametro("accion", "ConsultarCamposClientePorVersion");
            AgregarParametro("version", version.ToString());
            try
            {
                using (var cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri(DireccionBase);
                    List<KeyValuePair<string, string>> parametros = GetParametros();
                    var contenido = new FormUrlEncodedContent(parametros);
                    var resultado = await cliente.PostAsync(Url, contenido);
                    string resultadoContenido = await resultado.Content.ReadAsStringAsync();
                    datos = JsonConvert.DeserializeObject<Resultado<List<Componente>>>(resultadoContenido);
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            return datos;
        }

        public async Task<Resultado<Objeto>> ConsultarPorIdCliente(int idCliente, int version)
        {
            Resultado<Objeto> datos = new Resultado<Objeto>();
            //Resultado<List<Cliente>> datos = new Resultado<List<Cliente>>();
            DireccionBase = Constantes.DIRECCION_BASE;
            Url = "/BastiaanSoftwareCenter/php/repositorios/CRM.php";
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
        public async Task<Resultado<List<Tipificacion>>> ConsultarDatosTipificacion(string folio, string idCliente,int version)
        {
            Resultado<List<Tipificacion>> datos = new Resultado<List<Tipificacion>>();

            DireccionBase = Constantes.DIRECCION_BASE;
            Url = "/BastiaanSoftwareCenter/php/repositorios/CRM.php";
            AgregarParametro("accion", "ConsultarDatosTipificacion");
            AgregarParametro("folio", folio);
            AgregarParametro("idCliente", idCliente);
            AgregarParametro("version", version.ToString());
            //AgregarParametro("version", version.ToString());
            try
            {
                using (var cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri(DireccionBase);
                    List<KeyValuePair<string, string>> parametros = GetParametros();
                    var contenido = new FormUrlEncodedContent(parametros);
                    var resultado = await cliente.PostAsync(Url, contenido);
                    string resultadoContenido = await resultado.Content.ReadAsStringAsync();
                    resultadoContenido = Utilidades.UTF8_to_ISO(resultadoContenido);
                    datos = JsonConvert.DeserializeObject<Resultado<List<Tipificacion>>>(resultadoContenido);
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
