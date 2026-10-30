using System;
using System.Collections.Generic;
using System.Text;
using AcademicTracker.Models;

namespace AcademicTracker.Services
{
    public interface IAssessments
    {
        Task<List<Assessments>> GetAssessmentsByCourseIdAsync(int courseId);

        Task<Assessments> GetAssessmentByIdAsync(int assessmentId);

        Task DeleteAssessmentByCourseIdAsync(int courseId);

        Task DeleteAssessmentAsync(int assessmentId);

        Task SaveAssessmentAsync(Assessments assessment);
    }
}
