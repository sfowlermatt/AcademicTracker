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
            new Assessments { Id = 2, CourseId = 5, Name = "Assessment 2", Type = AssessmentType.Performance, StartDate = new DateTime(2024, 2, 1), EndDate = new DateTime(2024, 2, 15) },
            new Assessments { Id = 3, CourseId = 2, Name = "Assessment 3", Type = AssessmentType.Objective, StartDate = new DateTime(2024, 3, 1), EndDate = new DateTime(2024, 3, 15) },
            new Assessments { Id = 4, CourseId = 6, Name = "Assessment 4", Type = AssessmentType.Performance, StartDate = new DateTime(2024, 4, 1), EndDate = new DateTime(2024, 4, 15) },
            new Assessments { Id = 5, CourseId = 3, Name = "Assessment 5", Type = AssessmentType.Objective, StartDate = new DateTime(2024, 5, 1), EndDate = new DateTime(2024, 5, 15) },
            new Assessments { Id = 6, CourseId = 4, Name = "Assessment 6", Type = AssessmentType.Performance, StartDate = new DateTime(2024, 6, 1), EndDate = new DateTime(2024, 6, 15) }
        };

        public Task<List<Assessments>> GetAssessmentsByCourseIdAsync(int courseId)
        {
            var assessments = _assessments.Where(a => a.CourseId == courseId).ToList();
            return Task.FromResult(assessments);
        }

        public Task<Assessments> GetAssessmentByIdAsync(int assessmentId)
        {
            var assessment = _assessments.FirstOrDefault(a => a.Id == assessmentId);
            return Task.FromResult(assessment);
        }

        public Task DeleteAssessmentByCourseIdAsync(int courseId)
        {
            var assessmentsToDelete = _assessments.Where(a => a.CourseId == courseId).ToList();
            foreach (var assessment in assessmentsToDelete)
            {
                _assessments.Remove(assessment);
            }
            return Task.CompletedTask;
        }

        public Task SaveAssessmentAsync(Assessments assessment)
        {
            if (assessment.Id == 0)
            {
                // Assign a new Id for the new assessment
                assessment.Id = _assessments.Max(a => a.Id) + 1;
                _assessments.Add(assessment);
            }
            else
            {
                var existingAssessment = _assessments.FirstOrDefault(a => a.Id == assessment.Id);
                if (existingAssessment != null)
                {
                    // Update existing assessment
                    existingAssessment.Name = assessment.Name;
                    existingAssessment.Type = assessment.Type;
                    existingAssessment.StartDate = assessment.StartDate;
                    existingAssessment.EndDate = assessment.EndDate;
                }
                else
                {
                    // Add new assessment
                    _assessments.Add(assessment);
                }
            }
            return Task.CompletedTask;
        }

    }
}
