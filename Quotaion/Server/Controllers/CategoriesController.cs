using Microsoft.AspNetCore.Mvc;
using Quotaion.Server.Repository;
using Quotaion.Shared.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Quotaion.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMainRepository<Category>_repository;

        private readonly IMainRepository<LogClass> _LogRepository;

        public CategoriesController(IMainRepository<Category> repository, IMainRepository<LogClass> logRepository)
        {
            _repository = repository;
            _LogRepository = logRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_repository.GetAll());
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            if (id == 0) { return BadRequest(); }
            try
            {
                var category = _repository.GetById(id);
                if (category == null)
                {
                    return NotFound();
                }
                return Ok(category);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // POST api/<CategoriesController>
        [HttpPost]
      
        public IActionResult AddNew([FromBody] Category category)
        {
            if (category == null) { return NotFound("لا توجد بيانات مرسلة"); }
            if (ModelState.IsValid)
            {
                try
                {
                    var if_same_name = _repository.GetAll().Where(x => x.Name == category.Name).FirstOrDefault();
                    if (if_same_name != null)
                    {
                        ModelState.AddModelError("Error", "يوجد صنف بنفس الاسم ");
                        return BadRequest();
                    }
                    _repository.Add(category);
                    LogClass logClass = new LogClass
                    {
                        Description = " :تم اضافة صنف جديد" + category.Name,
                        UserAction = 1,

                    };
                    _LogRepository.Add(logClass);
                    return Ok(category);
                }
                catch (Exception ex)
                {
                    return BadRequest(ModelState);
                }
            }
            ModelState.AddModelError("Error", "في مشكلة");
            return BadRequest(ModelState);
        }

        // PUT api/<CategoriesController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Category category)
        {
            if (category == null) { return NotFound(); }
            if (id == 0) { return NotFound(); }
            try
            {
                _repository.Update(id, category);
                LogClass logClass = new LogClass
                {
                    Description = " :تم تعديل معلومات العميل " + category.Name,
                    UserAction = 1,

                };
                _LogRepository.Add(logClass);
                return Ok(category);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var category = _repository.GetById(id);
            _repository.Delete(id);
            LogClass logClass = new LogClass
            {
                Description = " :تم حذف الصنف " + category.Name,
                UserAction = 1,

            };
            _LogRepository.Add(logClass);
        }
    }
}
