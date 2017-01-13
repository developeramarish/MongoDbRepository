using MongoDbRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDbRepository.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            MongoRepository<User> userRepository = new MongoRepository<User>();

            //Insert
            userRepository.Insert(new User()
            {
                UserName = "fsefacan",
                Email = "fsefacan@gmail.com",
                Password = "123456"
            });

            //Insert
            userRepository.Insert(new User()
            {
                UserName = "sefacan01",
                Email = "sefacan01@windowslive.com",
                Password = "654321"
            });

            //Get All
            var users = userRepository.Table.ToList();
            foreach (var item in users)
                Console.WriteLine(string.Format("User List = Username : {0}, Email : {1}, Password : {2}", item.UserName, item.Email, item.Password));

            //Find Single
            var user = userRepository.Find(x => x.UserName == "sefacan01");
            Console.WriteLine(string.Format("Single User = Username : {0}, Email : {1}, Password : {2}", user.UserName, user.Email, user.Password));

            //Update
            user.Email = "sefacan01@hotmail.com";
            userRepository.Update(user);
            
            //Delete
            userRepository.Delete(user);
        }
    }

    //Inherit from MongoEntity !
    class User : MongoEntity
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}