namespace LocalGraaf;

public class AdjecentVertex
{

    public AdjecentVertex(int neighbourVertex, int weight)
    {
        VertexNumber = neighbourVertex;
        Weight = weight;
    }

    public int VertexNumber { get; set; }

    public int Weight { get; set; }
}