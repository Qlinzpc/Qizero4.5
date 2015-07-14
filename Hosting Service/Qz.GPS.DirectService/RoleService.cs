
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

    public class RoleService : IBaseService<Role>
    {
        private IUnitOfWorks db = GPSService.Instance(true);

        public Response<Role> Add(Request<Role> request)
        {
            var response = new Response<Role>();

            // 若角色名称已存在 
            if (ExistsRoleName(request.Obj.RoleName))
            {
                // 提示存在信息 
                response.Status = 1;
                response.Message = string.Format(@"角色名称: {0}, 已存在 !", request.Obj.RoleName);
            }
            else
            {
                // 添加角色 
                response.Data = db.Add<Role>(request.Obj);
            }

            response.Times = request.BeginTime.Interval();

            return response;
        }

        public Response<List<Role>> Add(Request<List<Role>> request)
        {
            return new Response<List<Role>>()
            {
                Data = db.Add<Role>(request.Obj.AsEnumerable()).ToList(),
                Times = request.BeginTime.Interval()
            };

        }

        public Response Remove(int id)
        {
            var rs = db.Delete<Role>(x => x.Id == id);

            return new Response()
            {
                Message = rs > 0 ? QConst.DELETE_SUCCESS : QConst.DELETE_FAIL,
                Status = rs > 0 ? 0 : 1
            };
        }

        public Response<Role> Modify(Request<Role> request)
        {
            var response = new Response<Role>();

            // 若角色名称已存在 
            if (ExistsRoleName(request.Obj.RoleName, request.Obj.Id))
            {
                // 提示存在信息 
                response.Status = 1;
                response.Message = string.Format(@"角色名称: {0}, 已存在 !", request.Obj.RoleName);
            }
            else
            {
                var sql = @"
UPDATE 
    dbo.Roles 
SET 
    RoleName = @RoleName,
    Remark = @Remark,
    SortCode = @SortCode,
    Enabled = @Enabled,
    ModifyDate = @ModifyDate,
    ModifyUserId = @ModifyUserId,
    ModifyUserName = @ModifyUserName
WHERE 
    Id = @Id";

                var parameters = new object[]{
                                    new SqlParameter("@Id", request.Obj.Id),
                                    new SqlParameter("@RoleName", request.Obj.RoleName),
                                    new SqlParameter("@Remark", request.Obj.Remark),
                                    new SqlParameter("@SortCode", request.Obj.SortCode),
                                    new SqlParameter("@Enabled", request.Obj.Enabled),
                                    new SqlParameter("@ModifyDate", request.Obj.ModifyDate),
                                    new SqlParameter("@ModifyUserId", request.Obj.ModifyUserId),
                                    new SqlParameter("@ModifyUserName", request.Obj.ModifyUserName),
                                };

                response.Status = db.SqlCommand(sql, parameters) > 0 ? 0 : 1;

                if (response.Status > 0)
                {
                    response.Message = "角色编辑失败 !";
                }

            }

            response.Times = request.BeginTime.Interval();

            return response;
        }

        public Page.Response<Role> List(Page.Request<Role> request)
        {
            return db.All(request);
        }

        public Response<List<Role>> List(Request<Role> request = null)
        {
            if (request == null) request = new Request<Role>();

            return new Response<List<Role>>()
            {
                Data = db.All<Role>().OrderBy(x => x.SortCode).ToList(),
                Times = request.BeginTime.Interval()
            };
        }

        public Response<Role> GetById(int id)
        {
            var response = new Response<Role>();

            var role = db.Find<Role>(x => x.Id == id);

            if (role == null || role.Id != id)
            {
                response.Status = 1;
                response.Message = "系统内无该角色 Id: " + id;
            }
            else
            {
                response.Data = new Role()
                {
                    Id = role.Id,
                    RoleName = role.RoleName,
                    Remark = role.Remark,
                    Enabled = role.Enabled,
                    SortCode = role.SortCode
                };
            }

            return response;
        }

        public Response<List<ViewModel.Tree>> GetRoleTree(Request<ViewModel.Tree> request)
        {
            var list = new List<ViewModel.Tree>();

            var roles = db.All<Role>().OrderBy(x => x.SortCode).ToList();

            foreach (var item in roles)
            {
                list.Add(new ViewModel.Tree()
                {
                    Id = item.Id,
                    ParentId = -1,
                    Name = item.RoleName
                });
            }

            var response = new Response<List<ViewModel.Tree>>()
            {
                Data = list,
                Times = request.BeginTime.Interval()
            };

            return response;
        }

        public bool ExistsRoleName(string name, int roleId = 0)
        {
            var role = db.All<Role>().Where(x => x.RoleName.Equals(name) && x.IsDelete.Equals(0)).FirstOrDefault();

            return (role != null && role.Id != roleId);
        }
    }
}
