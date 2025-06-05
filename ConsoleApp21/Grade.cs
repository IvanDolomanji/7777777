using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp21
{
    public class Grade
    {
        public string StudentId { get; set; }
        public string Subject { get; set; }
        public int Mark { get; set; }

        public override string ToString() => $"{StudentId};{Subject};{Mark}";

        public static Grade FromString(string line)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                Console.WriteLine($"Пропущена строка в grades.txt: {line}");
                return null;
            }

            var p = line.Split(';');
            if (p.Length < 3)
            {
                Console.WriteLine($"Неверный формат строки в grades.txt: {line}");
                return null;
            }

            if (!int.TryParse(p[2], out int mark) || mark < 2 || mark > 5)
            {
                Console.WriteLine($"Неверная оценка в grades.txt: {line}");
                return null;
            }

            return new Grade { StudentId = p[0], Subject = p[1], Mark = mark };
        }
    }
}
