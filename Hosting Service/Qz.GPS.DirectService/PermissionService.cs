
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

    public class PermissionService : IBaseService<Permission>
    {
        private IUnitOfWorks db = GPSService.Instance(true);

        public Response<Permission> Add(Request<Permission> request)
        {
            return new Response<Permission>()
            {
                Data = db.Add<Permission>(request.Obj),
                Times = request.BeginTime.Interval()
            };
        }

        public Response<List<Permission>> Add(Request<List<Permission>> request)
        {
            return new Response<List<Permission>>()
            {
                Data = db.Add<Permission>(request.Obj.AsEnumerable()).ToList(),
                Times = request.BeginTime.Interval()
            };
        }

        public Response Remove(int id)
        {
            var rs = db.Delete<Permission>(x => x.Id == id);

            return new Response()
            {
                Message = rs > 0 ? QConst.DELETE_SUCCESS : QConst.DELETE_FAIL,
                Status = rs > 0 ? 0 : 1
            };
        }

        public Response<Permission> Modify(Request<Permission> request)
        {
            return new Response<Permission>()
            {
                Data = db.Update(request.Obj),
                Times = request.BeginTime.Interval()
            };
        }

        public Page.Response<Permission> List(Page.Request<Permission> request)
        {
            return db.All(request);
        }

        public Response<List<Permission>> List(Request<Permission> request = null)
        {
            if (request == null) request = new Request<Permission>();

            return new Response<List<Permission>>()
            {
                Data = db.All<Permission>().ToList(),
                Times = request.BeginTime.Interval()
            };
        }

        public Response<Permission> GetById(int id)
        {
            return new Response<Permission>()
            {
                Data = db.Find<Permission>(x => x.Id == id)
            };
        }
    }
}
