using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Pharmacy_ASP_API.Models.Entities
{
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; }
        public DateTime OrderTime { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("Medication")]
        public Guid MedicationId { get; set; }
        [JsonIgnore]
        public virtual MedicationKnowledge? Medication { get; set; }

        [ForeignKey("MedicationRequest")]
        public Guid? MedicationRequestId { get; set; }
        [JsonIgnore]
        public virtual MedicationRequest? MedicationRequest { get; set; }

        [ForeignKey("Patient")]
        public Guid PatientId { get; set; }
        [JsonIgnore]
        public virtual Patient? Patient { get; set; }

        [ForeignKey("Report")]
        public Guid ReportId { get; set; }
        [JsonIgnore]
        public virtual Report? Report { get; set; }

        [ForeignKey("Stock")]
        public Guid StockId { get; set; }
        [JsonIgnore]
        public virtual Stock? Stock { get; set; }

        [JsonIgnore]
        public virtual ICollection<MedicationKnowledge>? MedicationKnowledges { get; set; }
    }
}