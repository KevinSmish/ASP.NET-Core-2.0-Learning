using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Metanit_11_05_ModelBinding.Models
{
    // Атрибут BindRequired требует обязательного наличия значения для свойства модели.
    // Атрибут BindNever указывает, что свойство модели надо исключить из механизма привязки.

    // Кроме того, мы можем применять атрибут BindingBehavior, который устанавливает поведение 
    // привязки с помощью одно из значений одноименного перечисления BindingBehavior:
    //  Required: аналогично примению атрибута BindRequired
    //  Never: аналогично примению атрибута BindNever
    //  Optional: действие по умолчанию, мы можем передавать значение, 
    //      а можем и не передавать, тогда будут применяться значения по умолчанию

    public class User
    {
        public int Id { get; set; }

        //[BindRequired]
        [BindingBehavior(BindingBehavior.Required)]
        public string Name { get; set; }

        [BindingBehavior(BindingBehavior.Optional)]
        public int Age { get; set; }

        //[BindNever]
        [BindingBehavior(BindingBehavior.Never)]
        public bool HasRight { get; set; }
    }
}
