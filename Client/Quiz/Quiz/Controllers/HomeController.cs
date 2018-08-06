using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Quiz.Models;
using FullTask = Quiz.Models.FullTask;

namespace Quiz.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOptions<AppSettings> _settings;
        private static CurrentTask _currentTask;
        private static int lastId = 0;

        public HomeController(IOptions<AppSettings> settings)
        {
            _settings = settings;
            _currentTask = new CurrentTask();
        }

        public IActionResult Index()
        {
            return View();
        }

        public static async Task<string> GetObject(string path)
        {
            var response = await HttpHelper.Get(path);
            var result = HttpHelper.ContentAsString(response);
            return result.Result;
        }

        public static async Task<string> PostObject(string path, object value)
        {
            var response = await HttpHelper.Post(path, value);
            var result = HttpHelper.ContentAsString(response);
            return result.Result;
        }

        public IActionResult Task()
        {

            string path = _settings.Value.BaseUrlApi + "/api/task" + "/" + (++lastId).ToString(); 
            FullTask task = JsonConvert.DeserializeObject<FullTask>(GetObject(path).Result);

            _currentTask.Id = task.Id;
            _currentTask.Description = task.Description;
            _currentTask.Name = task.Name;

            ViewBag.Description = task.Description;
            ViewBag.Name = task.Name;
            ViewBag.Id = task.Id;
            ViewBag.AlertClass = "";
            ViewBag.AlertText = "";

            return View();
        }
        
        public IActionResult Check(Quiz.Models.ReturnValues values)
        {
            _currentTask.Code = values.Code;

            string path = _settings.Value.BaseUrlApi + "/api/task";
            var obj = new CheckTaskRequest(){Code = values.Code, Id = values.Id};
            
            CheckTaskResponse resp = JsonConvert.DeserializeObject<CheckTaskResponse>(PostObject(path, obj).Result);

            if (resp.Result)
            {
                ViewBag.AlertClass = "alert alert-success";
                ViewBag.AlertText = "Success";
            }
            else
            {
                ViewBag.AlertClass = "alert alert-danger";
                ViewBag.AlertText = "Error";
                ViewBag.Message = resp.Message;
            }

            return View(_currentTask);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
