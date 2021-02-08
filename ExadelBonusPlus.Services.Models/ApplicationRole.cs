using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace ExadelBonusPlus.Services.Models
{
    [CollectionName("Roles")]
    public class ApplicationRole : MongoIdentityRole
    {
       
        public ApplicationRole() : base()
        {
        }

        public ApplicationRole(string roleName) : base(roleName)
        {

        }
    }
}