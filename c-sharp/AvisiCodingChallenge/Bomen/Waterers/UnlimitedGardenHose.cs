namespace Bomen.Waterers
{
    public class UnlimitedGardenHose : Waterer
    {
        public UnlimitedGardenHose()
        {
            WaterCapacity = World.nTrees * World.nWaterPerTree; // Represents unlimited
            WaterRatePerSecond = 0.5f;
            SwitchTreeTimeInSeconds = 3;
            TakeNewTimeInSeconds = 3;
            Name = "Unlimited Garden Hose";
            Amount = 1;
        }
    }
}
