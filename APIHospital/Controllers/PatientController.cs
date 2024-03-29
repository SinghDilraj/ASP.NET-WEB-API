﻿using APIHospital.Models.Domain;
using APIHospital.Models.ViewModels;
using System.Linq;
using System.Web.Http;

namespace APIHospital.Controllers
{
    [Authorize]
    [RoutePrefix("Api/Patient")]
    public class PatientController : BaseController
    {
        /// <summary>
        /// Get method to return all patients
        /// </summary>
        /// <returns>
        /// ok list of patients
        /// </returns>
        public IHttpActionResult Get()
        {
            System.Collections.Generic.List<PatientViewModel> patients = DbContext.Patients.Select(p => new PatientViewModel
            {
                FirstName = p.FirstName,
                LastName = p.LastName,
                Email = p.Email,
                DateOfBirth = p.DateOfBirth,
                HasInsurance = p.HasInsurance,
                Id = p.Id,
                Visits = p.Visits.Select(q => new VisitViewModel
                {
                    Id = q.Id,
                    Date = q.Date,
                    Comment = q.Comment,
                    PatientId = q.PatientId
                }).ToList()
            }).ToList();

            return Ok(patients);
        }

        /// <summary>
        /// get method to return a patient
        /// </summary>
        /// <param name="id">
        /// Patient id int required
        /// </param>
        /// <returns>
        /// ok patient
        /// </returns>
        [Route("{id:int}")]
        public IHttpActionResult Get(int? id)
        {
            if (id.HasValue)
            {
                Patient patient = DbContext.Patients.FirstOrDefault(p => p.Id == id);

                if (patient != null)
                {
                    PatientViewModel viewModel = new PatientViewModel
                    {
                        FirstName = patient.FirstName,
                        LastName = patient.LastName,
                        Email = patient.Email,
                        DateOfBirth = patient.DateOfBirth,
                        HasInsurance = patient.HasInsurance,
                        Id = patient.Id,
                        Visits = patient.Visits.Select(p => new VisitViewModel
                        {
                            Id = p.Id,
                            Date = p.Date,
                            Comment = p.Comment,
                            PatientId = p.PatientId
                        }).ToList()
                    };

                    return Ok(viewModel);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest("Patient Id not valid");
            }
        }

        /// <summary>
        /// Post method to create a patient
        /// </summary>
        /// <param name="model">
        /// model with patient info, first, last name and date of birth required
        /// </param>
        /// <returns>
        /// ok with patient
        /// </returns>
        public IHttpActionResult Post(PatientEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Patient patient = new Patient
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    DateOfBirth = model.DateOfBirth,
                    HasInsurance = model.HasInsurance,
                };

                DbContext.Patients.Add(patient);
                DbContext.SaveChanges();

                PatientViewModel viewModel = new PatientViewModel
                {
                    FirstName = patient.FirstName,
                    LastName = patient.LastName,
                    Email = patient.Email,
                    DateOfBirth = patient.DateOfBirth,
                    HasInsurance = patient.HasInsurance,
                    Id = patient.Id,
                    Visits = patient.Visits.Select(p => new VisitViewModel
                    {
                        Id = p.Id,
                        Date = p.Date,
                        Comment = p.Comment,
                        PatientId = p.PatientId
                    }).ToList()
                };

                return Ok(viewModel);
            }
            else
            {
                return BadRequest("Required Patient info missing.");
            }
        }

        /// <summary>
        /// Put method to edit patient info
        /// </summary>
        /// <param name="id">
        /// patient id int required
        /// </param>
        /// <param name="model">
        /// model with patient info, first, last name and date of birth required
        /// </param>
        /// <returns>
        /// ok patient
        /// </returns>
        [Route("{id:int}")]
        public IHttpActionResult Put(int? id, PatientEditViewModel model)
        {
            if (id.HasValue)
            {
                Patient patient = DbContext.Patients.FirstOrDefault(p => p.Id == id);

                if (patient != null)
                {
                    if (ModelState.IsValid)
                    {
                        patient.FirstName = model.FirstName;
                        patient.LastName = model.LastName;
                        patient.Email = model.Email;
                        patient.DateOfBirth = model.DateOfBirth;
                        patient.HasInsurance = model.HasInsurance;

                        DbContext.SaveChanges();

                        PatientViewModel viewModel = new PatientViewModel
                        {
                            FirstName = patient.FirstName,
                            LastName = patient.LastName,
                            Email = patient.Email,
                            DateOfBirth = patient.DateOfBirth,
                            HasInsurance = patient.HasInsurance,
                            Id = patient.Id,
                            Visits = patient.Visits.Select(p => new VisitViewModel
                            {
                                Id = p.Id,
                                Date = p.Date,
                                Comment = p.Comment,
                                PatientId = p.PatientId
                            }).ToList()
                        };

                        return Ok(viewModel);
                    }
                    else
                    {
                        return BadRequest("Patient info not valid");
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest("Patient id not valid");
            }
        }

        /// <summary>
        /// method to record visit to a patient
        /// </summary>
        /// <param name="id">
        /// patient id int required
        /// </param>
        /// <param name="model">
        /// model with visit info, visit comment required
        /// </param>
        /// <returns>
        /// ok visit info with patient id
        /// </returns>
        [Route("recordvisit/{id:int}")]
        public IHttpActionResult RecordVisit(int? id, VisitViewModel model)
        {
            if (id.HasValue)
            {
                Patient patient = DbContext.Patients.FirstOrDefault(p => p.Id == id);

                if (patient != null)
                {
                    if (ModelState.IsValid)
                    {
                        Visit visit = new Visit
                        {
                            Comment = model.Comment,
                            Patient = patient,
                            PatientId = patient.Id,
                        };

                        patient.Visits.Add(visit);

                        DbContext.SaveChanges();

                        VisitViewModel viewModel = new VisitViewModel
                        {
                            Id = visit.Id,
                            Comment = visit.Comment,
                            Date = visit.Date,
                            PatientId = visit.PatientId
                        };

                        return Ok(viewModel);
                    }
                    else
                    {
                        return BadRequest("Visit info required");
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest("Patient id not valid");
            }
        }
    }
}
