using System;
using System.Collections.Generic;
using System.Text;

namespace AcademicTracker.Models
{
    public class Term
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
