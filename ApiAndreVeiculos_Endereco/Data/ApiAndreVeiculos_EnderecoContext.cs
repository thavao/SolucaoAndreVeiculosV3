using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace ApiAndreVeiculos_Endereco.Data
{
    public class ApiAndreVeiculos_EnderecoContext : DbContext
    {
        public ApiAndreVeiculos_EnderecoContext (DbContextOptions<ApiAndreVeiculos_EnderecoContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Endereco> Endereco { get; set; } = default!;
    }
}
