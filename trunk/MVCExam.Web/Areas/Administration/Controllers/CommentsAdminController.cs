using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using MVCExam.Web.Areas.Administration.ViewModels;
using MVCExam.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MVCExam.Web.Areas.Administration.Controllers
{
    //[Authorize(Roles="Admin")]
    public class CommentsAdminController : BaseController
    {
        private IQueryable<CommentAdminViewModel> GetAllCommenst()
        {
            var data = this.Data.Comments.All().Select(c => new CommentAdminViewModel
            {
                Id = c.Id,
                AuthorName = c.Author.UserName,
                Content = c.Content,
                TicketId = c.TicketId,
                TicketName = c.Ticket.Title
            });

            return data;
        }

        //
        // GET: /Administration/Tickets/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(int? id)
        {
            int page = id ?? 0;

            ViewData["CurrentPage"] = page;

            int commentsCount = this.GetAllCommenst().Count();

            double pagesCount = commentsCount / 10;

            if (pagesCount % 10 != 0)
            {
                pagesCount = pagesCount + (pagesCount % 10);
            }

            ViewData["PagesCount"] = (int)pagesCount; //(int)(Math.Round((this.Data.Comments.All().Count() / 10.0), MidpointRounding.AwayFromZero) * 10);

            var commentsFiletered = this.Data.Comments.All().Select(c => new CommentAdminViewModel
                {
                    AuthorName = c.Author.UserName,
                    Content = c.Content,
                    Id = c.Id,
                    TicketId = c.TicketId,
                    TicketName = c.Ticket.Title
                });
            return View(commentsFiletered.ToList().Skip(page * 10).Take(10));
        }

        public JsonResult Read([DataSourceRequest] DataSourceRequest request)
        {
            var categories = this.GetAllCommenst();

            return Json(categories.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Update([DataSourceRequest] DataSourceRequest request, CommentAdminViewModel model)
        {
            var commentToUpdate = this.Data.Comments.All().FirstOrDefault(c => c.Id == model.Id);

            var results = new List<CommentAdminViewModel>();

            if (commentToUpdate != null && ModelState.IsValid)
            {
                commentToUpdate.Content = model.Content;

                this.Data.SaveChanges();

                results.Add(new CommentAdminViewModel
                {
                    Id = commentToUpdate.Id,
                    Content = commentToUpdate.Content
                });
            }

            return Json(results.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            var commentToUpdate = this.GetAllCommenst().FirstOrDefault(c => c.Id == id);

            if (commentToUpdate == null || id == null)
            {
                this.Error("No such comment!");
                return RedirectToAction("List");
            }

            return View(commentToUpdate);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var commentToDelete = this.Data.Comments.All().FirstOrDefault(c => c.Id == id);

            if (commentToDelete == null)
            {
                this.Error("No such comment!");
            }
            else
            {
                this.Data.Comments.Delete(commentToDelete);
                this.Data.SaveChanges();

                this.Success("Comment deleted.");
            }

            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult Edit(CommentAdminViewModel model)
        {
            var commentToUpdate = this.Data.Comments.All().FirstOrDefault(c => c.Id == model.Id);

            if (commentToUpdate == null)
            {
                this.Error("No such comment!");
            }

            if (ModelState.IsValid)
            {
                commentToUpdate.Content = model.Content;

                this.Data.SaveChanges();

                this.Success("Comment has been removed.");
            }

            return RedirectToAction("List");
        }

        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request, CommentAdminViewModel model)
        {
            var commentToDestroy = this.Data.Comments.All().FirstOrDefault(c => c.Id == model.Id);

            var results = new List<CategoryAdminViewModel>();

            if (commentToDestroy != null && ModelState.IsValid)
            {
                this.Data.Comments.Delete(commentToDestroy);

                this.Data.SaveChanges();
            }

            return Content("");
        }
    }
}