using System;
using System.Collections.Generic;
using System.Text;
using AcademicTracker.Models;


namespace AcademicTracker.Services
{
    public class CourseService : ICourses
    {
        private readonly List<Courses> _courses = new()
        {
            new Courses { TermId = 1, Id = 1, Name = "Math 101", StartDate = new DateTime(2023, 9, 1), EndDate = new DateTime(2023, 12, 15), Status = "Active", Notes = "This is a sample course." },
            new Courses { TermId = 1, Id = 2, Name = "History 201", StartDate = new DateTime(2024, 1, 10), EndDate = new DateTime(2024, 5, 5), Status = "Inactive", Notes = "This is another sample course." },
            new Courses { TermId = 1, Id = 3, Name = "Science 301", StartDate = new DateTime(2024, 6, 1), EndDate = new DateTime(2024, 8, 15), Status = "Active", Notes = "This is a third sample course." },
            new Courses { TermId = 1, Id = 4, Name = "English 401", StartDate = new DateTime(2024, 9, 1), EndDate = new DateTime(2024, 12, 15), Status = "Inactive", Notes = "This is a fourth sample course." },
            new Courses { TermId = 1, Id = 5, Name = "Art 501", StartDate = new DateTime(2024, 9, 1), EndDate = new DateTime(2024, 12, 15), Status = "Active", Notes = "This is a fifth sample course." },
            new Courses { TermId = 1, Id = 6, Name = "Music 601", StartDate = new DateTime(2024, 9, 1), EndDate = new DateTime(2024, 12, 15), Status = "Inactive", Notes = "This is a sixth sample course." }
        };

        public Task<List<Courses>> GetCourseAsync()
        {
            return Task.FromResult(_courses);
        }

        public Task<List<Courses>> GetCoursesByTermIdAsync(int termId)
        {
            var coursesByTerm = _courses.FindAll(c => c.TermId == termId);
            return Task.FromResult(coursesByTerm);
        }
    }
}
