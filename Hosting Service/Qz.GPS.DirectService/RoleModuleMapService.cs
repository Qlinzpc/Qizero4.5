
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

    public class RoleModuleMapService : IBaseService<RoleModuleMap>
    {
        private IUnitOfWorks db = GPSService.Instance(true);

        public Response<RoleModuleMap> Add(Request<RoleModuleMap> request)
        {
            return new Response<RoleModuleMap>()
            {
                Data = db.Add<RoleModuleMap>(request.Obj),
                Times = request.BeginTime.Interval()
            };
        }

        public Response Remove(int id)
        {
            var rs = db.Delete<RoleModuleMap>(x => x.Id == id);

            return new Response()
            {
                Message = rs > 0 ? QConst.DELETE_SUCCESS : QConst.DELETE_FAIL,
                Status = rs > 0 ? 0 : 1
            };
        }

        public Response<RoleModuleMap> Modify(Request<RoleModuleMap> request)
        {
            return new Response<RoleModuleMap>()
            {
                Data = db.Update(request.Obj),
                Times = request.BeginTime.Interval()
            };
        }

        public Page.Response<RoleModuleMap> List(Page.Request<RoleModuleMap> request)
        {
            return db.All(request);
        }

        public Response<List<RoleModuleMap>> List(Request<RoleModuleMap> request = null)
        {
            if (request == null) request = new Request<RoleModuleMap>();

            return new Response<List<RoleModuleMap>>()
            {
                Data = db.All<RoleModuleMap>().ToList(),
                Times = request.BeginTime.Interval()
            };
        }

        public Response<RoleModuleMap> GetById(int id)
        {
            return new Response<RoleModuleMap>()
            {
                Data = db.Find<RoleModuleMap>(x => x.Id == id)
            };
        }

        public Response Add(Request<Parameter.RoleModule.Add> request)
        {
            var moduleIds = request.Obj.ModuleIds.Split(',').Where(x => !string.IsNullOrEmpty(x)).Distinct().ToArray();
            var rs = 0;
            var sql = "";
            var msg = "";

            if (request.Obj.RoleId > 0 && moduleIds.Length > 0)
            {
                var values = string.Format("({0}, {1})", request.Obj.RoleId, moduleIds[0]);

                for (int i = 1; i < moduleIds.Length; i++)
                {
                    values += "," + string.Format("({0}, {1})", request.Obj.RoleId, moduleIds[i]);
                }

                sql = string.Format(@"
DELETE FROM dbo.RoleModuleMap WHERE RoleId = {0} 

INSERT INTO dbo.RoleModuleMap
	( RoleId, ModuleId )
VALUES  
	{1}",
        request.Obj.RoleId,
        values);

                rs = db.SqlCommand(sql);
            }
            else if (request.Obj.RoleId > 0)
            {
                RemoveByRole(request.Obj.RoleId);
                rs = 1;
            }

            return new Response()
            {
                Message = rs > 0 ? QConst.INSERT_SUCCESS : QConst.INSERT_FAIL,
                Status = rs > 0 ? 0 : 1
            };
        }

        public void RemoveByRole(int roleId)
        {
            db.SqlCommand(string.Format(@"
DELETE FROM dbo.RoleModuleMap WHERE RoleId = {0} ", roleId));
        }

        public Response Remove(Request<Parameter.RoleModule.Remove> request)
        {
            var rs = db.SqlCommand(string.Format(@"
DELETE FROM dbo.RoleModuleMap WHERE RoleId = {0} AND ModuleId IN ({1})",
                 request.Obj.RoleId,
                 request.Obj.ModuleIds));

            return new Response()
            {
                Message = rs > 0 ? QConst.DELETE_SUCCESS : QConst.DELETE_FAIL,
                Status = rs > 0 ? 0 : 1
            };
        }
    }
}
