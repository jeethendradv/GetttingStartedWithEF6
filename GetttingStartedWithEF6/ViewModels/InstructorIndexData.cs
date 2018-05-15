﻿using GetttingStartedWithEF6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GetttingStartedWithEF6.ViewModels
{
    public class InstructorIndexData
    {
        public IEnumerable<Instructor> Instructors { get; set; }
        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<Enrollment> Enrollments { get; set; }
    }
}