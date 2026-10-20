using System;
using System.Collections.Generic;
using System.Text;

namespace AcademicTracker.Models
{

    public enum AssessmentType
    {
        Objective,
        Performance
    }

    public class Assessments
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Name { get; set; } = string.Empty;
        public AssessmentType Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
