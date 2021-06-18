using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Core.IRpository.Base;
using WebApi.Core.Model.Entity;

namespace WebApi.Core.IRpository
{
    public interface IUserRepository:IBaseRepository<User>
    {
        /// <summary>
        /// 获取用户数量
        /// </summary>
        /// <returns></returns>
        Task<int> GetCount();
    }
}
