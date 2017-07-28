using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace TasksApp.Models
{
    public class AppUser : IdentityUser
    {
        public virtual ICollection<Task> Tasks { get; set; }
    }
}