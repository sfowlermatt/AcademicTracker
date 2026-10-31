using System;
using System.Collections.Generic;
using System.Text;
using AcademicTracker.Models;

namespace AcademicTracker.Services
{
    public class TermService : ITerms
    {
        //private readonly List<Term> _terms = new()
        //{
        //  new Term { Id = 1, Title = "Fall 2023", StartDate = new DateTime(2023, 9, 1), EndDate = new DateTime(2023, 12, 15) },
        //new Term { Id = 2, Title = "Spring 2024", StartDate = new DateTime(2024, 1, 10), EndDate = new DateTime(2024, 5, 5) },
        //new Term { Id = 3, Title = "Summer 2024", StartDate = new DateTime(2024, 6, 1), EndDate = new DateTime(2024, 8, 15) },
        //new Term { Id = 4, Title = "Fall 2024", StartDate = new DateTime(2024, 9, 1), EndDate = new DateTime(2024, 12, 15) }
        //};

        private readonly DatabaseService _database;
        public TermService(DatabaseService database)
        {
            _database = database;
        }

        public async Task<List<Term>> GetTermAsync()
        {
            var db = await _database.GetConnectionAsync();
            return await db.Table<Term>().ToListAsync();
        }

        public async Task<Term?> GetTermByIdAsync(int id)
        {
            // Simulate an asynchronous operation
            var db = await _database.GetConnectionAsync();
            return await db.Table<Term>().Where(testc => testc.Id == id).FirstOrDefaultAsync();
        }

        public async Task SaveTermAsync(Term term)
        {
            var db = await _database.GetConnectionAsync();
            if (term.Id == 0)
                await db.InsertAsync(term);
            else
                await db.UpdateAsync(term);
        }

        public async Task DeleteTermAsync(int id)
        {
            var db = await _database.GetConnectionAsync();
            await db.DeleteAsync<Term>(id);
        }
    }
}
