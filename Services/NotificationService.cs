using AcademicTracker.Models;
using Plugin.LocalNotification;
using Plugin.LocalNotification.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
//using static Android.Graphics.ImageDecoder;

namespace AcademicTracker.Services
{
    public class NotificationService : INotifications
    {

        private static int CourseStartId(int id) => id * 2;
        private static int CourseEndId(int id) => id * 2 + 1;
        private static int AssessmentStartId(int id) => id * 2;
        private static int AssessmentEndId(int id) => id * 2 + 1;

        public async Task CourseStartNotification(Course course)
        {
            if (course.StartDate < DateTime.Now)
            {
                return;
            }

            if (!await LocalNotificationCenter.Current.AreNotificationsEnabled())
            {
                if (!await LocalNotificationCenter.Current.RequestNotificationPermission())
                {
                    return;
                }
            }

            await CancelCourseNotifications(course.Id);

            var notification = new NotificationRequest
            {
                NotificationId = CourseStartId(course.Id),
                Title = "Course Start Date",
                Description = $"Your course {course.Name} starts on {course.StartDate.ToShortDateString()}",
                ReturningData = "CourseNotification"
            };
            notification.Schedule.NotifyTime = course.StartDate.AddDays(-1).AddHours(8);
            await LocalNotificationCenter.Current.Show(notification);
        }

        public async Task CourseEndNotification(Course course)
        {
            if (course.EndDate < DateTime.Now)
            {
                return;
            }
            //Adding in this check because timer was throwing an error when the end date was more than 50 days after the start date.
            if (course.EndDate > course.StartDate.AddDays(49))
            {
                return;
            }

            if (!await LocalNotificationCenter.Current.AreNotificationsEnabled())
            {
                if (!await LocalNotificationCenter.Current.RequestNotificationPermission())
                {
                    return;
                }
            }

            await CancelCourseNotifications(course.Id);

            var notification = new NotificationRequest
            {
                NotificationId = CourseEndId(course.Id),
                Title = "Course End Date",
                Description = $"Your course {course.Name} ends on {course.EndDate.ToShortDateString()}",
                ReturningData = "CourseNotification"
            };
            notification.Schedule.NotifyTime = course.EndDate.AddDays(-1).AddHours(8);
            await LocalNotificationCenter.Current.Show(notification);
        }

        public async Task AssessmentStartNotification(Assessments assessment)
        {
            if (assessment.StartDate < DateTime.Now)
            {
                return;
            }

            if (!await LocalNotificationCenter.Current.AreNotificationsEnabled())
            {
                if (!await LocalNotificationCenter.Current.RequestNotificationPermission())
                {
                    return;
                }
            }

            await CancelAssessmentNotifications(assessment.Id);

            var notification = new NotificationRequest
            {
                NotificationId = AssessmentStartId(assessment.Id),
                Title = "Assessment Start Date",
                Description = $"Your assessment {assessment.Name} starts on {assessment.StartDate.ToShortDateString()}",
                ReturningData = "AssessmentNotification"
            };
            notification.Schedule.NotifyTime = assessment.StartDate.AddDays(-1).AddHours(8);
            await LocalNotificationCenter.Current.Show(notification);
        }

        public async Task AssessmentEndNotification(Assessments assessment)
        {
            if (assessment.EndDate < DateTime.Now)
            {
                return;
            }
            //Adding in this check because timer was throwing an error when the end date was more than 50 days after the start date.
            if (assessment.EndDate > assessment.StartDate.AddDays(49))
            {
                return;
            }

            if (!await LocalNotificationCenter.Current.AreNotificationsEnabled())
            {
                if(!await LocalNotificationCenter.Current.RequestNotificationPermission())
                {
                    return;
                }
            }

            await CancelAssessmentNotifications(assessment.Id);

            var notification = new NotificationRequest
            {
                NotificationId = AssessmentEndId(assessment.Id),
                Title = "Assessment End Date",
                Description = $"Your assessment {assessment.Name} ends on {assessment.EndDate.ToShortDateString()}",
                ReturningData = "AssessmentNotification"
            };
            notification.Schedule.NotifyTime = assessment.EndDate.AddDays(-1).AddHours(8);
            await LocalNotificationCenter.Current.Show(notification);
        }

        public async Task CancelCourseNotifications(int courseId)
        {
            LocalNotificationCenter.Current.Cancel(CourseStartId(courseId), CourseEndId(courseId));
            
        }

        public async Task CancelAssessmentNotifications(int assessmentId)
        {
            LocalNotificationCenter.Current.Cancel(AssessmentStartId(assessmentId), AssessmentEndId(assessmentId));
        }
    }
}
