using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    /// <summary>
    /// Global definition of a person
    /// </summary>
    interface Person
    {
        /// <summary>
        /// Gender
        /// </summary>
        string Gender { get; set; }

        /// <summary>
        /// Last name
        /// </summary>
        string LastName { get; set; }

        /// <summary>
        /// First name
        /// </summary>
        string FirstName { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        string Email { get; }
    }
}
