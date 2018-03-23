using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace AdamT_CodingHW.API.Controllers
{
    public class RecordsController : ApiController
    {
        private static readonly List<Person> Persons = new List<Person>();

        public RecordsController()
        {
            // test data
            if (Persons == null || Persons.Count > 0) return;
            Persons.Add(new Person
            {
                FirstName = "Adam",
                LastName = "Taylor",
                Gender = "M",
                FavoriteColor = "Green",
                BirthDate = Convert.ToDateTime("1/4/2010")
            });
            Persons.Add(new Person
            {
                FirstName = "Mary",
                LastName = "Green",
                Gender = "F",
                FavoriteColor = "Red",
                BirthDate = Convert.ToDateTime("7/11/1960")
            });
            Persons.Add(new Person
            {
                FirstName = "Warren",
                LastName = "Bates",
                Gender = "M",
                FavoriteColor = "Black",
                BirthDate = Convert.ToDateTime("11/19/1939")
            });
            Persons.Add(new Person
            {
                FirstName = "Axl",
                LastName = "Rose",
                Gender = "M",
                FavoriteColor = "Purple",
                BirthDate = Convert.ToDateTime("5/24/1968")
            });
            Persons.Add(new Person
            {
                FirstName = "Sue",
                LastName = "Green",
                Gender = "F",
                FavoriteColor = "Orange",
                BirthDate = Convert.ToDateTime("10/4/1988")
            });
            Persons.Add(new Person
            {
                FirstName = "Paulette",
                LastName = "Groates",
                Gender = "F",
                FavoriteColor = "Gray",
                BirthDate = Convert.ToDateTime("3/17/1978")
            });
        }


        // GET: /Records/gender
        [Route("Records/gender")]
        public List<Person> GetByGender()
        {
            return Persons.OrderBy(r => r.Gender).ToList();
        }

        // GET: /Records/birthdate
        [Route("Records/birthdate")]
        public List<Person> GetByBirthdate()
        {
            return Persons.OrderBy(r => r.BirthDate).ToList();
        }

        // GET: /Records/name
        [Route("Records/name")]
        public List<Person> GetByName()
        {
            return Persons.OrderBy(r => r.LastName).ToList();
        }

        // POST: /Records
        public void Post(Person value)
        {
            Persons.Add(value);
        }
    }
}
