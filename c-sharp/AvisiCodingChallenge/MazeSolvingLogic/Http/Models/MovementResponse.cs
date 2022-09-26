namespace MazeSolvingLogic.Http.Models
{
    public class MovementResponse
    {
        public Position position { get; set; }

        public string[] openDirections { get; set; }

        public string[] lockedDirections { get; set; }

        public Item item { get; set; }
    }

    public class Position
    {
        public int x { get; set; }

        public int y { get; set; }
    }

    public class Item
    {
        public string type { get; set; }

        public string keyType { get; set; }
    }
}
