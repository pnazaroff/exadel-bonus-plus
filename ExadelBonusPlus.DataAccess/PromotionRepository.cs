using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using ExadelBonusPlus.Services.Models.Interfaces;
using ExadelBonusPlus.Services.Models;
using Microsoft.Extensions.Options;

namespace ExadelBonusPlus.DataAccess
{
    public class PromotionRepository : BaseRepository<Promotion>, IPromotionRepository
    {
        public PromotionRepository(IOptions<MongoDbSettings> mongoDbSettings) : base(mongoDbSettings)
        {
        }
        public Task<IEnumerable<Promotion>> FindPromotionByTagAsync(string tag)
        {
            //temporary
            var result = new List<Promotion>();
            return Task.FromResult(new List<Promotion>() as IEnumerable<Promotion>);
        }

    }
}