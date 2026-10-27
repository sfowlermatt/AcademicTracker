using System;
using System.Collections.Generic;
using System.Text;

namespace AcademicTracker.Models
{

    public enum Status
    {
        Active,
        InProgress,
        Completed,
        Dropped,

    }
    public class Course
    {
        public int TermId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Status Status { get; set; } = Status.Active;
        public string Notes { get; set; }
        public string InstructorName { get; set; } = string.Empty;
        public string InstructorPhone { get; set; } = string.Empty;
        public string InstructorEmail { get; set; } = string.Empty;


        //public Courses(string name, DateTime startDate, DateTime endDate, string status, string notes)
        //{
        //Name = name;
        //StartDate = startDate;
        //EndDate = endDate;
        //Status = status;
        //Notes = notes;
        //TermId = new Term();
        //}
    }
}
