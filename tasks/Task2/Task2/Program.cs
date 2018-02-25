using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            User a = new User("test", "test", "test@test.com", "test");

            Console.WriteLine(a.ToString());

            if (a.CheckPassword("test"))
            {
                Console.WriteLine("Login success!");
            }
            else
            {
                Console.WriteLine("Login failed!");
            }

            if (a.ChangePassword("test", "test1"))
                Console.WriteLine("Changed password!");
            else
                Console.WriteLine("Passwort change failed!");

            a.Title = "Dr.";
            a.FirstName = "Test1";
            a.LastName = "Test1";

            if (a.ChangeEmail("test1", "test1@test.com"))
                Console.WriteLine("Changed email: " + a.Email);
            else
                Console.WriteLine("Email  change failed!");

            Console.WriteLine(a.ToString());

            if (a.CheckPassword("test"))
            {
                Console.WriteLine("Login success!");
            }
            else
            {
                Console.WriteLine("Login failed!");
            }
        }
    }
}
