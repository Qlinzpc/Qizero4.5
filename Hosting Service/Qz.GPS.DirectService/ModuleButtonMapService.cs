
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
    using System.Linq.Expressions;
    using System.Data.SqlClient;

    public class ModuleButtonMapService : IBaseService<ModuleButtonMap>
    {
        private IUnitOfWorks db = GPSService.Instance(true);

        public Response<ModuleButtonMap> Add(Request<ModuleButtonMap> request)
        {
            return new Response<ModuleButtonMap>()
            {
                Data = db.Add<ModuleButtonMap>(request.Obj),
                Times = request.BeginTime.Interval()
            };
        }

        public Response Remove(int id)
        {
            var rs = db.Delete<ModuleButtonMap>(x => x.Id == id);

            return new Response()
            {
                Message = rs > 0 ? QConst.DELETE_SUCCESS : QConst.DELETE_FAIL,
                Status = rs > 0 ? 0 : 1
            };
        }

        public Response<ModuleButtonMap> Modify(Request<ModuleButtonMap> request)
        {
            return new Response<ModuleButtonMap>()
            {
                Data = db.Update(request.Obj),
                Times = request.BeginTime.Interval()
            };
        }

        public Page.Response<ModuleButtonMap> List(Page.Request<ModuleButtonMap> request)
        {
            return db.All(request);
        }

        public Response<List<ModuleButtonMap>> List(Request<ModuleButtonMap> request = null)
        {
            if (request == null) request = new Request<ModuleButtonMap>();

            return new Response<List<ModuleButtonMap>>()
            {
                Data = db.All<ModuleButtonMap>().ToList(),
                Times = request.BeginTime.Interval()
            };
        }

        public Response<ModuleButtonMap> GetById(int id)
        {
            return new Response<ModuleButtonMap>()
            {
                Data = db.Find<ModuleButtonMap>(x => x.Id == id)
            };
        }

        public Response Add(Request<Parameter.ModuleButton.Add> request)
        {
            var btnIds = request.Obj.ButtonIds.Split(',');
            var rs = 0;

            if (request.Obj.ModuleId > 0 && btnIds.Length > 0)
            {
                var values = string.Format("({0}, {1})", request.Obj.ModuleId, btnIds[0]);

                for (int i = 1; i < btnIds.Length; i++)
                {
                    values +="," + string.Format("({0}, {1})", request.Obj.ModuleId, btnIds[i]);
                }

                rs = db.SqlCommand(string.Format(@"
INSERT INTO dbo.ModuleButtonMap
	( ModuleId, ButtonId )
VALUES  
	{0}", values));

            }

            return new Response()
            {
                Message = rs > 0 ? QConst.INSERT_SUCCESS : QConst.INSERT_FAIL,
                Status = rs > 0 ? 0 : 1
            };
        }

        public Response Remove(Request<Parameter.ModuleButton.Remove> request)
        {
            var rs = db.SqlCommand(string.Format(@"
DELETE FROM dbo.ModuleButtonMap WHERE ModuleId = {0} AND ButtonId IN ({1})",
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
