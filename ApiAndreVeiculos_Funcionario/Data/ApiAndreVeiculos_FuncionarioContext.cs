using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace ApiAndreVeiculos_Funcionario.Data
{
    public class ApiAndreVeiculos_FuncionarioContext : DbContext
    {
        public ApiAndreVeiculos_FuncionarioContext (DbContextOptions<ApiAndreVeiculos_FuncionarioContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Funcionario> Funcionario { get; set; } = default!;
    }
}
