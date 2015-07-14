
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

    public class UserPermissionService : IBaseService<UserPermission>
    {
        private IUnitOfWorks db = GPSService.Instance(true);

        public Response<UserPermission> Add(Request<UserPermission> request)
        {
            return new Response<UserPermission>()
            {
                Data = db.Add<UserPermission>(request.Obj),
                Times = request.BeginTime.Interval()
            };
        }

        public Response Remove(int id)
        {
            var rs = db.Delete<UserPermission>(x => x.Id == id);

            return new Response()
            {
                Message = rs > 0 ? QConst.DELETE_SUCCESS : QConst.DELETE_FAIL,
                Status = rs > 0 ? 0 : 1
            };
        }

        public Response<UserPermission> Modify(Request<UserPermission> request)
        {
            return new Response<UserPermission>()
            {
                Data = db.Update(request.Obj),
                Times = request.BeginTime.Interval()
            };
        }

        public Page.Response<UserPermission> List(Page.Request<UserPermission> request)
        {
            return db.All(request);
        }

        public Response<List<UserPermission>> List(Request<UserPermission> request = null)
        {
            if (request == null) request = new Request<UserPermission>();

            return new Response<List<UserPermission>>()
            {
                Data = db.All<UserPermission>().ToList(),
                Times = request.BeginTime.Interval()
            };
        }

        public Response<UserPermission> GetById(int id)
        {
            return new Response<UserPermission>()
            {
                Data = db.Find<UserPermission>(x => x.Id == id)
            };
        }
    }
}
