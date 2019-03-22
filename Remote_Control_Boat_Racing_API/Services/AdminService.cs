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
    public class AdminService
    {
        private readonly IMongoCollection<Admin> _admin;
        private readonly string passPhrase = "l%HJb5N^O@fl0K02H9PsxlR9algJTzK7ARBjJsd3fPG0&GwkrU";
        private readonly string passPhrase2 = "yUVyb$shjp4*%S6G!fx5t%i!fTZ@b8KQ#ymQyfhgNQ$#mKB0vA";
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public AdminService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("RCBR"));
            var database = client.GetDatabase("RCBR");
            _admin = database.GetCollection<Admin>("Admin");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Admin> Get()
        {
            List<Admin> hold = _admin.Find(user => true).ToList();
            List<Admin> final = ChangeEnc(hold);

            return final;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="admins"></param>
        /// <returns></returns>
        public List<Admin> ChangeEnc(List<Admin> admins)
        {
            List<Admin> publicEncUsers = new List<Admin>();

            foreach (Admin admin in admins)
            {
                admin.Email = Crypto.Decrypt(admin.Email, passPhrase2);
                admin.Password = Crypto.Decrypt(admin.Password, passPhrase2);

                Admin crypto = new Admin();
                crypto.Id = admin.Id;
                crypto.Email = Crypto.Encrypt(admin.Email, passPhrase);
                crypto.Password = Crypto.Encrypt(admin.Password, passPhrase);


                publicEncUsers.Add(crypto);
            }
            return publicEncUsers;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Admin Get(string id)
        {
            Admin tempUser = _admin.Find<Admin>(user => user.Id == id).FirstOrDefault();
            List<Admin> temp = new List<Admin>();
            temp.Add(tempUser);
            List<Admin> final = ChangeEnc(temp);
            Admin sendUser = final[0];
            return sendUser;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="admin"></param>
        /// <returns></returns>
        public Admin Create(Admin admin)
        {
            List<Admin> users = Get();
            string tempHold = Crypto.Decrypt(admin.Email, passPhrase);
            foreach (Admin element in users)
            {
                string temp = Crypto.Decrypt(element.Email, passPhrase);
                if (admin.Email == temp)
                {
                    return null;
                }
            }
            Admin crypto = CreateEnc(admin);
            _admin.InsertOne(crypto);
            return crypto;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="admin"></param>
        /// <returns></returns>
        public Admin CreateEnc(Admin admin)
        {


            admin.Email = Crypto.Decrypt(admin.Email, passPhrase);
            admin.Password = Crypto.Decrypt(admin.Password, passPhrase);


            string passHold = Crypto.HashPassword(admin.Password);

            Admin crypto = new Admin();

            crypto.Email = Crypto.Encrypt(admin.Email, passPhrase2);

            crypto.Password = Crypto.Encrypt(passHold, passPhrase2);


            return crypto;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="adminIn"></param>
        public void Update(string id, Admin adminIn)
        {
            _admin.ReplaceOne(admin => admin.Id == id, adminIn);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adminIn"></param>
        public void Remove(Admin adminIn)
        {
            _admin.DeleteOne(admin => admin.Id == adminIn.Id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void Remove(string id)
        {
            _admin.DeleteOne(admin => admin.Id == id);
        }
    }
}
