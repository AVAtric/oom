using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Task2
{
    class User : Person
    {
        private string m_email;

        readonly Guid m_uuid = Guid.NewGuid();
        readonly byte[] 
            m_salt = new byte[16],
            m_hash = new byte[36];

        public User(string first_name, string last_name, string email, string password)
        {
            if (string.IsNullOrWhiteSpace(first_name))
                throw new ArgumentException("First name must not be empty.", nameof(first_name));
            if (string.IsNullOrWhiteSpace(last_name))
                throw new ArgumentException("Last name must not be empty.", nameof(last_name));
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email must not be empty.", nameof(email));
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password must not be empty.", nameof(password));

            this.m_email = email;
            this.FirstName = first_name;
            this.LastName = last_name;

            this.SavePassword(password);
        }

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
        public string Email
        {
            get
            {
                return this.m_email;
            }
        }

        /// <summary>
        /// Changes Password.
        /// </summary>
        /// <param name="old_password">Old password</param>
        /// <param name="new_password">New password</param>
        /// <returns></returns>
        public bool ChangePassword(string old_password, string new_password)
        {
            if (this.CheckPassword(old_password))
            {
                this.SavePassword(new_password);
                return true;
            }

            return false;

        }

        /// <summary>
        /// Checks password.
        /// </summary>
        /// <param name="password">Password</param>
        /// <returns></returns>
        public bool CheckPassword(string password)
        {
            byte[] hash = this.HashPassword(password);
            
            for (int i = 0; i < 20; i++)
                if (this.m_hash[i + 16] != hash[i])
                    return false;

            return true;
        }

        /// <summary>
        /// Saves new email and verify by passing password.
        /// </summary>
        /// <param name="password">Password to verify</param>
        /// <param name="email">Email</param>
        /// <returns></returns>
        public bool ChangeEmail(string password, string email)
        {
            if (this.CheckPassword(password))
            {
                this.m_email = email;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Salt and pepper.
        /// </summary>
        /// <param name="password">Password</param>
        /// <returns>Hashed passowrd</returns>
        private byte[] HashPassword(string password)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password, this.m_salt, 1000);
            return pbkdf2.GetBytes(20);
        }

        /// <summary>
        /// Saves password.
        /// </summary>
        /// <param name="password">Password</param>
        private void SavePassword(string password)
        {
            new RNGCryptoServiceProvider().GetBytes(this.m_salt);

            Array.Copy(this.m_salt, 0, this.m_hash, 0, 16);
            Array.Copy(this.HashPassword(password), 0, this.m_hash, 16, 20);
        }
        
        /// <summary>
        /// Override method to see all content.
        /// </summary>
        /// <returns></returns>
        override public string ToString()
        {
            return string.Join("\r\n",
                new string[] {
                    "Email: " + this.m_email,
                    "UUID: " + this.m_uuid.ToString(),
                    "Title: " + this.Title,
                    "First name: " + this.FirstName,
                    "Last name: " + this.LastName,
                    "Salt: " + Convert.ToBase64String(this.m_salt),
                    "Hash: " + Convert.ToBase64String(this.m_hash) });
        }
    }
}
