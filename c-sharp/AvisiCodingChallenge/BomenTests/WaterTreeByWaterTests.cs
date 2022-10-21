using Bomen.Waterers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BomenTests
{
    public class WaterTreeByWaterTests
    {
        private Waterer _waterer;

        [Test]
        public void Water_With_Gardening_Hose_With_1_Liter()
        {
            _waterer = new UnlimitedGardenHose();

            var litersOfWatering = 1;

            var result = _waterer.WaterTreeByWaterToGive(litersOfWatering);

            Assert.AreEqual(result.DurationOfGivingWater, 2);
            Assert.AreEqual(result.LitersOfWaterGiven, 1);
        }

        [Test]
        public void Water_With_Big_Bottle_For_0dot5_Liter()
        {
            _waterer = new BigBottle();

            var litersOfWatering = 0.5f;

            var result = _waterer.WaterTreeByWaterToGive(litersOfWatering);

            Assert.AreEqual(result.DurationOfGivingWater, 0.5);
            Assert.AreEqual(result.LitersOfWaterGiven, 0.5);
            Assert.AreEqual(result.WaterLeftInCurrentItem, 7.5);
        }

        [Test]
        public void Water_With_Small_Bottle_For_3_Liters()
        {
            _waterer = new SmallBottle();

            var litersOfWatering = 3;

            var result = _waterer.WaterTreeByWaterToGive(litersOfWatering);

            Assert.AreEqual(result.DurationOfGivingWater, 3);
            Assert.AreEqual(result.LitersOfWaterGiven, 3);
            Assert.AreEqual(result.WaterLeftInCurrentItem, 2);
        }

        [Test]
        public void Water_With_Fire_Hose_For_10_Liters()
        {
            _waterer = new UnlimitedFireHose();

            var litersOfWatering = 10;

            var result = _waterer.WaterTreeByWaterToGive(litersOfWatering);

            Assert.AreEqual(result.DurationOfGivingWater, 5);
            Assert.AreEqual(result.LitersOfWaterGiven, 10);
        }
    }
}
