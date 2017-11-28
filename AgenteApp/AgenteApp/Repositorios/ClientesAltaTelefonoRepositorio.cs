using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using AgenteApp.Clases;
using AgenteApp.DataAccess;
using AgenteApp.Modelos;
using Newtonsoft.Json;

namespace AgenteApp.Repositorios
{
    class ClientesAltaTelefonoRepositorio : RepositorioBase
    {
        // metodo de insertar para conectar y mandar a insertar el cliente....
        public async Task<Resultado<string>> Insertar(ClienteTelefono clienteTelefono)
        {
            Resultado<string> datos = new Resultado<string>();
            //Resultado<List<Cliente>> datos = new Resultado<List<Cliente>>();
            DireccionBase = Constantes.DIRECCION_BASE;
            Url = "/BastiaanSoftwareCenter/php/repositorios/ClientesTelefonos.php";
            AgregarParametro("accion", "insertar");
            AgregarParametro("clientetelefono", JsonConvert.SerializeObject(clienteTelefono));
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
                    datos = JsonConvert.DeserializeObject<Resultado<string>>(resultadoContenido);
                    //datos = JsonConvert.DeserializeObject<Resultado<List<Cliente>>>(resultadoContenido);

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
