using Metanit_15_01_EntityFrameworkSample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Metanit_15_01_EntityFrameworkSample.Controllers
{
    public class HomeController : Controller
    {
        private MobileContext db;
        public HomeController(MobileContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await db.Phones.ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Phone phone)
        {
            db.Phones.Add(phone);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                Phone phone = await db.Phones.FirstOrDefaultAsync(p => p.Id == id);
                if (phone != null)
                    return View(phone);
            }
            return NotFound();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Phone phone = await db.Phones.FirstOrDefaultAsync(p => p.Id == id);
                if (phone != null)
                    return View(phone);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Phone phone)
        {
            db.Phones.Update(phone);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        /*
            Здесь надо отметить, что оба метода принимают один единственный параметр id, 
            и из-за этого мы не можем назвать методы одинаково, как в случае с Edit или Create. 
            И поэтому первый метод назван ConfirmDelete. Однако используемый над ним атрибут
            ActionName("Delete") указывает, что этот метод также относится к действию Delete, 
            и поэтому мы к нему можем обращаться с запросом Home/Delete, а не Home/ConfirmDelete.

            В первом методе удаляемый объект просто извлекается из БД и передается в представление. 
            Во втором методе опять же по id получаем удаляемый объект и удаляем его с помощью метода
            db.Phones.Remove(). Данный метод генерирует sql-выражение DELETE, которое выполняется
            вызовом db.SaveChangesAsync().
        */

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                Phone phone = await db.Phones.FirstOrDefaultAsync(p => p.Id == id);
                if (phone != null)
                    return View(phone);
            }
            return NotFound();
        }

        /*
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Phone phone = await db.Phones.FirstOrDefaultAsync(p => p.Id == id);
                if (phone != null)
                {
                    db.Phones.Remove(phone);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }
        */
        // В post-методе Delete мы можем произвести небольшую оптимизацию.Иногда бывает важно 
        // узнать перед удалением, а есть ли такой объект в БД.Однако в данном случае мы получаем
        // два запроса к бд - один на получение объекта и второй на его удаление. И мы можем
        // оптимизировать метод следующим образом:
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Phone phone = new Phone { Id = id.Value };
                db.Entry(phone).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
