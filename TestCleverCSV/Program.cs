using CleverCSV;
using CsvHelper;
using System.Globalization;

namespace TestCleverCSV
{
    public class Program
    {
        
        static void Main(string[] args)
        {
            TypeGuesser _guesser;

            using (var reader = new StreamReader("Electricity_Production_By_Source.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                int max = 0;
                Type type;
                string? record = string.Empty;
                csv.Read();
                csv.ReadHeader();
                int colcount = csv.HeaderRecord.Count();
                
                while (csv.Read())
                {
                    for (int i = 0; i < colcount; i++)
                    {
                        record = csv.GetField(i);
                        _guesser = new TypeGuesser(record);

                        Console.WriteLine($"Value: {record} - Type: {_guesser.GuessValueType().Name}");
                    }
                    max++;

                    if (max >= 20)
                        break;
                }
            }
        }
    }
}