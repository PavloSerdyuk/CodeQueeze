using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIQuiz.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;

namespace APIQuiz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITasksManager _tasksManager;

        public TaskController(ITasksManager test)
        {
            _tasksManager = test;
        }
        //GET api/values
        [HttpGet]
        public ActionResult<TestTask> Get()
        {
            var res = new ObjectResult(JsonConvert.SerializeObject(_tasksManager.GetTask()));
            res.ContentTypes.Add(new MediaTypeHeaderValue("application/json"));
            return res;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<TestTask> Get(int id)
        {
            var task = new TestTask() {Description = "hgfsdsa", Id = id, Name = "hnfsdsa", SourceCode = "hdffs"};
            var res = new ObjectResult(JsonConvert.SerializeObject(task));
            res.ContentTypes.Add(new MediaTypeHeaderValue("application/json"));
            return res;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }



        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
