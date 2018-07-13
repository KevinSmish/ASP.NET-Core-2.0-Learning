using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Metanit_19_01_WebAPI_Sample.Models
{
    public class UsersContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public UsersContext(DbContextOptions<UsersContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}

// для создания базы данных перейдем к окну Package Manager Console и выполним 
// в нем последовательно две команды:
//      Add-Migration Initial
//      Update-Database