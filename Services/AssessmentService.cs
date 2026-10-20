using System;
using System.Collections.Generic;
using System.Text;
using AcademicTracker.Models;

namespace AcademicTracker.Services
{
    public class AssessmentService : IAssessments
    {
        private readonly List<Assessments> _assessments = new()
        {
            new Assessments { Id = 1, CourseId = 1, Name = "Assessment 1", Type = AssessmentType.Objective, StartDate = new DateTime(2024, 1, 1), EndDate = new DateTime(2024, 1, 15) },
            new Assessments { Id = 2, CourseId = 1, Name = "Assessment 2", Type = AssessmentType.Performance, StartDate = new DateTime(2024, 2, 1), EndDate = new DateTime(2024, 2, 15) },
            new Assessments { Id = 3, CourseId = 2, Name = "Assessment 3", Type = AssessmentType.Objective, StartDate = new DateTime(2024, 3, 1), EndDate = new DateTime(2024, 3, 15) },
            new Assessments { Id = 4, CourseId = 2, Name = "Assessment 4", Type = AssessmentType.Performance, StartDate = new DateTime(2024, 4, 1), EndDate = new DateTime(2024, 4, 15) }
        };

        public Task<List<Assessments>> GetAssessmentsByCourseIdAsync(int courseId)
        {
            var assessments = _assessments.Where(a => a.CourseId == courseId).ToList();
            return Task.FromResult(assessments);
        }

    }
}
