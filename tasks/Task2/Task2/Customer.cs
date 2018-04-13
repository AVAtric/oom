using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    class Customer : PersonBase
    {
        /// <summary>
        /// Creates a new customer object.
        /// </summary>
        /// <param name="first_name">First name</param>
        /// <param name="last_name">Last name</param>
        /// <param name="email">Email</param>
        public Customer(string first_name, string last_name, string email) : base(first_name, last_name, email)
        {
            this.Visits = 0;
            this.HighPriority = false;
        }

        /// <summary>
        /// Visits
        /// </summary>
        public int Visits { get; set; }

        /// <summary>
        /// Is customer high priority
        /// </summary>
        public bool HighPriority { get; set; }

        /// <summary>
        /// Increments visits by one
        /// </summary>
        public void AddVisit()
        {
            this.Visits = ++this.Visits;
        }


        /// <summary>
        /// Override method to see all content.
        /// </summary>
        /// <returns>Text with contents of this object.</returns>
        override public string ToString()
        {
            return string.Join("\r\n",
                new string[] {
                    "Gender: " + this.Gender,
                    "Title: " + this.Title,
                    "First name: " + this.FirstName,
                    "Last name: " + this.LastName,
                    "Email: " + this.Email,
                    "Visits: " + this.Visits
                });
        }
    }
}
