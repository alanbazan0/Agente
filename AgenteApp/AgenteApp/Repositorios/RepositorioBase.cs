using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using AgenteApp.Clases;

namespace AgenteApp.DataAccess
{
    public class RepositorioBase
    {
        public RepositorioBase()
        {
            HTTPParametros = new List<ParametroHTTP>();
        }

		public RepositorioBase(string direccionBase,string url)
		{
            Url = url;
            DireccionBase = direccionBase;
            HTTPParametros = new List<ParametroHTTP>();
		}
    

        public void AgregarParametro(string nombre, string valor)
        {
            HTTPParametros.Add(new ParametroHTTP(nombre, valor));        
        }

        //public async Task<Resultado<T>> Ejecutar()
        //{
        //    //T datos = default(T);
        //    Resultado<T> datos = new Resultado<T>();
        //    try
        //    {
        //        using (var cliente = new HttpClient())
        //        {
        //            cliente.BaseAddress = new Uri(DireccionBase);
        //            //string queryString = GetQueryString(Parameters);
        //            List<KeyValuePair<string, string>> parametros = GetParametros();
        //            var contenido = new FormUrlEncodedContent(parametros);

        //            var resultado = await cliente.PostAsync(Url, contenido);
        //            string resultadoContenido = await resultado.Content.ReadAsStringAsync();
        //            datos = JsonConvert.DeserializeObject<Resultado<T>>(resultadoContenido);
        //        }
        //    }
        //    catch (Exception ex)
        //    {


        //    }

        //    return datos;
        //}

  //      protected async Task<T> EjecutarGET()
		//{
		//	T datos = default(T);
		//	using (var cliente = new HttpClient())
		//	{
  //              cliente.BaseAddress = new Uri(DireccionBase);
  //              string queryString = GetQueryString();
  //              var result = await cliente.GetStringAsync(Url);
  //              datos = JsonConvert.DeserializeObject<T>(result);
		//	}
		//	return datos;
		//}

		//protected async Task<T> EjecutarPOST()
		//{
		//	T datos = default(T);
  //          try
  //          {
  //              using (var cliente = new HttpClient())
  //              {
  //                  cliente.BaseAddress = new Uri(DireccionBase);
  //                  //string queryString = GetQueryString(Parameters);
  //                  List<KeyValuePair<string, string>> parametros = GetParametros();
  //                  var contenido = new FormUrlEncodedContent(parametros);

  //                  var resultado = await cliente.PostAsync(Url, contenido);
  //                  string resultadoContenido = await resultado.Content.ReadAsStringAsync();
  //                  datos = JsonConvert.DeserializeObject<T>(resultadoContenido);
  //              }
  //          }
  //          catch (Exception ex)
  //          {

                
  //          }
			
		//	return datos;
		//}

        protected List<KeyValuePair<string, string>> GetParametros()
        {
            List<KeyValuePair<string, string>> parametros = new List<KeyValuePair<string, string>>();
			for (int i = 0; i < HTTPParametros.Count; i++)
            {
                ParametroHTTP httpParametros = HTTPParametros[i];
                KeyValuePair<string, string> parametro = new KeyValuePair<string, string>(httpParametros.Nombre, httpParametros.Valor);
                parametros.Add(parametro);

            }
            return parametros;
        }
	
		//private string GetQueryString()
  //      {
  //          string queryString= string.Empty;
  //          if(HTTPParametros.Count>0)
  //          {
  //              queryString = "?";
  //              for (int i = 0; i < HTTPParametros.Count; i++)
  //              {
  //                  ParametroHTTP parameter = HTTPParametros[i];
  //                  queryString += parameter.Nombre + "=" + parameter.Valor;
  //                  if(i< HTTPParametros.Count-1)
  //                      queryString+="&";
  //              }
  //          }
  //          return queryString;
  //      }

		public string Url
		{
			get;
			set;
		}

		public List<ParametroHTTP> HTTPParametros
		{
			get;
			set;
		}

        public string DireccionBase
        {
            get;
            set;
        }
        public MetodoHTTP Metodo { get; set; }
    }
}
