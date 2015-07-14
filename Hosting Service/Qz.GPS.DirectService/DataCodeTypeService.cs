
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

    public class DataCodeTypeService : IBaseService<DataCodeType>
    {
        private IUnitOfWorks db = GPSService.Instance(true);

        public Response<DataCodeType> Add(Request<DataCodeType> request)
        {
            return new Response<DataCodeType>()
            {
                Data = db.Add<DataCodeType>(request.Obj),
                Times = request.BeginTime.Interval()
            };
        }

        public Response Remove(int id)
        {
            var rs = db.Delete<DataCodeType>(x => x.Id == id);

            return new Response()
            {
                Message = rs > 0 ? QConst.DELETE_SUCCESS : QConst.DELETE_FAIL,
                Status = rs > 0 ? 0 : 1
            };
        }

        public Response<DataCodeType> Modify(Request<DataCodeType> request)
        {
            return new Response<DataCodeType>()
            {
                Data = db.Update(request.Obj),
                Times = request.BeginTime.Interval()
            };
        }

        public Page.Response<DataCodeType> List(Page.Request<DataCodeType> request)
        {
            return db.All(request);
        }

        public Response<List<DataCodeType>> List(Request<DataCodeType> request = null)
        {
            if (request == null) request = new Request<DataCodeType>();

            return new Response<List<DataCodeType>>()
            {
                Data = db.All<DataCodeType>().ToList(),
                Times = request.BeginTime.Interval()
            };
        }

        public Response<DataCodeType> GetById(int id)
        {
            return new Response<DataCodeType>()
            {
                Data = db.Find<DataCodeType>(x => x.Id == id)
            };
        }
    }
}
