using Magazijn;

namespace MagazijnTests
{
    public class Tests
    {

        private BoxMapper BoxMapper { get; set; }
        private BoxSorter BoxSorter { get; set; }

        [SetUp]
        public void Setup()
        {
            BoxMapper = new BoxMapper();
            BoxSorter = new BoxSorter();
        }

        [Test]
        public void Map_Count()
        {
            var result = BoxMapper.MapBoxJsonToDto("box-test-data.json");
            Assert.That(result.Count(), Is.EqualTo(8));
        }

        [Test]
        public void Map_Values_Filled()
        {
            var result = BoxMapper.MapBoxJsonToDto("box-test-data.json");

             Assert.That(string.IsNullOrEmpty(result.First().id), Is.False);
             Assert.That(string.IsNullOrEmpty(result.First().next), Is.False);
             Assert.That(string.IsNullOrEmpty(result.First().content), Is.False);
        }

        [Test]
        public void Map_Ids_String()
        {
            // Lazy tests but it works (dependency on result of other public functions)
            var expected = "QTE-XEE-IFU-ULB-GVF-PEX-FUP-DPU";

            var unsortedBoxes = BoxMapper.MapBoxJsonToDto("box-test-data.json");
            var sortedBoxes = BoxSorter.Sort(unsortedBoxes);
            var sortedIdString = BoxMapper.MapBoxesToStringWithIds(sortedBoxes);

            Assert.That(sortedIdString, Is.EqualTo(expected));
        }
    }
}