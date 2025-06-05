using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp21
{
    public static class ReportService
    {
        public static void ShowTeachers()
        {
            var teachers = DataManager.LoadTeachers();
            foreach (var t in teachers)
                Console.WriteLine($"{t.FullName} — {t.Subject}, кабинет {t.RoomNumber}");
        }

        public static void ShowStudents()
        {
            var students = DataManager.LoadStudents();
            foreach (var s in students)
                Console.WriteLine($"{s.FullName}, класс {s.Class}");
        }

        public static void ShowAllGrades()
        {
            var grades = DataManager.LoadGrades();
            var students = DataManager.LoadStudents();
            foreach (var g in grades)
            {
                var student = students.FirstOrDefault(s => s.Id == g.StudentId);
                if (student != null)
                    Console.WriteLine($"{student.FullName} ({student.Class}): {g.Subject} - {g.Mark}");
            }
        }

        public static void GradesBySubject()
        {
            Console.Write("Введите предмет: ");
            string subject = Console.ReadLine();
            var grades = DataManager.LoadGrades().Where(g => g.Subject == subject);
            var students = DataManager.LoadStudents();
            foreach (var g in grades)
            {
                var student = students.FirstOrDefault(s => s.Id == g.StudentId);
                if (student != null)
                    Console.WriteLine($"{student.FullName} ({student.Class}): {g.Mark}");
            }
        }

        public static void CountFailingStudents()
        {
            var grades = DataManager.LoadGrades().Where(g => g.Mark == 2);
            var students = DataManager.LoadStudents();
            var failing = grades.Select(g => g.StudentId).Distinct().Count();
            Console.WriteLine($"Количество неуспевающих учеников: {failing}");
        }

        public static void TeacherWithLowestPerformance()
        {
            var grades = DataManager.LoadGrades();
            var students = DataManager.LoadStudents();
            var teachers = DataManager.LoadTeachers();

            var avgByTeacher = teachers
                .Select(t => new
                {
                    Teacher = t,
                    Avg = grades.Where(g => g.Subject == t.Subject).Select(g => g.Mark).DefaultIfEmpty().Average()
                })
                .OrderBy(x => x.Avg)
                .FirstOrDefault();

            if (avgByTeacher != null)
                Console.WriteLine($"Учитель с самой низкой успеваемостью: {avgByTeacher.Teacher.FullName}, предмет: {avgByTeacher.Teacher.Subject}, средняя оценка: {avgByTeacher.Avg:F2}");
        }

        public static void AverageGradePerClass()
        {
            var grades = DataManager.LoadGrades();
            var students = DataManager.LoadStudents();
            var avgByClass = students
                .GroupBy(s => s.Class)
                .Select(g => new
                {
                    Class = g.Key,
                    Avg = g.SelectMany(s => grades.Where(gr => gr.StudentId == s.Id)).Select(gr => gr.Mark).DefaultIfEmpty().Average()
                });

            foreach (var c in avgByClass)
                Console.WriteLine($"Класс {c.Class}: средняя оценка = {c.Avg:F2}");
        }

        public static void BestPerformingClass()
        {
            var grades = DataManager.LoadGrades();
            var students = DataManager.LoadStudents();
            var best = students
                .GroupBy(s => s.Class)
                .Select(g => new
                {
                    Class = g.Key,
                    Avg = g.SelectMany(s => grades.Where(gr => gr.StudentId == s.Id)).Select(gr => gr.Mark).DefaultIfEmpty().Average()
                })
                .OrderByDescending(x => x.Avg)
                .FirstOrDefault();

            if (best != null)
                Console.WriteLine($"Класс с самой высокой успеваемостью: {best.Class}, средняя оценка: {best.Avg:F2}");
        }

        public static void WorstPerformingClass()
        {
            var grades = DataManager.LoadGrades();
            var students = DataManager.LoadStudents();
            var worst = students
                .GroupBy(s => s.Class)
                .Select(g => new
                {
                    Class = g.Key,
                    Avg = g.SelectMany(s => grades.Where(gr => gr.StudentId == s.Id)).Select(gr => gr.Mark).DefaultIfEmpty().Average()
                })
                .OrderBy(x => x.Avg)
                .FirstOrDefault();

            if (worst != null)
                Console.WriteLine($"Класс с самой низкой успеваемостью: {worst.Class}, средняя оценка: {worst.Avg:F2}");
        }
    }
}

