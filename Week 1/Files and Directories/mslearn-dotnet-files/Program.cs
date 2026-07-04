using Newtonsoft.Json;
using System.Text;

var currentDirectory = Directory.GetCurrentDirectory();
var storesDirectory = Path.Combine(currentDirectory, "stores");
var salesTotalDir = Path.Combine(currentDirectory, "salesTotalDir");
Directory.CreateDirectory(salesTotalDir);

var salesFiles = FindFiles(storesDirectory);

GenerateSalesSummaryReport(salesFiles, Path.Combine(salesTotalDir, "totals.txt"));

IEnumerable<string> FindFiles(string folderName)
{
    List<string> salesFiles = new List<string>();
    var foundFiles = Directory.EnumerateFiles(folderName, "*", SearchOption.AllDirectories);
    
    foreach (var file in foundFiles)
    {
        var extension = Path.GetExtension(file);
        if (extension == ".json")
        {
            salesFiles.Add(file);
        }
    }
    return salesFiles;
}

void GenerateSalesSummaryReport(IEnumerable<string> salesFiles, string reportPath)
{
    double grandTotal = 0;
    StringBuilder details = new StringBuilder();

    foreach (var file in salesFiles)
    {
        string salesJson = File.ReadAllText(file);
        SalesData? data = JsonConvert.DeserializeObject<SalesData?>(salesJson);
        double fileTotal = data?.Total ?? 0;
        grandTotal += fileTotal;

        string fileName = Path.GetFileName(file);
        details.AppendLine($"{fileName}: {fileTotal:C}");
    }

    StringBuilder report = new StringBuilder();
    report.AppendLine("Sales Summary");
    report.AppendLine($"Total Sales: {grandTotal:C}");
    report.AppendLine("Details:");
    report.Append(details.ToString());

    File.WriteAllText(reportPath, report.ToString());
}

record SalesData(double Total);