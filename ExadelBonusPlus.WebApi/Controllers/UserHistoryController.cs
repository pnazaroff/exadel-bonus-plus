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
        private readonly UserHistoryServices _UserHistoryServices;
        //private readonly IUserHistoryRepo _userHistoryServices;
        public UserHistoryController(IUserHistoryRepository UserHistoryRepositoryRepository)
        {
            _UserHistoryServices = new UserHistoryServices(UserHistoryRepositoryRepository);
        }

        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public async Task<IEnumerable<UserHistory>> GetUsersHistory()
        {
            return await _UserHistoryServices.GetAll();
        }

        [HttpGet("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public async Task<UserHistory> GetUserHistoryById(Guid id)
        {
            return await _UserHistoryServices.GetById(id);
        }

        [HttpGet]
        [Route("{id:Guid}/user")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public async Task<IEnumerable<UserHistory>> GetUserHistory(Guid Userid)
        {
            return await _UserHistoryServices.GetUserHistory(Userid);
        }

        //[HttpGet]
        //[Route("{id:Guid}/promo")]
        //[SwaggerResponse((int)HttpStatusCode.OK)]
        //[SwaggerResponse((int)HttpStatusCode.NotFound)]
        //public async Task<IEnumerable<UserHistory>> GetPromoHistory(Guid PromoId)
        //{
        //    return _userHistoryServices.GetPromoHistory(PromoId);
        //}

        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public async Task<UserHistory> PostUserHistory(UserHistory userHistory)
        {
            _UserHistoryServices.Add(userHistory);

            return await _UserHistoryServices.GetById(userHistory.Id);
        }

        [HttpPut]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public Task<UserHistory> PutUserHistory(UserHistory userHistory)
        {
            return null; //_userHistoryServices.UpdateUserHistory(userHistory);
        }

        [HttpDelete]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteUserHistory(Guid Id)
        {
            _UserHistoryServices.Remove(Id); //_userHistoryServices.DeleteUserHistory(Id);
            var UserHistory = await _UserHistoryServices.GetById(Id);
            if (UserHistory != null)
            {
                return NotFound();
            }

            return Ok();


        }
    }
}
