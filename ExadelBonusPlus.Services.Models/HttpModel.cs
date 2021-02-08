using System.Collections.Generic;

namespace ExadelBonusPlus.Services.Models
{

    public class HttpModel<TModel>
    {
        public TModel Value { get; set; }

        public List<string> Errors { get; set; }
    }

}
