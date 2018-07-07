using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Metanit_14_01_Validation_Sample.Models
{
    public class Person
    {
        /*
        Атрибут StringLength
        Чтобы пользователь не мог ввести очень длинный текст, применяется атрибут StringLength. 
        Первым параметром в конструкторе атрибута идет максимальная допустимая длина строки. 
        Именованные параметры, в частности MinimumLength и ErrorMessage, позволяют задать 
        дополнительные опции отображения.
        */

        [PersonName(new string[] { "Tom", "Sam", "Alice", "Вася" }, ErrorMessage = "Имя не из списка - (Tom, Sam, Alice, Вася)")]
        [Required(ErrorMessage = "Не указано имя. Только не указывайте имя Вася ;)")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        public string Name { get; set; }

        /*
        Атрибут RegularExpression
        Использование данного атрибута предполагает, что вводимое значение должно соответствовать 
        указанному в этом атрибуте регулярному выражению.

        Наиболее распространенный пример - это проверка адреса электронной почты на корректность.
        Например, класс модели содержит свойство Email:
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        public string Email { get; set; }

        Если введенное значение не будет соответствовать регулярному выражению, то будет отображено 
        сообщение об ошибке.

        Атрибут Remote
        Атрибут Remote из пространства имен Microsoft.AspNetCore.Mvc; для валидации свойства 
        выполняет запрос на сервер к определенному методу контроллера. И если требуемый метод 
        контроллера вернет значение false, то валидация не пройдена. Например:
            [Remote(action: "CheckEmail", controller: "Home", ErrorMessage ="Email уже используется")]
            public string Email { get; set; }
        */

        [Required(ErrorMessage = "Не указан электронный адрес. Учтите, что адрес 'admin@mail.ru' занят")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        [Remote(action: "CheckEmail", controller: "Home", ErrorMessage = "Email уже используется")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Имя и пароль не должны совпадать")]
        public string Password { get; set; }

        // Атрибут Compare гарантирует, что два свойства объекта модели имеют одно и то же значение. 
        //Если, например, надо, чтобы пользователь ввел пароль дважды:
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string PasswordConfirm { get; set; }

        // Атрибут Range определяет минимальные и максимальные ограничения для числовых данных.
        [Required]
        [Range(1, 110, ErrorMessage = "Недопустимый возраст")]
        public int Age { get; set; }

        /*
            Специальные атрибуты

            Выше мы применяли регулярное выражение для проверки адреса электронной почты. 
            Проверки на корректность электронной почты, адреса url, номера телефона и 
            кредитной карты довольно часто встречаются, что для них определены специальные атрибуты:

            [CreditCard]
            [EmailAddress]
            [Phone]
            [Url]

            Например:
                [EmailAddress (ErrorMessage = "Некорректный адрес")]
                public string Email { get; set; }
        */
    }
}
