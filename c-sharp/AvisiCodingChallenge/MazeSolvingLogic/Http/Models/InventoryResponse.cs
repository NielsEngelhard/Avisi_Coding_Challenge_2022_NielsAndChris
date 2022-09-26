namespace MazeSolvingLogic.Http.Models
{
    public class InventoryResponse
    {
        public string[] Keys { get; set; }

        public Fuse Fuse { get; set; }
    }

    public class Fuse
    {
        public string Type { get; set; }
    }
}
