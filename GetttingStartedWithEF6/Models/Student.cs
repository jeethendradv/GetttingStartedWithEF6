using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GetttingStartedWithEF6.Models
{
    public class Student : Person
    {
        [Required]
        [DataType(DataType.Date)]
        [Display(Name ="Enrollment Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EnrollmentDate { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}