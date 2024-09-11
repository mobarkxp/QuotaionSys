using Quotaion.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quotaion.Shared.TemoModels
{
    public class QoutationDTO
    {
        public int CustomerId { get; set; }
        public string PeriodTime { get; set; }
        public List<Offer> QoutaionProducts { get; set; }
    }
}
