using System;
using System.Collections.Generic;
using System.Text;
using AcademicTracker.Models;

namespace AcademicTracker.Services
{
    public class TermService : ITerms
    {
        private readonly List<Term> _terms = new()
        {
            new Term { Id = 1, Title = "Fall 2023", StartDate = new DateTime(2023, 9, 1), EndDate = new DateTime(2023, 12, 15) },
            new Term { Id = 2, Title = "Spring 2024", StartDate = new DateTime(2024, 1, 10), EndDate = new DateTime(2024, 5, 5) },
            new Term { Id = 3, Title = "Summer 2024", StartDate = new DateTime(2024, 6, 1), EndDate = new DateTime(2024, 8, 15) },
            new Term { Id = 4, Title = "Fall 2024", StartDate = new DateTime(2024, 9, 1), EndDate = new DateTime(2024, 12, 15) }
        };

        public Task<List<Term>> GetTermAsync()
        {
            // Simulate an asynchronous operation
            return Task.FromResult(_terms);
        }

        public Task<Term?> GetTermByIdAsync(int id)
        {
            // Simulate an asynchronous operation
            var term = _terms.FirstOrDefault(t => t.Id == id);
            return Task.FromResult(term);
        }

        public Task SaveTermAsync (Term term)
        {
            // Simulate saving the term (in a real application, you would save to a database)
            var existingTerm = _terms.FirstOrDefault(t => t.Id == term.Id);
            if (existingTerm != null)
            {
                // Update existing term
                existingTerm.Title = term.Title;
                existingTerm.StartDate = term.StartDate;
                existingTerm.EndDate = term.EndDate;
            }
            else
            {
                // Add new term
                _terms.Add(term);
            }
            return Task.CompletedTask;
        }
    }
}
