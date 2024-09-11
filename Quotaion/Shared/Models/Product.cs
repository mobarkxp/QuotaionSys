using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quotaion.Shared.Models

{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]

        public string Name { get; set; }=string.Empty;
        [Required]
        public string? CompanyName { get; set; } 
        public string? Code { get; set; }
        [Required]  
        public string Description { get; set; }=string.Empty;
        public int NormalPrice { get; set; }
        public string? Img { get; set; }
        public int Category_id { get; set; }
        [ForeignKey("Category_id")]
        public Category? Category { get; set; }

        public DateTime Date { get; set; }=DateTime.Now;
      
    }
}
