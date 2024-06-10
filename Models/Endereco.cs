using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Endereco
    {
        [JsonProperty("cep"), Key]
        public string CEP { get; set; }

        [JsonProperty("logradouro")]
        public string Logradouro { get; set; }

        [JsonProperty("bairro")]
        public string Bairro { get; set; }

        [JsonProperty("localidade")]
        public string Localidade { get; set; }

        [JsonProperty("uf")]
        public string Uf { get; set; }

        public Endereco() { }
        public Endereco(HttpClient httpClient, string cep)
        {
            string url = $"http://viacep.com.br/ws/{cep}/json/";
            HttpResponseMessage resposta = httpClient.GetAsync(url).Result;

            if (resposta.IsSuccessStatusCode)
            {
                var json = resposta.Content.ReadAsStringAsync().Result;
                var enderecoViaCep = JsonConvert.DeserializeObject<Endereco>(json);
                if (enderecoViaCep == null || enderecoViaCep.CEP == null)
                {
                    throw new Exception("Endereço ou CEP nullo");
                }
                else
                {
                    this.CEP = enderecoViaCep.CEP;
                    this.Logradouro = enderecoViaCep.Logradouro;
                    this.Bairro = enderecoViaCep.Bairro;
                    this.Localidade = enderecoViaCep.Localidade;
                    this.Uf = enderecoViaCep.Uf;
                }
            }
        }

        public async static Task<Endereco> ConstruirEndereco(HttpClient httpClient, string cep)
        {
            string url = $"https://localhost:7055/api/Enderecos/{cep}";
            HttpResponseMessage resposta = await httpClient.GetAsync(url);

            if (resposta.IsSuccessStatusCode)
            {
                var json = await resposta.Content.ReadAsStringAsync();
                var enderecoPelaApi = JsonConvert.DeserializeObject<Endereco>(json);

                if (enderecoPelaApi == null || enderecoPelaApi.CEP == null)
                {
                    throw new Exception("Endereço ou CEP nullo");
                }
                else
                {
                    Endereco e = new()
                    {
                        CEP = enderecoPelaApi.CEP,
                        Logradouro = enderecoPelaApi.Logradouro,
                        Bairro = enderecoPelaApi.Bairro,
                        Localidade = enderecoPelaApi.Localidade,
                        Uf = enderecoPelaApi.Uf
                    };
                    return e;
                }
            }
            return null;
        }

    }
}
