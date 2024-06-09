using Models.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CarroServico
    {
        public int Id { get; set; }
        [NotMapped]
        public Carro Carro { get; set; }
        [NotMapped]
        public Servico Servico { get; set; }

        public bool Status { get; set; }

        public CarroServico() { }
        public CarroServico(CarroServicoDTO dto)
        {
            Carro = new Carro { Placa = dto.CarroPlaca };
            Servico = new Servico { Id = dto.ServicoId };
            Status = dto.Status;
        }
    }
}
