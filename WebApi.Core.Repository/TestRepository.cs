using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Core.IRepository;

namespace WebApi.Core.Repository
{
    public class TestRepository :ITestRepository
    {

        public int Sum(int i, int j)
        {
            return i + j;
        }
    }
}
