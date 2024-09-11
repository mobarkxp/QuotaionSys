using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quotaion.Server.Repository;
using Quotaion.Shared.DtoModels;
using Quotaion.Shared.Models;

namespace Quotaion.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHost;
        private readonly IMainRepository<Product> _repository;

        private readonly IMainRepository<LogClass> _LogRepository;

        public ProductsController(IWebHostEnvironment webHost,IMainRepository<Product> repository, IMainRepository<LogClass> logRepository)
        {
            _webHost = webHost;
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
                var Product = _repository.GetById(id);
                if (Product == null)
                {
                    return NotFound();
                }
                return Ok(Product);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // POST api/<CategoriesController>
        [HttpPost]

        public async Task<IActionResult> AddNew([FromBody] ProductDto ProductDto)
        {
            if (ProductDto == null) { return NotFound("لا توجد بيانات مرسلة"); }
            if (ModelState.IsValid)
            {
                try
                {
                    var if_same_name = _repository.GetAll().Where(x => x.Name == ProductDto.Name).FirstOrDefault();

                    if (if_same_name != null)
                    {
                        ModelState.AddModelError("Error", "يوجد منتج بنفس الاسم ");
                        return BadRequest();
                    }
                    string FileName = "";
                    if (ProductDto.ImageFile != null)
                    {
                        string FilePath = Path.Combine(_webHost.WebRootPath, "ProductsImg");
                        if (!Directory.Exists(FilePath))
                        {
                            Directory.CreateDirectory(FilePath);

                        }
                        FileName = Guid.NewGuid() + "_" + ProductDto.Img;
                        string imagepath = Path.Combine(FilePath, FileName);
                        await using var stream = new FileStream(imagepath, FileMode.Create);
                        stream.Write(ProductDto.ImageFile, 0, ProductDto.ImageFile.Length);
                    }

                    Product product = new Product
                    {
                        Name = ProductDto.Name,
                        CompanyName = ProductDto.CompanyName,
                        Description = ProductDto.Description,
                        Category_id = ProductDto.Category_id,
                        NormalPrice = ProductDto.NormalPrice,
                        Code = ProductDto.Code,
                        Img = FileName,
                        Date = DateTime.Now,
                          
                    };
                    _repository.Add(product);
                    LogClass logClass = new LogClass
                    {
                        Description = " :تم اضافة منتج جديد" + ProductDto.Name,
                        UserAction = 1,

                    };
                    _LogRepository.Add(logClass);
                    return Ok(ProductDto);
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
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] ProductDto ProductDto)
        {
            if (ProductDto == null) { return NotFound(); }
            if (id == 0) { return NotFound(); }
            try
            {
                var ChickHaveImageProduct = _repository.GetById(id);
                if (ChickHaveImageProduct == null) { return NotFound(); }
                string FileName = "";
                if (ProductDto.ImageFile != null)
                {
                    string FilePath = Path.Combine(_webHost.WebRootPath, "ProductsImg");
                    if (!Directory.Exists(FilePath))
                    {
                        Directory.CreateDirectory(FilePath);

                    }
                    FileName = Guid.NewGuid() + "_" + ProductDto.Img;
                    string imagepath = Path.Combine(FilePath, FileName);
                    await using var stream = new FileStream(imagepath, FileMode.Create);
                    stream.Write(ProductDto.ImageFile, 0, ProductDto.ImageFile.Length);
                    if (ChickHaveImageProduct != null)
                    {
                        string FilePath2 = Path.Combine(_webHost.WebRootPath, "ProductsImg");
                        string imagepath2 = Path.Combine(FilePath2, ChickHaveImageProduct.Img);
                        System.IO.File.Delete(imagepath);
                    }

                   
                    ChickHaveImageProduct.Img = FileName;
                     
                }
                else
                {
                    
                    ChickHaveImageProduct.Img = ChickHaveImageProduct.Img;
                }
                ChickHaveImageProduct.Name = ProductDto.Name;
                ChickHaveImageProduct.CompanyName = ProductDto.CompanyName;
                ChickHaveImageProduct.Description = ProductDto.Description;
                ChickHaveImageProduct.Category_id = ProductDto.Category_id;
                ChickHaveImageProduct.NormalPrice = ProductDto.NormalPrice;
                ChickHaveImageProduct.Code = ProductDto.Code;

                ChickHaveImageProduct.Date = DateTime.Now;

                _repository.Update(id, ChickHaveImageProduct);
                LogClass logClass = new LogClass
                {
                    Description = " :تم تعديل معلومات المنتج " + ChickHaveImageProduct.Name,
                    UserAction = 1,

                };
                _LogRepository.Add(logClass);
                return Ok(ChickHaveImageProduct);

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
            var Product = _repository.GetById(id);
            _repository.Delete(id);
            string FilePath = Path.Combine(_webHost.WebRootPath, "ProductsImg");
             string imagepath = Path.Combine(FilePath, Product.Img);
            System.IO.File.Delete(imagepath);

            LogClass logClass = new LogClass
            {
                Description = " :تم حذف المنتج " + Product.Name,
                UserAction = 1,

            };
            _LogRepository.Add(logClass);
        }
    }
}
