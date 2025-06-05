using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp21
{
    public class Teacher
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Subject { get; set; }
        public int RoomNumber { get; set; }

        public override string ToString() => $"{Id};{FullName};{Subject};{RoomNumber}";

        public static Teacher FromString(string line)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                Console.WriteLine($"Пропущена строка в teachers.txt: {line}");
                return null;
            }

            var p = line.Split(';');
            if (p.Length < 4)
            {
                Console.WriteLine($"Неверный формат строки в teachers.txt: {line}");
                return null;
            }

            if (!int.TryParse(p[3], out int roomNum) || roomNum <= 0)
            {
                Console.WriteLine($"Неверный номер кабинета в teachers.txt: {line}");
                return null;
            }

            return new Teacher
            {
                Id = p[0],
                FullName = p[1],
                Subject = p[2],
                RoomNumber = roomNum
            };
        }
    }
}
