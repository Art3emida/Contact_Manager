using Contact_Manager.Dto;
using Contact_Manager.Models;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace Contact_Manager.Services
{
    public class CsvImportService : IFileImportService
    {
        public List<T> ExtractData<T>(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new FileNotFoundException();
            }

            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (fileExtension != ".csv")
            {
                throw new InvalidDataException();
            }

            var config = new CsvConfiguration(CultureInfo.InvariantCulture);
            config.Delimiter = DetectDelimiterForFile(file);

            var records = new List<T>();
            using (var streamReader = new StreamReader(file.OpenReadStream()))
            using (var csvReader = new CsvReader(streamReader, config))
            {
                records = csvReader.GetRecords<T>().ToList();
            }

            return records;
        }

        public string DetectDelimiterForFile(IFormFile file)
        {
            string filePath = Path.GetTempFileName();
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            var delimiters = new[] { ',', ';', '\t', '|' };
            var delimiterCounts = new Dictionary<char, int>();
            string? line;

            using (var reader = new StreamReader(filePath))
            {
                line = reader.ReadLine();
            }

            if (line == null)
            {
                return ";";
            }

            foreach (var delimiter in delimiters)
            {
                var count = line.Count(t => t == delimiter);
                delimiterCounts[delimiter] = count;
            }

            return delimiterCounts.OrderByDescending(d => d.Value).First().Key.ToString();
        }
    }
}
