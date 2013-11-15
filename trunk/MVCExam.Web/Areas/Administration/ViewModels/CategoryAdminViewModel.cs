using System.ComponentModel.DataAnnotations;

namespace MVCExam.Web.Areas.Administration.ViewModels
{
    public class CategoryAdminViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Name should be between 3 and 20 characters long!")]
        public string Name { get; set; }
    }
}