
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

    public class CompanyService : IBaseService<Company>
    {
        private IUnitOfWorks db = GPSService.Instance(true);

        public Response<Company> Add(Request<Company> request)
        {
            return new Response<Company>()
            {
                Data = db.Add<Company>(request.Obj),
                Times = request.BeginTime.Interval()
            };
        }

        public Response Remove(int id)
        {
            var rs = db.Delete<Company>(x => x.Id == id);

            return new Response()
            {
                Message = rs > 0 ? QConst.DELETE_SUCCESS : QConst.DELETE_FAIL,
                Status = rs > 0 ? 0 : 1
            };
        }

        public Response<Company> Modify(Request<Company> request)
        {
            return new Response<Company>()
            {
                Data = db.Update(request.Obj),
                Times = request.BeginTime.Interval()
            };
        }

        public Page.Response<Company> List(Page.Request<Company> request)
        {
            return db.All(request);
        }

        public Response<List<Company>> List(Request<Company> request = null)
        {
            if (request == null) request = new Request<Company>();

            return new Response<List<Company>>()
            {
                Data = db.All<Company>().ToList(),
                Times = request.BeginTime.Interval()
            };
        }

        public Response<Company> GetById(int id)
        {
            return new Response<Company>()
            {
                Data = db.Find<Company>(x => x.Id == id)
            };
        }
    }
}
