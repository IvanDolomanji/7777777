using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp21
{
    public static class DataManager
    {
        private static readonly string BasePath = AppDomain.CurrentDomain.BaseDirectory;
        public static string TeachersFile = Path.Combine(BasePath, "teachers.txt");
        public static string StudentsFile = Path.Combine(BasePath, "students.txt");
        public static string GradesFile = Path.Combine(BasePath, "grades.txt");

        public static List<Teacher> LoadTeachers()
        {
            try
            {
                if (!File.Exists(TeachersFile))
                    return new List<Teacher>();

                var lines = File.ReadAllLines(TeachersFile);
                var teachers = new List<Teacher>();
                foreach (var line in lines)
                {
                    var teacher = Teacher.FromString(line);
                    if (teacher != null)
                        teachers.Add(teacher);
                }
                return teachers;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при чтении teachers.txt: {ex.Message}");
                return new List<Teacher>();
            }
        }

        public static void SaveTeachers(List<Teacher> list)
        {
            try
            {
                File.WriteAllLines(TeachersFile, list.Select(t => t.ToString()));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при записи в teachers.txt: {ex.Message}");
            }
        }

        public static List<Student> LoadStudents()
        {
            try
            {
                if (!File.Exists(StudentsFile))
                    return new List<Student>();

                var lines = File.ReadAllLines(StudentsFile);
                var students = new List<Student>();
                foreach (var line in lines)
                {
                    var student = Student.FromString(line);
                    if (student != null)
                        students.Add(student);
                }
                return students;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при чтении students.txt: {ex.Message}");
                return new List<Student>();
            }
        }

        public static void SaveStudents(List<Student> list)
        {
            try
            {
                File.WriteAllLines(StudentsFile, list.Select(s => s.ToString()));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при записи в students.txt: {ex.Message}");
            }
        }

        public static List<Grade> LoadGrades()
        {
            try
            {
                if (!File.Exists(GradesFile))
                    return new List<Grade>();

                var lines = File.ReadAllLines(GradesFile);
                var grades = new List<Grade>();
                foreach (var line in lines)
                {
                    var grade = Grade.FromString(line);
                    if (grade != null)
                        grades.Add(grade);
                }
                return grades;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при чтении grades.txt: {ex.Message}");
                return new List<Grade>();
            }
        }

        public static void SaveGrades(List<Grade> list)
        {
            try
            {
                File.WriteAllLines(GradesFile, list.Select(g => g.ToString()));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при записи в grades.txt: {ex.Message}");
            }
        }
    }
}
