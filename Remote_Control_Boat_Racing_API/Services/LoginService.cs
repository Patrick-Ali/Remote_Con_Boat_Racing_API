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
    /// This controller is responsible for processing data
    /// for User Login services.
    /// </summary>
    public class LoginService
    {
        private readonly IMongoCollection<User> _user;
        private readonly string passPhrase = "l%HJb5N^O@fl0K02H9PsxlR9algJTzK7ARBjJsd3fPG0&GwkrU";
        private readonly string passPhrase2 = "yUVyb$shjp4*%S6G!fx5t%i!fTZ@b8KQ#ymQyfhgNQ$#mKB0vA";

        /// <summary>
        /// Initalisation action
        /// </summary>
        /// <param name="config">
        /// Configuratuion for the database.
        /// </param>
        public LoginService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("RCBR"));
            var database = client.GetDatabase("RCBR");
            _user = database.GetCollection<User>("User");
        }

        /// <summary>
        /// This action handels verifying the submited
        /// email and password belong to a User.
        /// </summary>
        /// <param name="email">
        /// Submited email for consideration.
        /// </param>
        /// <param name="password">
        /// submited password for consideration.
        /// </param>
        /// <param name="users">
        /// List of users from the API to compare
        /// the email and password against.
        /// </param>
        /// <returns>
        /// If successful returns the user's login details
        /// otherwise it returns null.
        /// </returns>
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

        /// <summary>
        /// This action can be used to determin if the given 
        /// email is associated with any user.
        /// </summary>
        /// <param name="email">
        /// The email address submited for consideration.
        /// </param>
        /// <param name="users">
        /// List of users from the API to compare
        /// the email and password against. 
        /// </param>
        /// <returns>
        /// If email is associated with a user it returns
        /// true.
        /// Otherwise it returns false.
        /// </returns>
        public bool Check(string email, List<User> users) {
            //string tempHold = Crypto.Decrypt(email, passPhrase);
            foreach (User element in users)
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
