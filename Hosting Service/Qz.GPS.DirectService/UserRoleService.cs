
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

    public class UserRoleService : IBaseService<UserRole>
    {
        private IUnitOfWorks db = GPSService.Instance(true);

        public Response<UserRole> Add(Request<UserRole> request)
        {
            return new Response<UserRole>()
            {
                Data = db.Add<UserRole>(request.Obj),
                Times = request.BeginTime.Interval()
            };
        }

        public Response Remove(int id)
        {
            var rs = db.Delete<UserRole>(x => x.Id == id);

            return new Response()
            {
                Message = rs > 0 ? QConst.DELETE_SUCCESS : QConst.DELETE_FAIL,
                Status = rs > 0 ? 0 : 1
            };
        }

        public Response<UserRole> Modify(Request<UserRole> request)
        {
            return new Response<UserRole>()
            {
                Data = db.Update(request.Obj),
                Times = request.BeginTime.Interval()
            };
        }

        public Page.Response<UserRole> List(Page.Request<UserRole> request)
        {
            return db.All(request);
        }

        public Response<List<UserRole>> List(Request<UserRole> request = null)
        {
            if (request == null) request = new Request<UserRole>();

            return new Response<List<UserRole>>()
            {
                Data = db.All<UserRole>().ToList(),
                Times = request.BeginTime.Interval()
            };
        }

        public Response<UserRole> GetById(int id)
        {
            return new Response<UserRole>()
            {
                Data = db.Find<UserRole>(x => x.Id == id)
            };
        }
    }
}
