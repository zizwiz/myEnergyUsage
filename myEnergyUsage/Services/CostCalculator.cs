using System;
using System.Collections.Generic;
using System.Linq;
using myEnergyUsage.Models;

public class CostResult
{
    public double TotalKWh { get; set; }
    public double EnergyCostPence { get; set; }
    public double NrabCostPence { get; set; }
    public double StandingChargePence { get; set; }

    public double TotalCostPence => EnergyCostPence + NrabCostPence + StandingChargePence;
}

public class CostCalculator
{
    private readonly TariffService _tariffService;

    public CostCalculator(TariffService tariffService)
    {
        _tariffService = tariffService;
    }

    // Calculate cost for a single reading
    public double CalculateCostForReading(EnergyReading reading)
    {
        var tariff = _tariffService.GetTariffForDate(reading.DateTimeUtc, reading.Type);
        if (tariff == null)
        {
            // No tariff found – treat cost as 0 or handle specially
            return 0.0;
        }

        double energyCostPence = 0.0;

        if (reading.Type == EnergyType.Electricity && tariff.HasTimeOfDayRates)
        {
            // Determine if this is day or night rate
            var localTime = reading.DateTimeUtc; // adjust to local if needed
            bool isWeekday = localTime.DayOfWeek != DayOfWeek.Saturday &&
                             localTime.DayOfWeek != DayOfWeek.Sunday;

            if (isWeekday &&
                localTime.TimeOfDay >= new TimeSpan(7, 0, 0) &&
                localTime.TimeOfDay < new TimeSpan(19, 0, 0))
            {
                // Day rate
                energyCostPence = reading.KWh * tariff.DayRatePencePerKWh;
            }
            else
            {
                // Night/other rate
                energyCostPence = reading.KWh * tariff.NightRatePencePerKWh;
            }
        }
        else if (reading.Type == EnergyType.Gas)
        {
            // Gas flat rate
            energyCostPence = reading.KWh * tariff.FlatRatePencePerKWh;
        }

        // NRAB cost
        double nrabCostPence = reading.KWh * tariff.NrabPencePerKWh;

        // Standing charge: we’ll handle per-day separately; for a single reading, we return only energy+NRAB
        return energyCostPence + nrabCostPence;
    }

    // Calculate cost for a set of readings over a period (including standing charge)
    public CostResult CalculateCostForPeriod(IEnumerable<EnergyReading> readings)
    {
        var result = new CostResult();

        DateTime? minDate = null;
        DateTime? maxDate = null;

        foreach (var r in readings)
        {
            result.TotalKWh += r.KWh;
            result.EnergyCostPence += CalculateCostForReading(r);

            if (!minDate.HasValue || r.DateTimeUtc < minDate.Value)
                minDate = r.DateTimeUtc;

            if (!maxDate.HasValue || r.DateTimeUtc > maxDate.Value)
                maxDate = r.DateTimeUtc;
        }

        if (minDate.HasValue && maxDate.HasValue)
        {
            // Standing charge: assume same tariff for whole period and same type
            var firstReading = readings.FirstOrDefault();
            if (firstReading != null)
            {
                var tariff = _tariffService.GetTariffForDate(firstReading.DateTimeUtc, firstReading.Type);
                if (tariff != null)
                {
                    int days = (int)Math.Ceiling((maxDate.Value.Date - minDate.Value.Date).TotalDays) + 1;
                    result.StandingChargePence = days * tariff.StandingChargePencePerDay;
                }
            }
        }

        // NRAB already included in EnergyCostPence via CalculateCostForReading
        // If you want to separate NRAB, adjust the logic accordingly.

        return result;
    }
}
