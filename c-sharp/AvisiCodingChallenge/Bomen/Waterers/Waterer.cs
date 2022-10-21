namespace Bomen.Waterers
{
    public abstract class Waterer
    {
        public string Name { get; set; }

        public float WaterCapacity { get; set; }

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

        public int GrabItem()
        {
            return TakeNewTimeInSeconds;
        }

        public int WalkToTree()
        {
            return SwitchTreeTimeInSeconds;
        }

        public bool IsEmpty()
        {
            return WaterCapacity == 0;
        }

        public JustWateredStats WaterTreeByWaterToGive(float waterToGive)
        {
            WaterCapacity -= waterToGive;

            return new JustWateredStats
            {
                WaterLeftInCurrentItem = WaterCapacity,
                DurationOfGivingWater = waterToGive / WaterRatePerSecond,
                LitersOfWaterGiven = waterToGive
            };
        }

        public JustWateredStats WaterTreeByTimeInSeconds(float time)
        {
            var waterGiven = (WaterRatePerSecond * time);

            WaterCapacity -= waterGiven;

            return new JustWateredStats
            {
                WaterLeftInCurrentItem = WaterCapacity,
                DurationOfGivingWater = time,
                LitersOfWaterGiven = waterGiven
            };
        }
    }
}
