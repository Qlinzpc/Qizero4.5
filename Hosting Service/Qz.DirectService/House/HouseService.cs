
namespace Qz.DirectService.House
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Qz.Core.Infrastructure.Interface.UnitOfWorks;
    using Qz.Core.Entity;
    using Qz.UnitOfWorks;
    using Qz.Models;

    public class HouseService
    {
        IUnitOfWorks works = new UnitOfWorks<QlinContext>(new QlinContext());

        public bool Add(House house)
        {
            works.Add(house);

            return works.SaveChange() > 0;
        }

        public Response<Page.Response<House>> List(int page, int size)
        {
            try
            {
                var entity = works.All<House>(new Page.Request<House>()
                {
                    BeginTime = DateTime.Now,
                    PageIndex = page,
                    PageSize = size,
                    SortName = "Id",
                    Sort = Sorting.Desc,
                    Filter = (x => x.HouseName.Contains("House"))
                });

                return new Response<Page.Response<House>>()
                {
                    Data = entity,
                    Times = entity.Times
                };
            }
            catch (Exception ex)
            {
                return new Response<Page.Response<House>>()
                {
                    Message = ex.ToString(),
                    Status = 1
                };
            }
        }

        public async Task<Response<Page.Response<House>>> ListAsync(int page, int size)
        {
            try
            {
                var entity = await works.AllAsync<House>(new Page.Request<House>()
                {
                    BeginTime = DateTime.Now,
                    PageIndex = page,
                    PageSize = size,
                    SortName = "Id",
                    Sort = Sorting.Desc
                });

                return new Response<Page.Response<House>>
                {
                    Data = entity,
                    Times = entity.Times
                };
            }
            catch (Exception ex)
            {
                return new Response<Page.Response<House>>()
                {
                    Message = ex.ToString(),
                    Status = 1
                };
            }
        }

    }
}
