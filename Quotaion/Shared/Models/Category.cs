using System.ComponentModel.DataAnnotations;

namespace Quotaion.Shared.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="الرجاء ادخال اسم الصنف")]
        [StringLength(50)]
        public string Name { get; set; }=string.Empty;
        public string? Description { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

       // public virtual ICollection<Product>? Products { get; set; }
    }
}
