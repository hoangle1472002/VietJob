using System;
using System.Collections.Generic;

namespace JobPortal.Models
{
    public partial class UserType
    {
        public UserType()
        {
            UserAccounts = new HashSet<UserAccount>();
        }

        public int Id { get; set; }
        public string UserTypeName { get; set; } = null!;

        public virtual ICollection<UserAccount> UserAccounts { get; set; }
    }
}
