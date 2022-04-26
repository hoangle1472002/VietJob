using System;
using System.Collections.Generic;

namespace JobPortal.Models
{
    public partial class JobPostActivity
    {
        public int UserAccountId { get; set; }
        public int JobPostId { get; set; }
        public DateTime ApplyDate { get; set; }

        public virtual JobPost JobPost { get; set; } = null!;
        public virtual UserAccount UserAccount { get; set; } = null!;
    }
}
