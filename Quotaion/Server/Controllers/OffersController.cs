using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quotaion.Server.Repository;
using Quotaion.Shared.DtoModels;
using Quotaion.Shared.Models;

namespace Quotaion.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OffersController : ControllerBase
    {
        private readonly IMainRepository<Offer> _offerRepository;
        private readonly IMainRepository<OfferNumber> _offernumberRepository;
        private readonly IMainRepository<LogClass> _LogRepository;

    

        public OffersController(IMainRepository<LogClass> logRepository,IMainRepository<Offer> offerRepository , IMainRepository<OfferNumber> offernumberRepository)
        {
            _offerRepository = offerRepository;
            _offernumberRepository = offernumberRepository;
            _LogRepository = logRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {          
            return Ok(_offernumberRepository.GetAll());
        }
        [HttpPost]

        public IActionResult AddNew([FromBody] OfferNumber OfferNumber)
        {
            if (OfferNumber == null) { return NotFound("لا توجد بيانات مرسلة"); }
            if (ModelState.IsValid)
            {
                try
                {
                   
                    _offernumberRepository.Add(OfferNumber);
                    LogClass logClass = new LogClass
                    {
                        Description = " :تم اضافة  عرض ل " + OfferNumber.Customer_id,
                        UserAction = 1,

                    };
                    _LogRepository.Add(logClass);
                    return Ok(OfferNumber);
                }
                catch (Exception ex)
                {
                    return BadRequest(ModelState);
                }
            }
            ModelState.AddModelError("Error", "في مشكلة");
            return BadRequest(ModelState);
        }
    }
}
