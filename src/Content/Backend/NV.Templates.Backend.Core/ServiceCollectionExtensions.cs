﻿using FluentValidation;
using Microsoft.Extensions.Hosting;
using NodaTime;
using NV.Templates.Backend.Core.Framework.Validation;
using NV.Templates.Backend.Core.General;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers Core services.
        /// </summary>
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services
                .AddSingleton<IClock>(x => SystemClock.Instance)
                .AddTransient<IValidatorFactory, ServiceProviderValidatorFactory>()
                .AddValidatorsFromAssemblyContaining<IApplicationInfo>()
                .AddSingleton<IApplicationInfo>(sp => new ApplicationInfo(sp.GetRequiredService<IHostingEnvironment>())) // We want to force the usage of the right constructor.
                .AddScoped<IOperationContext, OperationContext>();

            services
                .AddHealthChecks();

            return services;
        }
    }
}