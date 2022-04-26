using System;
using System.Collections.Generic;

namespace JobPortal.Models
{
    public partial class EducationDetail
    {
        public int UserAccountId { get; set; }
        public string CertificateDegreeName { get; set; } = null!;
        public string Major { get; set; } = null!;
        public string InstituteUniversityName { get; set; } = null!;

        public virtual SeekerProfile UserAccount { get; set; } = null!;
    }
}
