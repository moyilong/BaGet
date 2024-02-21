using System;
using BaGet.Core;
using BaGet.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace BaGet
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddBaGetWebApplication(
            this IServiceCollection services,
            Action<BaGetApplication> configureAction)
        {
#pragma warning disable ASP5001 // 类型或成员已过时
#pragma warning disable CS0618 // 类型或成员已过时
            services
                .AddRouting(options => options.LowercaseUrls = true)
                .AddControllers()
                .AddApplicationPart(typeof(PackageContentController).Assembly)
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddJsonOptions(options =>
                {
#pragma warning disable SYSLIB0020 // 类型或成员已过时
                    options.JsonSerializerOptions.IgnoreNullValues = true;
#pragma warning restore SYSLIB0020 // 类型或成员已过时
                });
#pragma warning restore CS0618 // 类型或成员已过时
#pragma warning restore ASP5001 // 类型或成员已过时

            services.AddRazorPages();

            services.AddHttpContextAccessor();
            services.AddTransient<IUrlGenerator, BaGetUrlGenerator>();

            services.AddBaGetApplication(configureAction);

            return services;
        }
    }
}
