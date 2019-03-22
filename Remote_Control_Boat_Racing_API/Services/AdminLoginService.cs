using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Remote_Control_Boat_Racing_API.Models;
using Microsoft.Extensions.Configuration;

namespace Remote_Control_Boat_Racing_API.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class AdminLoginService
    {
        private readonly IMongoCollection<Admin> _admin;
        private readonly string passPhrase = "l%HJb5N^O@fl0K02H9PsxlR9algJTzK7ARBjJsd3fPG0&GwkrU";
        private readonly string passPhrase2 = "yUVyb$shjp4*%S6G!fx5t%i!fTZ@b8KQ#ymQyfhgNQ$#mKB0vA";

        /// <summary>
        /// 
        /// </summary>
        public AdminLoginService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("RCBR"));
            var database = client.GetDatabase("RCBR");
            _admin = database.GetCollection<Admin>("Admin");
        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="email"></param>
       /// <param name="password"></param>
       /// <param name="admins"></param>
       /// <returns></returns>
        public Login Login(string email, string password, List<Admin> admins)
        {
            string emailDec = Crypto.Decrypt(email, passPhrase);
            string passwordDec = Crypto.Decrypt(password, passPhrase);
            foreach (Admin admin in admins)
            {
                string tempEmail = Crypto.Decrypt(admin.Email, passPhrase);
                if (tempEmail == emailDec)
                {
                    string tempPassword = Crypto.Decrypt(admin.Password, passPhrase);
                    bool confirm = Crypto.ConfirmPassword(passwordDec, tempPassword);
                    if (confirm)
                    {
                        return new Login()
                        {
                            Id = admin.Id,
                            Email = admin.Email
                        };
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="users"></param>
        /// <returns></returns>
        public bool Check(string email, List<Admin> users)
        {
            //string tempHold = Crypto.Decrypt(email, passPhrase);
            foreach (Admin element in users)
            {
                string temp = Crypto.Decrypt(element.Email, passPhrase);
                if (email == temp)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
