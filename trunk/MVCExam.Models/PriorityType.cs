using System.ComponentModel.DataAnnotations;

namespace MVCExam.Models
{
    public class PriorityType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Type { get; set; }
    }
}