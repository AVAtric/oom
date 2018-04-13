using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using Bogus;
using System.Reactive.Disposables;
using System.Reactive.Concurrency;
using static System.Console;

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Person> persons = new List<Person>();
            var gender = new[] { "male", "female" };

            var test_user = new Faker<User>()
                .CustomInstantiator(f =>
                {
                    var fn = f.Name.FirstName();
                    var ln = f.Name.LastName();

                    return new User(fn, ln, f.Internet.Email(fn, ln), f.Internet.Password());
                })
                .RuleFor(usr => usr.Gender, f => f.PickRandom(gender))
                .RuleFor(usr => usr.Title, f => f.Name.JobTitle())
                .RuleFor(usr => usr.IsAdmin, f => f.Random.Bool())
                ;

            var test_customer = new Faker<Customer>()
                .CustomInstantiator(f =>
                {
                    var fn = f.Name.FirstName();
                    var ln = f.Name.LastName();

                    return new Customer(fn, ln, f.Internet.Email(fn, ln));
                })
                .RuleFor(cust => cust.Gender, f => f.PickRandom(gender))
                .RuleFor(cust => cust.Title, f => f.Name.JobTitle())
                .RuleFor(cust => cust.Visits, f => f.Random.Number(1000))
                .RuleFor(cust => cust.HighPriority, f => f.Random.Bool())
                ;

            for (var i = 0; i < 3; i++)
            {
                persons.Add(test_user.Generate());
                persons.Add(test_customer.Generate());
            }

            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory.ToString(), "SerializedObjects.json");

            if (File.Exists(path))
                File.Delete(path);

            var persons1 = JsonConvert.SerializeObject(persons);

            WriteLine(persons1);

            File.WriteAllText(path, persons1);

            var persons2 = JsonConvert.DeserializeObject(File.ReadAllText(path));

            WriteLine(persons2.ToString());

            foreach (var onePerson in persons)
                WriteLine(onePerson.Email);

            WriteLine("-----------------------------------------------------------------------------------------------------");

            var a = persons
                .OfType<User>()
                .First()
                ;

            WriteLine(a.ToString());

            if (a.CheckPassword("test"))
                WriteLine("Login success!");
            else
                WriteLine("Login failed!");

            if (a.ChangePassword("test", "test1"))
                WriteLine("Changed password!");
            else
                WriteLine("Passwort change failed!");

            a.Title = "Dr.";
            a.FirstName = "Test1";
            a.LastName = "Test1";

            if (a.ChangeEmail("test1", "test1@test.com"))
                WriteLine("Changed email: " + a.Email);
            else
                WriteLine("Email  change failed!");

            WriteLine(a.ToString());

            if (a.CheckPassword("test"))
                WriteLine("Login success!");
            else
                WriteLine("Login failed!");

            WriteLine("-----------------------------------------------------------------------------------------------------");

            Thread.Sleep(200);

            var customer_producer = new Subject<Customer>();

            customer_producer
                .Where(x => x.Visits > 500)
                .Take(10)
                .Subscribe(x => 
                    {
                        WriteLine("*******************BEGIN**CUST*BY*VISITS*****************");
                        WriteLine(x.ToString());
                        WriteLine("*******************END****CUST*BY*VISITS*****************");
                    })
                ;

            var cts = new CancellationTokenSource();

            Task
                .Run(() =>
                    {
                        var cust_list = new List<Customer>();

                        for(var i = 0; i < 20 ; i++ )
                        {
                            Thread.Sleep(200);
                            cust_list.Add(test_customer.Generate());
                            customer_producer.OnNext(cust_list.Last());
                        }

                        return cust_list;
                    })
                .ContinueWith(cust => SearchImportantCustomers(cust.Result, cts.Token))
                ;

            IObservable<User> ob =
                Observable.Create<User>(o =>
                {
                    var cancel = new CancellationDisposable();
                    NewThreadScheduler.Default.Schedule(() =>
                    {
                        for (; ; )
                        {
                            Thread.Sleep(200);
                            if (!cancel.Token.IsCancellationRequested)
                            {
                                o.OnNext(test_user.Generate());
                            }
                            else
                            {
                                o.OnCompleted();
                                return;
                            }
                            
                        }
                    }
                    );

                    return cancel;
                });

            var user_subscription = ob
                .Where(usr => usr.IsAdmin == true)
                .Select(usr => $"{usr.Title} {usr.FirstName} {usr.LastName}")
                .Take(20)
                .Finally(() => WriteLine("Press any key to exit!"))
                .Subscribe(usr =>
                    {
                        WriteLine("*******************BEGIN**Admin*****************");
                        Console.WriteLine(usr);
                        WriteLine("*******************END****Admin*****************");
                    })
                ;

            ReadKey();
            cts.Cancel();
            user_subscription.Dispose();
        }

        static async Task SearchImportantCustomers(List<Customer> customers, CancellationToken ct)
        {
            foreach(var cust in customers)
            {
                ct.ThrowIfCancellationRequested();
                if (await IsHighPriorityCustomer(cust, ct)) WriteLine($"{cust.Email} is high priority");
            }
        }

        static Task<bool> IsHighPriorityCustomer(Customer usr, CancellationToken ct)
        {
            return Task.Run(() =>
            {
                Thread.Sleep(200);
                if (usr.HighPriority) return true;

                return false;
            }, ct);
        }
    }
}
