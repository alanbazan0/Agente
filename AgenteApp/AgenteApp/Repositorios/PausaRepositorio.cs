using System;
using System.Collections.Generic;
using System.Text;
using AgenteApp.DataAccess;
using AgenteApp.Modelos;
using AgenteApp.Clases;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AgenteApp.Repositorios
{
    class PausaRepositorio : RepositorioBase
    {
        public PausaRepositorio()
        {
        }

        public async Task<Resultado<string>> InsertarPausa(Pausas pausa)
        {
            Resultado<string> datos = new Resultado<string>();
            DireccionBase = Constantes.DIRECCION_BASE;
            Url = "/BastiaanSoftwareCenter/php/repositorios/Pausas.php";
            AgregarParametro("accion", "insertarPausas");
            AgregarParametro("IdLlamada", pausa.IdLlamada);
            AgregarParametro("Telefono", pausa.Telefono);
            AgregarParametro("Extencion", pausa.Extencion);
            AgregarParametro("IdAgente", pausa.IdAgente);

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

        public async Task<Resultado<string>> ActulizarPausa(Pausas pausa)
        {
            Resultado<string> datos = new Resultado<string>();

            DireccionBase = Constantes.DIRECCION_BASE;
            Url = "/BastiaanSoftwareCenter/php/repositorios/Pausas.php";
            AgregarParametro("accion", "actulizarPausas");
            AgregarParametro("IdLlamada", pausa.IdLlamada);
            AgregarParametro("Telefono", pausa.Telefono);
            AgregarParametro("Extencion", pausa.Extencion);
            AgregarParametro("IdAgente", pausa.IdAgente);


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

        public async Task<Resultado<List<Pausas>>> ConsultarPausa(Pausas pausa)
        {
            Resultado<List<Pausas>> datos = new Resultado<List<Pausas>>();

            DireccionBase = Constantes.DIRECCION_BASE;
            Url = "/BastiaanSoftwareCenter/php/repositorios/Pausas.php";
            AgregarParametro("accion", "consultarPausas");
            AgregarParametro("IdAgente", pausa.IdAgente);


            try
            {
                using (var cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri(DireccionBase);
                    List<KeyValuePair<string, string>> parametros = GetParametros();
                    var contenido = new FormUrlEncodedContent(parametros);
                    var resultado = await cliente.PostAsync(Url, contenido);
                    string resultadoContenido = await resultado.Content.ReadAsStringAsync();
                    datos = JsonConvert.DeserializeObject<Resultado<List<Pausas>>>(resultadoContenido);


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
