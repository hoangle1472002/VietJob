using System;
using System.Collections.Generic;

namespace JobPortal.Models
{
    public partial class SeekerProfile
    {
        public SeekerProfile()
        {
            EducationDetails = new HashSet<EducationDetail>();
            ExperienceDetails = new HashSet<ExperienceDetail>();
        }

        public int UserAccountId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        public virtual UserAccount UserAccount { get; set; } = null!;
        public virtual ICollection<EducationDetail> EducationDetails { get; set; }
        public virtual ICollection<ExperienceDetail> ExperienceDetails { get; set; }
    }
}
