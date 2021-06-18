using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebApi.Core.JsonConv
{
    public class LowercasePolicy:JsonNamingPolicy
    {
        /// <summary>
        /// 返回对象全小写
        /// </summary>
        public override string ConvertName(string name) => name.ToLower();

    }
}
