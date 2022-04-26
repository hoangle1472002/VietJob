using System;
using System.Collections.Generic;

namespace JobPortal.Models
{
    public partial class ExperienceDetail
    {
        public int UserAccountId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string JobTitle { get; set; } = null!;
        public string CompanyName { get; set; } = null!;
        public string Description { get; set; } = null!;

        public virtual SeekerProfile UserAccount { get; set; } = null!;
    }
}
