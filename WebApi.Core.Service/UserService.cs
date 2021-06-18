using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Core.Common.Attributes;
using WebApi.Core.IRepository;
using WebApi.Core.IRepository.Base;
using WebApi.Core.IService;
using WebApi.Core.Model.Entity;
using WebApi.Core.Model.Viewmodel;
using WebApi.Core.Service.Base;

namespace WebApi.Core.Service
{
    public class UserService : BaseService<User>, IUserService
    {
        private readonly IUserRepository userDal;
        public readonly IMapper iMpper;
        private readonly ILogger<UserService> _logger;
        public UserService(IBaseRepository<User> baseRepository,IUserRepository userRepository,IMapper Impper,ILogger<UserService> logger):
            base(baseRepository)
        {
            userDal = userRepository;
            iMpper=Impper;
            _logger = logger;
        }



        public async Task<int> GetCount()
        {
            return await userDal.GetCount();
        }

        [Caching(AbsoluteExpiration = 1)]
        public async Task<UserViewModel> GetUserDetails(int id)
        {
            var userinfo = await userDal.QueryByID(id);
            _logger.LogError("错误日志");
            if (userinfo != null)
            {
                //UserViewModel model = new UserViewModel()
                //{
                //    UserId = userinfo.UserId,
                //    UserName = userinfo.UserName,
                //    Address = "北京市xx区xx小区",
                //    Age = userinfo.Age,
                //    Birthday = "1996-06-26",
                //    Phone = "13888888888"

                //};
                UserViewModel model = iMpper.Map<UserViewModel>(userinfo);
                model.Address = "北京市xx区xx小区";
                model.Birthday = "1996-06-26";
                model.Phone = "13888888888";
                return model;
            }
            else {
                return null;
            } 
        }
    }
}
