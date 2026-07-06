using System;

namespace myEnergyUsage.Models
{
    public class Tariff
    {
        // Unique ID or name for this tariff period
        public string Name { get; set; }

        // Effective date range
        public DateTime EffectiveFrom { get; set; }
        public DateTime EffectiveTo { get; set; }

        // Energy type
        public EnergyType Type { get; set; }

        // Electricity: day rate and night/other rate (pence per kWh)
        public double DayRatePencePerKWh { get; set; }      // e.g. 28.512
        public double NightRatePencePerKWh { get; set; }    // e.g. 20.999

        // Gas: flat rate (pence per kWh)
        public double FlatRatePencePerKWh { get; set; }

        // Daily standing charge (pence per day)
        public double StandingChargePencePerDay { get; set; }

        // NRAB (pence per kWh)
        public double NrabPencePerKWh { get; set; }

        // Helper: is this electricity tariff with time-of-day rates?
        public bool HasTimeOfDayRates =>
            Type == EnergyType.Electricity && DayRatePencePerKWh > 0 && NightRatePencePerKWh > 0;
    }

}
