using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Metanit_08_02_Razor.Services
{
    public class EmailMessageSender: IMessageSender 
    {
        public string SendMessage()
        {
            return "сообщение отправлено на email";
        }
    }
}
