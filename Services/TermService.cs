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

        public Task SaveTermAsync(Term term)
        {
            if (term.Id == 0)
            {
                // New term: give it the next available ID
                term.Id = _terms.Count == 0 ? 1 : _terms.Max(t => t.Id) + 1;
                _terms.Add(term);
            }
            else
            {
                var existing = _terms.FirstOrDefault(t => t.Id == term.Id);
                if (existing is null)
                {
                    _terms.Add(term);
                }
                else
                {
                    existing.Title = term.Title;
                    existing.StartDate = term.StartDate;
                    existing.EndDate = term.EndDate;
                }
            }
            return Task.CompletedTask;
        }

        public Task DeleteTermAsync(int id)
        {
            var term = _terms.FirstOrDefault(t => t.Id == id);
            if (term is not null)
            {
                _terms.Remove(term);
            }
            return Task.CompletedTask;
        }
    }
}
