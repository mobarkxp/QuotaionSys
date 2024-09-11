using Quotaion.Shared.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quotaion.Shared.Models

{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="يرجا ادخال اسم العميل")]
        public string Name { get; set; }=string.Empty;
        
        public int Phone { get; set; }
        [EmailAddress(ErrorMessage ="يرجا ادخال ايميل صحيح")]
        public string?  Email { get; set; }
        public int? Vatnummber { get; set; }
       
        public string? Address { get; set; }
        public DateTime Date { get; set; }
        public int UserAddedId { get; set; }
        [ForeignKey("UserAddedId")]
        public Users? users { get; set; }

        
    }
}
