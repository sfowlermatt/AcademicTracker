using System;
using System.Collections.Generic;
using System.Text;
using AcademicTracker.Models;

namespace AcademicTracker.Services
{
    public interface ICourses
    {
        Task<List<Courses>> GetCourseAsync();
        Task<List<Courses>> GetCoursesByTermIdAsync(int termId);
    }
}
