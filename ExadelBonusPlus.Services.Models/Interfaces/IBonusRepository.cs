using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ExadelBonusPlus.Services.Models;

namespace ExadelBonusPlus.Services.Models
{
    public interface IBonusRepository: IRepository<Bonus,Guid>
    {

    }
}
