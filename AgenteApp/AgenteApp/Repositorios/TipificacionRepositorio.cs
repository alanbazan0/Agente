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
    class TipificacionRepositorio : RepositorioBase
    {
        public async Task<Resultado<List<Tipificacion>>> ConsultarConfiguracion()
        {
            Resultado<List<Tipificacion>> datos = new Resultado<List<Tipificacion>>();

            DireccionBase = Constantes.DIRECCION_BASE;
            Url = "/BastiaanSoftwareCenter/php/repositorios/Tipificacion.php";
            AgregarParametro("accion", "consultar");
            try
            {
                using (var cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri(DireccionBase);
                    List<KeyValuePair<string, string>> parametros = GetParametros();
                    var contenido = new FormUrlEncodedContent(parametros);
                    var resultado = await cliente.PostAsync(Url, contenido);
                    string resultadoContenido = await resultado.Content.ReadAsStringAsync();
                    datos = JsonConvert.DeserializeObject<Resultado<List<Tipificacion>>>(resultadoContenido);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);

            }
            return datos;
        }

        
            public async Task<Resultado<List<DatosAsistente>>> ConsultarDatosAsistente(DatosAsistente da)
        {
            Resultado<List<DatosAsistente>> datos = new Resultado<List<DatosAsistente>>();

            DireccionBase = Constantes.DIRECCION_BASE;
            Url = "/BastiaanSoftwareCenter/php/repositorios/Tipificacion.php";
            AgregarParametro("accion", "consultarAsistente");
            AgregarParametro("Vesion", da.VErsion);
            AgregarParametro("Secuencia", da.Secuencia);
            AgregarParametro("Criterio", da.CampoCriterio);
            try
            {
                using (var cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri(DireccionBase);
                    List<KeyValuePair<string, string>> parametros = GetParametros();
                    var contenido = new FormUrlEncodedContent(parametros);
                    var resultado = await cliente.PostAsync(Url, contenido);
                    string resultadoContenido = await resultado.Content.ReadAsStringAsync();
                    datos = JsonConvert.DeserializeObject<Resultado<List<DatosAsistente>>>(resultadoContenido);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);

            }
            return datos;
        }

        public async Task<Resultado<List<Tipificacion>>> GuardarTipificacion(List<Tipificacion> tipificaciones)
        {
            Resultado<List<Tipificacion>> datos = new Resultado<List<Tipificacion>>();

            DireccionBase = Constantes.DIRECCION_BASE;
            Url = "/BastiaanSoftwareCenter/php/repositorios/Tipificacion.php";
            AgregarParametro("accion", "GuardarTipificacion");
            AgregarParametro("tipificaciones", JsonConvert.SerializeObject(tipificaciones));
            try
            {
                using (var cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri(DireccionBase);
                    List<KeyValuePair<string, string>> parametros = GetParametros();
                    var contenido = new FormUrlEncodedContent(parametros);
                    var resultado = await cliente.PostAsync(Url, contenido);
                    string resultadoContenido = await resultado.Content.ReadAsStringAsync();
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
