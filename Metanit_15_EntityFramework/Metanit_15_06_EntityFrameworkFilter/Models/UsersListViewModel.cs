using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

/*
    Так как мы будем выводить в одном представлении и список пользователей, 
    и критерии для выбора, которые представляют список компаний и выбранное имя, 
    то добавим в проект специальную модель:
*/
namespace Metanit_15_06_EntityFrameworkFilter.Models
{
    public class UsersListViewModel
    {
        public List<User> Users { get; internal set; }
        public SelectList Companies { get; internal set; }
        public string Name { get; internal set; }
    }
}
