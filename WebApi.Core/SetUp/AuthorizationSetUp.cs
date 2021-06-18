using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Core.Common.Helper;

namespace WebApi.Core.SetUp
{
    public static class AuthorizationSetUp
    {
        public static void AddAuthorizationSetUp(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            // 1【授权】、这个和上边的异曲同工，好处就是不用在controller中，写多个 roles 。
            // 然后这么写 [Authorize(Policy = "Admin")]
            services.AddAuthorization(option=> {
                option.AddPolicy("User",policy=>policy.RequireRole("User").Build());
                option.AddPolicy("SystemOrAdmin", policy => policy.RequireRole("Admin", "System"));
            });


            //读取配置文件
            var symmetricKeyAsBase64 = AppSettings.app(new string[] { "AppSettings","JwtSetting","SecretKey"});
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);
            var Issuer = AppSettings.app(new string[] { "AppSettings", "JwtSetting", "Issuer" });
            var Audience = AppSettings.app(new string[] { "AppSettings", "JwtSetting", "Audience" });

            var tokenValidationParameters = new TokenValidationParameters 
            {
                ValidateIssuerSigningKey=true,
                IssuerSigningKey=signingKey,
                ValidateIssuer=true,
                ValidIssuer=Issuer,//发行人
                ValidateAudience=true,
                ValidAudience=Audience,//订阅人
                ValidateLifetime=true,
                ClockSkew=TimeSpan.FromSeconds(30),
                RequireExpirationTime=true

            };

            services.AddAuthentication("Bearer")
                .AddJwtBearer(o=> 
                {
                    o.TokenValidationParameters = tokenValidationParameters;
                    o.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("Token-Expired", "true");
                            }
                            return Task.CompletedTask;
                        }
                    };
                }
            );


        }
    }
}
