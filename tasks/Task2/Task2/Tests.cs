using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Task2
{
    class Tests
    {
        [Test]
        public void EmailPropertyReturnsSameValueUsedForInitialization()
        {
            var x = new User("First", "Last", "example@example.com", "password");
            Assert.IsTrue(x.Email == "example@example.com");
        }

        [Test]
        public void CannotCreateUserWithInvalEmail()
        {
            Assert.Catch(() =>
            {
                var x = new User("First", "Last", "test", "test");
            });
        }

        [Test]
        public void FirstNamePropertyReturnsSameValueUsedForInitialization()
        {
            var x = new Customer("First", "Last", "example@example.com");
            Assert.IsTrue(x.FirstName == "First");
        }

        [Test]
        public void VisitsIncrementedCorrect()
        {
            var x = new Customer("First", "Last", "example@example.com");
            x.AddVisit();
            Assert.IsTrue(x.Visits == 1);
        }

        [Test]
        public void UserChangesPasswordSuccess()
        {
            var x = new User("First", "Last", "example@example.com", "password");
            Assert.IsTrue(x.ChangePassword("password", "newpassword"));
        }

        [Test]
        public void UserChangesPasswordFail()
        {
            var x = new User("First", "Last", "example@example.com", "password");
            Assert.IsFalse(x.ChangePassword("wrong", "newpassword"));
        }

        [Test]
        public void UserCheckPasswordSuccess()
        {
            var x = new User("First", "Last", "example@example.com", "password");
            Assert.IsTrue(x.CheckPassword("password"));
        }

        [Test]
        public void UserCheckPasswordFail()
        {
            var x = new User("First", "Last", "example@example.com", "password");
            Assert.IsFalse(x.CheckPassword("wrong"));
        }
    }
}
