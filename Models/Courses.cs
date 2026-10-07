using System;
using System.Collections.Generic;
using System.Text;

namespace AcademicTracker.Models
{
    internal class Courses
    {
        public string Name { get; set; }
        public DatePicker StartDate { get; set; }
        public DatePicker EndDate { get; set; }
        public Picker Status { get; set; }
        public string Notes { get; set; }

    }
}
