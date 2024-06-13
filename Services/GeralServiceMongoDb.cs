using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class GeralServiceMongoDb<T>
    {
        private readonly IMongoCollection<T> _colecao;
        private readonly string _connectionString = "mongodb://root:Mongo%402024%23@localhost:27017/";
        private readonly string _dataBaseName = "AndreVeiculos";

        public GeralServiceMongoDb(string collectionName)
        {
            var client = new MongoClient(_connectionString);
            var dataBase = client.GetDatabase(_dataBaseName);
            _colecao = dataBase.GetCollection<T>(collectionName);
        }
        public List<T> Get() => _colecao.Find(T => true).ToList();
        public T Get(string id) => _colecao.Find(Builders<T>.Filter.Eq("_id", new ObjectId(id))).FirstOrDefault();

        public T Create(T item)
        {
            _colecao.InsertOne(item);
            return item;
        }
        public void Update(T item, string id)
        {
            _colecao.ReplaceOne(Builders<T>.Filter.Eq("_id", new ObjectId(id)), item);
        }

        public void Remove(string id)
        {
            _colecao.DeleteOne(Builders<T>.Filter.Eq("_id", new ObjectId(id)));
        }

    }
}
