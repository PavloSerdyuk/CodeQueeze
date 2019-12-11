using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
        private static int _tasksCounter = 0;

        public HomeController(IOptions<AppSettings> settings)
        {
            _settings = settings;
        }

        [HttpPost]
        public IActionResult TaskCreate(AdminViewModel data)
        {
            
            var model = new AdminViewModel();
            return RedirectToAction("Index");
        }

        public IActionResult Admin()
        {
            var model = new AdminViewModel();
            return View(model);
        }

        public IActionResult Index()
        {
            string path = _settings.Value.BaseUrlApi + "/api/task/";
            try
            {
                var tasks = JsonConvert.DeserializeObject<List<FullTask>>(GetObject(path).Result);
                ViewBag.MyTasks = tasks;
                _tasksCounter = tasks.Count;
            }
            catch (TimeoutException)
            {
                ViewBag.Message = "Timeout!";
                return View("Error");
            }
            catch (AggregateException)
            {
                ViewBag.Message = "Cannot reach API";
                return View("Error");
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
                return View("Error");
            }
            
            return View();
        }

        public IActionResult Task(int id)
        {
            if (id != _currentTask.Id)
            {
                string path = _settings.Value.BaseUrlApi + "/api/task/" + id;
                FullTask task;
                try
                {
                    task = JsonConvert.DeserializeObject<FullTask>(GetObject(path).Result);
                }
                catch (TimeoutException)
                {
                    ViewBag.Message = "API is thinking for too long";
                    return View("Error");
                }
                catch (AggregateException)
                {
                    ViewBag.Message = "Cannot reach API";
                    return View("Error");
                }
                catch (Exception e)
                {
                    ViewBag.Message = e.Message;
                    return View("Error");
                }

                _currentTask.Id = task.Id;
                _currentTask.ShortDescription = task.ShortDescription;
                _currentTask.FullDescription = task.FullDescription;
                _currentTask.Name = task.Name;
                var d = Directory.GetCurrentDirectory();
                _currentTask.Code = System.IO.File.ReadAllText(@".\wwwroot\txt\InitialCode.txt");
                _currentTask.Completed = false;
            }

            //ViewBag.CurrentTask = _currentTask;
            ViewBag.AlertClass = "";
            ViewBag.AlertText = "";

            return View(_currentTask);
        }
        
        public IActionResult Check(CurrentTask values)
        {
            _currentTask.Code = values.Code;

            string path = _settings.Value.BaseUrlApi + "/api/task";
            var obj = new CheckTaskRequest(){Code = _currentTask.Code, Id = _currentTask.Id};
            CheckTaskResponse resp;
            try
            {
                resp = JsonConvert.DeserializeObject<CheckTaskResponse>(PostObject(path, obj).Result);
            }
            catch (TimeoutException)
            {
                ViewBag.Message = "Timeout!";
                return View("Error");
            }
            catch (AggregateException)
            {
                ViewBag.Message = "Cannot reach API";
                return View("Error");
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
                return View("Error");
            }

            //ViewBag.CurrentTask = _currentTask;
            
            if (resp.Result)
            {
                ViewBag.AlertClass = "alert alert-success";
                ViewBag.AlertText = "Success";
                ViewBag.TasksCounter = _tasksCounter;
                _currentTask.Completed = true;
            }
            else
            {
                ViewBag.AlertClass = "alert alert-danger";
                ViewBag.AlertText = "Error";         
                _currentTask.Completed = false;
            }
            ViewBag.Message = resp.Message;
            return View("Task", _currentTask);
        }

        public IActionResult Congratulation()
        {
            return View();
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
