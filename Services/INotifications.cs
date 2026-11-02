using System;
using System.Collections.Generic;
using System.Text;
using AcademicTracker.Models;

namespace AcademicTracker.Services
{
    public interface INotifications
    {
        Task CourseStartNotification(Course course);
        Task CourseEndNotification(Course course);
        Task AssessmentStartNotification(Assessments assessment);
        Task AssessmentEndNotification(Assessments assessment);
        Task CancelCourseNotifications(int courseId);
        Task CancelAssessmentNotifications(int assessmentId);

    }
}
