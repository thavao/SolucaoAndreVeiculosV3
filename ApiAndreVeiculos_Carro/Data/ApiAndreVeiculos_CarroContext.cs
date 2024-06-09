using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace ApiAndreVeiculos_Carro.Data
{
    public class ApiAndreVeiculos_CarroContext : DbContext
    {
        public ApiAndreVeiculos_CarroContext (DbContextOptions<ApiAndreVeiculos_CarroContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Carro> Carro { get; set; } = default!;
    }
}
