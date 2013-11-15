using MVCExam.Web.ViewModels;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MVCExam.Web.Controllers
{
    public class HomeController : BaseController
    {
        private static readonly string TOP_TICKETS_HOMEPAGE_KEY = "HomePageTopTickets";
        private static readonly int TOP_TICKETS_HOMEPAGE_COUNT = 6;

        public ActionResult Index()
        {
            if (this.HttpContext.Cache[TOP_TICKETS_HOMEPAGE_KEY] == null)
            {
                var listOfTickets = this.Data.Tickets.All()
                    .OrderByDescending(t => t.Comments.Count())
                    .Take(TOP_TICKETS_HOMEPAGE_COUNT)
                    .Select(t => new TicketTopCommentedViewModel
                    {
                        Id = t.Id,
                        AuthorName = t.Author.UserName,
                        CategoryName = t.Category.Name,
                        CommentsCount = t.Comments.Count(),
                        Title = t.Title
                    });

                this.HttpContext.Cache.Add(TOP_TICKETS_HOMEPAGE_KEY, listOfTickets.ToList(), null, DateTime.Now.AddHours(1), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Default, null);
            }

            return View(this.HttpContext.Cache[TOP_TICKETS_HOMEPAGE_KEY]);
        }
    }
}