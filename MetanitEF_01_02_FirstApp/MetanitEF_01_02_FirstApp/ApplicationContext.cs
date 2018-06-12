using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MetanitEF_01_02_FirstApp
{
    // DbContext: определяет контекст данных, используемый для взаимодействия с базой данных
    class ApplicationContext : DbContext 
    {
        // DbSet/DbSet<TEntity>: представляет набор объектов, которые хранятся в базе данных
        public DbSet<User> Users { get; set; }

        public ApplicationContext()
        {
            // по умолчанию у нас нет базы данных. Поэтому в конструкторе класса контекста 
            // определен вызов метода Database.EnsureCreated(), который при создании контекста 
            // автоматически проверит наличие базы данных и, если она отсуствует, создаст ее.
            Database.EnsureCreated();
        }

        // DbContextOptionsBuilder: устанавливает параметры подключения
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;");
        }
    }
}
