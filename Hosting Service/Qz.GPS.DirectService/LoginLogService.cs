
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

    public class LoginLogService : IBaseService<LoginLog>
    {

        private IUnitOfWorks db = GPSService.Instance(true);

        public Response<LoginLog> Add(Request<LoginLog> request)
        {
            return new Response<LoginLog>()
            {
                Data = db.Add<LoginLog>(request.Obj),
                Times = request.BeginTime.Interval()
            };
        }

        public Response Remove(int id)
        {
            var rs = db.Delete<LoginLog>(x => x.Id == id);

            return new Response()
            {
                Message = rs > 0 ? QConst.DELETE_SUCCESS : QConst.DELETE_FAIL,
                Status = rs > 0 ? 0 : 1
            };
        }

        public Response<LoginLog> Modify(Request<LoginLog> request)
        {
            return new Response<LoginLog>()
            {
                Data = db.Update(request.Obj),
                Times = request.BeginTime.Interval()
            };
        }

        public Page.Response<LoginLog> List(Page.Request<LoginLog> request)
        {
            return db.All(request);
        }

        public Response<List<LoginLog>> List(Request<LoginLog> request = null)
        {
            if (request == null) request = new Request<LoginLog>();

            return new Response<List<LoginLog>>()
            {
                Data = db.All<LoginLog>().ToList(),
                Times = request.BeginTime.Interval()
            };
        }

        public Response<LoginLog> GetById(int id)
        {
            return new Response<LoginLog>()
            {
                Data = db.Find<LoginLog>(x => x.Id == id)
            };
        }
    }
}
