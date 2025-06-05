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
        public static string TeachersFile = "teachers.txt";
        public static string StudentsFile = "students.txt";
        public static string GradesFile = "grades.txt";

        public static List<Teacher> LoadTeachers()
        {
            if (!File.Exists(TeachersFile)) return new List<Teacher>();

            return File.ReadAllLines(TeachersFile)
                       .Select(Teacher.FromString)
                       .Where(t => t != null)
                       .ToList();
        }

        public static void SaveTeachers(List<Teacher> list) =>
            File.WriteAllLines(TeachersFile, list.Select(t => t.ToString()));

        public static List<Student> LoadStudents()
        {
            if (!File.Exists(StudentsFile)) return new List<Student>();

            return File.ReadAllLines(StudentsFile)
                       .Select(Student.FromString)
                       .Where(s => s != null)
                       .ToList();
        }


        public static void SaveStudents(List<Student> list) =>
            File.WriteAllLines(StudentsFile, list.Select(s => s.ToString()));

        public static List<Grade> LoadGrades() =>
            File.Exists(GradesFile) ? File.ReadAllLines(GradesFile).Select(Grade.FromString).ToList() : new List<Grade>();

        public static void SaveGrades(List<Grade> list) =>
            File.WriteAllLines(GradesFile, list.Select(g => g.ToString()));
    }
}
