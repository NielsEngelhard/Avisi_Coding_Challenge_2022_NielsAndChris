namespace MazeSolvingLogic.Models
{
    public class MazeTile
    {
        public int X { get; set; }
        public int Y { get; set; }

        [Obsolete("Use nTimesDiscovered instead")]
        public bool IsDiscoverd { get; set; } // deprecated as of doolhof 2

        [Obsolete("Use nTimesDiscovered instead")]
        public bool IsDiscoverdTwice { get; set; } // deprecated as of doolhof 2

        public int nTimesDiscovered { get; set; }

        public string Item { get; set; }

        public IList<MoveableDirection> OpenDirections { get; set; }

        public MazeTile(int x, int y)
        {
            X = x;
            Y = y;
            IsDiscoverd = false; // deprecated as of doolhof 2
            IsDiscoverdTwice = false; // deprecated as of doolhof 2
            nTimesDiscovered = 0;
        }
    }
}
