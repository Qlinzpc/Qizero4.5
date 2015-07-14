
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

    public class DepartmentService : IBaseService<Department>
    {
        private IUnitOfWorks db = GPSService.Instance(true);

        public Response<Department> Add(Request<Department> request)
        {
            return new Response<Department>()
            {
                Data = db.Add<Department>(request.Obj),
                Times = request.BeginTime.Interval()
            };
        }

        public Response<List<Department>> Add(Request<List<Department>> request)
        {
            return new Response<List<Department>>()
            {
                Data = db.Add<Department>(request.Obj.AsEnumerable()).ToList(),
                Times = request.BeginTime.Interval()
            };
        }

        public Response Remove(int id)
        {
            var rs = db.Delete<Department>(x => x.Id == id);

            return new Response()
            {
                Message = rs > 0 ? QConst.DELETE_SUCCESS : QConst.DELETE_FAIL,
                Status = rs > 0 ? 0 : 1
            };
        }

        public Response<Department> Modify(Request<Department> request)
        {
            return new Response<Department>()
            {
                Data = db.Update(request.Obj),
                Times = request.BeginTime.Interval()
            };
        }

        public Page.Response<Department> List(Page.Request<Department> request)
        {
            return db.All(request);
        }

        public Response<List<Department>> List(Request<Department> request = null)
        {
            if (request == null) request = new Request<Department>();

            return new Response<List<Department>>()
            {
                Data = db.All<Department>().ToList(),
                Times = request.BeginTime.Interval()
            };
        }

        public Response<Department> GetById(int id)
        {
            return new Response<Department>()
            {
                Data = db.Find<Department>(x => x.Id == id)
            };
        }
    }
}
