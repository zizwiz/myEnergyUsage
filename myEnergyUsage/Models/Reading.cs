using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myEnergyUsage.Models
{
    public enum EnergyType
    {
        Electricity,
        Gas
    }

    public class EnergyReading
    {
        // Epoch timestamp (seconds since Unix epoch)
        public long EpochTimestamp { get; set; }

        // kWh used in this interval (half-hour or daily)
        public double KWh { get; set; }

        // Parsed DateTime (UTC from your CSV)
        public DateTime DateTimeUtc { get; set; }

        // Electricity or Gas
        public EnergyType Type { get; set; }

        // Optional: source name (House, Garage, etc.)
        public string SourceName { get; set; }
    }

}
