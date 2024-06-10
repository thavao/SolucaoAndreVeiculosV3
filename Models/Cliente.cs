﻿using Models.DTO;
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
    }
}
