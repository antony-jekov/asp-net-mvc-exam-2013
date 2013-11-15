using Microsoft.AspNet.Identity.EntityFramework;
using MVCExam.Models;
using System.Data.Entity;

namespace MVCExam.Data
{
    public class ApplicationDbContext : IdentityDbContextWithCustomUser<ApplicationUser>
    {
        public IDbSet<Category> Category { get; set; }

        public IDbSet<Comment> Comments { get; set; }

        public IDbSet<PriorityType> PriorityTypes { get; set; }

        public IDbSet<Ticket> Tickets { get; set; }

        //public IDbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}