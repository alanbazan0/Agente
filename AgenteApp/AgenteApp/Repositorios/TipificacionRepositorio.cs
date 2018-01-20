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
        public async Task<Resultado<List<Tipificacion>>> ConsultarConfiguracion(string version)
        {
            Resultado<List<Tipificacion>> datos = new Resultado<List<Tipificacion>>();

            DireccionBase = Constantes.DIRECCION_BASE;
            Url = "/BastiaanSoftwareCenter/php/repositorios/Tipificacion.php";
            AgregarParametro("accion", "consultar");
            AgregarParametro("version", version);
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

        public async Task<Resultado<object>> GuardarTipificacion(List<CRM> crm1,List<Tipificacion> tipificaciones)
        {
            string datos = "";
            Resultado<object> res = new Resultado<object>();
            DireccionBase = Constantes.DIRECCION_BASE;
            Url = "/BastiaanSoftwareCenter/php/repositorios/Tipificacion.php";
            AgregarParametro("accion", "GuardarTipificacion");
            AgregarParametro("TipificacionCabecero", JsonConvert.SerializeObject(crm1));
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

                    res = JsonConvert.DeserializeObject<Resultado<object>>(resultadoContenido);
                    
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);

            }
            return res;
        }

        
        public async Task<Resultado<object>> ActulizarTipificacion(List<CRM> crm1, List<Tipificacion> tipificaciones)
        {
            string datos = "";
            Resultado<object> res = new Resultado<object>();
            DireccionBase = Constantes.DIRECCION_BASE;
            Url = "/BastiaanSoftwareCenter/php/repositorios/Tipificacion.php";
            AgregarParametro("accion", "ActualizarTipificacion");
            AgregarParametro("TipificacionCabecero", JsonConvert.SerializeObject(crm1));
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

                    res = JsonConvert.DeserializeObject<Resultado<object>>(resultadoContenido);

                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);

            }
            return res;
        }
        public async Task<Resultado<List<Tipificacion>>> ConsultarDetalleTipificacion(string version,List<Tipificacion> arrCampos)
        {
            Resultado<List<Tipificacion>> datos = new Resultado<List<Tipificacion>>();

            DireccionBase = Constantes.DIRECCION_BASE;
            Url = "/BastiaanSoftwareCenter/php/repositorios/CRM.php";
            AgregarParametro("accion", "ConsultarDetalleTipificacion");
            AgregarParametro("version", version);
            AgregarParametro("arrCampos", JsonConvert.SerializeObject(arrCampos));
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
