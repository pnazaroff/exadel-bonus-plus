using System;
using System.Collections.Generic;
using System.Text;
using ExadelBonusPlus.DataAccess;
using ExadelBonusPlus.Services;
using ExadelBonusPlus.Services.Models;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ExadelBonusPlus.WebApi
{
    public static class IServiceCollectionExtensions
    {
        public static void AddBonusTransient(this IServiceCollection iServiceCollection, IServiceCollection services)
        {
            services.AddTransient<IBonusRepository, BonusRepository>();
            services.AddTransient<IBonusService, BonusService>();

            services.AddTransient<IValidator<AddBonusDto>, AddBonusDtoValidator>();
            services.AddTransient<IValidator<BonusDto>, BonusDtoValidator>();
        }
    }
}
