namespace Bomen.Waterers
{
    public abstract class Waterer
    {
        public string Name { get; set; }

        public int WaterCapacity { get; set; }

        public int Amount { get; set; }

        public float WaterRatePerSecond { get; set; }

        public int SwitchTreeTimeInSeconds { get; set; }

        public int TakeNewTimeInSeconds { get; set; }

        public void PrintWaterStats()
        {
            Console.WriteLine("");
            Console.WriteLine(Name);
            Console.WriteLine("----------default-------");

            Console.WriteLine($"Capacity: {WaterCapacity}L");
            Console.WriteLine($"Water rate: {WaterRatePerSecond}L p/s");
            Console.WriteLine($"Switch tree time: {SwitchTreeTimeInSeconds} seconds");
            Console.WriteLine($"Take new time: {TakeNewTimeInSeconds} seconds");

            Console.WriteLine("----------advanced-------");
            PrintAdvancedWaterStats();
            Console.WriteLine("-------------------------");
        }

        public void PrintAdvancedWaterStats()
        {
            // How much water is there for how much trees
            Console.WriteLine($"with {WaterCapacity}L water (times {Amount}) you can water {(WaterCapacity * Amount) / World.nWaterPerTree} trees");

            Console.WriteLine($"You can take ");
        }
    }
}
