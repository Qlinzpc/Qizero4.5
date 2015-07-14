
namespace Qz.GPS.DirectService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Qz.Core.Entity;
    using Qz.GPS.Models;
    using Qz.Core.Infrastructure.Interface.Service;
    using Qz.Core.Infrastructure.Interface.UnitOfWorks;
    using Qz.Common;

    public class ApplicationService : IBaseService<Application>
    {
        private IUnitOfWorks db = GPSService.Instance(true);

        public Response<Application> Add(Request<Application> request)
        {
            // 1. 判断数据有效性 

            // 2. 生成 code 

            var data = db.Add<Application>(request.Obj);

            return new Response<Application>()
            {
                Data = data
            };
        }

        public Response Remove(int id)
        {
            var rs = db.Delete<Application>(x => x.Id.Equals(id));

            return new Response()
            {
                Message = rs > 0 ? QConst.DELETE_SUCCESS : QConst.DELETE_FAIL,
                Status = rs > 0 ? 0 : 1
            };

        }

        public Response<Application> Modify(Request<Application> request)
        {
            var data = db.Update<Application>(request.Obj);

            return new Response<Application>()
            {
                Data = data
            };
        }

        public Response<Application> GetById(int id)
        {

            return new Response<Application>()
            {
                Data = db.Find<Application>(x => x.Id == id)
            };
        }

        public Page.Response<Application> List(Page.Request<Application> request)
        {
            throw new NotImplementedException();
        }

        public Response<List<Application>> List(Request<Application> request = null)
        {
            var list = db.All<Application>().ToList();

            return new Response<List<Application>>()
            {
                Data = list,
            };
        }
    }
}
