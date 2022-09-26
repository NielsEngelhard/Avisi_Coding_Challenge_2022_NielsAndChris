namespace Magazijn
{
    public class BoxSorter
    {
        public IList<BoxDTO> Sort(IList<BoxDTO> input)
        {
            IList<BoxDTO> sortedBoxes = new List<BoxDTO>();
            List<BoxDTO> unsortedBoxes = input.ToList();

            // Get first item
            var startBox = GetFirstBoxId(unsortedBoxes);
            sortedBoxes.Add(startBox);
            unsortedBoxes.Remove(startBox);

            while (unsortedBoxes.Count != 0)
            {
                foreach(var box in unsortedBoxes)
                {
                    if (box.id == sortedBoxes.Last().next)
                    {
                        sortedBoxes.Add(box);
                        unsortedBoxes.Remove(box);
                        break; // get out of the loop if found for this iteration
                    }
                }
            }

            return sortedBoxes;
        }

        private BoxDTO GetFirstBoxId(IList<BoxDTO> boxes)
        {
            IList<string> boxesIdsInNext = new List<string>();

            // Get all ids that are in a next
            foreach (var box in boxes)
            {
                boxesIdsInNext.Add(box.next);
            }

            // The one box that is not in a next is the start box
            foreach (var box in boxes)
            {
                if (!boxesIdsInNext.Contains(box.id))
                {
                    return box;
                }
            }

            throw new Exception("Cant find the start box because all boxes are in a next");
        }
    }
}
