using System;
using System.Security.Cryptography;
using ExadelBonusPlus.Services.Models;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace ExadelBonusPlus.DataAccess
{
    public class TokenRefreshRepository : BaseRepository<TokenRefresh>, ITokenRefreshRepository
    {
        public TokenRefreshRepository(IOptions<MongoDbSettings> mongoDbSettings) : base(mongoDbSettings)
        {
        }

      
    }
}
