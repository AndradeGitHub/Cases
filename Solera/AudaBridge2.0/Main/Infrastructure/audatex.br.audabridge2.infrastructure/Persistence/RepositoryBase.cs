using MongoDB.Driver;
using MongoDB.Bson;

namespace audatex.br.audabridge2.infrastructure.persistence
{
    public class RepositoryBase
    {
        private readonly string _connectionString;
        private readonly string _databaseName;

        public RepositoryBase(string connectionString, string databaseName)
        {
            _connectionString = connectionString;
            _databaseName = databaseName;
        }

        public IMongoDatabase CreateDataBase()
        {
            IMongoClient _client = new MongoClient(_connectionString);

            IMongoDatabase _db = _client.GetDatabase(_databaseName);            

            return _db;
        }
    }
}