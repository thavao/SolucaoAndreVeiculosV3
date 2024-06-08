using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Endereco
    {
        [Key]
        public string CEP { get; set; }
        public  string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string TipoLogradouro { get; set; }
        public string Uf { get; set; }
        public string Cidade { get; set; }
    }
}
