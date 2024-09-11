using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quotaion.Shared.DtoModels
{
    public class ProductDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? CompanyName { get; set; }
        public string? Code { get; set; }
        [Required]
        public string Description { get; set; } = string.Empty;
        public int NormalPrice { get; set; }
        public byte[]? ImageFile { get; set; }
        public string? Img { get; set; }
        public int Category_id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
