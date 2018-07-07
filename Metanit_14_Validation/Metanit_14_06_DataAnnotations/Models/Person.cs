﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Metanit_14_06_DataAnnotations.Models
{
    public class Person
    {
        [Display(Name = "Имя и фамилия")]
        public string Name { get; set; }

        /*
        Атрибут UIHint указывает, какой будет использоваться шаблон отображения при создании 
        разметки html для данного свойства. Шаблон управляет, как свойство будет рендерится 
        на странице.

        Имеются следующие встроенные шаблоны:

        Boolean
            Хелперы редактирования создают флажок (checkbox) для булевых значений. 
            Для значений типа bool? (nullable) создается элемент select с параметрами True, False 
            и Not Set
            Хелперы отображения генерируют те же элементы html, что и хелперы редактирования, 
            только с атрибутом disabled
        Collection
            Используется соответствующий шаблон для рендеринга каждого элемента коллекции. 
            Причем элементы могут быть разных типов.
        Decimal
            Хелперы редактирования создают однострочное текстовое поле - элемент input
        EmailAddress
            Хелперы редактирования создают однострочное текстовое поле.
            Хелперы отображения генерируют элемент ссылка, где атрибут href имеет значение mailto:url
        HiddenInput
            Создается скрытое поле - элемент hidden input
        Html
            Хелперы редактирования создают однострочное текстовое поле.
            Хелперы отображения просто показывают текст
        MultilineText
            Хелперы редактирования создают многострочное текстовое поле (элемент textarea)
        Object
            Хелперы изучают свойства объекта и выбирают наиболее подходящие для него шаблоны.
        Password
            Хелперы редактирования создают текстовое поле для ввода символов с использованием маски
            Хелперы отображения показывают пароль как есть, без использования маски
        String
            Хелперы редактирования создают однострочное текстовое поле
        Url
            Хелперы редактирования создают текстовое поле
            Хелперы отображения создают элемент ссылки для данного Url
        */
        [Display(Name = "Электронная почта")]
        [UIHint("Url")]
        public string Email { get; set; }

        [Display(Name = "Домашняя страница")]
        public string HomePage { get; set; }

        // Атрибут ScaffoldColumn позволяет скрыть отображение свойства при использовании хелперов 
        // Html.DisplayForModel() и Html.EditorForModel():
        // [ScaffoldColumn(false)]

        // Атрибут DataType позволяет предоставлять среде выполнения информацию об использовании 
        // свойства. Так, в классе Person имеется свойство Password, и с помощью атрибута мы указываем 
        // системе, что это свойство предназначено для хранения пароля:
        // [DataType(DataType.Password)]
        /*
            Для подобного свойства с атрибутом DataType.Password хелперы создают элемент ввода, 
            у которого атрибут type имеет значение "password". Тогда в браузере вы при вводе данных 
            вы не увидите вводимые символы, а вместо них будут выводиться точки.

            Перечисление DataType может принимать несколько различных значений:
                CreditCard: отображает номер кредитной карты
                Currency: отображает текст в виде валюты
                Date: отображает только дату, без времени
                DateTime: отображает дату и время
                Time: отображает только время
                Duration: отображает число - некоторую продолжительность
                EmailAddress: отображает электронный адрес
                Password: отображает символы с использованием маски
                PhoneNumber: отображает номер телефона
                PostalCode: отображает почтовый индекс
                ImageUrl: представляет путь к изображению
                Url: отображает строку Url
                MultilineText: отображает многострочный текст (элемент textarea)
                Text: отображает однострочный текст
        */

        [ScaffoldColumn(false)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        /*
            Атрибут DisplayFormat позволяет задать формат отображения свойства. 
            Например, пусть у нас будет в модели свойство по типу DateTime:
        */
        [Display(Name = "Дата рождения")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        /*
        [Display(Name = "Возраст")]
        public int Age { get; set; }
        */
    }
}
