﻿using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Banco 
    {
        [Key]
        [BsonId]
        public string CNPJ { get; set; }
        public string NomeBanco { get; set; }
        public DateTime DataFundacao { get; set; }
    }
}