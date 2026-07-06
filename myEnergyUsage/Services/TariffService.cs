using System;
using System.Collections.Generic;
using System.Linq;
using myEnergyUsage.Models;

public class TariffService
{
    private readonly List<Tariff> _tariffs;

    public TariffService(List<Tariff> tariffs)
    {
        _tariffs = tariffs ?? new List<Tariff>();
    }

    // Find tariff that covers the given date and energy type
    public Tariff GetTariffForDate(DateTime dateUtc, EnergyType type)
    {
        return _tariffs
            .Where(t => t.Type == type &&
                        dateUtc >= t.EffectiveFrom.ToUniversalTime() &&
                        dateUtc <= t.EffectiveTo.ToUniversalTime())
            .OrderByDescending(t => t.EffectiveFrom)
            .FirstOrDefault();   // returns null if nothing found
    }

}