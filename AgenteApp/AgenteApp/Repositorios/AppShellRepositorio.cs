using AgenteApp.Clases;
using Newtonsoft.Json;
using AgenteApp.DataAccess;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AgenteApp.Repositorios
{
    class AppShellRepositorio : RepositorioBase
    {
        public async Task<Resultado<string>> consultarFotoUsuario(string usuario)
        {
            Resultado<string> datos = null;
            DireccionBase = Constantes.DIRECCION_BASE;
            Url = "/BastiaanSoftwareCenter/php/repositorios/AppShell.php";
            AgregarParametro("accion", "consultarFotoUsuario");
            AgregarParametro("usuario", usuario);
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
                    datos = JsonConvert.DeserializeObject<Resultado<string>>(resultadoContenido);
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
