using System.Collections.Generic;

namespace MVCExam.Web.ViewModels
{
    public class TicketViewModel
    {
        public int Id { get; set; }

        public string AuthorName { get; set; }

        public string CategoryName { get; set; }

        public string Title { get; set; }

        public string PriorityType { get; set; }

        public string ScreenshotUrl { get; set; }

        public string Description { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }
    }
}