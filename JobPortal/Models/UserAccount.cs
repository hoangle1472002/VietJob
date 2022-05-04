﻿using System;
using System.Collections.Generic;

namespace JobPortal.Models
{
    public partial class UserAccount
    {
        public UserAccount()
        {
            JobPostActivities = new HashSet<JobPostActivity>();
        }

        public int Id { get; set; }
        public int UserTypeId { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; } = null!;
        public string ContactNumber { get; set; } = null!;
        public string? UserImageUrl { get; set; }
        public DateTime RegistrationDate { get; set; }
     

        public virtual UserType UserType { get; set; } = null!;
        public virtual SeekerProfile SeekerProfile { get; set; } = null!;
        public virtual ICollection<JobPostActivity> JobPostActivities { get; set; }
    }
}
