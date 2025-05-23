using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy_ASP_API.Models.Entities
{
    public class Finance
    {
        [Key]
        public Guid ReportId { get; set; }

        [Key]
        public Guid OrderId { get; set; }

        [Key]
        public Guid PatientId { get; set; }
        public DateTime ReportDate { get; set; }



        

        //Relations
        public ICollection<Report> Reports { get; set; }

    }

}