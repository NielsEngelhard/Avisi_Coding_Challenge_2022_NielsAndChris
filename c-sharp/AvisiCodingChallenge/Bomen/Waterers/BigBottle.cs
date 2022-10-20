namespace Bomen.Waterers
{
    public class BigBottle : Waterer
    {
        public BigBottle()
        {
            WaterCapacity = 8;
            WaterRatePerSecond = 1;
            SwitchTreeTimeInSeconds = 1;
            TakeNewTimeInSeconds = 6;
            Name = "Big Bottle";
            Amount = 8;
        }
    }
}
