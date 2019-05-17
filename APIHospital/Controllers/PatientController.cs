using APIHospital.Models;
using APIHospital.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIHospital.Controllers
{
    public class PatientController : ApiController
    {
        public IHttpActionResult Get()
        {
            var dbContext = new ApplicationDbContext();
            return Ok(dbContext.Patients.ToList());
        }

        public IHttpActionResult Get(int id)
        {
            var dbContext = new ApplicationDbContext();
            return Ok(dbContext.Patients.Where(p => p.Id == id).FirstOrDefault());
        }

        public IHttpActionResult Post(Patient patient)
        {
            var dbContext = new ApplicationDbContext();
            dbContext.Patients.Add(patient);
            return Ok(patient);
        }

        public IHttpActionResult Put(int id, Patient patient)
        {
            var dbContext = new ApplicationDbContext();
            var patient1 = dbContext.Patients.Where(p => p.Id == id).FirstOrDefault();
            patient1.HasInsurance = patient.HasInsurance;
            patient1.FirstName = patient.FirstName;
            patient1.LastName = patient.LastName;
            dbContext.SaveChanges();

            return Ok(patient);
        }
        
        [Route("api/{id:int}/recordvisit")]
        public IHttpActionResult RecordVisit(int id, Visit visit)
        {
            var dbContext = new ApplicationDbContext();
            var patient = dbContext.Patients.Where(p => p.Id == id).FirstOrDefault();
            patient.Visits.Add(visit);
            dbContext.SaveChanges();
            return Ok(patient);
        }
    }
}
