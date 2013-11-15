using System.ComponentModel.DataAnnotations;

namespace MVCExam.Web.ViewModels
{
    public class PostCommentViewModel
    {
        [Required]
        public int TicketId { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Text must be between 12 and 200 characters long!", MinimumLength = 12)]
        public string Content { get; set; }
    }
}