using Microsoft.AspNetCore.Mvc;
using Quotaion.Server.Repository;
using Quotaion.Shared.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Quotaion.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMainRepository<Customer> _repository;
        private readonly IMainRepository<LogClass> _logRepository;

        public CustomersController(IMainRepository<Customer> repository,IMainRepository<LogClass> logRepository )
        {
            _repository = repository;
            _logRepository = logRepository;
        }
        // GET: api/<CustomersController>
        [HttpGet]
        public IActionResult GetAll()
        {

            return Ok(_repository.GetAll());
        }

        // GET api/<CustomersController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            if (id == 0) { return BadRequest(); }
            try
            {
                var customer = _repository.GetById(id);
                if(customer == null)
                {
                    return NotFound();
                }
                return Ok(customer);

            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }
            // POST api/<CustomersController>
            [HttpPost]
        public IActionResult AddNew([FromBody] Customer customer)
        {
            if (customer == null) { return NotFound("لا توجد بيانات مرسلة"); }
            if(ModelState.IsValid)
            {
                try
                {
                    var if_same_name=_repository.GetAll().Where(x=>x.Name == customer.Name).FirstOrDefault();
                    if(if_same_name != null)
                    {
                        ModelState.AddModelError("Error", "يوجد عميل بنفس الاسم ");
                        return BadRequest();
                    }
                    _repository.Add(customer);
                    LogClass logClass = new LogClass
                    {
                         Description=" :تم اضافة عميل جديد"+customer.Name,
                         UserAction=1,
                           
                    };
                    _logRepository.Add(logClass);
                    return Ok(customer);
                }catch (Exception ex)
                {
                    return BadRequest(ModelState);
                }
            }
            ModelState.AddModelError("Error", "في مشكلة");
            return BadRequest(ModelState);
        }

        // PUT api/<CustomersController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Customer customer)
        {
            if (customer == null) { return NotFound(); }
           if(id == 0) { return NotFound(); }
                try
                {
                 _repository.Update(id,customer);
                LogClass logClass = new LogClass
                {
                    Description = " :تم تعديل معلومات العميل " + customer.Name,
                    UserAction = 1,

                };
                _logRepository.Add(logClass);
                return Ok(customer);

                }catch (Exception ex)
                {
                return BadRequest(ex.Message);
            }
           
          
        }

        // DELETE api/<CustomersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var Customer=_repository.GetById(id);
            _repository.Delete(id);
            LogClass logClass = new LogClass
            {
                Description = " :تم حذف العميل " + Customer.Name,
                UserAction = 1,

            };
            _logRepository.Add(logClass);
        }
    }
}
