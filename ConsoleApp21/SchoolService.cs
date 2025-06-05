using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp21
{
    public static class SchoolService
    {
        public static void AddStudent()
        {
            Console.Write("ФИО ученика: ");
            string name = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Ошибка: ФИО не может быть пустым.");
                return;
            }

            Console.Write("Класс: ");
            string cls = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(cls))
            {
                Console.WriteLine("Ошибка: класс не может быть пустым.");
                return;
            }

            try
            {
                var students = DataManager.LoadStudents();
                string id = Guid.NewGuid().ToString();
                students.Add(new Student { Id = id, FullName = name, Class = cls });
                DataManager.SaveStudents(students);
                Console.WriteLine("Ученик добавлен.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при добавлении ученика: {ex.Message}");
            }
        }

        public static void RemoveStudent()
        {
            Console.Write("Введите ФИО ученика: ");
            string name = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Ошибка: ФИО не может быть пустым.");
                return;
            }

            try
            {
                var students = DataManager.LoadStudents();
                var student = students.FirstOrDefault(s => s.FullName == name);
                if (student != null)
                {
                    students.Remove(student);
                    DataManager.SaveStudents(students);
                    var grades = DataManager.LoadGrades().Where(g => g.StudentId != student.Id).ToList();
                    DataManager.SaveGrades(grades);
                    Console.WriteLine("Ученик удален.");
                }
                else
                {
                    Console.WriteLine("Ученик не найден.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при удалении ученика: {ex.Message}");
            }
        }

        public static void AddTeacher()
        {
            Console.Write("ФИО учителя: ");
            string name = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Ошибка: ФИО не может быть пустым.");
                return;
            }

            Console.Write("Предмет: ");
            string subject = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(subject))
            {
                Console.WriteLine("Ошибка: предмет не может быть пустым.");
                return;
            }

            Console.Write("Номер кабинета: ");
            if (!int.TryParse(Console.ReadLine()?.Trim(), out int room) || room <= 0)
            {
                Console.WriteLine("Ошибка: номер кабинета должен быть положительным числом.");
                return;
            }

            try
            {
                var teachers = DataManager.LoadTeachers();
                string id = Guid.NewGuid().ToString();
                teachers.Add(new Teacher { Id = id, FullName = name, Subject = subject, RoomNumber = room });
                DataManager.SaveTeachers(teachers);
                Console.WriteLine("Учитель добавлен.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при добавлении учителя: {ex.Message}");
            }
        }

        public static void RemoveTeacher()
        {
            Console.Write("Введите ФИО учителя: ");
            string name = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Ошибка: ФИО не может быть пустым.");
                return;
            }

            try
            {
                var teachers = DataManager.LoadTeachers();
                var teacher = teachers.FirstOrDefault(t => t.FullName == name);
                if (teacher != null)
                {
                    teachers.Remove(teacher);
                    DataManager.SaveTeachers(teachers);
                    Console.WriteLine("Учитель удален.");
                }
                else
                {
                    Console.WriteLine("Учитель не найден.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при удалении учителя: {ex.Message}");
            }
        }

        public static void EnterGrades()
        {
            var students = DataManager.LoadStudents();
            if (!students.Any())
            {
                Console.WriteLine("Ошибка: нет зарегистрированных учеников.");
                return;
            }

            Console.Write("Введите предмет: ");
            string subject = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(subject))
            {
                Console.WriteLine("Ошибка: предмет не может быть пустым.");
                return;
            }

            var grades = DataManager.LoadGrades();
            foreach (var student in students)
            {
                if (string.IsNullOrWhiteSpace(student.Id) || string.IsNullOrWhiteSpace(student.FullName))
                {
                    Console.WriteLine($"Ошибка: недопустимые данные для ученика (ID: {student.Id}, Имя: {student.FullName}). Пропуск.");
                    continue;
                }

                Console.Write($"{student.FullName} ({student.Class}) - оценка (2-5, Enter для пропуска): ");
                string input = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine($"Оценка для {student.FullName} пропущена.");
                    continue;
                }

                if (int.TryParse(input, out int mark) && mark >= 2 && mark <= 5)
                {
                    grades.RemoveAll(g => g.StudentId == student.Id && g.Subject == subject);
                    grades.Add(new Grade { StudentId = student.Id, Subject = subject, Mark = mark });
                    Console.WriteLine($"Оценка {mark} для {student.FullName} сохранена.");
                }
                else
                {
                    Console.WriteLine($"Ошибка: неверная оценка ({input}) для {student.FullName}. Оценка должна быть числом от 2 до 5.");
                }
            }

            try
            {
                DataManager.SaveGrades(grades);
                Console.WriteLine("Оценки успешно обновлены.");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Ошибка при сохранении оценок: {ex.Message}");
            }
        }
    }
}

        

