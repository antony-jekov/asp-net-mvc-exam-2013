using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using MVCExam.Models;
using MVCExam.Web.Areas.Administration.ViewModels;
using MVCExam.Web.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MVCExam.Web.Areas.Administration.Controllers
{
    //[Authorize(Roles="Admin")]
    public class CategoriesAdminController : BaseController
    {
        private IQueryable<CategoryAdminViewModel> GetAllCategories()
        {
            var data = this.Data.Categories.All().Select(c => new CategoryAdminViewModel
            {
                Id = c.Id,
                Name = c.Name
            });

            return data;
        }

        //
        // GET: /Administration/Tickets/
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Read([DataSourceRequest] DataSourceRequest request)
        {
            var categories = this.GetAllCategories();

            return Json(categories.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Create([DataSourceRequest] DataSourceRequest request, CategoryAdminViewModel model)
        {
            if (this.Data.Categories.All().Any(c => c.Name.ToLower() == model.Name.ToLower()))
            {
                ModelState.AddModelError("Name", "There is a category with this name already!");
                return Json(ModelState);
            }

            var results = new List<CategoryAdminViewModel>();

            if (ModelState.IsValid)
            {
                Category category = new Category()
                {
                    Name = model.Name
                };

                this.Data.Categories.Add(category);

                this.Data.SaveChanges();

                results.Add(new CategoryAdminViewModel
                {
                    Id = category.Id,
                    Name = category.Name
                });
            }

            return Json(results.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Update([DataSourceRequest] DataSourceRequest request, CategoryAdminViewModel model)
        {
            var categoryToUpdate = this.Data.Categories.All().FirstOrDefault(c => c.Id == model.Id);

            var results = new List<CategoryAdminViewModel>();

            if (categoryToUpdate != null && ModelState.IsValid)
            {
                categoryToUpdate.Name = model.Name;

                this.Data.SaveChanges();

                results.Add(new CategoryAdminViewModel
                    {
                        Id = categoryToUpdate.Id,
                        Name = categoryToUpdate.Name
                    });
            }

            return Json(results.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request, CategoryAdminViewModel model)
        {
            var categoryToDestroy = this.Data.Categories.All().FirstOrDefault(c => c.Id == model.Id);

            var results = new List<CategoryAdminViewModel>();

            if (categoryToDestroy != null && ModelState.IsValid)
            {
                foreach (var ticket in categoryToDestroy.Tickets.ToList())
                {
                    foreach (var comment in ticket.Comments.ToList())
                    {
                        this.Data.Comments.Delete(comment);
                    }

                    this.Data.Tickets.Delete(ticket);
                }

                this.Data.Categories.Delete(categoryToDestroy);

                this.Data.SaveChanges();
            }

            return Content("");
        }
    }
}