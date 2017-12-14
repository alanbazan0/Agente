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
    public class CorreoRepositorio : RepositorioBase
    {

        public CorreoRepositorio()
        {
        }

        public async Task<Resultado<List<Correos>>> consultarCorreoEntrada(string nombre)
        {
            Resultado<List<Correos>> datos = new Resultado<List<Correos>>();

            DireccionBase = Constantes.DIRECCION_BASE;
            Url = "/BastiaanSoftwareCenter/php/repositorios/Correo.php";
            AgregarParametro("accion", "consultarCorreoEntrada");
            AgregarParametro("idNombre", nombre);

            try
            {
                using (var cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri(DireccionBase);
                    List<KeyValuePair<string, string>> parametros = GetParametros();
                    var contenido = new FormUrlEncodedContent(parametros);
                    var resultado = await cliente.PostAsync(Url, contenido);
                    string resultadoContenido = await resultado.Content.ReadAsStringAsync();
                    datos = JsonConvert.DeserializeObject<Resultado<List<Correos>>>(resultadoContenido);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);
            }
            return datos;
        }

        public async Task<Resultado<List<Correos>>> consultarCorreoEntradaDia(string nombre)
        {
            Resultado<List<Correos>> datos = new Resultado<List<Correos>>();

            DireccionBase = Constantes.DIRECCION_BASE;
            Url = "/BastiaanSoftwareCenter/php/repositorios/Correo.php";
            AgregarParametro("accion", "consultarCorreoEntradaDia");
            AgregarParametro("idNombre", nombre);

            try
            {
                using (var cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri(DireccionBase);
                    List<KeyValuePair<string, string>> parametros = GetParametros();
                    var contenido = new FormUrlEncodedContent(parametros);
                    var resultado = await cliente.PostAsync(Url, contenido);
                    string resultadoContenido = await resultado.Content.ReadAsStringAsync();
                    datos = JsonConvert.DeserializeObject<Resultado<List<Correos>>>(resultadoContenido);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);
            }
            return datos;
        }

        public async Task<Resultado<List<Correos>>> consultarCorreoEntradaMes(string nombre)
        {
            Resultado<List<Correos>> datos = new Resultado<List<Correos>>();

            DireccionBase = Constantes.DIRECCION_BASE;
            Url = "/BastiaanSoftwareCenter/php/repositorios/Correo.php";
            AgregarParametro("accion", "consultarCorreoEntradaMes");
            AgregarParametro("idNombre", nombre);

            try
            {
                using (var cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri(DireccionBase);
                    List<KeyValuePair<string, string>> parametros = GetParametros();
                    var contenido = new FormUrlEncodedContent(parametros);
                    var resultado = await cliente.PostAsync(Url, contenido);
                    string resultadoContenido = await resultado.Content.ReadAsStringAsync();
                    datos = JsonConvert.DeserializeObject<Resultado<List<Correos>>>(resultadoContenido);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);
            }
            return datos;
        }

        public async Task<Resultado<List<Correos>>> consultarCorreoEntradaSemana(string nombre)
        {
            Resultado<List<Correos>> datos = new Resultado<List<Correos>>();

            DireccionBase = Constantes.DIRECCION_BASE;
            Url = "/BastiaanSoftwareCenter/php/repositorios/Correo.php";
            AgregarParametro("accion", "consultarCorreoEntradaSemana");
            AgregarParametro("idNombre", nombre);

            try
            {
                using (var cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri(DireccionBase);
                    List<KeyValuePair<string, string>> parametros = GetParametros();
                    var contenido = new FormUrlEncodedContent(parametros);
                    var resultado = await cliente.PostAsync(Url, contenido);
                    string resultadoContenido = await resultado.Content.ReadAsStringAsync();
                    datos = JsonConvert.DeserializeObject<Resultado<List<Correos>>>(resultadoContenido);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);
            }
            return datos;
        }
        public async Task<Resultado<Correos>> consultarCorreoEntradaInfo(string nombre)
        {
            Resultado<Correos> datos = new Resultado<Correos>();

            DireccionBase = Constantes.DIRECCION_BASE;
            Url = "/BastiaanSoftwareCenter/php/repositorios/Correo.php";
            AgregarParametro("accion", "consultarCorreoEntradaInfo");
            AgregarParametro("idNombre", nombre);

            try
            {
                using (var cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri(DireccionBase);
                    List<KeyValuePair<string, string>> parametros = GetParametros();
                    var contenido = new FormUrlEncodedContent(parametros);
                    var resultado = await cliente.PostAsync(Url, contenido);
                    string resultadoContenido = await resultado.Content.ReadAsStringAsync();
                    datos = JsonConvert.DeserializeObject<Resultado<Correos>>(resultadoContenido);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);
            }
            return datos;
        }

        public async Task<Resultado<Correos>> consultarCorreoEntradaDiaInfo(string nombre)
        {
            Resultado<Correos> datos = new Resultado<Correos>();

            DireccionBase = Constantes.DIRECCION_BASE;
            Url = "/BastiaanSoftwareCenter/php/repositorios/Correo.php";
            AgregarParametro("accion", "consultarCorreoEntradaDiaInfo");
            AgregarParametro("idNombre", nombre);

            try
            {
                using (var cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri(DireccionBase);
                    List<KeyValuePair<string, string>> parametros = GetParametros();
                    var contenido = new FormUrlEncodedContent(parametros);
                    var resultado = await cliente.PostAsync(Url, contenido);
                    string resultadoContenido = await resultado.Content.ReadAsStringAsync();
                    datos = JsonConvert.DeserializeObject<Resultado<Correos>>(resultadoContenido);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);
            }
            return datos;
        }

        public async Task<Resultado<Correos>> consultarCorreoEntradaMesInfo(string nombre)
        {
            Resultado<Correos> datos = new Resultado<Correos>();

            DireccionBase = Constantes.DIRECCION_BASE;
            Url = "/BastiaanSoftwareCenter/php/repositorios/Correo.php";
            AgregarParametro("accion", "consultarCorreoEntradaMesInfo");
            AgregarParametro("idNombre", nombre);

            try
            {
                using (var cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri(DireccionBase);
                    List<KeyValuePair<string, string>> parametros = GetParametros();
                    var contenido = new FormUrlEncodedContent(parametros);
                    var resultado = await cliente.PostAsync(Url, contenido);
                    string resultadoContenido = await resultado.Content.ReadAsStringAsync();
                    datos = JsonConvert.DeserializeObject<Resultado<Correos>>(resultadoContenido);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);
            }
            return datos;
        }

        public async Task<Resultado<Correos>> consultarCorreoEntradaSemanaInfo(string nombre)
        {
            Resultado<Correos> datos = new Resultado<Correos>();

            DireccionBase = Constantes.DIRECCION_BASE;
            Url = "/BastiaanSoftwareCenter/php/repositorios/Correo.php";
            AgregarParametro("accion", "consultarCorreoEntradaSemanaInfo");
            AgregarParametro("idNombre", nombre);

            try
            {
                using (var cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri(DireccionBase);
                    List<KeyValuePair<string, string>> parametros = GetParametros();
                    var contenido = new FormUrlEncodedContent(parametros);
                    var resultado = await cliente.PostAsync(Url, contenido);
                    string resultadoContenido = await resultado.Content.ReadAsStringAsync();
                    datos = JsonConvert.DeserializeObject<Resultado<Correos>>(resultadoContenido);
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
