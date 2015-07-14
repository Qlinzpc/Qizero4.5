
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

    public class DataCodeService : IBaseService<DataCode>
    {
        private IUnitOfWorks db = GPSService.Instance(true);

        public Response<DataCode> Add(Request<DataCode> request)
        {
            return new Response<DataCode>()
            {
                Data = db.Add<DataCode>(request.Obj),
                Times = request.BeginTime.Interval()
            };
        }

        public Response Remove(int id)
        {
            var rs = db.Delete<DataCode>(x => x.Id == id);

            return new Response()
            {
                Message = rs > 0 ? QConst.DELETE_SUCCESS : QConst.DELETE_FAIL,
                Status = rs > 0 ? 0 : 1
            };
        }

        public Response<DataCode> Modify(Request<DataCode> request)
        {
            return new Response<DataCode>()
            {
                Data = db.Update(request.Obj),
                Times = request.BeginTime.Interval()
            };
        }

        public Page.Response<DataCode> List(Page.Request<DataCode> request)
        {
            return db.All(request);
        }

        public Response<List<DataCode>> List(Request<DataCode> request = null)
        {
            if (request == null) request = new Request<DataCode>();

            return new Response<List<DataCode>>()
            {
                Data = db.All<DataCode>().ToList(),
                Times = request.BeginTime.Interval()
            };
        }

        public Response<DataCode> GetById(int id)
        {
            return new Response<DataCode>()
            {
                Data = db.Find<DataCode>(x => x.Id == id)
            };
        }
    }
}
