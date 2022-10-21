using Bomen.Statistics;
using Bomen.Waterers;

namespace Bomen
{
    public class WaterArsenal
    {
        // The arsenal of waterers
        public List<Waterer> BigBottles;
        public List<Waterer> SmallBottles;
        public Waterer GardeningHose;
        public Waterer FireHose;

        public WaterArsenal()
        {
            // Create waterers
            BigBottles = new List<Waterer>();
            for (var i=0; i<8; i++)
            {
                BigBottles.Add(new BigBottle());
            }

            SmallBottles = new List<Waterer>();
            for (var i = 0; i < 8; i++)
            {
                SmallBottles.Add(new SmallBottle());
            }

            GardeningHose = new UnlimitedGardenHose();
            FireHose = new UnlimitedFireHose();
        }

        public void PrintArsenalValues()
        {
            BigBottles.First().PrintWaterStats();
            SmallBottles.First().PrintWaterStats();
            GardeningHose.PrintWaterStats();
            FireHose.PrintWaterStats();
        }

        public void PrintXYsPerWaterer()
        {
            var r1List = new List<Waterer>();
            for (int i=0; i<8; i++)
            {
                r1List.Add(new BigBottle());
            }
            var r1 = XYWatererPlotter.CreateXYTableOfWaterer(r1List);

            var dictionary2 = XYWatererPlotter.CreateXYTableOfWaterer(new List<Waterer>() { new UnlimitedFireHose() });

            String csv = String.Join(
                Environment.NewLine,
                dictionary2.Select(d => $"{d.Key};{d.Value};"));
            System.IO.File.WriteAllText("./bigbottle.csv", csv);
        }
    }
}
