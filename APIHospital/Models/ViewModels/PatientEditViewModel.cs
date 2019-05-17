using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace APIHospital.Models.ViewModels
{
    public class PatientEditViewModel
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        public bool HasInsurance { get; set; }
        public List<VisitViewModel> Visits { get; set; }

        public PatientEditViewModel()
        {
            Visits = new List<VisitViewModel>();
        }
    }
}