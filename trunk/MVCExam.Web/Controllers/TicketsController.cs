using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using MVCExam.Data;
using MVCExam.Models;
using MVCExam.Web.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace MVCExam.Web.Controllers
{
    public class TicketsController : BaseController
    {
        private IQueryable<Ticket> GetAllTickets()
        {
            var data = this.Data.Tickets.All();

            return data;
        }

        //
        // GET: /Tickets/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                this.Error("No such ticket!");
                return View("Error");
            }

            var ticketViewModel = this.GetAllTickets()
                .Where(t => t.Id == id)
                .Select(t => new TicketViewModel
                {
                    AuthorName = t.Author.UserName,
                    CategoryName = t.Category.Name,
                    Comments = t.Comments.Select(c => new CommentViewModel { AuthorName = c.Author.UserName, Content = c.Content, AuthorPoints = c.Author.Points }),
                    Description = t.Description,
                    Id = t.Id,
                    PriorityType = t.Priority.Type,
                    ScreenshotUrl = t.ScreenshotUrl,
                    Title = t.Title
                }).FirstOrDefault();

            if (ticketViewModel == null)
            {
                this.Error("No such ticket!");
                return View("Error");
            }

            return View(ticketViewModel);
        }

        public JsonResult TicketsRead([DataSourceRequest] DataSourceRequest request)
        {
            var tickets = this.GetAllTickets().Select
                (t => new TicketListViewModel
                {
                    AuthorName = t.Author.UserName,
                    Category = t.Category.Name,
                    PriorityType = t.Priority.Type,
                    Title = t.Title,
                    Id = t.Id
                }).OrderByDescending(t => t.Id);

            return Json(tickets.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCategoriesDropDown()
        {
            var result = this.Data.Categories
                .All()
                .Select(c => new CategoryDropDownViewModel
                {
                    CategoryName = c.Name,
                    CategoryId = c.Id
                });

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPrioritiesDropDown()
        {
            var result = this.Data.PriorityTypes
                .All()
                .Select(p => new PriorityDropDownViewModel
                {
                    PriorityId = p.Id,
                    PriorityType = p.Type
                });

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TicketCreateRequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationDbContext db = new ApplicationDbContext();

                var user = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);

                var userId = user.Id;

                var ticket = new Ticket()
                {
                    AuthorId = userId,
                    CategoryId = model.CategoryId,
                    Description = model.Description,
                    PriorityId = model.PriorityId ?? 2, // medium is default category
                    ScreenshotUrl = model.ScreenshotUrl,
                    Title = model.Title
                };

                this.Data.Tickets.Add(ticket);
                user.Points++;
                db.SaveChanges();

                this.Data.SaveChanges();

                this.Success("Ticket was created successfuly.");

                return RedirectToAction("Index");
            }

            TempData["Error"] = "The word 'bug' cannot be used in the title!";

            return RedirectToAction("Add");
        }

        public ActionResult PostComment(PostCommentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var db = new ApplicationDbContext();

                var user = db.Users.FirstOrDefault(u => u.UserName.ToLower() == User.Identity.Name.ToLower());

                this.Data.Comments.Add(new Comment()
                {
                    AuthorId = user.Id,
                    Content = model.Content,
                    TicketId = model.TicketId,
                });

                this.Data.SaveChanges();

                this.Success("Comment posted");

                var viewModel = new CommentViewModel { AuthorName = user.UserName, Content = model.Content, AuthorPoints = user.Points };
                return PartialView("_CommentPartial", viewModel);
            }

            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest, ModelState.Values.First().ToString());
        }

        public ActionResult Search(int? categoryId)
        {
            if (categoryId == null)
            {
                var data = GetAllTickets().Select(t => new TicketListViewModel
                {
                    AuthorName = t.Author.UserName,
                    Category = t.Category.Name,
                    PriorityType = t.Priority.Type,
                    Title = t.Title,
                    Id = t.Id
                });

                return View(data.ToList());
            }

            var dataFiltered = GetAllTickets().Where(t => t.CategoryId == categoryId).Select(t => new TicketListViewModel
                {
                    AuthorName = t.Author.UserName,
                    Category = t.Category.Name,
                    PriorityType = t.Priority.Type,
                    Title = t.Title
                });

            return View(dataFiltered.ToList());
        }
    }
}