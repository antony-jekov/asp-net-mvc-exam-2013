using MVCExam.Web.ViewModels.CustomValidations;
using System.ComponentModel.DataAnnotations;

namespace MVCExam.Web.ViewModels
{
    public class TicketCreateRequestViewModel
    {
        [Required(ErrorMessage = "Please choose category!")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Please enter a title!")]
        [ShouldNotContainBugAsWord(ErrorMessage = "The word 'bug' is not allowed in the title!")]
        [StringLength(50, ErrorMessage = "Title must be between 6 and 50 characters long!", MinimumLength = 6)]
        public string Title { get; set; }

        [Display(Name = "Priority")]
        public int? PriorityId { get; set; }

        [StringLength(255, ErrorMessage = "Image Url is too long!")]
        [Url(ErrorMessage = "Please choose a valid Url!")]
        public string ScreenshotUrl { get; set; }

        [StringLength(500, ErrorMessage = "Text is too long! Maximum 500 characters.")]
        public string Description { get; set; }
    }
}