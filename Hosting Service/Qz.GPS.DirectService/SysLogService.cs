
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

    public class SysLogService : IBaseService<SysLog>
    {
        private IUnitOfWorks db = GPSService.Instance(true);

        public Response<SysLog> Add(Request<SysLog> request)
        {
            return new Response<SysLog>()
            {
                Data = db.Add<SysLog>(request.Obj),
                Times = request.BeginTime.Interval()
            };
        }

        public Response Remove(int id)
        {
            var rs = db.Delete<SysLog>(x => x.Id == id);

            return new Response()
            {
                Message = rs > 0 ? QConst.DELETE_SUCCESS : QConst.DELETE_FAIL,
                Status = rs > 0 ? 0 : 1
            };
        }

        public Response<SysLog> Modify(Request<SysLog> request)
        {
            return new Response<SysLog>()
            {
                Data = db.Update(request.Obj),
                Times = request.BeginTime.Interval()
            };
        }

        public Page.Response<SysLog> List(Page.Request<SysLog> request)
        {
            return db.All(request);
        }

        public Response<object> ListNew(Request<SysLog> request = null)
        {
            if (request == null) request = new Request<SysLog>();

            return new Response<object>()
            {
                Data = db.All<SysLog>().Select( x=> new { 
                    Id = x.Id,
                    UserName = x.UserName
                }).OrderByDescending(x => x.Id).ToList(),
                Times = request.BeginTime.Interval()
            };
        }

        public Response<SysLog> GetById(int id)
        {
            return new Response<SysLog>()
            {
                Data = db.Find<SysLog>(x => x.Id == id)
            };
        }

        public Response<List<SysLog>> List(Request<SysLog> request = null)
        {
            if (request == null) request = new Request<SysLog>();

            return new Response<List<SysLog>>()
            {
                Data = db.All<SysLog>().OrderByDescending(x => x.Id).ToList(),
                Times = request.BeginTime.Interval()
            };
        }
    }
}
