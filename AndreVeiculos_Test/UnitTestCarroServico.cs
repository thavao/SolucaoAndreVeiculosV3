using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using _01_ApiAndreVeiculos_Geral.Data;
using Models;
using ApiAndreVeiculos_CarroServico.Controllers;
using ApiAndreVeiculos_CarroServico.Data;
using Models.DTO;
namespace AndreVeiculos_Test
{
    public class UnitTestCarroServico
    {
        private DbContextOptions<ApiAndreVeiculos_CarroServicoContext> options;

        public void IniciarBanco()
        {
            options = new DbContextOptionsBuilder<ApiAndreVeiculos_CarroServicoContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            using (var context = new ApiAndreVeiculos_CarroServicoContext(options))
            {
                List<Carro> carros = new List<Carro>
                {
                    new Carro
                    {
                        Placa = "ABC1234",
                        Marca = "Toyota",
                        Quilometragem = 12000.5f,
                        NumeroChassi = "1HGBH41JXMN109186",
                        Nome = "Corolla",
                        AnoModelo = 2022,
                        AnoFabricacao = 2021,
                        Cor = "Preto",
                        Vendido = false
                    },
                    new Carro
                    {
                        Placa = "DEF5678",
                        Marca = "Honda",
                        Quilometragem = 30000.2f,
                        NumeroChassi = "2HGBH41JXMN109186",
                        Nome = "Civic",
                        AnoModelo = 2021,
                        AnoFabricacao = 2020,
                        Cor = "Branco",
                        Vendido = true
                    },
                    new Carro
                    {
                        Placa = "GHI9012",
                        Marca = "Ford",
                        Quilometragem = 45000.0f,
                        NumeroChassi = "3HGBH41JXMN109186",
                        Nome = "Focus",
                        AnoModelo = 2019,
                        AnoFabricacao = 2018,
                        Cor = "Azul",
                        Vendido = false
                    },
                    new Carro
                    {
                        Placa = "JKL3456",
                        Marca = "Chevrolet",
                        Quilometragem = 5000.3f,
                        NumeroChassi = "4HGBH41JXMN109186",
                        Nome = "Onix",
                        AnoModelo = 2023,
                        AnoFabricacao = 2022,
                        Cor = "Vermelho",
                        Vendido = true
                    },
                    new Carro
                    {
                        Placa = "MNO7890",
                        Marca = "Volkswagen",
                        Quilometragem = 20000.8f,
                        NumeroChassi = "5HGBH41JXMN109186",
                        Nome = "Gol",
                        AnoModelo = 2020,
                        AnoFabricacao = 2019,
                        Cor = "Prata",
                        Vendido = false
                    }
                };
                List<Servico> servicos = new List<Servico>
                {
                new Servico
                {
                    Id = 1,
                    Descricao = "Troca de óleo"
                },
                new Servico
                {
                    Id = 2,
                    Descricao = "Balanceamento e alinhamento"
                },
                new Servico
                {
                    Id = 3,
                    Descricao = "Revisão dos 10.000 km"
                }
            };
                context.CarroServico.Add(new CarroServico { Id = 1, Carro = carros[0], Servico = servicos[0], Status = true });
                context.CarroServico.Add(new CarroServico { Id = 2, Carro = carros[1], Servico = servicos[1], Status = false });
                context.CarroServico.Add(new CarroServico { Id = 3, Carro = carros[2], Servico = servicos[2], Status = true });
                context.CarroServico.Add(new CarroServico { Id = 4, Carro = carros[3], Servico = servicos[0], Status = true });
                context.CarroServico.Add(new CarroServico { Id = 5, Carro = carros[4], Servico = servicos[1], Status = false });

                context.SaveChanges();
            }
        }

        [Fact]
        public void TestGetAll()
        {
            IniciarBanco();

            using (var context = new ApiAndreVeiculos_CarroServicoContext(options))
            {
                CarroServicosController controller = new(context);

                var CarrosServicos = controller.GetCarroServico().Result.Value;
                Assert.Equal(CarrosServicos.Count(), 5);
            };
        }

        [Fact]
        public void TestGet()
        {
            IniciarBanco();
            using (var context = new ApiAndreVeiculos_CarroServicoContext(options))
            {
                CarroServicosController controller = new CarroServicosController(context);

                var returned = controller.GetCarroServico(1).Result.Value;

                Assert.Equal(returned.Id, 1);
            }

        }
        [Fact]
        public void TestPost()
        {
            IniciarBanco();
            Carro carro = new Carro
            {
                Placa = "MNO7890",
                Marca = "Volkswagen",
                Quilometragem = 20000.8f,
                NumeroChassi = "5HGBH41JXMN109186",
                Nome = "Gol",
                AnoModelo = 2020,
                AnoFabricacao = 2019,
                Cor = "Prata",
                Vendido = false
            };
            var servico = new Servico { Id = 1, Descricao = "Troca de óleo" };
            

            using (var context = new ApiAndreVeiculos_CarroServicoContext(options))
            {
                CarroServicosController controller = new CarroServicosController(context);

                CarroServicoDTO carroServico = new()
                {
                    Id = 0,
                    CarroPlaca = "MNO7890",
                    ServicoId = 1,
                    Status = true
                };
                var returned = controller.PostCarroServico(carroServico).Result.Value;
                Assert.Equal(returned.Id, 6);
            }
        }
    }
}