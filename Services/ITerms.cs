using System;
using System.Collections.Generic;
using System.Text;
using AcademicTracker.Models;

namespace AcademicTracker.Services
{
    public interface ITerms
    {
        Task<List<Term>> GetTermAsync();
        Task<Term?> GetTermByIdAsync(int id);
        Task SaveTermAsync(Term term);
    }



   
}
