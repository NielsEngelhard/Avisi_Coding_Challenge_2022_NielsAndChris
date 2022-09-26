namespace Magazijn
{
    public class BoxDTO
    {
        public string id { get; set; }
        public string content { get; set; }
        public string next { get; set; }
    }

    public class BoxesDto
    {
        public BoxDTO[] items { get; set; }
    }
}
