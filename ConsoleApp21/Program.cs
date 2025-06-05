using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp21
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("\n--- Меню ---");
                    Console.WriteLine("1. Добавить ученика");
                    Console.WriteLine("2. Отчислить ученика");
                    Console.WriteLine("3. Добавить учителя");
                    Console.WriteLine("4. Уволить учителя");
                    Console.WriteLine("5. Внести оценки ученикам");
                    Console.WriteLine("6. Сведения об учителях");
                    Console.WriteLine("7. Сведения об учениках");
                    Console.WriteLine("8. Сведения об успеваемости");
                    Console.WriteLine("9. Успеваемость по предмету");
                    Console.WriteLine("10. Кол-во неуспевающих по всем классам");
                    Console.WriteLine("11. Учитель с самой низкой успеваемостью");
                    Console.WriteLine("12. Средняя оценка по каждому классу");
                    Console.WriteLine("13. Класс с самой высокой успеваемостью");
                    Console.WriteLine("14. Класс с самой низкой успеваемостью");
                    Console.WriteLine("0. Выход");
                    Console.Write("Выберите пункт: ");

                    var choice = Console.ReadLine()?.Trim();
                    if (string.IsNullOrWhiteSpace(choice))
                    {
                        Console.WriteLine("Ошибка: выберите пункт меню.");
                        continue;
                    }

                    switch (choice)
                    {
                        case "1": SchoolService.AddStudent(); break;
                        case "2": SchoolService.RemoveStudent(); break;
                        case "3": SchoolService.AddTeacher(); break;
                        case "4": SchoolService.RemoveTeacher(); break;
                        case "5": SchoolService.EnterGrades(); break;
                        case "6": ReportService.ShowTeachers(); break;
                        case "7": ReportService.ShowStudents(); break;
                        case "8": ReportService.ShowAllGrades(); break;
                        case "9": ReportService.GradesBySubject(); break;
                        case "10": ReportService.CountFailingStudents(); break;
                        case "11": ReportService.TeacherWithLowestPerformance(); break;
                        case "12": ReportService.AverageGradePerClass(); break;
                        case "13": ReportService.BestPerformingClass(); break;
                        case "14": ReportService.WorstPerformingClass(); break;
                        case "0": return;
                        default: Console.WriteLine("Неверный выбор. Пожалуйста, выберите существующий пункт меню."); break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Произошла ошибка: {ex.Message}");
                }
            }
        }
    }
}
