using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quotaion.Shared.Models
{
    public class LogClass
    {
        [Key]
        public int Id { get; set; }
        public string? Description { get; set; }
        public int UserAction { get; set; }
        [ForeignKey(nameof(UserAction))]
        public Users? Users { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
