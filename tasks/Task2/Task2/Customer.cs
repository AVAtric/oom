using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    class Customer : PersonBase
    {
        public Customer(string first_name, string last_name, string email)
        {
            if (string.IsNullOrWhiteSpace(first_name))
                throw new ArgumentException("First name must not be empty.", nameof(first_name));
            if (string.IsNullOrWhiteSpace(last_name))
                throw new ArgumentException("Last name must not be empty.", nameof(last_name));
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email must not be empty.", nameof(email));
            
            this.Email = email;
            this.FirstName = first_name;
            this.LastName = last_name;
            this.Visits = 0;
        }

        /// <summary>
        /// Gets email.
        /// </summary>
        public override string Email { get; }

        /// <summary>
        /// Gets the visits of the customer
        /// </summary>
        public int Visits { get; set; }

        public void addVisit()
        {
            this.Visits = ++this.Visits;
        }
    }
}
