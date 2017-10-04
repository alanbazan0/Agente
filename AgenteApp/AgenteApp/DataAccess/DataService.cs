using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using AgenteApp.Models;
using System.Collections.Generic;

namespace AgenteApp.DataAccess
{
    public class DataService<T>
    {
        public DataService()
        {
            HTTPParameters = new List<HTTPParameter>();
        }

		public DataService(string baseAddress,string url)
		{
            Url = url;
            BaseAddress = baseAddress;
            HTTPParameters = new List<HTTPParameter>();
		}
    

        public void AddParameter(string name, string value)
        {
            HTTPParameters.Add(new HTTPParameter(name,value));        
        }

        public async Task<T> ExecuteGET()
		{
			T data = default(T);
			using (var client = new HttpClient())
			{
                client.BaseAddress = new Uri(BaseAddress);
                string queryString = GetQueryString();
                var result = await client.GetStringAsync(Url);               
				data = JsonConvert.DeserializeObject<T>(result);
			}
			return data;
		}

		public async Task<T> ExecutePOST()
		{
			T data = default(T);
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseAddress);
                    //string queryString = GetQueryString(Parameters);
                    List<KeyValuePair<string, string>> parameters = GetParameters();
                    var content = new FormUrlEncodedContent(parameters);

                    var result = await client.PostAsync(Url, content);
                    string resultContent = await result.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<T>(resultContent);
                }
            }
            catch (Exception)
            {

                
            }
			
			return data;
		}

        private List<KeyValuePair<string, string>> GetParameters()
        {
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
			for (int i = 0; i < HTTPParameters.Count; i++)
            {
                HTTPParameter httpParameter = HTTPParameters[i];
                KeyValuePair<string, string> parameter = new KeyValuePair<string, string>(httpParameter.Name, httpParameter.Value);
                parameters.Add(parameter);

            }
            return parameters;
        }
	
		private string GetQueryString()
        {
            string queryString= string.Empty;
            if(HTTPParameters.Count>0)
            {
                queryString = "?";
                for (int i = 0; i < HTTPParameters.Count; i++)
                {
                    HTTPParameter parameter = HTTPParameters[i];
                    queryString += parameter.Name + "=" + parameter.Value;
                    if(i< HTTPParameters.Count-1)
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

		public List<HTTPParameter> HTTPParameters
		{
			get;
			set;
		}

        public string BaseAddress
        {
            get;
            set;
        }
    }
}
