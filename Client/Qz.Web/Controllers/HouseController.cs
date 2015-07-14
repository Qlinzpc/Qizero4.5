
namespace Qz.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;

    using Qz.DirectService.House;
    using Qz.Models;

    public class HouseController : Controller
    {
        private HouseService service = new HouseService();

        public ActionResult Add()
        {
            return View();
        }

        public JsonResult Create(House house = null)
        {
            house = new House()
            {
                CreateTime = DateTime.Now,
                HouseFloor = "11",
                HouseName = "House-1",
                BuildNo = "3519",
                HouseNo = "123456"
            };


            service.Add(house);

            return Json(house, JsonRequestBehavior.AllowGet);
        }

        public JsonResult List(int page = 1, int size = 10)
        {
            var list = service.List(page, size);

            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> ListAsync(int page = 1, int size = 10)
        {
            var list = await service.ListAsync(page, size);

            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}
