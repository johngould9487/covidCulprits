using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using Spire.Xls;

namespace missingEngine
{
    public class MissingEngine : IMissingEngine
    {
        private const string STAFF_LIST_CSV_PATH = "staff.csv";
        private const string RESPONSES_CSV_PATH = "responses.csv";
        private readonly string _staffListPath;
        private readonly string _responsesPath;
        private IEnumerable<string> StaffList { get; set; }
        private IEnumerable<string> ResponseList { get; set; }
        
        public MissingEngine(string staffListPath, string responsesPath)
        {
            _staffListPath = staffListPath;
            _responsesPath = responsesPath;
        }
        public IEnumerable<string> GetMissing()
        {
            GetStaffList();
            GetResponses();
            var missing = StaffList.Except(ResponseList).ToList();
            return missing;
        }

        private void GetStaffList()
        {
            Workbook workbook = new Workbook();
            workbook.LoadFromFile(_staffListPath);
            Worksheet sheet = workbook.Worksheets[0];
            sheet.SaveToFile(STAFF_LIST_CSV_PATH, ",", Encoding.UTF8);
            using (var reader = new StreamReader(STAFF_LIST_CSV_PATH))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<PersonMap>();
                StaffList = csv.GetRecords<Person>().Select(p => $"{p.FirstName.Trim()} {p.LastName.Trim()}").ToList();
            }
        }

        private void GetResponses()
        {
            Workbook workbook = new Workbook();
            workbook.LoadFromFile(_responsesPath);
            Worksheet sheet = workbook.Worksheets[0];
            sheet.SaveToFile(RESPONSES_CSV_PATH, ",", Encoding.UTF8);
            using (var reader = new StreamReader(RESPONSES_CSV_PATH))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<PersonResponseMap>();
                ResponseList = csv.GetRecords<PersonResponse>().Select(pr => GetPersonStringFromResponse(pr)).ToList();
            }
        }

        private string GetPersonStringFromResponse(PersonResponse r)
        {
            var names = r.FullName.Split(' ');
            return $"{names[0].Trim()} {string.Join(" ", names.Skip(1)).Trim()}";
        }
    }
}
