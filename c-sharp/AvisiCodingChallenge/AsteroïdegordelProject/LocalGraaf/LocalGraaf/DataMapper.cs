namespace LocalGraaf
{
    public static class DataMapper
    {
        public static string[] MapDataToStringArrays()
        {
            var directory = @"C:\Users\nedev\source\repos\LocalGraaf\Data\test_data.txt";

            List<string[]> list = File.ReadLines(directory)
                .Select(line => line.Split("],"))
                .ToList();


            return list.First();
        }

        public static string[] GetStrippedStringArrays()
        {
            var stringArrays = MapDataToStringArrays();

            for (var i=0; i<stringArrays.Length; i++)
            {
                stringArrays[i] = stringArrays[i].Replace("[", "");
                stringArrays[i] = stringArrays[i].Replace("]", "");

                if (stringArrays[i][stringArrays[i].Length-1] == ',')
                {
                    stringArrays[i] = stringArrays[i].Remove(stringArrays[i].Length - 1);
                }
            }

            return stringArrays;
        }

        public static List<int[]> GetAllPointsAsIntArray()
        {
            var strippedStringArrays = GetStrippedStringArrays();
            var intList = new List<int[]>();

            foreach (var stringWithNumbers in strippedStringArrays)
            {
                var splittedString = stringWithNumbers.Split(',');

                if (splittedString.Length != 3)
                {
                    throw new Exception("Something went wrong with the input");
                }

                var numbers = new int[3];
                numbers[0] = int.Parse(splittedString[0]);
                numbers[1] = int.Parse(splittedString[1]);
                numbers[2] = int.Parse(splittedString[2]);

                intList.Add(numbers);
            }


            return intList;
        }


    }
}
