using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace ApiAndreVeiculos_SQLServer.Data
{
    public class ApiAndreVeiculos_SQLServerContext : DbContext
    {
        public ApiAndreVeiculos_SQLServerContext (DbContextOptions<ApiAndreVeiculos_SQLServerContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Banco> Banco { get; set; } = default!;
    }
}
