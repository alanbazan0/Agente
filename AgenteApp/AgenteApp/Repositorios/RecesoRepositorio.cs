using AgenteApp.Clases;
using AgenteApp.DataAccess;
using AgenteApp.Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AgenteApp.Repositorios
{
    public class RecesoRepositorio : RepositorioBase
        {
      

            public RecesoRepositorio()
            {
            }
        
            public async Task<Resultado<List<TipoRecesos>>> Consultar()
            {
               Resultado<List<TipoRecesos>> datos = new Resultado<List<TipoRecesos>>();

                DireccionBase = Constantes.DIRECCION_BASE;
                Url = "/BastiaanSoftwareCenter/php/repositorios/Recesos.php";
                AgregarParametro("accion", "consultar2");

                try
                {
                    using (var cliente = new HttpClient())
                    {
                        cliente.BaseAddress = new Uri(DireccionBase);
                        List<KeyValuePair<string, string>> parametros = GetParametros();
                        var contenido = new FormUrlEncodedContent(parametros);
                        var resultado = await cliente.PostAsync(Url, contenido);
                        string resultadoContenido = await resultado.Content.ReadAsStringAsync();
                        datos = JsonConvert.DeserializeObject<Resultado<List<TipoRecesos>>>(resultadoContenido);


                    }
                }
                catch (Exception ex)
                {
                    Console.Out.WriteLine(ex.Message);

                }
                return datos;
            }
           
            public async Task<Resultado<String>> SolictarReceso(string NombreUsuario, string idTipoSolicitud, string DescTipoSolicitud, string EstatusSolicitud, string LlamadaId)
            {
                Resultado<String> datos = new Resultado<String>();

                DireccionBase = Constantes.DIRECCION_BASE;
                Url = "/BastiaanSoftwareCenter/php/repositorios/Recesos.php";
                AgregarParametro("accion", "SolictarReceso");
                AgregarParametro("NombreUsuario", NombreUsuario);
                AgregarParametro("idTipoSolicitud", idTipoSolicitud);
                AgregarParametro("DescTipoSolicitud", DescTipoSolicitud);
                AgregarParametro("EstatusSolicitud", EstatusSolicitud);
                AgregarParametro("LlamadaId", LlamadaId);

            try
                {
                    using (var cliente = new HttpClient())
                    {
                        cliente.BaseAddress = new Uri(DireccionBase);
                        List<KeyValuePair<string, string>> parametros = GetParametros();
                        var contenido = new FormUrlEncodedContent(parametros);
                        var resultado = await cliente.PostAsync(Url, contenido);
                        string resultadoContenido = await resultado.Content.ReadAsStringAsync();
                        datos = JsonConvert.DeserializeObject<Resultado<String>>(resultadoContenido);
                    }
                }
                catch (Exception ex)
                {
                    Console.Out.WriteLine(ex.Message);

                }
                return datos;
            }

        public async Task<Resultado<String>> autorizar(string NombreUsuario)
        {
            Resultado<String> datos = new Resultado<String>();

            DireccionBase = Constantes.DIRECCION_BASE;
            Url = "/BastiaanSoftwareCenter/php/repositorios/Recesos.php";
            AgregarParametro("accion", "autoriza");
            AgregarParametro("NombreUsuario", NombreUsuario);
            AgregarParametro("EstatusSolicitud", "SOLAUT");

            try
            {
                using (var cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri(DireccionBase);
                    List<KeyValuePair<string, string>> parametros = GetParametros();
                    var contenido = new FormUrlEncodedContent(parametros);
                    var resultado = await cliente.PostAsync(Url, contenido);
                    string resultadoContenido = await resultado.Content.ReadAsStringAsync();
                    datos = JsonConvert.DeserializeObject<Resultado<String>>(resultadoContenido);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);

            }
            return datos;
        }

        public async Task<Resultado<List<MovimientoPersonal>>> ConsultaMovimientos(string NombreUsuario)
        {
            Resultado<List<MovimientoPersonal>> datos = new Resultado<List<MovimientoPersonal>>();

            DireccionBase = Constantes.DIRECCION_BASE;
            Url = "/BastiaanSoftwareCenter/php/repositorios/Recesos.php";
            AgregarParametro("accion", "consultaMovimientos");
            AgregarParametro("NombreUsuario", NombreUsuario);

            try
            {
                using (var cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri(DireccionBase);
                    List<KeyValuePair<string, string>> parametros = GetParametros();
                    var contenido = new FormUrlEncodedContent(parametros);
                    var resultado = await cliente.PostAsync(Url, contenido);
                    string resultadoContenido = await resultado.Content.ReadAsStringAsync();
                    datos = JsonConvert.DeserializeObject<Resultado<List<MovimientoPersonal>>>(resultadoContenido);


                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);

            }
            return datos;
        }

        public async Task<Resultado<List<EstatusUsuario>>> ConsultarStatus(string NombreUsuario)
        {
            Resultado<List<EstatusUsuario>> datos = new Resultado<List<EstatusUsuario>>();

            DireccionBase = Constantes.DIRECCION_BASE;
            Url = "/BastiaanSoftwareCenter/php/repositorios/Recesos.php";
            AgregarParametro("accion", "ConsultarStatus");
            AgregarParametro("NombreUsuario", NombreUsuario);

            try
            {
                using (var cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri(DireccionBase);
                    List<KeyValuePair<string, string>> parametros = GetParametros();
                    var contenido = new FormUrlEncodedContent(parametros);
                    var resultado = await cliente.PostAsync(Url, contenido);
                    string resultadoContenido = await resultado.Content.ReadAsStringAsync();
                    datos = JsonConvert.DeserializeObject<Resultado<List<EstatusUsuario>>>(resultadoContenido);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);

            }
            return datos;
        }
        public async Task<Resultado<String>> actualizarEstatus(string NombreUsuario,String estatus)
        {
            Resultado<String> datos = new Resultado<String>();

            DireccionBase = Constantes.DIRECCION_BASE;
            Url = "/BastiaanSoftwareCenter/php/repositorios/Recesos.php";
            AgregarParametro("accion", "actualizarEstatus");
            AgregarParametro("NombreUsuario", NombreUsuario);
            AgregarParametro("EstatusSolicitud", estatus);

            try
            {
                using (var cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri(DireccionBase);
                    List<KeyValuePair<string, string>> parametros = GetParametros();
                    var contenido = new FormUrlEncodedContent(parametros);
                    var resultado = await cliente.PostAsync(Url, contenido);
                    string resultadoContenido = await resultado.Content.ReadAsStringAsync();
                    datos = JsonConvert.DeserializeObject<Resultado<String>>(resultadoContenido);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);

            }
            return datos;
        }

        public async Task<Resultado<String>> actualizarMovimientos(string NombreUsuario, string fechaInicialGuardada, string horaInicialGuardada, string duracion, string duracionSeg)
        {
            Resultado<String> datos = new Resultado<String>();

            DireccionBase = Constantes.DIRECCION_BASE;
            Url = "/BastiaanSoftwareCenter/php/repositorios/Recesos.php";
            AgregarParametro("accion", "actualizarMovimientos");
            AgregarParametro("NombreUsuario", NombreUsuario);
            AgregarParametro("fechaInicialGuardada", fechaInicialGuardada);
            AgregarParametro("horaInicialGuardada", horaInicialGuardada);
            AgregarParametro("duracion", duracion);
            AgregarParametro("duracionSeg", duracionSeg);

            try
            {
                using (var cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri(DireccionBase);
                    List<KeyValuePair<string, string>> parametros = GetParametros();
                    var contenido = new FormUrlEncodedContent(parametros);
                    var resultado = await cliente.PostAsync(Url, contenido);
                    string resultadoContenido = await resultado.Content.ReadAsStringAsync();
                    datos = JsonConvert.DeserializeObject<Resultado<String>>(resultadoContenido);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);

            }
            return datos;
        }
        public async Task<Resultado<List<MovimientoPersonal>>> insertarMovimiento(string NombreUsuario,string idTipoReceso, string dscTipoSolicutd)
        {
            Resultado<List<MovimientoPersonal>> datos = new Resultado<List<MovimientoPersonal>>();

            DireccionBase = Constantes.DIRECCION_BASE;
            Url = "/BastiaanSoftwareCenter/php/repositorios/Recesos.php";
            AgregarParametro("accion", "insertarMovimiento");
            AgregarParametro("NombreUsuario", NombreUsuario);
            AgregarParametro("idTipoReceso", idTipoReceso);
            AgregarParametro("dscTipoSolicutd", dscTipoSolicutd);


            try
            {
                using (var cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri(DireccionBase);
                    List<KeyValuePair<string, string>> parametros = GetParametros();
                    var contenido = new FormUrlEncodedContent(parametros);
                    var resultado = await cliente.PostAsync(Url, contenido);
                    string resultadoContenido = await resultado.Content.ReadAsStringAsync();
                    datos = JsonConvert.DeserializeObject<Resultado<List<MovimientoPersonal>>>(resultadoContenido);


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
