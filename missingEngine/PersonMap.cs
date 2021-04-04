using CsvHelper.Configuration;

namespace missingEngine
{
    public class PersonMap : ClassMap<Person>
    {
        public PersonMap()
        {
            Map(x => x.FirstName).Name("Forename", "forename", "ForeName");
            Map(x => x.LastName).Name("Surname", "surname");
        }
    }
}