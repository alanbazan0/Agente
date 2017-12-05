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
    class FormularioAltaRepositorio:RepositorioBase
    {
        public async Task<Resultado<List<Componente>>> ConsultarPorVersion(int version)
        {
            Resultado<List<Componente>> datos = null;
            DireccionBase = Constantes.DIRECCION_BASE;
            Url = "/BastiaanSoftwareCenter/php/repositorios/CamposFormularioAlta.php";
            AgregarParametro("accion", "consultarPorVersion");
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

        public async Task<Resultado<List<TipoTelefono>>> ConsultarTiopTelefono()
        {
            Resultado<List<TipoTelefono>> datos = null;
            DireccionBase = Constantes.DIRECCION_BASE;
            Url = "/BastiaanSoftwareCenter/php/repositorios/Telefonos.php";
            AgregarParametro("accion", "ConsultarCatalogoTipoTelefono");            
            try
            {
                using (var cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri(DireccionBase);
                    List<KeyValuePair<string, string>> parametros = GetParametros();
                    var contenido = new FormUrlEncodedContent(parametros);
                    var resultado = await cliente.PostAsync(Url, contenido);
                    string resultadoContenido = await resultado.Content.ReadAsStringAsync();
                    datos = JsonConvert.DeserializeObject<Resultado<List<TipoTelefono>>>(resultadoContenido);
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            return datos;
        }



    }
}
