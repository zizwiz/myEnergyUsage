namespace myEnergyUsage.Models
{
    public class CsvSourceInfo
    {
        public string FilePath { get; set; }
        public string Location { get; set; }   // House, Garage, Hall
        public EnergyType Type { get; set; }   // Electricity or Gas

        public override string ToString()
        {
            return $"{Location} ({(Type == EnergyType.Electricity ? "Electricity" : "Gas")})";
        }
    }

}
