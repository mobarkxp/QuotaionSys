using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quotaion.Shared.Models

{
    public class OfferNumber
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public int Customer_id { get; set; }
        [ForeignKey("Customer_id")]
        public Customer? Customer { get; set; }
        public string? PeroudTime { get; set; }
        public int UserAdded { get; set; }

    }
}
