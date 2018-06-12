using System;
using System.Linq;

namespace MetanitEF_01_03_SecondApp
{
    // ToDo
    // Для работы с существующей БД MS SQL Server нам надо добавить три пакета:

    // Install-Package Microsoft.EntityFrameworkCore.SqlServer
    // Install-Package Microsoft.EntityFrameworkCore.Tools
    // Install-Package Microsoft.EntityFrameworkCore.SqlServer.Design
    // Scaffold-DbContext "Server=(localdb)\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer

    class Program
    {
        static void Main(string[] args)
        {
            using (helloappdbContext db = new helloappdbContext())
            {
                // получаем объекты из бд и выводим на консоль
                var users = db.Users.ToList();
                Console.WriteLine("Список объектов:");
                foreach (Users u in users)
                {
                    Console.WriteLine($"{u.Id}.{u.Name} - {u.Age}");
                }
            }
            Console.ReadKey();
        }
    }
}
