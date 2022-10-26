using System.Text.Json;

namespace LINQ_Tips_Tricks
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var reader = new StreamReader("./selic.json");
            var json = reader.ReadToEnd();
            var data = JsonSerializer.Deserialize<List<SelicData>>(json).OrderBy(x => x.Date);

            // --------------------------------------//
            // Don't use Commands unnecessarily

            // Bad - Will it will run the query 2 times
            var selicMinBad = data.Select(x => x.Selic).Min();

            // Good
            var selicMinGood = data.Min(x => x.Selic);

            // --------------------------------------//
            // Current index X Previous index iteration on linq
            // Example using where: Find months that occurred a change on Selic Value
            // Skip(1) because the 'where' clause will always return the first element of the data
            var changingMonths_Where = data
                .Where((item, index) => index == 0 || item.Selic != data.ElementAt(index - 1).Selic)
                .Skip(1);

            // Example using Zip
            var changingMonths_Zip = data
                .Zip(data.Skip(1), (first, second) => new { Date = second.Date, Diff = second.Selic - first.Selic })
                .Where(x => x.Diff != 0);

            // --------------------------------------//
            // Selic Average Value per Quarter
            var quarterlyAvgList = data
                .Where(x => x.Date.Year >= 2016)
                .GroupBy(x => Math.Ceiling(x.Date.Month / 3m) + "/" + x.Date.Year)
                .Select(x => new
                {
                    Quarterly = x.Key,
                    Average = x.Average(x => x.Selic)
                })
                .OrderBy(x => DateTime.Parse(x.Quarterly))
                .ToList();

            // --------------------------------------//
            // Using zip to compare previous x current
            // Increase rate since mar/21
            var filteredData = data
                .Where(x => x.Date > new DateTime(2021, 3, 1))
                .DistinctBy(x => x.Selic)
                .Select(x => x.Selic);

            var res = filteredData
                .Zip(filteredData.Skip(1), (a, b) => b - a)
                .Average();


        }
    }
}