using Autofac;
using Autofac.Extras.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WebApi.Core.AOP;

namespace WebApi.Core.SetUp
{
    public class AutofacModuleRegister:Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RedisCacheAOP>();
            //注册Service
            var assemblyServices = Assembly.Load("WebApi.Core.Service");
            builder.RegisterAssemblyTypes(assemblyServices)
                .InstancePerDependency()//瞬时单例
                .AsImplementedInterfaces()//自动以其实现的所有接口类型暴露（包括IDisposable接口）
                .EnableInterfaceInterceptors()//引用Autofac.Extras.DynamicProxy;
                .InterceptedBy(typeof(RedisCacheAOP));//可以放一个AOP拦截器集合

            //注册Repository
            var assemblyRepository = Assembly.Load("WebApi.Core.Repository");
            builder.RegisterAssemblyTypes(assemblyRepository)
                .InstancePerDependency()//瞬时单例
                .AsImplementedInterfaces()//自动以其实现的所有接口类型暴露（包括IDisposable接口）
                .EnableInterfaceInterceptors();//引用Autofac.Extras.DynamicProxy;

        }

    }
}
