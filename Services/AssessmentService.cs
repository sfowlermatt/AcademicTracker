using System;
using System.Collections.Generic;
using System.Text;
using AcademicTracker.Models;

namespace AcademicTracker.Services
{
    public class AssessmentService : IAssessments
    {
        //private readonly List<Assessments> _assessments = new()
        //{
        //  new Assessments { Id = 1, CourseId = 1, Name = "Assessment 1", Type = AssessmentType.Objective, StartDate = new DateTime(2024, 1, 1), EndDate = new DateTime(2024, 1, 15) },
        //new Assessments { Id = 2, CourseId = 5, Name = "Assessment 2", Type = AssessmentType.Performance, StartDate = new DateTime(2024, 2, 1), EndDate = new DateTime(2024, 2, 15) },
        //new Assessments { Id = 3, CourseId = 2, Name = "Assessment 3", Type = AssessmentType.Objective, StartDate = new DateTime(2024, 3, 1), EndDate = new DateTime(2024, 3, 15) },
        //new Assessments { Id = 4, CourseId = 6, Name = "Assessment 4", Type = AssessmentType.Performance, StartDate = new DateTime(2024, 4, 1), EndDate = new DateTime(2024, 4, 15) },
        //new Assessments { Id = 5, CourseId = 3, Name = "Assessment 5", Type = AssessmentType.Objective, StartDate = new DateTime(2024, 5, 1), EndDate = new DateTime(2024, 5, 15) },
        //new Assessments { Id = 6, CourseId = 4, Name = "Assessment 6", Type = AssessmentType.Performance, StartDate = new DateTime(2024, 6, 1), EndDate = new DateTime(2024, 6, 15) }
        //};

        private readonly DatabaseService _database;

        public AssessmentService(DatabaseService database)
        {
            _database = database;
        }

        public async Task<List<Assessments>> GetAssessmentsByCourseIdAsync(int courseId)
        {
            var db = await _database.GetConnectionAsync();
            return await db.Table<Assessments>().Where(a => a.CourseId == courseId).ToListAsync();
        
        }

        public async Task<Assessments> GetAssessmentByIdAsync(int assessmentId)
        {
            var db = await _database.GetConnectionAsync();
            return await db.Table<Assessments>().Where(a => a.Id == assessmentId).FirstOrDefaultAsync();
        }

        public async Task DeleteAssessmentByCourseIdAsync(int courseId)
        {
            var db = await _database.GetConnectionAsync();
            await db.Table<Assessments>().DeleteAsync(a => a.CourseId == courseId);
        }

        public async Task DeleteAssessmentAsync(int assessmentId)
        {
            var db = await _database.GetConnectionAsync();
            await db.DeleteAsync<Assessments>(assessmentId);
        }

        public async Task SaveAssessmentAsync(Assessments assessment)
        {
            var db = await _database.GetConnectionAsync();
            if (assessment.Id == 0)
                await db.InsertAsync(assessment);
            else
                await db.UpdateAsync(assessment);
        }

    }
}
