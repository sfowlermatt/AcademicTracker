using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using AcademicTracker.Models;

namespace AcademicTracker.Services
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection? _database;
        private readonly SemaphoreSlim _locked = new(1, 1);

        public async Task<SQLiteAsyncConnection> GetConnectionAsync()
        {
            if (_database is not null)
                return _database;
            await _locked.WaitAsync();
            try
            {
                if (_database is not null)
                    return _database;

                var path = Path.Combine(FileSystem.AppDataDirectory, "academictracker.db3");
                var db = new SQLiteAsyncConnection(path);

                await db.CreateTableAsync<Term>();
                await db.CreateTableAsync<Course>();
                await db.CreateTableAsync<Assessments>();

                await SeedAsync(db);

                _database = db;
                return _database;
            }
            finally
            {
                _locked.Release();
            }
        }

        private static async Task SeedAsync(SQLiteAsyncConnection db)
        {
            if (await db.Table<Term>().CountAsync() > 0)
                return;

            var fall = new Term { Title = "Fall 2026", StartDate = new DateTime(2026, 09, 01), EndDate = new DateTime(2026, 12, 31) };
             await db.InsertAsync(fall);

            var math = new Course
            {
                TermId = fall.Id,
                Name = "Math 101",
                StartDate = new DateTime(2026, 09, 01),
                EndDate = new DateTime(2026, 12, 31),
                Status = Status.InProgress,
                Notes = "This is a sample course.",
                InstructorName = "Anika Patel",
                InstructorPhone = "555-123-4567",
                InstructorEmail = "anika.patel@strimeuniversity.edu"
            };
            
            await db.InsertAsync(math);

            await db.InsertAsync(new Assessments { CourseId = math.Id, Name = "Math 101 Objective Exam", Type = AssessmentType.Objective, StartDate = new DateTime(2026, 11, 20) });
            await db.InsertAsync(new Assessments { CourseId = math.Id, Name = "Math 101 Performance Project", Type = AssessmentType.Performance, StartDate = new DateTime(2023, 12, 10) });

        

        }
    }
}
