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

            var gardeningHose = new List<Waterer>() { new UnlimitedGardenHose() };

            //var SmallBottles = new List<Waterer>();
            //for(var i=0; i<16; i++)
            //{
            //    SmallBottles.Add(new SmallBottle());
            //}

            var BigBottles = new List<Waterer>();
            for (var i = 0; i < 8; i++)
            {
                BigBottles.Add(new BigBottle());
            }

            var dictionary2 = XYWatererPlotter.CreateXYTableOfWaterer(BigBottles);
            Console.WriteLine("dictionary2");

            var weirdJsonString = MyDictionaryToJson(dictionary2);

            Console.WriteLine("crazy json string: " + weirdJsonString);

            var strippedString = ConvertCrazyJsonStringToNormalJson(weirdJsonString);
            Console.WriteLine("");
            Console.WriteLine("Real: " + strippedString);
        }

        public void PrintMultipleStagesXY()
        {
            // big bottles

            var lastBigBottle = new BigBottle();
            lastBigBottle.WaterCapacity = 8;

            var lastSmallBottle = new SmallBottle();
            lastSmallBottle.WaterCapacity = 5;

            var bigBottles = new List<Waterer>() { new BigBottle(), new BigBottle(), new BigBottle(), new BigBottle(), new BigBottle(), new BigBottle(), new BigBottle(), lastBigBottle };
            var smallBottles = new List<Waterer> { new SmallBottle(), new SmallBottle(), new SmallBottle(), new SmallBottle(), new SmallBottle(), new SmallBottle(), new SmallBottle(), new SmallBottle(), new SmallBottle(), new SmallBottle(), new SmallBottle(), new SmallBottle(), new SmallBottle(), new SmallBottle(), new SmallBottle(), lastSmallBottle };
            var FireHose = new List<Waterer>() { new UnlimitedFireHose() }; // 1 fire hose


            var dictionary2 = XYWatererPlotter.CreateXYTableOfWatererWithDifferntLines(smallBottles, bigBottles, FireHose);
            Console.WriteLine("AAN ELKAAR");

            var weirdJsonString = MyDictionaryToJson(dictionary2);

            Console.WriteLine("crazy json string: " + weirdJsonString);

            var strippedString = ConvertCrazyJsonStringToNormalJson(weirdJsonString);
            Console.WriteLine("");
            Console.WriteLine("Real: " + strippedString);
        }

        public static string MyDictionaryToJson(Dictionary<int, float> dict)
        {
            var entries = dict.Select(d =>
                string.Format("\"{0}\": [{1}]", d.Key, string.Join(",", d.Value)));
            return "{" + string.Join(",", entries) + "}";
        }

        public static string ConvertCrazyJsonStringToNormalJson(string crazyJsonString)
        {
            var charArray = crazyJsonString.ToCharArray();

            var lol = char.IsDigit('[');

            for (var i=0; i<crazyJsonString.Length; i++)
            {
                if (i != 0 && i != charArray.Length)
                {
                    if (charArray[i] == ',')
                    {
                        var charLeftOfI = charArray[i - 1];
                        var charItself = charArray[i];
                        var charRightOfI = charArray[i + 1];

                        if (char.IsDigit(charLeftOfI) && char.IsDigit(charRightOfI))
                        {
                            charArray[i] = '.';
                        }
                    }
                }
            }

            var toStringAgain = new string(charArray);

            var strip1 = toStringAgain.Replace("[", "");
            var strip2 = strip1.Replace("]", "");

            return strip2;
        }
    }
}
