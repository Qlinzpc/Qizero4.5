
namespace Qz.Core.Infrastructure.Interface.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Qz.Core.Entity;

    public interface IBaseService<T> where T : class
    {
        Response<T> Add(Request<T> request);

        Response Remove(int id);

        Response<T> Modify(Request<T> request);

        Page.Response<T> List(Page.Request<T> request);

        Response<List<T>> List(Request<T> request = null);

        Response<T> GetById(int id);
    }
}
