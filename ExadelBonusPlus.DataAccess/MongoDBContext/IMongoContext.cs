using System;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace ExadelBonusPlus.DataAccess
{
    public interface IMongoContext : IDisposable
    {
        IMongoCollection<T> GetCollection<T>(string name);
    }
}