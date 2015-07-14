
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

    public class RoleModuleButtonMapService : IBaseService<RoleModuleButtonMap>
    {
        private IUnitOfWorks db = GPSService.Instance(true);

        public Response<RoleModuleButtonMap> Add(Request<RoleModuleButtonMap> request)
        {
            return new Response<RoleModuleButtonMap>()
            {
                Data = db.Add<RoleModuleButtonMap>(request.Obj),
                Times = request.BeginTime.Interval()
            };
        }

        public Response Remove(int id)
        {
            var rs = db.Delete<RoleModuleButtonMap>(x => x.Id == id);

            return new Response()
            {
                Message = rs > 0 ? QConst.DELETE_SUCCESS : QConst.DELETE_FAIL,
                Status = rs > 0 ? 0 : 1
            };
        }

        public Response<RoleModuleButtonMap> Modify(Request<RoleModuleButtonMap> request)
        {
            return new Response<RoleModuleButtonMap>()
            {
                Data = db.Update(request.Obj),
                Times = request.BeginTime.Interval()
            };
        }

        public Page.Response<RoleModuleButtonMap> List(Page.Request<RoleModuleButtonMap> request)
        {
            return db.All(request);
        }

        public Response<List<RoleModuleButtonMap>> List(Request<RoleModuleButtonMap> request = null)
        {
            if (request == null) request = new Request<RoleModuleButtonMap>();

            return new Response<List<RoleModuleButtonMap>>()
            {
                Data = db.All<RoleModuleButtonMap>().ToList(),
                Times = request.BeginTime.Interval()
            };
        }

        public Response<RoleModuleButtonMap> GetById(int id)
        {
            return new Response<RoleModuleButtonMap>()
            {
                Data = db.Find<RoleModuleButtonMap>(x => x.Id == id)
            };
        }

        public Response Add(Request<Parameter.RoleModuleButton.Add> request)
        {
            var btnIds = request.Obj.ButtonIds.Split(',').Where(x=> !string.IsNullOrEmpty(x)).Distinct().ToArray();
            var rs = 0;

            if (request.Obj.RoleId > 0 && request.Obj.ModuleId > 0 && btnIds.Length > 0)
            {
                var values = string.Format("({0}, {1}, {2})", request.Obj.RoleId, request.Obj.ModuleId, btnIds[0]);

                for (int i = 1; i < btnIds.Length; i++)
                {
                    values += "," + string.Format("({0}, {1}, {2})", request.Obj.RoleId, request.Obj.ModuleId, btnIds[i]);
                }

                rs = db.SqlCommand(string.Format(@"
INSERT INTO dbo.RoleModuleButtonMap
	( RoleId, ModuleId, ButtonId )
VALUES  
	{0}", values));

            }

            return new Response()
            {
                Message = rs > 0 ? QConst.INSERT_SUCCESS : QConst.INSERT_FAIL,
                Status = rs > 0 ? 0 : 1
            };
        }

        public Response Remove(Request<Parameter.RoleModuleButton.Remove> request)
        {
            var rs = db.SqlCommand(string.Format(@"
DELETE FROM dbo.RoleModuleButtonMap WHERE RoleId = {0} AND ModuleId = {1} AND ButtonId IN ({2})",
                 request.Obj.RoleId,
                 request.Obj.ModuleId,
                 request.Obj.ButtonIds));

            return new Response()
            {
                Message = rs > 0 ? QConst.DELETE_SUCCESS : QConst.DELETE_FAIL,
                Status = rs > 0 ? 0 : 1
            };
        }
    }
}
