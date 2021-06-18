using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Core.IRepository;
using WebApi.Core.IService;
using WebApi.Core.Repository;

namespace WebApi.Core.Service
{
    public class TestService: ITestService
    {
        ITestRepository test = new TestRepository();
        public int Sum(int i, int j)
        {
            return test.Sum(i, j);
        }
    }
}
