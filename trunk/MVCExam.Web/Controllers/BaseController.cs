using MVCExam.Data;
using System.Web.Mvc;

namespace MVCExam.Web.Controllers
{
    public class BaseController : Controller
    {
        protected IUowData Data;

        public BaseController(IUowData data)
        {
            this.Data = data;
        }

        protected void Success(string message)
        {
            TempData["Success"] = message;
        }

        protected void Error(string message)
        {
            TempData["Error"] = message;
        }

        public BaseController()
            : this(new UowData())
        {
        }
    }
}