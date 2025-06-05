using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp21
{
    public class Student
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Class { get; set; }

        public override string ToString() => $"{Id};{FullName};{Class}";

        public static Student FromString(string line)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                Console.WriteLine($"Пропущена строка в students.txt: {line}");
                return null;
            }

            var p = line.Split(';');
            if (p.Length < 3)
            {
                Console.WriteLine($"Неверный формат строки в students.txt: {line}");
                return null;
            }

            return new Student
            {
                Id = p[0],
                FullName = p[1],
                Class = p[2]
            };
        }
    }
}

