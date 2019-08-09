using Hackaton.Business;
using Hackaton.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hackaton.Tests
{
    [TestClass()]
    public class AttendeBusinessTest
    {

        private HashSet<Attende> LoadDemoAttendants1()
        {
            return new HashSet<Attende>()
            {
                new Attende()
                {
                    City = "Raccoon City",
                    Email = "leon.kennedy@example.com",
                    Dates = new List<DateTime>()
                    {
                        new DateTime(2019,7,20),
                        new DateTime(2019,7,21),
                        new DateTime(2019,8,15),
                        new DateTime(2019,10,15)
                    }
                },
                new Attende()
                {
                    City = "Raccoon City",
                    Email = "jill.valentine@example.com",
                    Dates = new List<DateTime>()
                    {
                        new DateTime(2019,7,20),
                        new DateTime(2019,8,15),
                        new DateTime(2019,10,15)

                    }
                }
            };
        }

        /// <summary>
        /// This is the same case that in the documentation. Extra dates were added in the wrong range to validate 
        /// they are not being considered
        /// </summary>
        [TestMethod()]
        public void ProcessAttendants_Test()
        {
            var attendants = LoadDemoAttendants1();

            var result = new AttendeBusiness().ProcessAttendants(attendants);

            Assert.AreEqual("Raccoon City", result.FirstOrDefault().City);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(0, DateTime.Compare(result.FirstOrDefault().StartDate, new DateTime(2019, 7, 20)));
            Assert.AreEqual(0, DateTime.Compare(result.FirstOrDefault().EndDate, new DateTime(2019, 7, 21)));
            Assert.AreEqual(2, result.FirstOrDefault().Total);

        }

        private HashSet<Attende> LoadDemoAttendants2()
        {
            var attendants = LoadDemoAttendants1();
            attendants.Add(new Attende()
            {
                City = "New York",
                Dates = new List<DateTime>()
                {
                    new DateTime(2019,7,30),
                    new DateTime(2019,7,28)
                },
                Email = "2"
            });

            attendants.Add(new Attende()
            {
                City = "Raccoon City",
                Dates = new List<DateTime>()
                {
                    new DateTime(2019,7,30),
                    new DateTime(2019,7,28),
                    new DateTime(2019,8,15),
                    new DateTime(2019,10,15)

                },
                Email = "3"
            });

            attendants.Add(new Attende()
            {
                City = "New York",
                Dates = new List<DateTime>()
                {
                    new DateTime(2019,7,30),
                    new DateTime(2019,7,29),
                    new DateTime(2019,8,15)
                },
                Email = "4"
            });

            attendants.Add(new Attende()
            {
                City = "Raccoon City",
                Dates = new List<DateTime>()
                {
                    new DateTime(2019,10,15)

                },
                Email = "5"
            });

            return attendants;

        }

        /// <summary>
        /// In this case, for New York we have more that 1 city with the same number of attendants per date
        /// but 29-30 should be selected because there is an attendant that can assist in both dates.
        /// </summary>
        [TestMethod()]
        public void ProcessAttendants_Test2()
        {
            var attendants = LoadDemoAttendants2();

            var result = new AttendeBusiness().ProcessAttendants(attendants);

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(0, DateTime.Compare(result.FirstOrDefault(t => t.City == "Raccoon City").StartDate, new DateTime(2019, 8, 15)));
            Assert.AreEqual(0, DateTime.Compare(result.FirstOrDefault(t => t.City == "Raccoon City").EndDate, new DateTime(2019, 8, 16)));
            Assert.AreEqual(3, result.FirstOrDefault(t => t.City == "Raccoon City").Total);

            Assert.AreEqual(0, DateTime.Compare(result.FirstOrDefault(t => t.City == "New York").StartDate, new DateTime(2019, 7, 29)));
            Assert.AreEqual(0, DateTime.Compare(result.FirstOrDefault(t => t.City == "New York").EndDate, new DateTime(2019, 7, 30)));
            Assert.AreEqual(2, result.FirstOrDefault(t => t.City == "New York").Total);

        }
    }
}
