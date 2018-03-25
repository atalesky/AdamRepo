using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Text.RegularExpressions;

namespace AdamT_CodingHW
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                List<string>
                    readInLines =
                        new List<string>(); //we use a list vs an array so we dont have to keep resizing and slowing our app

                // Ensure we have args
                if (args == null || args.Length == 0)
                {
                    Console.WriteLine("You must supply at least one filename to parse.");
                    return;
                }

                Console.WriteLine($"You provided {args.Length} filenames, attempting to read them in...");

                // Loop through vars, clean and attempt to read in.
                ActOnEachVariable(args, ref readInLines);

                // Attempt to Parse Lines
                List<Person> parsedPersons = ParseLines(readInLines);

                // Do our sorts and printing
                SortAndPrint(parsedPersons);

                Console.WriteLine(Environment.NewLine + "Application finished, press any key to exit...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error running the program: {ex.Message}");
            }
        }

        public static void ActOnEachVariable(string[] args, ref List<string> readInLines)
        {
            foreach (var argument in args)
            {
                var cleanedArg = argument.Trim();
                if (!string.IsNullOrEmpty(cleanedArg))
                {
                    readInLines.AddRange(ReadInFile(cleanedArg, string.Empty));
                }
                else
                {
                    Console.WriteLine(Environment.NewLine + "Unable to read file, exiting...");
                    return;
                }
            }
        }

        public static List<string> ReadInFile(string filename, string overridepath)
        {
            // Over ride of the file path allows for testing.
            var fileLocation = string.IsNullOrEmpty(overridepath)
                ? Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]) + "\\" + filename
                : overridepath;

            List<string> lines;

            try
            {
                lines = new List<string>();

                var reader = new StreamReader(fileLocation);

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }

                Console.WriteLine($"Successfully read in {lines.Count} lines.");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Unable to locate input file at path: " + fileLocation);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error detected: " + ex.Message);
                throw;
            }

            return lines;
        }

        public static List<Person> ParseLines(IReadOnlyCollection<string> inputLines)
        {
            if (inputLines.Count < 1) return null;

            var retValue = new List<Person>();

            foreach (var inputLine in inputLines)
            {
                retValue.Add(ParseSingleLine(inputLine));
            }

            return retValue;
        }

        public static Person ParseSingleLine(string line)
        {
            // Assumptions
            // One type of delimiter per line
            // delimiter is a comma, pipe or space

            // our return value
            var retValue = new Person();

            // clean the line, remove multiple spaces
            const RegexOptions options = RegexOptions.None;
            var regex = new Regex("[ ]{2,}", options);
            var cleanedLine = regex.Replace(line, " ");

            // a place to store our parsed data line
            string[] valueArray;

            // determine line delimiter
            var commaDelimiter = cleanedLine.IndexOf(",", StringComparison.Ordinal);
            var pipeDelimiter = cleanedLine.IndexOf("|", StringComparison.Ordinal);

            if (commaDelimiter > -1)
            {
                valueArray = cleanedLine.Split(',');
            }
            else if (pipeDelimiter > -1)
            {
                valueArray = cleanedLine.Split('|');
            }
            else
            {                
                valueArray = cleanedLine.Split(' ');
            }

            if (valueArray.Length != 5) return null;

            try
            {
                retValue.LastName = valueArray[0].Trim();
                retValue.FirstName = valueArray[1].Trim();
                retValue.Gender = valueArray[2].Trim();
                retValue.FavoriteColor = valueArray[3].Trim();
                retValue.BirthDate = Convert.ToDateTime(valueArray[4].Trim());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            return retValue;
        }

        public static void SortAndPrint(List<Person> parsedPersons)
        {
            // Sort by gender then last name ascending
            parsedPersons.Sort(delegate (Person a, Person b)
            {
                var xdiff = string.Compare(a.Gender, b.Gender, StringComparison.Ordinal);
                return xdiff != 0 ? xdiff : string.Compare(a.LastName, b.LastName, StringComparison.Ordinal);
            });

            // Print Results
            PrintResults(parsedPersons, "sort by gender then last name ascending");

            // Sort by birth date, ascending -- use simpler sort for one field
            parsedPersons.Sort((x, y) => DateTime.Compare(x.BirthDate, y.BirthDate));

            // Print Results
            PrintResults(parsedPersons, "sort by birth date ascending.");

            // Sort by last name, descending
            parsedPersons.Sort((x, y) => string.Compare(y.LastName, x.LastName, StringComparison.Ordinal));

            // Print Results
            PrintResults(parsedPersons, "sort by last name descending.");
        }

        public static void PrintResults(List<Person> results, string label)
        {
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Output => " + label);
            Console.WriteLine("Last Name First Name Gender Favorite Color Birthdate");
            Console.WriteLine("--------- ---------- ------ -------------- ---------");
            foreach (Person result in results)
            {                
                Console.WriteLine($"{result.LastName} {result.FirstName} {result.Gender} {result.FavoriteColor} {result.BirthDate.ToShortDateString()}");
            }
        }
    }
}
