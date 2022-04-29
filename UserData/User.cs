using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserData
{

    public class User
    {
            public static List<User> users = new List<User>();
            public static int count = 0;
            public int Id;
            public string Name;
            public string Email;
            public int Age;
            public string MaritalStatus;
            
            DatabaseDataSet databaseDataSet = new DatabaseDataSet();

        public User()
            {

                
            }

        public static List<User> LoadUsersFromFile(string filename)
        {
            //var users = new List<User>();
            foreach (var line in File.ReadAllLines(filename))
            {
                var columns = line.Split(',');
                users.Add(new User
                {
                    Id = Convert.ToInt32(columns[0]),
                    Name = columns[1],
                    Email = columns[2],
                    Age = Convert.ToInt32(columns[3]),
                    MaritalStatus = columns[4]
                });
            }
            return users;
        }


        public int IdCounter(string filename)
        {
            while (File.ReadAllLines(filename) != null)
            {
                count++;
            }
            return count;
        }
        /*
        public int AddUser(int Id, string Name, string Email, int Age, string MaritalStatus)
            {
                string filename = @"C:\Users\space\Documents\vor2022\c_sharp\UserStatus\user.txt";
                if (File.Exists(filename))
                {
                    Id = File.ReadAllLines(filename).Length;
                }
                userList.Add($"Id: {Id} Name: {Name} Email: {Email}, Age: {Age} Marital satus: {MaritalStatus} \n");
                return Id;
            }
        


            public override string ToString()
            {
                return String.Join(Environment.NewLine, userList);
            }

        */
        

        public class NewUser
        {
            public void SaveToFile(User user, string filename, bool overwrite = false)
            {
                File.AppendAllText(filename, user.ToString());
            }
        }
    }
}
