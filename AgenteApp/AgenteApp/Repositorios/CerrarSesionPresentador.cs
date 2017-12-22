using System;
using System.Collections.Generic;
using System.Text;

namespace AgenteApp.Repositorios
{
    class CerrarSesionPresentador
    {
        public CerrarSesionPresentador()
        {
        }

        public async Task<Resultado<List<Correos>>> consultarCorreoEntrada(string nombre)
        {
            Resultado<List<Correos>> datos = new Resultado<List<Correos>>();

            DireccionBase = Constantes.DIRECCION_BASE;
            Url = "/BastiaanSoftwareCenter/php/repositorios/Correo.php";
            AgregarParametro("accion", "consultarCorreoEntrada");
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
                    datos = JsonConvert.DeserializeObject<Resultado<List<Correos>>>(resultadoContenido);
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
