using System;
using System.Collections.Generic;

namespace JobPortal.Models
{
    public partial class JobPost
    {
        public JobPost()
        {
            JobPostActivities = new HashSet<JobPostActivity>();
        }

        public int Id { get; set; }
        public int JobTypeId { get; set; }
        public int CompanyId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string JobDescription { get; set; } = null!;
        public string JobLocation { get; set; } = null!;

        public virtual Company Company { get; set; } = null!;
        public virtual JobType JobType { get; set; } = null!;
        public virtual ICollection<JobPostActivity> JobPostActivities { get; set; }
    }
}
