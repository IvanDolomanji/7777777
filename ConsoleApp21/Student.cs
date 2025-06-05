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
            if (string.IsNullOrWhiteSpace(line)) return null;

            var p = line.Split(';');
            if (p.Length < 3) return null;

            return new Student
            {
                Id = p[0],
                FullName = p[1],
                Class = p[2]
            };
        }


    }
}

