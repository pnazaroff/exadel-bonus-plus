using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ExadelBonusPlus.DataAccess.UserHistory;
using ExadelBonusPlus.Services.Models.UserHistoryManager;
using ExadelBonusPlus.Services.UserHistoryService;
using Swashbuckle.AspNetCore.Annotations;

namespace ExadelBonusPlus.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserHistoryController : ControllerBase
    {
        private readonly UserHistoryServices _userHistoryServices ;
        public UserHistoryController()
        {
            _userHistoryServices = new UserHistoryServices();
        }

        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public async Task<IEnumerable<UserHistory>> GetUsersHistory()
        {
            return _userHistoryServices.GetAllUserHistory();
        }

        [HttpGet("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public async Task<UserHistory> GetUserHistoryById(Guid id)
        {
            return _userHistoryServices.GetUserHistoryById(id);
        }

        [HttpGet]
        [Route("{id:Guid}/user")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public async Task<IEnumerable<UserHistory>> GetUserHistory(Guid Userid)
        {
            return _userHistoryServices.GetUserHistory(Userid);
        }

        [HttpGet]
        [Route("{id:Guid}/promo")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public async Task<IEnumerable<UserHistory>> GetPromoHistory(Guid PromoId)
        {
            return _userHistoryServices.GetPromoHistory(PromoId);
        }

        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public async Task<UserHistory> PostUserHistory(UserHistory userHistory)
        {
            return _userHistoryServices.AddUserHistory(userHistory);
        }

        [HttpPut]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public async Task<UserHistory> PutUserHistory(UserHistory userHistory)
        {
            return _userHistoryServices.UpdateUserHistory(userHistory);
        }

        [HttpDelete]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public async Task<UserHistory> DeleteUserHistory(Guid Id)
        {
            return _userHistoryServices.DeleteUserHistory(Id);
        }
    }
}
