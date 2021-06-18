using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Core.IRepository.Base;

namespace WebApi.Core.Rpository.Base
{

    public class BaseRepository<TEntity> : DbContext<TEntity>, IBaseRepository<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// 写入实体数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> Add(TEntity model)
        {
            var i = await Task.Run(()=>Db.Insertable(model).ExecuteReturnBigIdentity());
            return i>0;
        }
        /// <summary>
        /// 根据ID删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<bool> DeleteByIds(object[] ids)
        {
            var i = await Task.Run(()=>Db.Deleteable<TEntity>().In(ids).ExecuteCommand());
            return i>0;
        }
        /// <summary>
        /// 根据ID查询一条数据
        /// </summary>
        /// <param name="objId"></param>
        /// <returns></returns>
        public async Task<TEntity> QueryByID(object objid)
        {
            return await Task.Run(()=>Db.Queryable<TEntity>().InSingle(objid));
        }

        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> Update(TEntity model)
        {
            var i = await Task.Run(()=>Db.Updateable(model).ExecuteCommand());
            return i > 0;
        }

    }
}
