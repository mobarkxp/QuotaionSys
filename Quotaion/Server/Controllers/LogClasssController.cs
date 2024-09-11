using Microsoft.AspNetCore.Mvc;
using Quotaion.Server.Repository;
using Quotaion.Shared.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Quotaion.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LogClasssController : ControllerBase
    {
        private readonly IMainRepository<LogClass> _logclassRepository;

        public LogClasssController(IMainRepository<LogClass> LogclassRepository)
        {
            _logclassRepository = LogclassRepository;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_logclassRepository.GetAll().OrderByDescending(x=>x.Date));
        }

        // GET api/<LogClasssController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<LogClasssController>
        [HttpPost]
        public IActionResult AddNew([FromBody] LogClass logClass)
        {
            _logclassRepository.Add(logClass);
            return Ok(logClass);
        }

        // PUT api/<LogClasssController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LogClasssController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
