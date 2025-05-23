using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy_ASP_API.Models.Entities
{
    public class Report
    {

        [Key]
        public Guid ReportId { get; set; }
        public int TotalSales { get; set; }
        public int OrderCount { get; set; }
        public int StockAcidPrice { get; set; }

        //Relationships

        public ICollection<Finance> Finances { get; set; }
        public ICollection<Order> Orders { get; set; }

    }
}
