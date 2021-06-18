﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Core.Model.Entity;
using WebApi.Core.Model.Viewmodel;

namespace WebApi.Core.AutoMapper
{
    public class CustomProfile:Profile
    {
        /// <summary>
        /// 配置构造函数，用来创建关系映射
        /// </summary>
        public CustomProfile()
        {
            CreateMap<User, UserViewModel>();
        }

    }
}
