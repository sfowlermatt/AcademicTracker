using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace AcademicTracker.Models
{

    public enum AssessmentType
    {
        Objective,
        Performance
    }

    public class Assessments
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Name { get; set; } = string.Empty;
        public AssessmentType Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool NotificationsEnabled { get; set; }


    }
}
