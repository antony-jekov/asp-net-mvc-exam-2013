using System.ComponentModel.DataAnnotations;

namespace MVCExam.Web.Areas.Administration.ViewModels
{
    public class CommentAdminViewModel
    {
        [Required]
        public int Id { get; set; }

        public string AuthorName { get; set; }

        [Required]
        public int TicketId { get; set; }

        public string TicketName { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 12, ErrorMessage = "Comment must be between 6 and 200 characters long!")]
        public string Content { get; set; }
    }
}