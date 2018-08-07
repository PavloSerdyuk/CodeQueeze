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
        private static CurrentTask _currentTask = new CurrentTask();

        public HomeController(IOptions<AppSettings> settings)
        {
            _settings = settings;
        }

        public IActionResult Index()
        {
            string path = _settings.Value.BaseUrlApi + "/api/task/";
            var tasks = JsonConvert.DeserializeObject<List<FullTask>>(GetObject(path).Result);
            ViewBag.MyTasks = tasks;
            return View();
        }

        public IActionResult Task(int id)
        {
            if (id != _currentTask.Id)
            {
                string path = _settings.Value.BaseUrlApi + "/api/task/" + id;
                FullTask task = JsonConvert.DeserializeObject<FullTask>(GetObject(path).Result);

                _currentTask.Id = task.Id;
                _currentTask.ShortDescription = task.ShortDescription;
                _currentTask.FullDescription = task.FullDescription;
                _currentTask.Name = task.Name;
                _currentTask.Completed = false;
            }

            ViewBag.CurrentTask = _currentTask;
            ViewBag.AlertClass = "";
            ViewBag.AlertText = "";

            return View();
        }
        
        public IActionResult Check(Quiz.Models.ReturnValues values)
        {
            _currentTask.Code = values.Code;

            string path = _settings.Value.BaseUrlApi + "/api/task";
            var obj = new CheckTaskRequest(){Code = _currentTask.Code, Id = _currentTask.Id};
            
            CheckTaskResponse resp = JsonConvert.DeserializeObject<CheckTaskResponse>(PostObject(path, obj).Result);

            ViewBag.CurrentTask = _currentTask;
            
            if (resp.Result)
            {
                ViewBag.AlertClass = "alert alert-success";
                ViewBag.AlertText = "Success";
                _currentTask.Completed = true;
            }
            else
            {
                ViewBag.AlertClass = "alert alert-danger";
                ViewBag.AlertText = "Error";
                ViewBag.Message = resp.Message;
                _currentTask.Completed = false;
            }

            return View("Task");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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

    }
}
