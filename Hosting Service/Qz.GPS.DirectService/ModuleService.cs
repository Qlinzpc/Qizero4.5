
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

    public class ModuleService : IBaseService<Module>
    {

        private IUnitOfWorks db = GPSService.Instance(true);

        public Response<Module> Add(Request<Module> request)
        {
            return new Response<Module>()
            {
                Data = db.Add<Module>(request.Obj),
                Times = request.BeginTime.Interval()
            };
        }

        public Response<List<Module>> Add(Request<List<Module>> request)
        {
            return new Response<List<Module>>()
            {
                Data = db.Add<Module>(request.Obj.AsEnumerable()).ToList(),
                Times = request.BeginTime.Interval()
            };
        }

        public Response Remove(int id)
        {
            var rs = db.Delete<Module>(x => x.Id == id);

            return new Response()
            {
                Message = rs > 0 ? QConst.DELETE_SUCCESS : QConst.DELETE_FAIL,
                Status = rs > 0 ? 0 : 1
            };
        }

        public Response<Module> Modify(Request<Module> request)
        {
            return new Response<Module>()
            {
                Data = db.Update(request.Obj),
                Times = request.BeginTime.Interval()
            };
        }

        public Page.Response<Module> List(Page.Request<Module> request)
        {
            return db.All(request);
        }

        public Response<List<Module>> List(Request<Module> request = null)
        {
            if (request == null) request = new Request<Module>();

            return new Response<List<Module>>()
            {
                Data = db.All<Module>().ToList(),
                Times = request.BeginTime.Interval()
            };
        }

        public Response<Module> GetById(int id)
        {
            return new Response<Module>()
            {
                Data = db.Find<Module>(x => x.Id == id)
            };
        }

        /// <summary>
        /// 根据用户 ( User ) 获得模块 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Response<List<ViewModel.Module>> GetModuleByUser(Request<Parameter.Module.ByUser> request)
        {
            var module = db.SqlQuery<Qz.GPS.ViewModel.Module>("exec proc_select_module_by_user @UserId, @ApplicationId, @ParentId ", new object[] { 
                new SqlParameter("@UserId", request.Obj.UserId),
                new SqlParameter("@ApplicationId", request.Obj.ApplicationId),
                new SqlParameter("@ParentId", request.Obj.ParentId)
            }).ToList();

            var response = new Response<List<ViewModel.Module>>()
            {
                Data = module,
                Times = request.BeginTime.Interval()
            };

            return response;
        }

        /// <summary>
        /// 根据角色 ( Role ) 获得模块 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Response<List<ViewModel.Module>> GetModuleByRole(Request<Parameter.Module.ByRole> request)
        {
            var module = db.SqlQuery<Qz.GPS.ViewModel.Module>("exec proc_select_module_by_role @RoleId ", new object[] { 
                new SqlParameter("@RoleId", request.Obj.RoleId)
            }).ToList();

            var response = new Response<List<ViewModel.Module>>()
            {
                Data = module,
                Times = request.BeginTime.Interval()
            };

            return response;
        }

        public Response<List<ViewModel.Tree>> GetModuleTree(Request<ViewModel.Tree> request)
        {
            var list = new List<ViewModel.Tree>();

            var applications = db.All<Application>().OrderBy(x=>x.SortCode).ToList();

            foreach (var item in applications)
            {
                list.Add(new ViewModel.Tree()
                {
                    Id = item.Id,
                    Name = item.Name,
                    ParentId = -1
                });
            }

            var modules = db.Where<Module>(x =>x.IsDelete.Equals(0)).OrderBy(x=>x.SortCode).ToList();

            foreach (var item in modules)
            {
                list.Add(new ViewModel.Tree()
                {
                    Id = item.Id,
                    Name = item.Name,
                    ParentId = item.ParentId,
                    SubCount = db.Where<Module>(x => x.IsDelete.Equals(0) && x.ParentId.Equals(item.Id)).Count()
                });
            }

            var response = new Response<List<ViewModel.Tree>>()
            {
                Data = list,
                Times = request.BeginTime.Interval()
            };

            return response;
        }

    }
}

