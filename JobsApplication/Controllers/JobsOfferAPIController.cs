using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using JobsApplication.Models;

namespace JobsApplication
{
    public class JobsOfferAPIController : ApiController
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET api/JobsOfferControllerAPI
        public IEnumerable<JobsOfferViewModel> Get()
        {
            var query = from jo in db.JobOffers
                        select new JobsOfferViewModel() {
                            Id = jo.Id,
                            Category = jo.Category.Name,
                            JobType = jo.JobType.Name,
                            Company = jo.Company,
                            Description = jo.Description,
                            Location = jo.Location,
                            Logo = jo.Logo,
                            Position = jo.Position,
                            URL = jo.URL
                        };
            return query.ToList().AsEnumerable();
        }

        // GET api/JobsOfferControllerAPI/5
        public IHttpActionResult Get(int id)
        {
            var query = from jo in db.JobOffers
                        where jo.Id == id
                        select new JobsOfferViewModel() {
                            Id = jo.Id,
                            Category = jo.Category.Name,
                            JobType = jo.JobType.Name,
                            Company = jo.Company,
                            Description = jo.Description,
                            Location = jo.Location,
                            Logo = jo.Logo,
                            Position = jo.Position,
                            URL = jo.URL
                        };
            if (query.SingleOrDefault() != null)
            {
                return Ok(query);
            }
            else
            {
                return BadRequest("No se encontro una oferta de trabajo con el id proporcionado");
            }
            
        }

        // POST api/JobsOfferControllerAPI
        public void Post(JobsOfferViewModel value)
        {
            JobOffer jobOffer = new JobOffer()
            {
                CategoryId = int.Parse(value.Category),
                Company = value.Company,
                Description = value.Description,
                JobTypeId = int.Parse(value.JobType),
                Location = value.Location,
                Logo = value.Logo,
                Position = value.Position,
                URL = value.URL
            };

            db.JobOffers.Add(jobOffer);
            db.SaveChanges();
        }

        // PUT api/JobsOfferControllerAPI/5
        public void Put(int id, JobsOfferViewModel value)
        {
            JobOffer jobOffer = db.JobOffers.Find(id);
            if (jobOffer != null)
            {
                jobOffer.CategoryId = int.Parse(value.Category);
                jobOffer.Company = value.Company;
                jobOffer.Description = value.Description;
                jobOffer.JobTypeId = int.Parse(value.JobType);
                jobOffer.Location = value.Location;
                jobOffer.Position = value.Position;
                jobOffer.Logo = value.Logo;
                jobOffer.URL = value.URL;
                
                db.SaveChanges();
            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        // DELETE api/JobsOffer/5
        public void Delete(int id)
        {
            JobOffer jobOffer = db.JobOffers.Find(id);
            if (jobOffer != null)
            {
                db.JobOffers.Remove(jobOffer);
                db.SaveChanges();
            }
            else
            {
                BadRequest("No se encontro una oferta de trabajo con el id proporcionado");
            }
        }
    }
}