using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCExam.Models
{
    //Each ticket has mandatory author (user), category, title, priority (low, medium (default value), high) and optionally screenshot URL and description (string, no html allowed). Tickets also have a set of comments.

    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public string Title { get; set; }

        public int PriorityId { get; set; }

        public virtual PriorityType Priority { get; set; }

        public string ScreenshotUrl { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public Ticket()
        {
            this.Comments = new HashSet<Comment>();
        }
    }
}