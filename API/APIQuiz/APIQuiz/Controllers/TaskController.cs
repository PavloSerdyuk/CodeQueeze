using System;
using System.Collections.Generic;
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

        public TaskController(IBlInterface service)
        {
            _tasksManager = service;
        }
        //GET api/task
        //id 1!!!!!!!!!!!!
        [HttpGet]
        public ActionResult<IQuizTask> Get()
        {
            var res = new ObjectResult(JsonConvert.SerializeObject(_tasksManager.GetTask(1)));
            res.ContentTypes.Add(new MediaTypeHeaderValue("application/json"));
            return res;
        }

        // GET api/task/5
        [HttpGet("{id}")]
        public ActionResult<IQuizTask> Get(int id)
        {
            var res = new ObjectResult(JsonConvert.SerializeObject(_tasksManager.GetTask(id)));
            res.ContentTypes.Add(new MediaTypeHeaderValue("application/json"));
            return res;
        }

        // POST api/task
        [HttpPost]
        public CheckTaskResponse Post([FromBody] string request)
        {
            var result = JsonConvert.DeserializeObject<CheckTaskRequest>(request);
            
            return _tasksManager.CheckCode(result); 
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
