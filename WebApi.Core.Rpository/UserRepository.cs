using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Core.IRpository;
using WebApi.Core.Model.Entity;
using WebApi.Core.Rpository.Base;

namespace WebApi.Core.Rpository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public async Task<int> GetCount()
        {
            var i = await Task.Run(() => UserDb.Count(x => 1 == 1));
            return i;
        }
    }
}
