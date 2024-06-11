using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace _01_ApiAndreVeiculos_Geral.Data
{
    public class _01_ApiAndreVeiculos_GeralContext : DbContext
    {
        public _01_ApiAndreVeiculos_GeralContext (DbContextOptions<_01_ApiAndreVeiculos_GeralContext> options)
            : base(options)
        {
        }

        public DbSet<Models.TermoDeUso> TermoDeUso { get; set; } = default!;

    }
}
