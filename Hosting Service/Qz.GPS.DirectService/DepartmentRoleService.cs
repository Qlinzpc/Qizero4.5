
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

    public class DepartmentRoleService : IBaseService<DepartmentRole>
    {
        private IUnitOfWorks db = GPSService.Instance(true);

        public Response<DepartmentRole> Add(Request<DepartmentRole> request)
        {
            return new Response<DepartmentRole>()
            {
                Data = db.Add<DepartmentRole>(request.Obj),
                Times = request.BeginTime.Interval()
            };
        }

        public Response Remove(int id)
        {
            var rs = db.Delete<DepartmentRole>(x => x.Id == id);

            return new Response()
            {
                Message = rs > 0 ? QConst.DELETE_SUCCESS : QConst.DELETE_FAIL,
                Status = rs > 0 ? 0 : 1
            };
        }

        public Response<DepartmentRole> Modify(Request<DepartmentRole> request)
        {
            return new Response<DepartmentRole>()
            {
                Data = db.Update(request.Obj),
                Times = request.BeginTime.Interval()
            };
        }

        public Page.Response<DepartmentRole> List(Page.Request<DepartmentRole> request)
        {
            return db.All(request);
        }

        public Response<List<DepartmentRole>> List(Request<DepartmentRole> request = null)
        {
            if (request == null) request = new Request<DepartmentRole>();

            return new Response<List<DepartmentRole>>()
            {
                Data = db.All<DepartmentRole>().ToList(),
                Times = request.BeginTime.Interval()
            };
        }

        public Response<DepartmentRole> GetById(int id)
        {
            return new Response<DepartmentRole>()
            {
                Data = db.Find<DepartmentRole>(x => x.Id == id)
            };
        }
    }
}
