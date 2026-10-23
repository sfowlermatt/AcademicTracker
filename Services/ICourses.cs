using System;
using System.Collections.Generic;
using System.Text;
using AcademicTracker.Models;

namespace AcademicTracker.Services
{
    public interface ICourses
    {
        Task<List<Course>> GetCourseAsync();
        Task<List<Course>> GetCoursesByTermIdAsync(int termId);
        Task<Course?> GetCourseByIdAsync(int id);
        Task DeleteCoursesByTermIdAsync(int termId);
    }
}
