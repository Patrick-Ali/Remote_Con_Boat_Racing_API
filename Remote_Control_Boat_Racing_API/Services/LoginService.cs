using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Remote_Control_Boat_Racing_API.Models;
using Microsoft.Extensions.Configuration;

namespace Remote_Control_Boat_Racing_API.Services
{
    public class LoginService
    {
        private readonly IMongoCollection<User> _user;
        private readonly string passPhrase = "l%HJb5N^O@fl0K02H9PsxlR9algJTzK7ARBjJsd3fPG0&GwkrU";
        private readonly string passPhrase2 = "yUVyb$shjp4*%S6G!fx5t%i!fTZ@b8KQ#ymQyfhgNQ$#mKB0vA";

        public LoginService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("RCBR"));
            var database = client.GetDatabase("RCBR");
            _user = database.GetCollection<User>("User");
        }

        public Login Login(string email, string password, List<User> users) {
            string emailDec = Crypto.Decrypt(email, passPhrase);
            string passwordDec = Crypto.Decrypt(password, passPhrase);
            foreach (User user in users)
            {
                string tempEmail = Crypto.Decrypt(user.Email, passPhrase);
                if (tempEmail == emailDec) {
                    string tempPassword = Crypto.Decrypt(user.Password, passPhrase);
                    bool confirm = Crypto.ConfirmPassword(passwordDec, tempPassword);
                    if (confirm)
                    {
                        return new Login()
                        {
                            Id = user.Id,
                            Email = user.Email
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


    }
}
