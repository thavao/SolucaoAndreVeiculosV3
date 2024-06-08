using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class ClienteDTO : PessoaDTO
    {
        public Decimal Renda { get; set; }
    }
}
