using Models.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public abstract class Pessoa
    {
        [Key]
        public string Documento { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        
        [NotMapped]
        public Endereco Endereco { get; set; }
        public string CEP { get; set; }
        public int NumeroEndereco { get; set; }
        public string Telefone { get; set; }

        public string Email { get; set; }


        public Pessoa() { }
        public Pessoa(PessoaDTO dto)
        {
            Documento = dto.Documento;
            Nome = dto.Nome;
            DataNascimento = dto.DataNascimento;
            Email = dto.Email;
            CEP = dto.CEP;
            NumeroEndereco = dto.NumeroEndereco;
            Telefone = dto.Telefone;
            Email = dto.Email;
        }
    }
}
