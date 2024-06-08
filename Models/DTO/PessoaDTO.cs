using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class PessoaDTO
    {
        public string Documento { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string CEP { get; set; }
        public int NumeroEndereco { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
    }
}
