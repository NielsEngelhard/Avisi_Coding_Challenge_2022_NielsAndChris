namespace MazeSolvingLogic.Models
{
    public class MazeTile
    {
        public int X { get; set; }
        public int Y { get; set; }

        public bool IsDiscoverd { get; set; }

        public string Item { get; set; }

        public IList<MoveableDirection> OpenDirections { get; set; }

        public MazeTile(int x, int y)
        {
            X = x;
            Y = y;
            IsDiscoverd = false;
        }
    }
}
