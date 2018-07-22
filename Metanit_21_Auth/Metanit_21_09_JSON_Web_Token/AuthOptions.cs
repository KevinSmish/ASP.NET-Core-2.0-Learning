using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metanit_21_09_JSON_Web_Token.Models
{
    //  класс AuthOptions будет описывать ряд свойств, нужных для генерации токена:
    // Константа ISSUER представляет издателя токена. Здесь можно определить любое название. 
    // AUDIENCE представляет потребителя токена - опять же может быть любая строка, 
    // но в данном случае указан адрес текущего приложения.
    // Константа KEY хранит ключ, который будет применяться для создания токена

    public class AuthOptions
    {
        public const string ISSUER = "MyAuthServer"; // издатель токена
        public const string AUDIENCE = "http://localhost:44349/"; // потребитель токена
        const string KEY = "mysupersecret_secretkey!123";   // ключ для шифрации
        public const int LIFETIME = 1; // время жизни токена - 1 минута
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
