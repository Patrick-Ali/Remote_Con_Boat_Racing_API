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
    /// for Admin Login services.
    /// </summary>
    public class AdminLoginService
    {
        private readonly IMongoCollection<Admin> _admin;
        private readonly string passPhrase = "l%HJb5N^O@fl0K02H9PsxlR9algJTzK7ARBjJsd3fPG0&GwkrU";
        private readonly string passPhrase2 = "yUVyb$shjp4*%S6G!fx5t%i!fTZ@b8KQ#ymQyfhgNQ$#mKB0vA";

        /// <summary>
        /// Initalisation action
        /// </summary>
        /// <param name="config">
        /// Configuratuion for the database.
        /// </param>
        public AdminLoginService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("RCBR"));
            var database = client.GetDatabase("RCBR");
            _admin = database.GetCollection<Admin>("Admin");
        }

        /// <summary>
        /// This action handels verifying the submited
        /// email and password belong to an Admin.
        /// </summary>
        /// <param name="email">
        /// Submited email for consideration.
        /// </param>
        /// <param name="password">
        /// submited password for consideration.
        /// </param>
        /// <param name="admins">
        /// List of admins from the API to compare
        /// the email and password against.
        /// </param>
        /// <returns>
        /// If successful returns the admin's login details
        /// otherwise it returns null.
        /// </returns>
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
        /// This action can be used to determin if the given 
        /// email is associated with any admin.
        /// </summary>
        /// <param name="email">
        /// The email address submited for consideration.
        /// </param>
        /// <param name="users">
        /// List of admins from the API to compare
        /// the email and password against. 
        /// </param>
        /// <returns>
        /// If email is associated with an Admin it returns
        /// true.
        /// Otherwise it returns false.
        /// </returns>
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
