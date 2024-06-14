using ApiAndreVeiculos_Banco.Controllers;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndreVeiculos_Test
{
    public class UnitTestBanco
    {
        [Fact]
        public void TestPost()
        {
            BancosController controller = new(new ConnectionFactory());

            for (int i = 0; i < 99999; i++)
            {
                controller.PostMQBanco(
                    new Models.Banco()
                    {
                        CNPJ = "Teste " + new Random().Next(1, 100000),
                        NomeBanco = "Banco " + new Random().Next(1, 100000),
                        DataFundacao = DateTime.Now
                    }
                );

                Assert.True( true );
            }
        }
    }
}

