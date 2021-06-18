using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Core.Common.Helper;
using WebApi.Core.Common.Redis;
using WebApi.Core.Filter;
using WebApi.Core.JsonConv;
using WebApi.Core.Middleware;
using WebApi.Core.Repository.suger;
using WebApi.Core.SetUp;

namespace WebApi.Core
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

           

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //ע��appsettings��ȡ��
            services.AddSingleton(new AppSettings(Configuration));
            //ע��Redis
            services.AddSingleton<IRedisCacheManager, RedisCacheManager>();

            //���ݿ�����
            BaseDBConfig.ConnectionString = Configuration.GetSection("AppSettings:ConnectionString").Value;
   
            //ע��Swagger
            services.AddSwaggerSetup();

            //jwt��Ȩ��֤
            services.AddAuthorizationSetUp();

            //ע��automapper
            services.AddAutoMapper(typeof(Startup));

            services.AddControllers(option =>
            {
                option.Filters.Add(typeof(GlobalExceptionsFilter));
            }).AddJsonOptions(option =>
            {
                //�յ��ֶβ�����
                option.JsonSerializerOptions.IgnoreNullValues = true;
                //����jsonСд
                option.JsonSerializerOptions.PropertyNamingPolicy = new LowercasePolicy();


                //ʱ���ʽ��ʽ��
                option.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
                option.JsonSerializerOptions.Converters.Add(new DateTimeNullableConverter());


            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/V1/swagger.json", "WebApi.Core V1");

                //·�����ã�����Ϊ�գ���ʾֱ���ڸ�������localhost:8001�����ʸ��ļ�,ע��localhost:8001/swagger�Ƿ��ʲ����ģ�ȥlaunchSettings.json��launchUrlȥ����������뻻һ��·����ֱ��д���ּ��ɣ�����ֱ��дc.RoutePrefix = "doc";
                c.RoutePrefix = "";
            });

            app.UseCustomExceptionMiddleware();
            //ע���м����˳��UseRouting������ǰ�ߣ�UseAuthentication��UseAuthorizationǰ��
            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

 

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacModuleRegister());
        }
    }
}
