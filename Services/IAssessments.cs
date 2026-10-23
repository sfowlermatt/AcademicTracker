using System;
using System.Collections.Generic;
using System.Text;
using AcademicTracker.Models;

namespace AcademicTracker.Services
{
    public interface IAssessments
    {
        Task<List<Assessments>> GetAssessmentsByCourseIdAsync(int courseId);
        Task DeleteAssessmentByCourseIdAsync(int courseId);
    }
}
