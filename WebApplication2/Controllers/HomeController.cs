using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Collections.Generic;
using WebApplication2.Models;
using Microsoft.Extensions.Logging;
using static WebApplication2.Models.AppDbContext;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApplication2.Service;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        private readonly CommonService _commonService;

        private readonly HomeService _service;

        public HomeController(AppDbContext context,HomeService service,CommonService commonService)
        {
            _context = context;
            _service = service;
            _commonService = commonService;
        }
        

        public IActionResult Home()
        {
            return View();
        }

        public ActionResult GetMessage()
        {
            return Json(new { message = "Greetings from server!" });
        }

        public ActionResult GetData()
        {
            return Json(new { data = _service.GetDataService() });
        }

        public ActionResult GetDataTodo()
        {
            return Json(new { data = _service.GetDataServiceTodo() });
        }

        [HttpPost]
        public ActionResult CreateData(HomeViewModel viewModel)
        {
            return Json(new { result = _service.CreateDataService(viewModel) });
        }

        [HttpPost]
        public ActionResult CreateDataTodo(HomeViewModel viewModel)
        {
            return Json(new { result = _service.CreateDataServiceTodo(viewModel) });
        }

        public ActionResult GetDataById(int id)
        {
            return Json(new { data = _service.GetDataByIdService(id) });
        }

        //目標:GetDataByIdTodoメソッドを修正し、Service/HomeService.csのGetDataByIdServiceTodoメソッドを呼び出して返されたデータをJson形式で返して下さい。
        //ヒント:GetDataByIdメソッドを参考にして下さい。

        public ActionResult GetDataByIdTodo(int id)
        {
            return Json(new { data = _service.GetDataByIdServiceTodo(id)});
        }

        [HttpPost]
        public ActionResult EditData(HomeViewModel viewModel) 
        {
            return Json(new { result = _service.EditDataService(viewModel) });
        }

        [HttpPost]
        public ActionResult EditDataTodo(HomeViewModel viewModel)
        {
            return Json(new { result = _service.EditDataServiceTodo(viewModel) });
        }
    }
}
