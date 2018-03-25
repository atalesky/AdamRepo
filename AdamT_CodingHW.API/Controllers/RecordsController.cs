using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AdamT_CodingHW.API.Controllers
{
    public class RecordsController : ApiController
    {
        //  I have not implemented logging or authentication/authorization

        private static readonly List<Person> Persons = new List<Person>();
        private static int _nextId = 7;

        public RecordsController()
        {
            // test data
            if (Persons == null || Persons.Count > 0) return;

            Persons.Add(new Person
            {
                Id = 1,
                FirstName = "Adam",
                LastName = "Taylor",
                Gender = "M",
                FavoriteColor = "Green",
                BirthDate = Convert.ToDateTime("1/4/2010")
            });
            Persons.Add(new Person
            {
                Id = 2,
                FirstName = "Mary",
                LastName = "Green",
                Gender = "F",
                FavoriteColor = "Red",
                BirthDate = Convert.ToDateTime("7/11/1960")
            });
            Persons.Add(new Person
            {
                Id = 3,
                FirstName = "Warren",
                LastName = "Bates",
                Gender = "M",
                FavoriteColor = "Black",
                BirthDate = Convert.ToDateTime("11/19/1939")
            });
            Persons.Add(new Person
            {
                Id = 4,
                FirstName = "Axl",
                LastName = "Rose",
                Gender = "M",
                FavoriteColor = "Purple",
                BirthDate = Convert.ToDateTime("5/24/1968")
            });
            Persons.Add(new Person
            {
                Id = 5,
                FirstName = "Sue",
                LastName = "Green",
                Gender = "F",
                FavoriteColor = "Orange",
                BirthDate = Convert.ToDateTime("10/4/1988")
            });
            Persons.Add(new Person
            {
                Id = 6,
                FirstName = "Paulette",
                LastName = "Groates",
                Gender = "F",
                FavoriteColor = "Gray",
                BirthDate = Convert.ToDateTime("3/17/1978")
            });
        }


        // GET: /Records/gender
        [System.Web.Http.Route("Records/gender")]
        public List<Person> GetByGender()
        {
            return Persons.OrderBy(r => r.Gender).ToList();
        }

        // GET: /Records/birthdate
        [System.Web.Http.Route("Records/birthdate")]
        public List<Person> GetByBirthdate()
        {
            return Persons.OrderBy(r => r.BirthDate).ToList();
        }

        // GET: /Records/name
        [System.Web.Http.Route("Records/name")]
        public List<Person> GetByName()
        {
            return Persons.OrderBy(r => r.LastName).ToList();
        }

        // POST: /Records
        public HttpResponseMessage Post(Person value)
        {
            try
            {               
                if (string.IsNullOrEmpty(value.FirstName) || string.IsNullOrEmpty(value.LastName) || string.IsNullOrEmpty(value.FavoriteColor) 
                    || string.IsNullOrEmpty(value.Gender) || (value.Gender.ToLower() != "m" || (value.Gender.ToLower() != "f")))
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }

                Persons.Add(value);
                _nextId++;  //simulate identiy column
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);

                //todo: logging
            }          
        }
    }
}
