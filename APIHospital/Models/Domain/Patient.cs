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
        //public string Address { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool HasInsurance { get; set; }

        public virtual List<Visit> Visits { get; set; }

        public Patient()
        {
            Visits = new List<Visit>();
        }
    }
}