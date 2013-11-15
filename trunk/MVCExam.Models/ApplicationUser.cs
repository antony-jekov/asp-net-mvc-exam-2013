using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel;

namespace MVCExam.Models
{
    public class ApplicationUser : User
    {
        [DefaultValue(10)]
        public int Points { get; set; }
    }
}