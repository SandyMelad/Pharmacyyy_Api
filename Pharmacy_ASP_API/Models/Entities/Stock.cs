using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy_ASP_API.Models.Entities
{
    public class Stock
    {
        [Key]
        public Guid StockId { get; set; }

        [Key]
        public Guid MedicationId { get; set; }
        public int Quantity { get; set; }
        public DateTime WarningDate { get; set;}

        


      
        public ICollection<MedicationKnowledge> MedicationKnowledges { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}