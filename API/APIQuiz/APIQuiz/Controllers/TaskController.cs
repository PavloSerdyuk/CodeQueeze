using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using APIQuiz.Test;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using TestRunner.Models;

namespace APIQuiz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IBlInterface _tasksManager;
        private ConfigurationPaths path;

        public TaskController(IBlInterface service)
        {   
            _tasksManager = service;
            path = new ConfigurationPaths()
            {
                CompilerPath = @"C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\Common7\Tools",
                FolderPath = @".\TestTask",
                //current - \API\APIQuiz\APIQuiz
                //CsFilePath = Directory.GetCurrentDirectory()
                CsFilePath = Directory.GetParent(@"..\").FullName
            };

        }
        //GET api/task
        [HttpGet]
        public ActionResult<IQuizTask> Get()
        {
            List<IQuizTask> allTasks = new List<IQuizTask>();
            int id = 1;
            
            while(_tasksManager.GetTask(id, path) != null)
                allTasks.Add(_tasksManager.GetTask(id++, path));

            var res = new ObjectResult(JsonConvert.SerializeObject(allTasks));
            res.ContentTypes.Add(new MediaTypeHeaderValue("application/json"));
            return res;
        }

        // GET api/task/5
        [HttpGet("{id}")]
        public ActionResult<IQuizTask> Get(int id)
        {
            var res = new ObjectResult(JsonConvert.SerializeObject(_tasksManager.GetTask(id, path)));
            res.ContentTypes.Add(new MediaTypeHeaderValue("application/json"));
            return res;
        }

        // POST api/task
        [HttpPost]
        public ActionResult<CheckTaskResponse> Post([FromBody] CheckTaskRequest value)
        {
            var res = new ObjectResult(JsonConvert.SerializeObject(_tasksManager.CheckCode(value, path)));
            res.ContentTypes.Add(new MediaTypeHeaderValue("application/json"));
            return res;
        }



        // PUT api/task/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/task/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
