using Models.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Cliente : Pessoa
    {
        public Decimal Renda { get; set; }

        public Cliente()
        {

        }

        public Cliente(ClienteDTO dto) : base(dto)
        {
            Renda = dto.Renda;
        }
        public async Task ConstruirEndereco(HttpClient httpClient)
        {
            string url = $"http://localhost:7055/api/Enderecos/viaCep/{this.CEP}";
            var resposta = await httpClient.GetAsync(url);

            if (resposta.IsSuccessStatusCode)
            {
                var json = await resposta.Content.ReadAsStringAsync();
                var enderecoViaApiEndereco = JsonConvert.DeserializeObject<Endereco>(json);
                if (enderecoViaApiEndereco == null || enderecoViaApiEndereco.CEP == null)
                {
                    throw new Exception("Endereço ou CEP nullo");
                }
                else
                {
                    this.Endereco = new()
                    {
                        CEP = enderecoViaApiEndereco.CEP,
                        Logradouro = enderecoViaApiEndereco.Logradouro,
                        Bairro = enderecoViaApiEndereco.Bairro,
                        Localidade = enderecoViaApiEndereco.Localidade,
                        Uf = enderecoViaApiEndereco.Uf
                    };
                }
            }
        }
    }
}
