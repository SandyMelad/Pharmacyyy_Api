using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy_ASP_API.Models.Entities
{
    public enum Gender
    {
        Male,
        Female
    }

    public class Patient
    {
        [Key]
        public Guid PatientId { get; set; }
        public required string PatientName { get; set; }
        public required string PhoneNo { get; set; }
        public string? Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }

        //Relations 
        public ICollection<Order> Orders { get; set; }
        public ICollection<Finance> Finances { get; set; } //One Patient can have:Many Orders(they ordered many medications)
                                                           //Many Finance records(they have multiple bills/financial reports)
    }

}
