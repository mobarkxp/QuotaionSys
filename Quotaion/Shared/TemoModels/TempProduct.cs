using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quotaion.Shared.TemoModels
{
    public class TempProduct
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Img { get; set; }
        public double NewPrice { get; set; }
        public int Qountity { get; set; }
        public double Total { get; set; }

    }
}
