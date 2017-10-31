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
        public async Task<Resultado<List<Cliente>>> Consultar(object criteriosSeleccion)
        {
            Resultado<List<Cliente>> datos = null;
            DireccionBase = Constantes.DIRECCION_BASE;
            Url = "/BastiaanSoftwareCenter/php/repositorios/Clientes.php";         
            AgregarParametro("accion", "consultar");
            AgregarParametro("criteriosSeleccion", JsonConvert.SerializeObject(criteriosSeleccion));
            try
            {
                using (var cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri(DireccionBase);
                    List<KeyValuePair<string, string>> parametros = GetParametros();
                    var contenido = new FormUrlEncodedContent(parametros);
                    var resultado = await cliente.PostAsync(Url, contenido);
                    string resultadoContenido = await resultado.Content.ReadAsStringAsync();
                    datos = JsonConvert.DeserializeObject<Resultado<List<Cliente>>>(resultadoContenido);
                    //dynamic data = Json.Decode(json);

              
                }
            }
            catch (Exception ex)
            {


            }
            return datos;
        }
    }
}
