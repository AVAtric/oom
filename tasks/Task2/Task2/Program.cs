using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Person> persons = new List<Person>();

            User a = new User("test1", "test1", "test1@test.com", "test");
            User b = new User("test2", "test2", "test2@test.com", "test");
            User c = new User("test3", "test3", "test3@test.com", "test");

            Customer c1 = new Customer("cust1", "cust1", "cust1@test.com");
            Customer c2 = new Customer("cust2", "cust2", "cust2@test.com");
            Customer c3 = new Customer("cust3", "cust3", "cust3@test.com");

            persons.Add(a);
            persons.Add(b);
            persons.Add(c);
            persons.Add(c1);
            persons.Add(c2);
            persons.Add(c3);

            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory.ToString(), "SerializedObjects.json");

            if (File.Exists(path))
                File.Delete(path);

            var persons1 = JsonConvert.SerializeObject(persons);

            Console.WriteLine(persons1);

            File.WriteAllText(path, persons1);

            var persons2 = JsonConvert.DeserializeObject(File.ReadAllText(path));

            Console.WriteLine(persons2.ToString());

            foreach(var onePerson in persons)
                Console.WriteLine(onePerson.Email);

            Console.WriteLine(a.ToString());

            if (a.CheckPassword("test"))
                Console.WriteLine("Login success!");
            else
                Console.WriteLine("Login failed!");

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
                Console.WriteLine("Login success!");
            else
                Console.WriteLine("Login failed!");
        }
    }
}
