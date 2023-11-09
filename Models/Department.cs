using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace MyFirstApp.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; } = 0;

        [Required(ErrorMessage = "Name can't be empty!")] 
        [StringLength(300)]
        public string DepartmentName { get; set; } = String.Empty;

        [StringLength(1000)]
        public string? DepartmentDescription { get; set; } = String.Empty;

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        public decimal? DepartmentBudget { get; set; }

        public virtual ICollection<Service>? Services { get; set; }

    }

}