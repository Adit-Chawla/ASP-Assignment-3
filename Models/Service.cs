using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyFirstApp.Models;

namespace MyFirstApp.Models
{
    public class Service
    {
        [Key]
        public int Id { get; set; } = 0;

        [Required]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; } // Foreign key to Department

        [Required, StringLength(300)]
        public string ServiceName { get; set; } = String.Empty;

        [StringLength(1000)]
        public string? ServiceDescription { get; set; } = String.Empty;

        [StringLength(250)]
        public string? SampleImage { get; set; } = String.Empty;

        [Required]
        [Range(0.01, 999999.99)]
        [DataType(DataType.Currency)]
        public decimal MSRP { get; set; } = 0.01M;
        
        [ForeignKey("DepartmentId")]
        public virtual Department? Department { get; set; } // Navigation property

    }
}
