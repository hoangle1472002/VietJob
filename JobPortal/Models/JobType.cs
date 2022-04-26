using System;
using System.Collections.Generic;

namespace JobPortal.Models
{
    public partial class JobType
    {
        public JobType()
        {
            JobPosts = new HashSet<JobPost>();
        }

        public int Id { get; set; }
        public string JobType1 { get; set; } = null!;

        public virtual ICollection<JobPost> JobPosts { get; set; }
    }
}
