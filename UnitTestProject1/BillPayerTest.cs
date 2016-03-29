using System;
using BillPayerCore.DataModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class BillPayerTest
    {
        [TestMethod]
        public void UserTest1()
        {
            User user = new User(100, "Matthew", "Williams", "matthewfoucher@gmail.com", "Password1", "Male");
            Assert.AreEqual("Matthew", user.FirstName);
            Assert.AreEqual("Williams", user.LastName);
            Assert.AreEqual(100, user.Id);
            Assert.AreEqual("matthewfoucher@gmail.com", user.Email);
            Assert.AreEqual("Password1", user.Password);
            Assert.AreEqual("Male", user.Sex);
        }

        [TestMethod]
        public void BillTest1()
        {
            Bill bill = new Bill(73, "Rent", 1700, true);
            Assert.AreEqual(73, bill.Id);
            Assert.AreEqual("Rent", bill.Name);
            Assert.AreEqual(1700, bill.Cost);
            Assert.AreEqual(true, bill.Recurring);
        }
    }
}
