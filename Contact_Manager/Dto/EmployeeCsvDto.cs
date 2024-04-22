using CsvHelper.Configuration.Attributes;

namespace Contact_Manager.Dto
{
    public class EmployeeCsvDto : IImportDto
    {
        [Name("Name")]
        public string Name { get; set; }

        [Name("Date of birth")]
        public DateOnly DateOfBirth { get; set; }

        [Name("Married")]
        public bool Married { get; set; }

        [Name("Phone")]
        public string Phone { get; set; }

        [Name("Salary")]
        public decimal Salary { get; set; }
    }
}
