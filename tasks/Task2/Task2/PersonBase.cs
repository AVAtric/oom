using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace Task2
{
    abstract class PersonBase : Person
    {
        /// <summary>
        /// Creates object with base data.
        /// </summary>
        /// <param name="first_name">First name</param>
        /// <param name="last_name">Last name</param>
        /// <param name="email">Email</param>
        public PersonBase(string first_name, string last_name, string email)
        {
            if (string.IsNullOrWhiteSpace(first_name))
                throw new ArgumentException("First name must not be empty.", nameof(first_name));
            if (string.IsNullOrWhiteSpace(last_name))
                throw new ArgumentException("Last name must not be empty.", nameof(last_name));
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email must not be empty.", nameof(email));
            if (!this.isValidEmail(email))
                throw new FormatException("Email must be a valit mail adress.");
            
            this.FirstName = first_name;
            this.LastName = last_name;
            this.Email = email;
        }

        /// <summary>
        /// Gender
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// First name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; }

        /// <summary>
        /// Validates email.
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>True if email has correct format. If not then false.</returns>
        protected bool isValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
