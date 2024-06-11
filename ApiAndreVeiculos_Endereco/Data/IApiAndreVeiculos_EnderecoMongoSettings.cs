namespace ApiAndreVeiculos_Endereco.Data
{
	public interface IApiAndreVeiculos_EnderecoMongoSettings
	{
		string ConnectionsString { get; set; }
		string DatabaseName { get; set; }
		string EnderecoCollectionName { get; set; }
	}
}
