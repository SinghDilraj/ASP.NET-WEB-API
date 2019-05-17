using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace APIHospital.Models.ViewModels
{
    public class VisitViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        [Required]
        public string Comment { get; set; }
        public int PatientId { get; set; }
    }
}