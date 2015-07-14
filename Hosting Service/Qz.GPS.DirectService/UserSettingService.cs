
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

    public class UserSettingService: IBaseService<UserSetting>
    {
        private IUnitOfWorks db = GPSService.Instance(true);

        public Response<UserSetting> Add(Request<UserSetting> request)
        {
            return new Response<UserSetting>()
            {
                Data = db.Add<UserSetting>(request.Obj),
                Times = request.BeginTime.Interval()
            };
        }

        public Response Remove(int id)
        {
            var rs = db.Delete<UserSetting>(x => x.Id == id);

            return new Response()
            {
                Message = rs > 0 ? QConst.DELETE_SUCCESS : QConst.DELETE_FAIL,
                Status = rs > 0 ? 0 : 1
            };
        }

        public Response<UserSetting> Modify(Request<UserSetting> request)
        {
            return new Response<UserSetting>()
            {
                Data = db.Update(request.Obj),
                Times = request.BeginTime.Interval()
            };
        }

        public Page.Response<UserSetting> List(Page.Request<UserSetting> request)
        {
            return db.All(request);
        }

        public Response<List<UserSetting>> List(Request<UserSetting> request = null)
        {
            if (request == null) request = new Request<UserSetting>();

            return new Response<List<UserSetting>>()
            {
                Data = db.All<UserSetting>().ToList(),
                Times = request.BeginTime.Interval()
            };
        }

        public Response<UserSetting> GetById(int id)
        {
            return new Response<UserSetting>()
            {
                Data = db.Find<UserSetting>(x => x.Id == id)
            };
        }
    }
}
