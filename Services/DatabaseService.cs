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

            var fall = new Term { Title = "Fall 2026", StartDate = new DateTime(2026, 10, 01), EndDate = new DateTime(2026, 12, 31) };
            var spring = new Term { Title = "Spring 2027", StartDate = new DateTime(2027, 01, 01), EndDate = new DateTime(2027, 04, 30) };
            var summer = new Term { Title = "Summer 2026", StartDate = new DateTime(2027, 05, 01), EndDate = new DateTime(2027, 08, 31) };
            var fall2 = new Term { Title = "Fall 2027", StartDate = new DateTime(2027, 09, 01), EndDate = new DateTime(2027, 12, 31) };
            await db.InsertAsync(fall);
            await db.InsertAsync(spring);
            await db.InsertAsync(summer);
            await db.InsertAsync(fall2);


            var math = new Course
            {
                TermId = fall.Id,
                Name = "Math 101",
                StartDate = new DateTime(2026, 09, 01),
                EndDate = new DateTime(2026, 12, 31),
                Status = Status.Active,
                Notes = "This is a sample course.",
                InstructorName = "Prof. Professorson",
                InstructorPhone = "555-123-4567",
                InstructorEmail = "mathteacher@school.edu"
            };
            var Language = new Course
            {
                TermId = fall.Id,
                Name = "English 1010",
                StartDate = new DateTime(2027, 09, 01),
                EndDate = new DateTime(2027, 12, 31),
                Status = Status.Active,
                Notes = "This is a sample course.",
                InstructorName = "Mr. Hugh Man",
                InstructorPhone = "555-123-4567",
                InstructorEmail = "realperson@school.edu"
            };
            var History = new Course
            {
                TermId = fall.Id,
                Name = "Ancient History",
                StartDate = new DateTime(2027, 09, 01),
                EndDate = new DateTime(2027, 12, 31),
                Status = Status.Active,
                Notes = "This is a sample course.",
                InstructorName = "Prof. Tory Hiss",
                InstructorPhone = "555-123-4567",
                InstructorEmail = "hiss.tory@school.edu"
            };
            var Chemistry = new Course
            {
                TermId = fall.Id,
                Name = "Chemistry",
                StartDate = new DateTime(2027, 09, 01),
                EndDate = new DateTime(2027, 12, 31),
                Status = Status.Active,
                Notes = "This is a sample course.",
                InstructorName = "Prof. W. White",
                InstructorPhone = "555-123-4567",
                InstructorEmail = "wwhite@school.edu"
            };
            var Music = new Course
            {
                TermId = fall.Id,
                Name = "Bongos",
                StartDate = new DateTime(2027, 09, 01),
                EndDate = new DateTime(2027, 12, 31),
                Status = Status.Active,
                Notes = "This is a sample course.",
                InstructorName = "Dr. Bigo Bongo",
                InstructorPhone = "555-123-4567",
                InstructorEmail = "bigobongo@school.edu"
            };
            var Bowling = new Course
            {
                TermId = fall.Id,
                Name = "Bowling for Dummies",
                StartDate = new DateTime(2027, 09, 01),
                EndDate = new DateTime(2027, 12, 31),
                Status = Status.Active,
                Notes = "This is a sample course.",
                InstructorName = "Jan Bowlman",
                InstructorPhone = "555-123-4567",
                InstructorEmail = "elbowler@school.edu"
            };
            await db.InsertAsync(math);
            await db.InsertAsync(Language);
            await db.InsertAsync(History);
            await db.InsertAsync(Chemistry);
            await db.InsertAsync(Music);
            await db.InsertAsync(Bowling);

            await db.InsertAsync(new Assessments { CourseId = math.Id, Name = "Math 101 Objective Exam", Type = AssessmentType.Objective, StartDate = new DateTime(2026, 11, 20) });
            await db.InsertAsync(new Assessments { CourseId = math.Id, Name = "Math 101 Performance Project", Type = AssessmentType.Performance, StartDate = new DateTime(2023, 12, 10) });

            await db.InsertAsync(new Assessments { CourseId = Language.Id, Name = "History Objective Exam", Type = AssessmentType.Objective, StartDate = new DateTime(2026, 11, 20) });
            await db.InsertAsync(new Assessments { CourseId = Language.Id, Name = "History Performance Project", Type = AssessmentType.Performance, StartDate = new DateTime(2026, 12, 10) });

            await db.InsertAsync(new Assessments { CourseId = History.Id, Name = "History Objective Exam", Type = AssessmentType.Objective, StartDate = new DateTime(2026, 11, 20) });
            await db.InsertAsync(new Assessments { CourseId = History.Id, Name = "History Performance Project", Type = AssessmentType.Performance, StartDate = new DateTime(2026, 12, 10) });

            await db.InsertAsync(new Assessments { CourseId = Chemistry.Id, Name = "Chemistry Objective Exam", Type = AssessmentType.Objective, StartDate = new DateTime(2026, 11, 20) });
            await db.InsertAsync(new Assessments { CourseId = Chemistry.Id, Name = "Chemistry Performance Project", Type = AssessmentType.Performance, StartDate = new DateTime(2026, 12, 10) });

            await db.InsertAsync(new Assessments { CourseId = Music.Id, Name = "Bongos Objective Exam", Type = AssessmentType.Objective, StartDate = new DateTime(2026, 11, 20) });
            await db.InsertAsync(new Assessments { CourseId = Music.Id, Name = "Bongos Performance Project", Type = AssessmentType.Performance, StartDate = new DateTime(2026, 12, 10) });

            await db.InsertAsync(new Assessments { CourseId = Bowling.Id, Name = "Bowling Objective Exam", Type = AssessmentType.Objective, StartDate = new DateTime(2026, 11, 20) });
            await db.InsertAsync(new Assessments { CourseId = Bowling.Id, Name = "Bowling Performance Project", Type = AssessmentType.Performance, StartDate = new DateTime(2026, 12, 10) });


        }
    }
}
