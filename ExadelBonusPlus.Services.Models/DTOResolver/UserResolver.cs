using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace ExadelBonusPlus.Services.Models
{
    public class UserResolver : IValueResolver<ApplicationUser, UserInfoDTO, IList<string>>, IValueResolver<History, HistoryDto, UserInfoHistoryDto>, IValueResolver<History, BonusHistoryDto, UserInfoHistoryDto>
    { 
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        public UserResolver(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        //to do Task
        public IList<string> Resolve(ApplicationUser source, UserInfoDTO destination, IList<string> destMember, ResolutionContext context)
        {
            try
            {
                var role = _userManager.GetRolesAsync(source).Result;
                return role;
            }
            catch (Exception ex)
            {
                return new[] {ex.Message};
            }
        }

        public UserInfoHistoryDto Resolve(History source, HistoryDto destination, UserInfoHistoryDto destMember, ResolutionContext context)
        {
            try
            {
                return _mapper.Map<UserInfoHistoryDto>(_userManager.FindByIdAsync(source.CreatorId.ToString())
                                                                        .GetAwaiter().GetResult()); }
            catch
            {
                return new UserInfoHistoryDto();
            }
        }

        public UserInfoHistoryDto Resolve(History source, BonusHistoryDto destination, UserInfoHistoryDto destMember,
            ResolutionContext context)
        {
            try
            {
                return _mapper.Map<UserInfoHistoryDto>(_userManager.FindByIdAsync(source.CreatorId.ToString())
                                                                        .GetAwaiter().GetResult());
            }
            catch
            {
                return new UserInfoHistoryDto();
            }
        }
    }
}
