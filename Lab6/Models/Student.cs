using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab6.Models
{
    public class Student
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [SwaggerSchema(ReadOnly = true)]
        [Display(Name = "Student ID")]
        [Required]
        public string ID { get; set; }


        [Required]
        [StringLength(50)]
        [Column("FirstName")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Column("LastName")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        [Required]
        [StringLength(50)]
        [Column("Program")]
        [Display(Name = "Program")]
        public string Program { get; set; }


    }
}
