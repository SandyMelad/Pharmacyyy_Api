using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Pharmacy_ASP_API.Models.Entities
{
    public class MedicationRequest
    {
        [Key]
        public Guid RequestId { get; set; }

        [Required]
        public required string DrOutBed { get; set; }

        [Required]
        public required string DrInBed { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public DateTime StatusTime { get; set; }

        public string? Note { get; set; }

        [Required]
        public string DoseInstruction { get; set; }

        [Required]
        public string authoredTime { get; set; }

        [JsonIgnore]
        public virtual Order? Order { get; set; }
    }
}

