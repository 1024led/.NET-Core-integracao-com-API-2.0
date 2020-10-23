using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Net.Http;
using Newtonsoft.Json;

namespace Cotacao.Models
{
    public class root
    {
        [JsonProperty("value")]
        public List<CotacaoService> Results { get; set; }
    }
    public class CotacaoService
    {

        [JsonProperty("cotacaoCompra")]
        public string cotacaoCompra { get; set; }

        [JsonProperty("cotacaoVenda")]
        public string cotacaoVenda { get; set; }

        [JsonProperty("dataHoraCotacao")]
        public string dataHoraCotacao { get; set; }


        
        public static async Task<CotacaoService> Coletar(Models.Services.RequisicaoService requisicao)
        {
            CotacaoService coletor = new CotacaoService();
            string errorString;

            using (HttpClient client = new HttpClient())
            {
                var content = new HttpRequestMessage(HttpMethod.Get, requisicao.ObterUrl());

                HttpResponseMessage response = await client.SendAsync(content);

                Console.WriteLine(requisicao.ObterUrl());



                if (response.IsSuccessStatusCode)
                {
                    await response.Content.ReadFromJsonAsync<CotacaoService>();
                    string responseBody = await response.Content.ReadAsStringAsync();

                    root raiz = JsonConvert.DeserializeObject<root>(responseBody);
                    
                    errorString = null;

                    if (raiz.Results.Count > 0)
                    {
                        int index = raiz.Results.Count - 1;
                        coletor.cotacaoCompra = raiz.Results[index].cotacaoCompra;
                        coletor.cotacaoVenda = raiz.Results[index].cotacaoVenda;
                        coletor.dataHoraCotacao = raiz.Results[index].dataHoraCotacao;

                    }



                    Console.WriteLine(responseBody);
                    Console.WriteLine(coletor.cotacaoCompra);
                }
                else
                {   
                    errorString = $"algo deu errado: {response.ReasonPhrase}";
                }
            }

            return coletor;
        }



    }


}
