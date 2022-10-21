using Bomen;
using Bomen.Statistics;
using Bomen.Waterers;
using NUnit.Framework;
using System.Linq;

namespace BomenTests
{
    public class XYPlotterTests
    {
        [Test]
        public void Plot_One_Big_Bottle()
        {
            var input = new Waterer[1];
            input[0] = new BigBottle();

            var result = XYWatererPlotter.CreateXYTableOfWaterer(input);

            Assert.AreEqual(result.Count, 2);

            Assert.AreEqual(result.First().Key, 1); // record 1 X
            Assert.AreEqual(result.First().Value, 10); // record 1 Y

            Assert.AreEqual(result.Last().Key, 2); // record 2 X
            Assert.AreEqual(result.Last().Value, 14); // record 2 Y
        }

        [Test]
        public void Plot_Three_Small_Bottles()
        {
            var input = new Waterer[3];
            input[0] = new SmallBottle();
            input[1] = new SmallBottle();
            input[2] = new SmallBottle();

            var result = XYWatererPlotter.CreateXYTableOfWaterer(input);

            Assert.AreEqual(5, result.Count);

            Assert.AreEqual(1, result.ElementAt(0).Key); // record 1 X
            Assert.AreEqual(6, result.ElementAt(0).Value); // record 1 Y

            Assert.AreEqual(2, result.ElementAt(1).Key); // record 2 X
            Assert.AreEqual(12, result.ElementAt(1).Value); // record 2 Y

            Assert.AreEqual(3, result.ElementAt(2).Key); // record 3 X
            Assert.AreEqual(16, result.ElementAt(2).Value); // record 3 Y

            Assert.AreEqual(4, result.ElementAt(3).Key);
            Assert.AreEqual(22, result.ElementAt(3).Value);

            Assert.AreEqual(5, result.ElementAt(4).Key);
            Assert.AreEqual(26, result.ElementAt(4).Value);
        }

        [Test]
        public void Plot_Unlimited_Fire_Hose()
        {
            var input = new Waterer[3];
            input[0] = new UnlimitedFireHose();

            var result = XYWatererPlotter.CreateXYTableOfWaterer(input);

            Assert.AreEqual(50, result.Count);

            Assert.AreEqual(1, result.ElementAt(0).Key); // record 1 X
            Assert.AreEqual(11.5, result.ElementAt(0).Value); // record 1 Y

            Assert.AreEqual(2, result.ElementAt(1).Key); // record 2 X
            Assert.AreEqual(18, result.ElementAt(1).Value); // record 2 Y

            Assert.AreEqual(3, result.ElementAt(2).Key); // record 3 X
            Assert.AreEqual(24.5, result.ElementAt(2).Value); // record 3 Y

            Assert.AreEqual(4, result.ElementAt(3).Key);
            Assert.AreEqual(31, result.ElementAt(3).Value);

            Assert.AreEqual(50, result.Last().Key);
            Assert.AreEqual(330, result.Last().Value);
        }
    }
}
