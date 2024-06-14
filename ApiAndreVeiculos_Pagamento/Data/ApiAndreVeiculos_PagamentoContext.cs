using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace ApiAndreVeiculos_Pagamento.Data
{
    public class ApiAndreVeiculos_PagamentoContext : DbContext
    {
        public ApiAndreVeiculos_PagamentoContext(DbContextOptions<ApiAndreVeiculos_PagamentoContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Pagamento> Pagamento { get; set; } = default!;
        public DbSet<Models.Pix> Pix { get; set; } = default!;
        public DbSet<Models.Cartao> Cartao { get; set; } = default!;
        public DbSet<Models.Boleto> Boleto { get; set; } = default!;

    }
}
