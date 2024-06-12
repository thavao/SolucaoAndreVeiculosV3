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

        public DbSet<Models.AceiteTermoDeUso> AceiteTermoDeUso { get; set; } = default!;
        public DbSet<Models.Banco> Banco { get; set; } = default!;
        public DbSet<Models.Boleto> Boleto { get; set; } = default!;
        public DbSet<Models.Cargo> Cargo { get; set; } = default!;
        public DbSet<Models.Carro> Carro { get; set; } = default!;
        public DbSet<Models.CarroServico> CarroServico { get; set; } = default!;
        public DbSet<Models.Cartao> Cartao { get; set; } = default!;
        public DbSet<Models.Categoria> Categoria { get; set; } = default!;
        public DbSet<Models.Cliente> Cliente { get; set; } = default!;
        public DbSet<Models.CNH> CNH { get; set; } = default!;
        public DbSet<Models.Compra> Compra { get; set; } = default!;
        public DbSet<Models.Condutor> Condutor { get; set; } = default!;
        public DbSet<Models.Dependente> Dependente { get; set; } = default!;
        public DbSet<Models.Endereco> Endereco { get; set; } = default!;
        public DbSet<Models.Financiamento> Financiamento { get; set; } = default!;
        public DbSet<Models.Funcionario> Funcionario { get; set; } = default!;
        public DbSet<Models.Pagamento> Pagamento { get; set; } = default!;
        public DbSet<Models.PendenciaFinanceira> PendenciaFinanceira { get; set; } = default!;
        public DbSet<Models.Pix> Pix { get; set; } = default!;
        public DbSet<Models.Seguro> Seguro { get; set; } = default!;
        public DbSet<Models.Servico> Servico { get; set; } = default!;
        public DbSet<Models.TermoDeUso> TermoDeUso { get; set; } = default!;
        public DbSet<Models.TipoPix> TipoPix { get; set; } = default!;
        public DbSet<Models.Venda> Venda { get; set; } = default!;



    }
}
