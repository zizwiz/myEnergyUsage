using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using myEnergyUsage.Models;

public class CsvLoader
{
    // Reads a single CSV file and returns a list of EnergyReading
    public List<EnergyReading> LoadCsvFile(string filePath, EnergyType type)
    {
        var readings = new List<EnergyReading>();

        if (!File.Exists(filePath))
        {
            // File not found – return empty list or throw, depending on your preference
            return readings;
        }

        try
        {
            using (var reader = new StreamReader(filePath))
            {
                string line;
                bool isHeader = true;

                while ((line = reader.ReadLine()) != null)
                {
                    // Skip header line
                    if (isHeader)
                    {
                        isHeader = false;
                        continue;
                    }

                    // Split CSV line
                    var parts = line.Split(',');

                    // Basic validation
                    if (parts.Length < 3)
                        continue; // malformed line, skip

                    // Parse epoch timestamp
                    if (!long.TryParse(parts[0], out long epoch))
                        continue; // invalid epoch, skip

                    // Parse kWh
                    if (!double.TryParse(parts[1], NumberStyles.Float, CultureInfo.InvariantCulture, out double kwh))
                        continue; // invalid kWh, skip

                    // Parse dateTime
                    if (!DateTime.TryParse(parts[2], CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out DateTime dt))
                        continue; // invalid date, skip

                    readings.Add(new EnergyReading
                    {
                        EpochTimestamp = epoch,
                        KWh = kwh,
                        DateTimeUtc = dt.ToUniversalTime(),
                        Type = type,
                        SourceName = GetSourceNameFromFile(filePath)
                    });
                }
            }
        }
        catch (IOException ex)
        {
            // Handle IO errors (file locked, etc.)
            // Log or show message to user
            Console.WriteLine($"IO error reading {filePath}: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Catch-all for unexpected errors
            Console.WriteLine($"Unexpected error reading {filePath}: {ex.Message}");
        }

        return readings;
    }

    // Extract source name from file name, e.g. HouseE.csv -> "House"
    private string GetSourceNameFromFile(string filePath)
    {
        var fileName = Path.GetFileNameWithoutExtension(filePath);
        if (string.IsNullOrEmpty(fileName))
            return string.Empty;

        // Last character is E or G
        if (fileName.EndsWith("E", StringComparison.OrdinalIgnoreCase) ||
            fileName.EndsWith("G", StringComparison.OrdinalIgnoreCase))
        {
            return fileName.Substring(0, fileName.Length - 1);
        }

        return fileName;
    }
}
