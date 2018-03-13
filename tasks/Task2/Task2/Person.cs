using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    interface Person
    {
        string LastName { get; set; }

        string FirstName { get; set; }

        string Title { get; set; }

        string Email { get; }
    }
}
