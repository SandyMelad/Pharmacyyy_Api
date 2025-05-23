using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy_ASP_API.Models.Entities
{
    public class MedicationRequest
    {
        [Key]
        public Guid RequestId { get; set; }
        public required string DrOutBed { get; set; }
        public required string DrInBed { get; set; }
        public string Status { get; set; }
        public DateTime StatusTime { get; set; }
        public string? Note { get; set; }
        public string DoseInstruction { get; set; }
        public string authoredTime { get; set; }

        //One MedicationRequest ➜ can be linked to one Order

        [ForeignKey("Order")]
        public Guid OrderId { get; set; }


    }
}

