namespace Bomen.Waterers
{
    public class UnlimitedFireHose : Waterer
    {
        public UnlimitedFireHose()
        {
            WaterCapacity = World.nTrees*World.nWaterPerTree; // Represents unlimited
            WaterRatePerSecond = 2;
            SwitchTreeTimeInSeconds = 5;
            TakeNewTimeInSeconds = 5;
            Name = "Unlimited Fire Hose";
            Amount = 1;
        }
    }
}
