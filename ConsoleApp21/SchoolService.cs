using System;
using System.Collections.Generic;
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
            string name = Console.ReadLine();
            Console.Write("Класс: ");
            string cls = Console.ReadLine();

            var students = DataManager.LoadStudents();
            string id = Guid.NewGuid().ToString();
            students.Add(new Student { Id = id, FullName = name, Class = cls });
            DataManager.SaveStudents(students);
            Console.WriteLine("Ученик добавлен.");
        }

        public static void RemoveStudent()
        {
            Console.Write("Введите ФИО ученика: ");
            string name = Console.ReadLine();
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
            else Console.WriteLine("Ученик не найден.");
        }
        public static void AddTeacher()
        {
            Console.Write("ФИО учителя: ");
            string name = Console.ReadLine();
            Console.Write("Предмет: ");
            string subject = Console.ReadLine();
            Console.Write("Номер кабинета: ");
            int room = int.Parse(Console.ReadLine());

            var teachers = DataManager.LoadTeachers();
            string id = Guid.NewGuid().ToString();
            teachers.Add(new Teacher { Id = id, FullName = name, Subject = subject, RoomNumber = room });
            DataManager.SaveTeachers(teachers);
            Console.WriteLine("Учитель добавлен.");
        }
        public static void RemoveTeacher()
        {
            Console.Write("Введите ФИО учителя: ");
            string name = Console.ReadLine();
            var teachers = DataManager.LoadTeachers();
            var teacher = teachers.FirstOrDefault(t => t.FullName == name);
            if (teacher != null)
            {
                teachers.Remove(teacher);
                DataManager.SaveTeachers(teachers);
                Console.WriteLine("Учитель удален.");
            }
            else Console.WriteLine("Учитель не найден.");
        }

        public static void EnterGrades()
        {
            var students = DataManager.LoadStudents();
            var grades = DataManager.LoadGrades();

            Console.Write("Введите предмет: ");
            string subject = Console.ReadLine();

            foreach (var student in students)
            {
                Console.Write($"{student.FullName} ({student.Class}) - оценка: ");
                int mark;
                if (int.TryParse(Console.ReadLine(), out mark) && mark >= 2 && mark <= 5)
                {
                    grades.RemoveAll(g => g.StudentId == student.Id && g.Subject == subject);
                    grades.Add(new Grade { StudentId = student.Id, Subject = subject, Mark = mark });
                }
            }

            DataManager.SaveGrades(grades);
            Console.WriteLine("Оценки обновлены.");
        }
    }
}
