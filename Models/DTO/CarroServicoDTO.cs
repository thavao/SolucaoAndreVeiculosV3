using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class CarroServicoDTO
    {
        public string CarroPlaca { get; set; }
        public int ServicoId { get; set; }
        public bool Status { get; set; }
    }
}
