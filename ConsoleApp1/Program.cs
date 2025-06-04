using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static List<string> students = new List<string>();
    static List<string> teachers = new List<string>();
    static List<string> schedule = new List<string>();

    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== СИСТЕМА ДЛЯ ЗАВУЧА ШКОЛЫ ===");
            Console.WriteLine("1. Добавить ученика");
            Console.WriteLine("2. Удалить ученика");
            Console.WriteLine("3. Показать список учеников");
            Console.WriteLine("4. Добавить учителя");
            Console.WriteLine("5. Удалить учителя");
            Console.WriteLine("6. Показать список учителей");
            Console.WriteLine("7. Добавить расписание");
            Console.WriteLine("8. Показать расписание");
            Console.WriteLine("9. Сохранить все данные в файл");
            Console.WriteLine("10. Выход");
            Console.Write("Выберите пункт меню: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": AddStudent(); break;
                case "2": RemoveStudent(); break;
                case "3": ShowList(students, "Ученики"); break;
                case "4": AddTeacher(); break;
                case "5": RemoveTeacher(); break;
                case "6": ShowList(teachers, "Учителя"); break;
                case "7": AddSchedule(); break;
                case "8": ShowList(schedule, "Расписание"); break;
                case "9": SaveToFile(); break;
                case "10": return;
                default: Console.WriteLine("Неверный выбор. Нажмите любую клавишу..."); Console.ReadKey(); break;
            }
        }
    }

    static void AddStudent()
    {
        Console.Write("Введите имя ученика: ");
        string name = Console.ReadLine();
        students.Add(name);
        Console.WriteLine("Ученик добавлен. Нажмите любую клавишу...");
        Console.ReadKey();
    }

    static void RemoveStudent()
    {
        ShowList(students, "Ученики");
        Console.Write("Введите номер ученика для удаления: ");
        if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= students.Count)
        {
            students.RemoveAt(index - 1);
            Console.WriteLine("Ученик удалён.");
        }
        else
        {
            Console.WriteLine("Неверный номер.");
        }
        Console.ReadKey();
    }

    static void AddTeacher()
    {
        Console.Write("Введите имя учителя: ");
        string name = Console.ReadLine();
        teachers.Add(name);
        Console.WriteLine("Учитель добавлен. Нажмите любую клавишу...");
        Console.ReadKey();
    }

    static void RemoveTeacher()
    {
        ShowList(teachers, "Учителя");
        Console.Write("Введите номер учителя для удаления: ");
        if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= teachers.Count)
        {
            teachers.RemoveAt(index - 1);
            Console.WriteLine("Учитель удалён.");
        }
        else
        {
            Console.WriteLine("Неверный номер.");
        }
        Console.ReadKey();
    }

    static void AddSchedule()
    {
        Console.Write("Введите запись расписания (например, Понедельник 9:00 Математика): ");
        string item = Console.ReadLine();
        schedule.Add(item);
        Console.WriteLine("Запись добавлена. Нажмите любую клавишу...");
        Console.ReadKey();
    }

    static void ShowList(List<string> list, string title)
    {
        Console.WriteLine($"=== {title} ===");
        if (list.Count == 0)
        {
            Console.WriteLine("Список пуст.");
        }
        else
        {
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {list[i]}");
            }
        }
        Console.WriteLine("Нажмите любую клавишу...");
        Console.ReadKey();
    }

    static void SaveToFile()
    {
        string filename = $"SchoolData_{DateTime.Now:yyyyMMdd_HHmmss}.txt";

        using (StreamWriter writer = new StreamWriter(filename))
        {
            writer.WriteLine("=== УЧЕНИКИ ===");
            foreach (string s in students) writer.WriteLine(s);

            writer.WriteLine("\n=== УЧИТЕЛЯ ===");
            foreach (string t in teachers) writer.WriteLine(t);

            writer.WriteLine("\n=== РАСПИСАНИЕ ===");
            foreach (string r in schedule) writer.WriteLine(r);
        }

        Console.WriteLine($"Данные сохранены в файл: {filename}");
        Console.WriteLine("Нажмите любую клавишу...");
        Console.ReadKey();
    }
}
