using Bomen;
using Bomen.Waterers;
using NUnit.Framework;

namespace BomenTests
{
    public class WaterTreeForSecondsTests
    {
        private Waterer _waterer;

        [Test]
        public void Water_With_Gardening_Hose_For_1_Second()
        {
            _waterer = new UnlimitedGardenHose();

            var secondsOfWatering = 1;

            var result = _waterer.WaterTreeByTimeInSeconds(secondsOfWatering);

            Assert.AreEqual(result.DurationOfGivingWater, 1);
            Assert.AreEqual(result.LitersOfWaterGiven, 0.5);
        }

        [Test]
        public void Water_With_Big_Bottle_For_0dot5_Second()
        {
            _waterer = new BigBottle();

            var secondsOfWatering = 0.5f;

            var result = _waterer.WaterTreeByTimeInSeconds(secondsOfWatering);

            Assert.AreEqual(result.DurationOfGivingWater, 0.5);
            Assert.AreEqual(result.LitersOfWaterGiven, 0.5);
            Assert.AreEqual(result.WaterLeftInCurrentItem, 7.5);
        }

        [Test]
        public void Water_With_Small_Bottle_For_3_Second()
        {
            _waterer = new SmallBottle();

            var secondsOfWatering = 3;

            var result = _waterer.WaterTreeByTimeInSeconds(secondsOfWatering);

            Assert.AreEqual(result.DurationOfGivingWater, 3);
            Assert.AreEqual(result.LitersOfWaterGiven, 3);
            Assert.AreEqual(result.WaterLeftInCurrentItem, 2);
        }

        [Test]
        public void Water_With_Fire_Hose_For_10_Second()
        {
            _waterer = new UnlimitedFireHose();

            var secondsOfWatering = 10;

            var result = _waterer.WaterTreeByTimeInSeconds(secondsOfWatering);

            Assert.AreEqual(result.DurationOfGivingWater, 10);
            Assert.AreEqual(result.LitersOfWaterGiven, 20);
        }
    }
}
