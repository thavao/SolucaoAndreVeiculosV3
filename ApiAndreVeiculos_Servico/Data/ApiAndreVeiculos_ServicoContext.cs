using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace ApiAndreVeiculos_Servico.Data
{
    public class ApiAndreVeiculos_ServicoContext : DbContext
    {
        public ApiAndreVeiculos_ServicoContext (DbContextOptions<ApiAndreVeiculos_ServicoContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Servico> Servico { get; set; } = default!;
    }
}
