using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy_ASP_API.Models.Entities
{
    public class MedicationKnowledge
    {
        [Key]
        public Guid MedicationId { get; set; }
        public string MedicationName { get; set; }
        public string ClinicalUse { get; set; }
        public decimal Cost { get; set; }
        public string ProductType { get; set; }
        public string Status { get; set; }
        public DateTime Expirydate { get; set; }




        [ForeignKey("Stock")]
        public Guid StockId { get; set; }
        public Stock Stock { get; set; }



        public ICollection<Order> Orders { get; set; }
       

        
    }
}
