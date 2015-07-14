
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

    public class RoleModuleColumnMapService : IBaseService<RoleModuleColumnMap>
    {
        private IUnitOfWorks db = GPSService.Instance(true);

        public Response<RoleModuleColumnMap> Add(Request<RoleModuleColumnMap> request)
        {
            return new Response<RoleModuleColumnMap>()
            {
                Data = db.Add<RoleModuleColumnMap>(request.Obj),
                Times = request.BeginTime.Interval()
            };
        }

        public Response Remove(int id)
        {
            var rs = db.Delete<RoleModuleColumnMap>(x => x.Id == id);

            return new Response()
            {
                Message = rs > 0 ? QConst.DELETE_SUCCESS : QConst.DELETE_FAIL,
                Status = rs > 0 ? 0 : 1
            };
        }

        public Response<RoleModuleColumnMap> Modify(Request<RoleModuleColumnMap> request)
        {
            return new Response<RoleModuleColumnMap>()
            {
                Data = db.Update(request.Obj),
                Times = request.BeginTime.Interval()
            };
        }

        public Page.Response<RoleModuleColumnMap> List(Page.Request<RoleModuleColumnMap> request)
        {
            return db.All(request);
        }

        public Response<List<RoleModuleColumnMap>> List(Request<RoleModuleColumnMap> request = null)
        {
            if (request == null) request = new Request<RoleModuleColumnMap>();

            return new Response<List<RoleModuleColumnMap>>()
            {
                Data = db.All<RoleModuleColumnMap>().ToList(),
                Times = request.BeginTime.Interval()
            };
        }

        public Response<RoleModuleColumnMap> GetById(int id)
        {
            return new Response<RoleModuleColumnMap>()
            {
                Data = db.Find<RoleModuleColumnMap>(x => x.Id == id)
            };
        }
    }
}
