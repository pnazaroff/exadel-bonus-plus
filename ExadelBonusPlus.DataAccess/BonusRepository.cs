using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using ExadelBonusPlus.Services.Models.Interfaces;
using ExadelBonusPlus.Services.Models;
using Microsoft.Extensions.Options;

namespace ExadelBonusPlus.DataAccess
{
    public class BonusRepository : BaseRepository<Bonus>, IBonusRepository
    {
        public BonusRepository(IOptions<MongoDbSettings> mongoDbSettings) : base(mongoDbSettings)
        {
        }
        public Task<IEnumerable<Bonus>> FindBonusByTagAsync(string tag, CancellationToken cancellationToken)
        {
            //temporary
            var result = new List<Bonus>();
            return Task.FromResult(new List<Bonus>() as IEnumerable<Bonus>);
        }

    }
}