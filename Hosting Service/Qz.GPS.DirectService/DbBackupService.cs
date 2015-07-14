
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

    public class DbBackupService : IBaseService<DbBackup>
    {
        private IUnitOfWorks db = GPSService.Instance(true);

        public Response<DbBackup> Add(Request<DbBackup> request)
        {
            return new Response<DbBackup>()
            {
                Data = db.Add<DbBackup>(request.Obj),
                Times = request.BeginTime.Interval()
            };
        }

        public Response Remove(int id)
        {
            var rs = db.Delete<DbBackup>(x => x.Id == id);

            return new Response()
            {
                Message = rs > 0 ? QConst.DELETE_SUCCESS : QConst.DELETE_FAIL,
                Status = rs > 0 ? 0 : 1
            };
        }

        public Response<DbBackup> Modify(Request<DbBackup> request)
        {
            return new Response<DbBackup>()
            {
                Data = db.Update(request.Obj),
                Times = request.BeginTime.Interval()
            };
        }

        public Page.Response<DbBackup> List(Page.Request<DbBackup> request)
        {
            return db.All(request);
        }

        public Response<List<DbBackup>> List(Request<DbBackup> request = null)
        {
            if (request == null) request = new Request<DbBackup>();

            return new Response<List<DbBackup>>()
            {
                Data = db.All<DbBackup>().ToList(),
                Times = request.BeginTime.Interval()
            };
        }

        public Response<DbBackup> GetById(int id)
        {
            return new Response<DbBackup>()
            {
                Data = db.Find<DbBackup>(x => x.Id == id)
            };
        }
    }
}
