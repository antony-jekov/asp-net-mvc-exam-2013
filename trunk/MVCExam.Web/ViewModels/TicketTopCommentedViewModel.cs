namespace MVCExam.Web.ViewModels
{
    // At the application start page display the 6 most commented tickets. Display the title of the ticket, the name of the category, the name of the author and the number of comments. Show a link to the tickets details page.
    public class TicketTopCommentedViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string CategoryName { get; set; }

        public string AuthorName { get; set; }

        public int CommentsCount { get; set; }
    }
}