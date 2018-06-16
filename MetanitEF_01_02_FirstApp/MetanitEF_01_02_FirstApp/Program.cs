﻿using System;
using System.Linq;

// ToDo
// 1. To add Microsoft.EntityFrameworkCore.SqlServer
//      Install-Package Microsoft.EntityFrameworkCore.SqlServer
// 2. To add Microsoft.EntityFrameworkCore.Tools
//      Install-Package Microsoft.EntityFrameworkCore.Tools

namespace MetanitEF_01_02_FirstApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                // создаем два объекта User
                User user1 = new User { Name = "Tom", Age = 33 };
                User user2 = new User { Name = "Alice", Age = 26 };

                // добавляем их в бд
                db.Users.Add(user1);
                db.Users.Add(user2);
                db.SaveChanges();
                Console.WriteLine("Объекты успешно сохранены");

                // получаем объекты из бд и выводим на консоль
                var users = db.Users.ToList();
                Console.WriteLine("Список объектов:");
                foreach (User u in users)
                {
                    Console.WriteLine($"{u.Id}.{u.Name} - {u.Age}");
                }
            }
            Console.Read();
        }
    }
}