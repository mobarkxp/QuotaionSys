using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quotaion.Shared.Models

{
    public class Offer
    {
        [Key]
        public int Id { get; set; }
        public int Offer_id { get; set; }
        [ForeignKey("Offer_id")]
        public OfferNumber? OfferNumber { get; set; }
        
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product? product { get; set; }

        public double NewPrice { get; set; }
        public int Quntity { get; set; }
       
      

    }
}
