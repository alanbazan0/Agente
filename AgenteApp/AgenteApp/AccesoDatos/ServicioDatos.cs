using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using AgenteApp.Models;
using System.Collections.Generic;

namespace AgenteApp.DataAccess
{
    public class ServicioDatos<T>
    {
        public ServicioDatos()
        {
            HTTPParametros = new List<HTTPParametros>();
        }

		public ServicioDatos(string direccionBase,string url)
		{
            Url = url;
            DireccionBase = direccionBase;
            HTTPParametros = new List<HTTPParametros>();
		}
    

        public void agregarParametros(string nombre, string valor)
        {
            HTTPParametros.Add(new HTTPParametros(nombre, valor));        
        }

        public async Task<T> EjecutarGET()
		{
			T datos = default(T);
			using (var cliente = new HttpClient())
			{
                cliente.BaseAddress = new Uri(DireccionBase);
                string queryString = GetQueryString();
                var result = await cliente.GetStringAsync(Url);
                datos = JsonConvert.DeserializeObject<T>(result);
			}
			return datos;
		}

		public async Task<T> EjecutarPOST()
		{
			T datos = default(T);
            try
            {
                using (var cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri(DireccionBase);
                    //string queryString = GetQueryString(Parameters);
                    List<KeyValuePair<string, string>> parametros = GetParametros();
                    var contenido = new FormUrlEncodedContent(parametros);

                    var resultado = await cliente.PostAsync(Url, contenido);
                    string resultadoContenido = await resultado.Content.ReadAsStringAsync();
                    datos = JsonConvert.DeserializeObject<T>(resultadoContenido);
                }
            }
            catch (Exception)
            {

                
            }
			
			return datos;
		}

        private List<KeyValuePair<string, string>> GetParametros()
        {
            List<KeyValuePair<string, string>> parametros = new List<KeyValuePair<string, string>>();
			for (int i = 0; i < HTTPParametros.Count; i++)
            {
                HTTPParametros httpParametros = HTTPParametros[i];
                KeyValuePair<string, string> parametro = new KeyValuePair<string, string>(httpParametros.Nombre, httpParametros.Valor);
                parametros.Add(parametro);

            }
            return parametros;
        }
	
		private string GetQueryString()
        {
            string queryString= string.Empty;
            if(HTTPParametros.Count>0)
            {
                queryString = "?";
                for (int i = 0; i < HTTPParametros.Count; i++)
                {
                    HTTPParametros parameter = HTTPParametros[i];
                    queryString += parameter.Nombre + "=" + parameter.Valor;
                    if(i< HTTPParametros.Count-1)
                        queryString+="&";
                }
            }
            return queryString;
        }

		public string Url
		{
			get;
			set;
		}

		public List<HTTPParametros> HTTPParametros
		{
			get;
			set;
		}

        public string DireccionBase
        {
            get;
            set;
        }
    }
}
