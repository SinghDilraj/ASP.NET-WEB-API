using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIHospital.Models.Domain
{
    public class Patient
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string DateOfBirth { get; set; }
        public string HasInsurance { get; set; }

        public List<Visit> Visits { get; set; }
    }
}