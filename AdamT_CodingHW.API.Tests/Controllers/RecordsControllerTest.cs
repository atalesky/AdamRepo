using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdamT_CodingHW.API.Controllers;

namespace AdamT_CodingHW.API.Tests.Controllers
{
    [TestClass]
    public class RecordsControllerTest
    {
        [TestMethod]
        public void AdamT_AllApiTests()
        {
            // We need to do these in order every time, as the post can throw off 
            // other test counts - so we need to combine them.

            AssertScenarioGetByGender();

            AssertScenarioGetByBirthDate();

            AssertScenarioGetByLastName();

            AssertScenarioPostNewPerson();
        }

        public void AssertScenarioGetByGender()
        {
            // Setup TEst
            RecordsController controller = new RecordsController();
            const int expectedRecordCount = 6;
            const string expectedGenderFeMale = "F";
            const string expectedGenderMale = "M";

            // Act
            List<Person> result = controller.GetByGender();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedRecordCount, result.Count());

            Person recordOne = result.ElementAt(0);
            Assert.AreEqual(expectedGenderFeMale, recordOne.Gender);
            Person recordTwo = result.ElementAt(1);
            Assert.AreEqual(expectedGenderFeMale, recordTwo.Gender);
            Person recordThree = result.ElementAt(2);
            Assert.AreEqual(expectedGenderFeMale, recordThree.Gender);
            Person recordFour = result.ElementAt(3);
            Assert.AreEqual(expectedGenderMale, recordFour.Gender);
            Person recordFive = result.ElementAt(4);
            Assert.AreEqual(expectedGenderMale, recordFive.Gender);
            Person recordSix = result.ElementAt(5);
            Assert.AreEqual(expectedGenderMale, recordSix.Gender);
        }

        public void AssertScenarioGetByBirthDate()
        {
            // Setup TEst
            RecordsController controller = new RecordsController();
            const int expectedRecordCount = 6;
            DateTime expectedDateOne = Convert.ToDateTime("11/19/1939");
            DateTime expectedDateTwo = Convert.ToDateTime("7/11/1960");
            DateTime expectedDateThree = Convert.ToDateTime("5/24/1968");
            DateTime expectedDateFour = Convert.ToDateTime("3/17/1978");
            DateTime expectedDateFive = Convert.ToDateTime("10/4/1988");
            DateTime expectedDateSix = Convert.ToDateTime("1/4/2010");


            // Act
            List<Person> result = controller.GetByBirthdate();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedRecordCount, result.Count());

            Person recordOne = result.ElementAt(0);
            Assert.AreEqual(expectedDateOne, recordOne.BirthDate);
            Person recordTwo = result.ElementAt(1);
            Assert.AreEqual(expectedDateTwo, recordTwo.BirthDate);
            Person recordThree = result.ElementAt(2);
            Assert.AreEqual(expectedDateThree, recordThree.BirthDate);
            Person recordFour = result.ElementAt(3);
            Assert.AreEqual(expectedDateFour, recordFour.BirthDate);
            Person recordFive = result.ElementAt(4);
            Assert.AreEqual(expectedDateFive, recordFive.BirthDate);
            Person recordSix = result.ElementAt(5);
            Assert.AreEqual(expectedDateSix, recordSix.BirthDate);
        }

        public void AssertScenarioGetByLastName()
        {
            // Setup Test
            RecordsController controller = new RecordsController();
            const int expectedRecordCount = 6;
            const string expectedLastNameOne = "Bates";
            const string expectedLastNameTwo = "Green";
            const string expectedLastNameThree = "Green";
            const string expectedLastNameFour = "Groates";
            const string expectedLastNameFive = "Rose";
            const string expectedLastNameSix = "Taylor";

            // Act
            List<Person> result = controller.GetByName();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedRecordCount, result.Count());

            Person recordOne = result.ElementAt(0);
            Assert.AreEqual(expectedLastNameOne, recordOne.LastName);
            Person recordTwo = result.ElementAt(1);
            Assert.AreEqual(expectedLastNameTwo, recordTwo.LastName);
            Person recordThree = result.ElementAt(2);
            Assert.AreEqual(expectedLastNameThree, recordThree.LastName);
            Person recordFour = result.ElementAt(3);
            Assert.AreEqual(expectedLastNameFour, recordFour.LastName);
            Person recordFive = result.ElementAt(4);
            Assert.AreEqual(expectedLastNameFive, recordFive.LastName);
            Person recordSix = result.ElementAt(5);
            Assert.AreEqual(expectedLastNameSix, recordSix.LastName);
        }

        public void AssertScenarioPostNewPerson()
        {
            // Setup Test
            RecordsController controller = new RecordsController();
            const int expectedRecordCount = 7;

            Person personToAdd = new Person
            {
                FirstName = "TestFirst",
                LastName = "TestLast",
                Gender = "F",
                BirthDate = Convert.ToDateTime("1/1/2000"),
                FavoriteColor = "Infrared"
            };

            // Act
            controller.Post(personToAdd);

            // Get the people
            List<Person> result = controller.GetByGender();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedRecordCount, result.Count());
        }
    }
}
