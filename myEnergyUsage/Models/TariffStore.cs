using System.Collections.Generic;

namespace myEnergyUsage.Models
{
    public class TariffStore
    {
        public List<Tariff> Tariffs { get; set; } = new List<Tariff>();
    }
}
