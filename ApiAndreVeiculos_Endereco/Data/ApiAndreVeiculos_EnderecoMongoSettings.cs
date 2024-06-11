namespace ApiAndreVeiculos_Endereco.Data
{
	public class ApiAndreVeiculos_EnderecoMongoSettings : IApiAndreVeiculos_EnderecoMongoSettings
	{
		public string ConnectionsString { get; set; }
		public string DatabaseName { get; set; }
		public string EnderecoCollectionName { get; set; }
	}
}
