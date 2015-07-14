
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
    using System.Data.SqlClient;

    public class ButtonService : IBaseService<Button>
    {
        private IUnitOfWorks db = GPSService.Instance(true);

        public Response<Button> Add(Request<Button> request)
        {
            return new Response<Button>()
            {
                Data = db.Add<Button>(request.Obj),
                Times = request.BeginTime.Interval()
            };
        }

        public Response<List<Button>> Add(Request<List<Button>> request)
        {
            return new Response<List<Button>>()
            {
                Data = db.Add<Button>(request.Obj.AsEnumerable()).ToList(),
                Times = request.BeginTime.Interval()
            };
        }

        public Response Remove(int id)
        {
            var rs = db.Delete<Button>(x => x.Id == id);

            return new Response()
            {
                Message = rs > 0 ? QConst.DELETE_SUCCESS : QConst.DELETE_FAIL,
                Status = rs > 0 ? 0 : 1
            };
        }

        public Response<Button> Modify(Request<Button> request)
        {
            return new Response<Button>()
            {
                Data = db.Update(request.Obj),
                Times = request.BeginTime.Interval()
            };
        }

        public Page.Response<Button> List(Page.Request<Button> request)
        {
            return db.All(request);
        }

        public Response<List<Button>> List(Request<Button> request = null)
        {
            if (request == null) request = new Request<Button>();

            return new Response<List<Button>>()
            {
                Data = db.All<Button>().ToList(),
                Times = request.BeginTime.Interval()
            };
        }

        public Response<Button> GetById(int id)
        {
            return new Response<Button>()
            {
                Data = db.Find<Button>(x => x.Id == id)
            };
        }

        /// <summary>
        /// 根据模块 ( Module ) 获得授权模块按钮 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Response<object> GetButtonByModule(Request<Parameter.Button.ByModule> request)
        {
            var query = db.SqlQuery<Qz.GPS.ViewModel.Button>("exec proc_select_button_by_module @ModuleId ", new object[]{
                new SqlParameter("@ModuleId", request.Obj.ModuleId)
            });
            
            var button = query.ToList();

            return new Response<object>()
            {
                Data = button,
                Times = request.BeginTime.Interval()
            };
        }

        /// <summary>
        /// 根据模块 ( Module ) 和 角色 ( Role ) 获得授权模块按钮 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Response<object> GetButtonByModuleAndRole(Request<Parameter.Button.ByModuleAndRole> request)
        {
            var query = db.SqlQuery<Qz.GPS.ViewModel.Button>("exec proc_select_button_by_module_role @ModuleId, @RoleId ", new object[]{
                new SqlParameter("@ModuleId", request.Obj.ModuleId),
                new SqlParameter("@RoleId", request.Obj.RoleId)
            });

            var button = query.ToList();

            return new Response<object>()
            {
                Data = button,
                Times = request.BeginTime.Interval()
            };
        }

    }
}
