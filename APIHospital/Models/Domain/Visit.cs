using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIHospital.Models.Domain
{
    public class Visit
    {
        public int Id { get; set; }

        public string Date { get; set; }
        public string Comments { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; }
    }
}