using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Core.IRepository;
using WebApi.Core.Model.Entity;
using WebApi.Core.Repository.Base;

namespace WebApi.Core.Repository
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
