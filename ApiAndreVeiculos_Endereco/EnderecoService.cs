
using ApiAndreVeiculos_Endereco.Data;
using Models;
using MongoDB.Driver;

namespace ApiAndreVeiculos_Endereco
{
	public class EnderecoService
	{
		private readonly IMongoCollection<Endereco> _endereco;

		public EnderecoService(IApiAndreVeiculos_EnderecoMongoSettings settings)
		{
			var client = new MongoClient(settings.ConnectionsString);
			var database = client.GetDatabase(settings.DatabaseName);
			_endereco = database.GetCollection<Endereco>(settings.EnderecoCollectionName);
		}
		public void InserirUm(Endereco endereco) => _endereco.InsertOne(endereco);
		

	}
}