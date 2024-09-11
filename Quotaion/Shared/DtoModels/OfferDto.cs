using Quotaion.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quotaion.Shared.DtoModels
{
    public class OfferDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public int Customer_id { get; set; }
        [ForeignKey("Customer_id")]
        public Customer? Customer { get; set; }
        public int PeroudTime { get; set; }
        public int UserAdded { get; set; }

        public List<Offer>? OfferPrducts { get; set; }
    }
}
