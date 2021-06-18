using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Core.IService.Base;
using WebApi.Core.Model.Entity;
using WebApi.Core.Model.Viewmodel;

namespace WebApi.Core.IService
{
    public interface IUserService:IBaseService<User>
    {
        /// <summary>
        /// 获取用户数量
        /// </summary>
        /// <returns></returns>
        Task<int> GetCount();

        /// <summary>
        /// 获取用户详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<UserViewModel> GetUserDetails(int id);


    }
}
