using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ExadelBonusPlus.Services.Models;

namespace ExadelBonusPlus.Services.Models.Interfaces
{
    public interface IPromotionRepository: IRepository<Promotion,Guid>
    {
        Task<IEnumerable<Promotion>> FindPromotionByTagAsync(string tag, CancellationToken cancellationToken);

        //to be continued ...
    }
}
