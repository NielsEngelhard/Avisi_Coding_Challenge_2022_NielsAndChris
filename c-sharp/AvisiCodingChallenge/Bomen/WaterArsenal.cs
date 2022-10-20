using Bomen.Waterers;

namespace Bomen
{
    public class WaterArsenal
    {
        // The arsenal of waterers
        public Waterer BigBottles;
        public Waterer SmallBottles;
        public Waterer GardeningHose;
        public Waterer FireHose;

        public WaterArsenal()
        {
            // Create waterers
            BigBottles = new BigBottle();
            SmallBottles = new SmallBottle();
            GardeningHose = new UnlimitedGardenHose();
            FireHose = new UnlimitedFireHose();
        }

        public void PrintArsenalValues()
        {
            BigBottles.PrintWaterStats();
            SmallBottles.PrintWaterStats();
            GardeningHose.PrintWaterStats();
            FireHose.PrintWaterStats();
        }
    }
}
