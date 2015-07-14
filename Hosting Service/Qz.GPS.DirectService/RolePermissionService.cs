
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

    public class RolePermissionService : IBaseService<RolePermission>
    {
        private IUnitOfWorks db = GPSService.Instance(true);

        public Response<RolePermission> Add(Request<RolePermission> request)
        {
            return new Response<RolePermission>()
            {
                Data = db.Add<RolePermission>(request.Obj),
                Times = request.BeginTime.Interval()
            };
        }

        public Response Remove(int id)
        {
            var rs = db.Delete<RolePermission>(x => x.Id == id);

            return new Response()
            {
                Message = rs > 0 ? QConst.DELETE_SUCCESS : QConst.DELETE_FAIL,
                Status = rs > 0 ? 0 : 1
            };
        }

        public Response<RolePermission> Modify(Request<RolePermission> request)
        {
            return new Response<RolePermission>()
            {
                Data = db.Update(request.Obj),
                Times = request.BeginTime.Interval()
            };
        }

        public Page.Response<RolePermission> List(Page.Request<RolePermission> request)
        {
            return db.All(request);
        }

        public Response<List<RolePermission>> List(Request<RolePermission> request = null)
        {
            if (request == null) request = new Request<RolePermission>();

            return new Response<List<RolePermission>>()
            {
                Data = db.All<RolePermission>().ToList(),
                Times = request.BeginTime.Interval()
            };
        }

        public Response<RolePermission> GetById(int id)
        {
            return new Response<RolePermission>()
            {
                Data = db.Find<RolePermission>(x => x.Id == id)
            };
        }
    }
}
