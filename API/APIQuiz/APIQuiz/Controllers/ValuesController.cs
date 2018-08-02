using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIQuiz.Models;
using Microsoft.AspNetCore.Mvc;

namespace APIQuiz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ITasksManager _tasksManager;

        public ValuesController(TestTasksManager test)
        {
            _tasksManager = test;
        }
        //GET api/values
        [HttpGet]
        public ActionResult<TestTask> Get()
        {
            return new ObjectResult(_tasksManager.GetTask());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
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
