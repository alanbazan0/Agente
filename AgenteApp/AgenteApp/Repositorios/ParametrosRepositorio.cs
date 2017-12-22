using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AgenteApp.DataAccess;
using Newtonsoft.Json;
using AgenteApp.Modelos;
using AgenteApp.Clases;
using System.Net.Http;

namespace AgenteApp.Repositorios
{
    class ParametrosRepositorio : RepositorioBase
    {

        public async Task<Resultado<string>> Insertar(Parametros param)
        {
            Resultado<string> datos = new Resultado<string>();
            DireccionBase = Constantes.DIRECCION_BASE;
            Url = "/BastiaanSoftwareCenter/php/repositorios/Parametros.php";
            AgregarParametro("accion", "insertarParametros");
            AgregarParametro("IdParametro", param.IdParamtro);
            AgregarParametro("NumeroMaquina", param.NumeroMaquina);
            AgregarParametro("DireccionIp", param.DireccionIp);
            AgregarParametro("DescripcionValor", param.DescripcionValor);
            AgregarParametro("PalabraReservada", param.PalabraReservada);
            AgregarParametro("ValorParametro", param.ValorParametro);

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

            }
            return datos;
        }

        
            public async Task<Resultado<string>> Borrar(Parametros param)
        {
            Resultado<string> datos = new Resultado<string>();
            DireccionBase = Constantes.DIRECCION_BASE;
            Url = "/BastiaanSoftwareCenter/php/repositorios/Parametros.php";
            AgregarParametro("accion", "borrarParametros");
            AgregarParametro("IdParametro", param.IdParamtro);
            AgregarParametro("NumeroMaquina", param.NumeroMaquina);
            AgregarParametro("DireccionIp", param.DireccionIp);
            AgregarParametro("DescripcionValor", param.DescripcionValor);
            AgregarParametro("PalabraReservada", param.PalabraReservada);
            AgregarParametro("ValorParametro", param.ValorParametro);

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

            }
            return datos;
        }
    }
}
