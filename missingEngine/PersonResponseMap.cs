using CsvHelper.Configuration;

namespace missingEngine
{
    public class PersonResponseMap : ClassMap<PersonResponse>
    {
        public PersonResponseMap()
        {
            Map(x => x.FullName).Name("Name", "name");
        }
    }
}