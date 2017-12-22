using AgenteApp.Clases;
using AgenteApp.DataAccess;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AgenteApp.Repositorios
{
    class CerrarSesionRepositorio: RepositorioBase
    {
        public CerrarSesionRepositorio()
        {
        }

        public async Task<Resultado<string>> CerrarSesion(string nombre)
        {
            Resultado<string> datos = new Resultado<string>();

            DireccionBase = Constantes.DIRECCION_BASE;
            Url = "/BastiaanSoftwareCenter/php/repositorios/Usuarios.php";
            AgregarParametro("accion", "CerrarSesion");
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
