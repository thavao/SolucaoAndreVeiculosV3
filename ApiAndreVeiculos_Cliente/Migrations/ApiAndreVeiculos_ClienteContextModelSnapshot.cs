﻿// <auto-generated />
using System;
using ApiAndreVeiculos_Cliente.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ApiAndreVeiculos_Cliente.Migrations
{
    [DbContext(typeof(ApiAndreVeiculos_ClienteContext))]
    partial class ApiAndreVeiculos_ClienteContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.29")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Models.Cliente", b =>
                {
                    b.Property<string>("Documento")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CEP")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumeroEndereco")
                        .HasColumnType("int");

                    b.Property<decimal>("Renda")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Documento");

                    b.HasIndex("CEP");

                    b.ToTable("Cliente");
                });

            modelBuilder.Entity("Models.Endereco", b =>
                {
                    b.Property<string>("CEP")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Logradouro")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipoLogradouro")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Uf")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CEP");

                    b.ToTable("Endereco");
                });

            modelBuilder.Entity("Models.Cliente", b =>
                {
                    b.HasOne("Models.Endereco", "Endereco")
                        .WithMany()
                        .HasForeignKey("CEP")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Endereco");
                });
#pragma warning restore 612, 618
        }
    }
}
