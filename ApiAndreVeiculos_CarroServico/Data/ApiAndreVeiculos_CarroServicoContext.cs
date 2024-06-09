using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace ApiAndreVeiculos_CarroServico.Data
{
    public class ApiAndreVeiculos_CarroServicoContext : DbContext
    {
        public ApiAndreVeiculos_CarroServicoContext (DbContextOptions<ApiAndreVeiculos_CarroServicoContext> options)
            : base(options)
        {
        }

        public DbSet<Models.CarroServico> CarroServico { get; set; } = default!;
    }
}
