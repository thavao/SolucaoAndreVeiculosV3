using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace ApiAndreVeiculos_Cliente.Data
{
    public class ApiAndreVeiculos_ClienteContext : DbContext
    {
        public ApiAndreVeiculos_ClienteContext (DbContextOptions<ApiAndreVeiculos_ClienteContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Cliente> Cliente { get; set; } = default!;
        public DbSet<Models.Endereco> Endereco { get; set; } = default!;
    }
}
