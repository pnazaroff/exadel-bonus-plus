using System;
using System.Collections.Generic;
using System.Text;

namespace ExadelBonusPlus.Services.Models.CommentManager
{
    /// <summary>
    /// Represents a user's comment about discount.
    /// </summary>
    class Comment
    {
        public Guid id { get; set; }
        public string textOfComment { get; set; }
        public bool isPublish { get; set; }
        public Guid idUser { get; set; }
        public Guid idPromo { get; set; }
        public int estimate { get; set; }
    }
}
