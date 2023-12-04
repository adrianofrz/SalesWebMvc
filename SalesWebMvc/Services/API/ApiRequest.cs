using SalesWebMvc.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SalesWebMvc.Services.API
{
    public class ApiRequest
    {        
        public string Url { get; set; }
        public string RequestUri { get; set; }

        public ApiRequest(string url, string requestUri)
        {
            Url = url;
            RequestUri = requestUri;
        }

        public async Task<JsonObject> ConectRequestAsync()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(this.Url); // https://api.adviceslip.com/advice
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html"));

                    HttpResponseMessage response = await client.GetAsync(RequestUri);

                    List<System.Net.Http.Formatting.MediaTypeFormatter> formatters = new List<System.Net.Http.Formatting.MediaTypeFormatter>();
                    formatters.Add(new System.Net.Http.Formatting.JsonMediaTypeFormatter());
                    formatters.Add(new VndApiJsonMediaTypeFormatter());

                    if (response.IsSuccessStatusCode)
                    {
                        var product = await response.Content.ReadAsAsync<JsonObject>(formatters);
                        return product;
                    }
                }
            }
            catch (UnsupportedMediaTypeException e)
            {
                throw new HttpClientException("Erro: " + e.Message);
            }
            catch (UriFormatException e)
            {
                throw new HttpClientException("URL Error: " + e.Message);
            }
            return null;
        }
    }
    public class VndApiJsonMediaTypeFormatter : JsonMediaTypeFormatter
    {
        public VndApiJsonMediaTypeFormatter()
        {
            //SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/vnd.api+json"));
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));

        }
    }
}
