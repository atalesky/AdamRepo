using System;

namespace AdamT_CodingHW
{
    // Normally, we would put this in it's own library
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string FavoriteColor { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
