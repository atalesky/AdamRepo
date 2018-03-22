using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdamT_CodingHW.Tests
{
    [TestClass]
    public class MainUnitTests
    {
        [TestMethod]
        public void RunMainNoArgs()
        {
            // setup test
            string[] args = new string[0];
            string expectedOutput = $"You must supply at least one filename to parse.{Environment.NewLine}";

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                using (StringReader reader = new StringReader(string.Empty))
                {
                    Console.SetIn(reader);
                    Program.Main(args);
                    Assert.AreEqual(expectedOutput, sw.ToString());
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void RunMainNoFile()
        {
            // setup test
            string[] args = new string[1];
            args[0] = "somenonexistantfile.txt";

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                using (StringReader reader = new StringReader(string.Empty))
                {
                    Console.SetIn(reader);
                    Program.Main(args);
                }
            }
        }

        [TestMethod]
        public void ReadInFile()
        {
            // setup test
            const string filenameToTest = @"C:\Users\ThunderMk2\Documents\visual studio 2017\Projects\AdamT_CodingHW\AdamT_CodingHW\bin\Debug\peopleTest.txt";
            const string expectedLine1 = "Smith Joe M Green 10/8/1984";
            const string expectedLine2 = "Smith, Joe, M, Green, 10/8/1984";
            const string expectedLine3 = "Smith | Joe | M | Green | 10/8/1984";
            const int expecteRecordCount = 3;

            // act on test
            List<string> returnedPeople = Program.ReadInFile(filenameToTest, filenameToTest);

            // assert test
            Assert.AreEqual(expecteRecordCount, returnedPeople.Count);
            Assert.AreEqual(expectedLine1, returnedPeople[0]);
            Assert.AreEqual(expectedLine2, returnedPeople[1]);
            Assert.AreEqual(expectedLine3, returnedPeople[2]);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void ReadInFileNotFound()
        {
            // setup test
            const string filenameToTest = @"C:\Users\ThunderMk2\Documents\visual studio 2017\Projects\AdamT_CodingHW\AdamT_CodingHW\bin\Debug\peopleTestNotExisting.txt";

            // act on test
            List<string> returnedPeople = Program.ReadInFile(filenameToTest, filenameToTest);
        }

        [TestMethod]
        public void TestPrintOutput()
        {
            // setup test
            List<Person> inputListPeople = new List<Person>();
            Person person1 = new Person
            {
                FirstName = "TestFirst",
                LastName = "TestLast",
                Gender = "M",
                BirthDate = Convert.ToDateTime("1/2/2010"),
                FavoriteColor = "Green"
            };

            string expectedOutput = string.Format("{0}{0}Output => test results{0}Last Name First Name Gender Favorite Color Birthdate{0}--------- ---------- ------ -------------- ---------{0}TestLast TestFirst M Green 1/2/2010{0}", Environment.NewLine);

            inputListPeople.Add(person1);

            // act on test
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                Program.PrintResults(inputListPeople, "test results");

                // assert test
                Assert.AreEqual(expectedOutput, sw.ToString());
            }
        }

        [TestMethod]
        public void ParseMultipleLines()
        {
            // setup test
            List<string> inputListOfStrings = new List<string>();
            const string strInputLine1 = "Smith Joe M Green 10/8/1984";
            const string strInputLine2 = "Smith, Joe, M, Green, 10/8/1984";
            const string strInputLine3 = "Smith | Joe | M | Green | 10/8/1984";

            inputListOfStrings.Add(strInputLine1);
            inputListOfStrings.Add(strInputLine2);
            inputListOfStrings.Add(strInputLine3);

            const string expectedLastName = "Smith";
            const string expectedFirstName = "Joe";
            const string expectedGender = "M";
            const string expectedFavoriteColor = "Green";
            DateTime expectedBirthDate = Convert.ToDateTime("10/8/1984");
            const int expecteRecordCount = 3;

            // act on test
            List<Person> returnedPeople = Program.ParseLines(inputListOfStrings);

            // assert test
            Assert.AreEqual(expecteRecordCount, returnedPeople.Count);

            Assert.AreEqual(expectedFirstName, returnedPeople[0].FirstName);
            Assert.AreEqual(expectedLastName, returnedPeople[0].LastName);
            Assert.AreEqual(expectedGender, returnedPeople[0].Gender);
            Assert.AreEqual(expectedFavoriteColor, returnedPeople[0].FavoriteColor);
            Assert.AreEqual(expectedBirthDate, returnedPeople[0].BirthDate);

            Assert.AreEqual(expectedFirstName, returnedPeople[1].FirstName);
            Assert.AreEqual(expectedLastName, returnedPeople[1].LastName);
            Assert.AreEqual(expectedGender, returnedPeople[1].Gender);
            Assert.AreEqual(expectedFavoriteColor, returnedPeople[1].FavoriteColor);
            Assert.AreEqual(expectedBirthDate, returnedPeople[1].BirthDate);

            Assert.AreEqual(expectedFirstName, returnedPeople[2].FirstName);
            Assert.AreEqual(expectedLastName, returnedPeople[2].LastName);
            Assert.AreEqual(expectedGender, returnedPeople[2].Gender);
            Assert.AreEqual(expectedFavoriteColor, returnedPeople[2].FavoriteColor);
            Assert.AreEqual(expectedBirthDate, returnedPeople[2].BirthDate);
        }

        [TestMethod]
        public void ParseLineSpaces()
        {
            // setup test
            const string strInputLine = "Smith Joe M Green 10/8/1984";
            const string expectedLastName = "Smith";
            const string expectedFirstName = "Joe";
            const string expectedGender = "M";
            const string expectedFavoriteColor = "Green";
            DateTime expectedBirthDate = Convert.ToDateTime("10/8/1984");

            // act on test
            Person returnedPerson = Program.ParseSingleLine(strInputLine);

            // assert test
            Assert.AreEqual(expectedFirstName, returnedPerson.FirstName);
            Assert.AreEqual(expectedLastName, returnedPerson.LastName);
            Assert.AreEqual(expectedGender, returnedPerson.Gender);
            Assert.AreEqual(expectedFavoriteColor, returnedPerson.FavoriteColor);
            Assert.AreEqual(expectedBirthDate, returnedPerson.BirthDate);
        }

        [TestMethod]
        public void ParseLineComma()
        {
            // setup test
            const string strInputLine = "Smith, Joe, M, Green, 10/8/1984";
            const string expectedLastName = "Smith";
            const string expectedFirstName = "Joe";
            const string expectedGender = "M";
            const string expectedFavoriteColor = "Green";
            DateTime expectedBirthDate = Convert.ToDateTime("10/8/1984");

            // act on test
            Person returnedPerson = Program.ParseSingleLine(strInputLine);

            // assert test
            Assert.AreEqual(expectedFirstName, returnedPerson.FirstName);
            Assert.AreEqual(expectedLastName, returnedPerson.LastName);
            Assert.AreEqual(expectedGender, returnedPerson.Gender);
            Assert.AreEqual(expectedFavoriteColor, returnedPerson.FavoriteColor);
            Assert.AreEqual(expectedBirthDate, returnedPerson.BirthDate);
        }

        [TestMethod]
        public void ParseLinePipe()
        {
            // setup test
            const string strInputLine = "Smith | Joe | M | Green | 10/8/1984";
            const string expectedLastName = "Smith";
            const string expectedFirstName = "Joe";
            const string expectedGender = "M";
            const string expectedFavoriteColor = "Green";
            DateTime expectedBirthDate = Convert.ToDateTime("10/8/1984");

            // act on test
            Person returnedPerson = Program.ParseSingleLine(strInputLine);

            // assert test
            Assert.AreEqual(expectedFirstName, returnedPerson.FirstName);
            Assert.AreEqual(expectedLastName, returnedPerson.LastName);
            Assert.AreEqual(expectedGender, returnedPerson.Gender);
            Assert.AreEqual(expectedFavoriteColor, returnedPerson.FavoriteColor);
            Assert.AreEqual(expectedBirthDate, returnedPerson.BirthDate);
        }

        [TestMethod]
        public void ParseLineToFailMissingGender()
        {
            // setup test
            const string strInputLine = "Smith Joe Green 10/8/1984";

            // act on test
            Person returnedPerson = Program.ParseSingleLine(strInputLine);

            // assert test
            Assert.IsNull(returnedPerson);
        }

        [TestMethod]
        public void ParseLineToFailInvalidBirthDate()
        {
            // setup test
            const string strInputLine = "Smith Joe Green 10/80/1984";

            // act on test
            Person returnedPerson = Program.ParseSingleLine(strInputLine);

            // assert test
            Assert.IsNull(returnedPerson);
        }
    }
}
