using System.ComponentModel.DataAnnotations;

namespace MVCExam.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public int TicketId { get; set; }

        public virtual Ticket Ticket { get; set; }

        public string Content { get; set; }
    }
}