using ExadelBonusPlus.Services.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExadelBonusPlus.DataAccess
{
    public class GenericCrud : IGenericCrud
    {
        private IMongoDatabase database;

        public GenericCrud(IExadelBonusDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            database = client.GetDatabase(settings.DatabaseName);
        }



        public async void InsertRecord<T>(string collectionName, T record)
        {
            var collection = database.GetCollection<T>(collectionName);
            await collection.InsertOneAsync(record);
        }

        public async Task<T> LoadRecordById<T>(string collectionName, Guid id)
        {
            var collection = database.GetCollection<T>(collectionName);
            var filter = Builders<T>.Filter.Eq("Id", id);

            return await collection.Find(filter).FirstAsync();
        }

        public async Task<List<T>> LoadRecords<T>(string collectionName)
        {
            var collection = database.GetCollection<T>(collectionName);
            return await collection.Find(new BsonDocument()).ToListAsync();
        }

        public void UpsertRecord<T>(string collectionName, Guid id, T record)
        {
            var collection = database.GetCollection<T>(collectionName);
            var result = collection.ReplaceOne(
                new BsonDocument("_id", id),
                record,
                new ReplaceOptions { IsUpsert = true }
                );
        }

        public void DeleteRecord<T>(string collectionName, Guid id)
        {
            var collection = database.GetCollection<T>(collectionName);
            var filter = Builders<T>.Filter.Eq("Id", id);
            collection.DeleteOne(filter);
        }
    }
}
