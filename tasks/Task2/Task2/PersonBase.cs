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
        /// Gets and sets title of person.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets and sets first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets and sets last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets email.
        /// </summary>
        public virtual string Email { get; }

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
