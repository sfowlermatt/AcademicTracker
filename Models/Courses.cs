using System;
using System.Collections.Generic;
using System.Text;

namespace AcademicTracker.Models
{
    public class Courses
    {
        public int TermId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }


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
