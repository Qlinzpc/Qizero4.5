
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
    using System.Transactions;
    using System.Data.SqlClient;

    public class UserService : IBaseService<User>
    {
        private IUnitOfWorks ownDb = GPSService.Instance(true);
        private IUnitOfWorks db = GPSService.Instance();

        public Response<User> Add(Request<User> request)
        {
            // 对密码进行 加密 
            request.Obj.Password = QSecurity.DESEncrypt(request.Obj.Password, QConst.SECURITY_KEY);

            return new Response<User>()
            {
                Data = ownDb.Add<User>(request.Obj),
                Times = request.BeginTime.Interval()
            };
        }

        public Response Remove(int id)
        {
            var rs = ownDb.Delete<User>(x => x.Id == id);

            return new Response()
            {
                Message = rs > 0 ? QConst.DELETE_SUCCESS : QConst.DELETE_FAIL,
                Status = rs > 0 ? 0 : 1
            };
        }

        public Response<User> Modify(Request<User> request)
        {
            return new Response<User>()
            {
                Data = ownDb.Update(request.Obj),
                Times = request.BeginTime.Interval()
            };
        }

        public Page.Response<User> List(Page.Request<User> request)
        {
            return ownDb.All(request);
        }

        public Response<List<User>> List(Request<User> request = null)
        {
            if (request == null) request = new Request<User>();

            return new Response<List<User>>()
            {
                Data = ownDb.All<User>().ToList(),
                Times = request.BeginTime.Interval()
            };
        }

        public Response<User> GetById(int id)
        {
            return new Response<User>()
            {
                Data = ownDb.Find<User>(x => x.Id == id)
            };
        }

        public ViewModel.User GetUserByAccountAndPassword(Request<DirectService.Parameter.User.ByAccountAndPassword> request)
        {
            var query = db.SqlQuery<ViewModel.User>("exec proc_login @Account, @Password", new object[] { 
                new SqlParameter("@Account", request.Obj.Account),
                new SqlParameter("@Password", request.Obj.Password)
            });

            var user = query.FirstOrDefault();

            return user;
        }

        public Response<ViewModel.User> Login(Request<Parameter.User.Login> request, LoginLog log)
        {
            // 1. 验证数据有效性 

            // 2.0 密码加密 
            request.Obj.Password = QSecurity.DESEncrypt(request.Obj.Password, QConst.SECURITY_KEY);

            // 2.1 登录 
            var user = GetUserByAccountAndPassword(new Request<Parameter.User.ByAccountAndPassword>()
            {
                Obj = new Parameter.User.ByAccountAndPassword()
                {
                    Account = request.Obj.Account,
                    Password = request.Obj.Password
                }
            });

            var response = new Response<ViewModel.User>();

            if (user == null || user.Id == 0)
            {
                response.Status = 1;
                response.Message = QConst.LOGIN_FAIL;
            }

            if (response.Status == 0)
            {
                try
                {
                    // 3. LoginLog 记录 
                    db.Add<LoginLog>(new LoginLog() {
                            LoginDate = DateTime.Now,
                            UserId = user.Id,
                            UserName = user.UserName,
                            HostIP = log.HostIP,
                            HostName = log.HostName,
                            LoginMsg = log.LoginMsg
                        });

                    db.SaveChange();
                }
                catch (Exception ex)
                {
                    response.Status = 1;
                    response.Message = ex.Message;
                }

            }

            response.Data = user;
            response.Times = request.BeginTime.Interval();

            return response;
        }

        private void Login(User user)
        {

        }

        public bool SignOut(User user)
        {
            User modifyUser = new User();
            modifyUser.Id = user.Id;
            modifyUser.Online = 0;

            var response = Modify(new Request<User>()
            {
                Obj = modifyUser
            });

            return response.Data.Online == 0;
        }
    }
}
