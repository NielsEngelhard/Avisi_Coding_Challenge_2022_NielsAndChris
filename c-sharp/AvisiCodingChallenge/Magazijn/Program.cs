using Magazijn;

BoxMapper BoxMapper = new BoxMapper();
BoxSorter BoxSorter = new BoxSorter();

var unsortedBoxes = BoxMapper.MapBoxJsonToDto("boxdata.json");
var sortedBoxes = BoxSorter.Sort(unsortedBoxes);
var sortedIdString = BoxMapper.MapBoxesToStringWithIds(sortedBoxes);

Console.WriteLine(sortedIdString);