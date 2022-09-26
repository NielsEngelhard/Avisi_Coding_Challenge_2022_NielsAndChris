using Magazijn;

namespace MagazijnTests
{
    public class SorterTests
    {
        private BoxSorter BoxSorter { get; set; }
        private BoxMapper BoxMapper { get; set; }

        [SetUp]
        public void Setup()
        {
            BoxSorter = new BoxSorter();
            BoxMapper = new BoxMapper();
        }

        [Test]
        public void Map_Count()
        {
            // Should have made mock data but was 2 lazy
            var unsortedBoxes = BoxMapper.MapBoxJsonToDto("box-test-data.json");
            var sortedBoxes = BoxSorter.Sort(unsortedBoxes);


            Assert.That(sortedBoxes[0].id, Is.EqualTo("QTE"));
            Assert.That(sortedBoxes[1].id, Is.EqualTo("XEE"));
            Assert.That(sortedBoxes[2].id, Is.EqualTo("IFU"));
            Assert.That(sortedBoxes[3].id, Is.EqualTo("ULB"));
            Assert.That(sortedBoxes[4].id, Is.EqualTo("GVF"));
            Assert.That(sortedBoxes[5].id, Is.EqualTo("PEX"));
            Assert.That(sortedBoxes[6].id, Is.EqualTo("FUP"));
            Assert.That(sortedBoxes[7].id, Is.EqualTo("DPU"));
        }
    }
}
