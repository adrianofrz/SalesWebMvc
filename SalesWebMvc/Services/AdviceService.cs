using Newtonsoft.Json;
using SalesWebMvc.Models;
using SalesWebMvc.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
    //Classe para testar chamada de API
    public class AdviceService
    {
        private readonly SalesWebMvcContext _context;

        public AdviceService(SalesWebMvcContext context)
        {
            _context = context;
        }
               
        public async Task<Slip> GetAdvice()
        {
            using (var client = new HttpClient())
            {
                Slip retorno ; 
                var response = await client.GetAsync("https://api.adviceslip.com/advice");
                string content = await response.Content.ReadAsStringAsync();

                retorno = JsonConvert.DeserializeObject<Slip>(content);
                return retorno;
            }
        }


         public async Task<Advice> ConectRequestAsync()
        {
            try
            {
                using (var client = new HttpClient())
                {                    
                    client.BaseAddress = new Uri("https://api.adviceslip.com"); // https://api.adviceslip.com/advice
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html"));
                    
                    HttpResponseMessage response = await client.GetAsync("advice");

                    List<System.Net.Http.Formatting.MediaTypeFormatter> formatters = new List<System.Net.Http.Formatting.MediaTypeFormatter>();
                    formatters.Add(new System.Net.Http.Formatting.JsonMediaTypeFormatter());
                    formatters.Add(new VndApiJsonMediaTypeFormatter());

                    if (response.IsSuccessStatusCode)
                    {
                        var product = await response.Content.ReadAsAsync<Advice>(formatters);                        
                        //Console.WriteLine("{0}\t${1}\t{2}", product.Name, product.Price, product.Category);
                        return product;
                    }
                }
            }
            catch (UnsupportedMediaTypeException e)
            {
                throw new HttpClientException("Erro: " + e.Message);
            }
            catch(UriFormatException e)
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
