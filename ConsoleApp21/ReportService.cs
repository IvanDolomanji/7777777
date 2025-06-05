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
            try
            {
                var teachers = DataManager.LoadTeachers();
                if (!teachers.Any())
                {
                    Console.WriteLine("Нет зарегистрированных учителей.");
                    return;
                }

                foreach (var t in teachers)
                {
                    Console.WriteLine($"{t.FullName ?? "Неизвестный"} — {t.Subject ?? "Не указан"}, кабинет {t.RoomNumber}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке учителей: {ex.Message}");
            }
        }

        public static void ShowStudents()
        {
            try
            {
                var students = DataManager.LoadStudents();
                if (!students.Any())
                {
                    Console.WriteLine("Нет зарегистрированных учеников.");
                    return;
                }

                foreach (var s in students)
                {
                    Console.WriteLine($"{s.FullName ?? "Неизвестный"}, класс {s.Class ?? "Не указан"}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке учеников: {ex.Message}");
            }
        }

        public static void ShowAllGrades()
        {
            try
            {
                var grades = DataManager.LoadGrades();
                var students = DataManager.LoadStudents();
                if (!grades.Any())
                {
                    Console.WriteLine("Нет зарегистрированных оценок.");
                    return;
                }

                foreach (var g in grades)
                {
                    var student = students.FirstOrDefault(s => s.Id == g.StudentId);
                    Console.WriteLine($"{(student?.FullName ?? "Неизвестный")} ({student?.Class ?? "Не указан"}): {g.Subject} - {g.Mark}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке оценок: {ex.Message}");
            }
        }

        public static void GradesBySubject()
        {
            Console.Write("Введите предмет: ");
            string subject = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(subject))
            {
                Console.WriteLine(" gyakorlatОшибка: предмет не может быть пустым.");
                return;
            }

            try
            {
                var grades = DataManager.LoadGrades().Where(g => g.Subject == subject).ToList();
                var students = DataManager.LoadStudents();
                if (!grades.Any())
                {
                    Console.WriteLine($"Нет оценок по предмету {subject}.");
                    return;
                }

                foreach (var g in grades)
                {
                    var student = students.FirstOrDefault(s => s.Id == g.StudentId);
                    Console.WriteLine($"{(student?.FullName ?? "Неизвестный")} ({student?.Class ?? "Не указан"}): {g.Mark}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке оценок по предмету: {ex.Message}");
            }
        }

        public static void CountFailingStudents()
        {
            try
            {
                var grades = DataManager.LoadGrades().Where(g => g.Mark == 2).ToList();
                var failingStudentIds = grades.Select(g => g.StudentId).Distinct().Count();
                Console.WriteLine($"Количество неуспевающих учеников: {failingStudentIds}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при подсчете неуспевающих учеников: {ex.Message}");
            }
        }

        public static void TeacherWithLowestPerformance()
        {
            try
            {
                var grades = DataManager.LoadGrades();
                var teachers = DataManager.LoadTeachers();
                if (!teachers.Any() || !grades.Any())
                {
                    Console.WriteLine("Нет учителей или оценок для анализа.");
                    return;
                }

                var avgByTeacher = teachers
                    .Select(t => new
                    {
                        Teacher = t,
                        Avg = grades.Where(g => g.Subject == t.Subject).Select(g => g.Mark).DefaultIfEmpty().Average()
                    })
                    .OrderBy(x => x.Avg)
                    .FirstOrDefault();

                if (avgByTeacher != null)
                {
                    Console.WriteLine($"Учитель с самой низкой успеваемостью: {avgByTeacher.Teacher.FullName ?? "Неизвестный"}, предмет: {avgByTeacher.Teacher.Subject ?? "Не указан"}, средняя оценка: {avgByTeacher.Avg:F2}");
                }
                else
                {
                    Console.WriteLine("Не удалось определить учителя с самой низкой успеваемостью.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при анализе успеваемости учителей: {ex.Message}");
            }
        }

        public static void AverageGradePerClass()
        {
            try
            {
                var grades = DataManager.LoadGrades();
                var students = DataManager.LoadStudents();
                if (!students.Any() || !grades.Any())
                {
                    Console.WriteLine("Нет учеников или оценок для анализа.");
                    return;
                }

                var avgByClass = students
                    .GroupBy(s => s.Class)
                    .Select(g => new
                    {
                        Class = g.Key ?? "Не указан",
                        Avg = g.SelectMany(s => grades.Where(gr => gr.StudentId == s.Id)).Select(gr => gr.Mark).DefaultIfEmpty().Average()
                    })
                    .OrderBy(c => c.Class);

                foreach (var c in avgByClass)
                {
                    Console.WriteLine($"Класс {c.Class}: средняя оценка = {c.Avg:F2}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при подсчете средней оценки по классам: {ex.Message}");
            }
        }

        public static void BestPerformingClass()
        {
            try
            {
                var grades = DataManager.LoadGrades();
                var students = DataManager.LoadStudents();
                if (!students.Any() || !grades.Any())
                {
                    Console.WriteLine("Нет учеников или оценок для анализа.");
                    return;
                }

                var best = students
                    .GroupBy(s => s.Class)
                    .Select(g => new
                    {
                        Class = g.Key ?? "Не указан",
                        Avg = g.SelectMany(s => grades.Where(gr => gr.StudentId == s.Id)).Select(gr => gr.Mark).DefaultIfEmpty().Average()
                    })
                    .OrderByDescending(x => x.Avg)
                    .FirstOrDefault();

                if (best != null)
                {
                    Console.WriteLine($"Класс с самой высокой успеваемостью: {best.Class}, средняя оценка: {best.Avg:F2}");
                }
                else
                {
                    Console.WriteLine("Не удалось определить класс с самой высокой успеваемостью.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при определении лучшего класса: {ex.Message}");
            }
        }

        public static void WorstPerformingClass()
        {
            try
            {
                var grades = DataManager.LoadGrades();
                var students = DataManager.LoadStudents();
                if (!students.Any() || !grades.Any())
                {
                    Console.WriteLine("Нет учеников или оценок для анализа.");
                    return;
                }

                var worst = students
                    .GroupBy(s => s.Class)
                    .Select(g => new
                    {
                        Class = g.Key ?? "Не указан",
                        Avg = g.SelectMany(s => grades.Where(gr => gr.StudentId == s.Id)).Select(gr => gr.Mark).DefaultIfEmpty().Average()
                    })
                    .OrderBy(x => x.Avg)
                    .FirstOrDefault();

                if (worst != null)
                {
                    Console.WriteLine($"Класс с самой низкой успеваемостью: {worst.Class}, средняя оценка: {worst.Avg:F2}");
                }
                else
                {
                    Console.WriteLine("Не удалось определить класс с самой низкой успеваемостью.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при определении худшего класса: {ex.Message}");
            }
        }
    }
}

