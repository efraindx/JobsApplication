using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobsApplication.Models
{
    public class JobOffer
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public int JobTypeId { get; set; }
        public virtual JobType JobType { get; set; }

        public string Company { get; set; }
        public string Logo { get; set; }
        public string URL { get; set; }
        public string Position { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
    }
}