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
            var p = line.Split(';');
            return new Grade { StudentId = p[0], Subject = p[1], Mark = int.Parse(p[2]) };
        }
    }
}
