using System;
using System.Collections.Generic;

namespace JobPortal.Models
{
    public partial class Company
    {
        public Company()
        {
            JobPosts = new HashSet<JobPost>();
        }

        public int Id { get; set; }
        public string CompanyName { get; set; } = null!;
        public string ProfileDescription { get; set; } = null!;
        public DateTime EstablishmentDate { get; set; }
        public string CompanyWebsiteUrl { get; set; } = null!;
        public string CompanyLogoUrl { get; set; } = null!;

        public virtual ICollection<JobPost> JobPosts { get; set; }
    }
}
