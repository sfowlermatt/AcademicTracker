using System;
using System.Collections.Generic;
using System.Text;
using AcademicTracker.Models;
using AcademicTracker.Services;


namespace AcademicTracker.Services
{
    public class CourseService : ICourses
    {
        private readonly List<Course> _courses = new()
        {
            new Course { TermId = 1, Id = 1, Name = "Math 101", StartDate = new DateTime(2023, 9, 1), EndDate = new DateTime(2023, 12, 15), Status = Status.Active, Notes = "This is a sample course.", InstructorName = "John Doe", InstructorPhone = "555-1234", InstructorEmail = "john.doe@university.edu" },
            new Course { TermId = 1, Id = 2, Name = "History 201", StartDate = new DateTime(2024, 1, 10), EndDate = new DateTime(2024, 5, 5), Status = Status.Inactive, Notes = "This is another sample course.", InstructorName = "Jane Smith", InstructorPhone = "555-5678", InstructorEmail = "jane.smith@university.edu" },
            new Course { TermId = 1, Id = 3, Name = "Science 301", StartDate = new DateTime(2024, 6, 1), EndDate = new DateTime(2024, 8, 15), Status = Status.Active, Notes = "This is a third sample course.", InstructorName = "Bob Johnson", InstructorPhone = "555-9012", InstructorEmail = "bob.johnson@university.edu" },
            new Course { TermId = 1, Id = 4, Name = "English 401", StartDate = new DateTime(2024, 9, 1), EndDate = new DateTime(2024, 12, 15), Status = Status.Inactive, Notes = "This is a fourth sample course.", InstructorName = "Alice Brown", InstructorPhone = "555-3456", InstructorEmail = "alice.brown@university.edu" },
            new Course { TermId = 1, Id = 5, Name = "Art 501", StartDate = new DateTime(2024, 9, 1), EndDate = new DateTime(2024, 12, 15), Status = Status.Active, Notes = "This is a fifth sample course.", InstructorName = "Charlie Wilson", InstructorPhone = "555-7890", InstructorEmail = "charlie.wilson@university.edu" },
            new Course { TermId = 1, Id =6, Name = "Music 601", StartDate = new DateTime(2024, 9, 1), EndDate = new DateTime(2024, 12, 15), Status = Status.Inactive, Notes = "This is a sixth sample course.", InstructorName = "David Lee", InstructorPhone = "555-2345", InstructorEmail = "david.lee@university.edu" },

            new Course { TermId = 2, Id = 7, Name = "Physics 101", StartDate = new DateTime(2024, 1, 10), EndDate = new DateTime(2024, 5, 5), Status = Status.Active, Notes = "This is a sample course for Spring 2024." },
            new Course { TermId = 4, Id = 8, Name = "Chemistry 201", StartDate = new DateTime(2024, 1, 10), EndDate = new DateTime(2024, 5, 5), Status = Status.Inactive, Notes = "This is another sample course for Spring 2024." }
        };

        public Task<List<Course>> GetCourseAsync()
        {
            return Task.FromResult(_courses);
        }

        public Task<List<Course>> GetCoursesByTermIdAsync(int termId)
        {
            var coursesByTerm = _courses.FindAll(c => c.TermId == termId).ToList();
            return Task.FromResult(coursesByTerm);
        }

        public Task<Course?> GetCourseByIdAsync(int id)
        {
            var course = _courses.FirstOrDefault(c => c.Id == id);
            return Task.FromResult(course);
        }

        public Task DeleteCoursesByTermIdAsync(int termId)
        {
            var courses = _courses.FindAll(c => c.TermId == termId);
            foreach (var course in courses)
            {
                _courses.Remove(course);
            }
            return Task.CompletedTask;
        }
    }
}