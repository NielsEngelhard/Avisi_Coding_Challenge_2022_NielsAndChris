using Newtonsoft.Json;

namespace Magazijn
{
    public class BoxMapper
    {
        public IList<BoxDTO> MapBoxJsonToDto(string jsonLocation)
        {
            using (StreamReader r = File.OpenText(jsonLocation))
            {
                string json = r.ReadToEnd();
                BoxesDto items = JsonConvert.DeserializeObject<BoxesDto>(json);

                Console.WriteLine($"Found {items.items.Length} records in the JSON array");

                return items.items;
            }
        }

        public string MapBoxesToStringWithIds(IList<BoxDTO> boxes)
        {
            string stringWithBoxIds = "";

            for(int i=0; i < boxes.Count(); i++)
            {
                if (i == 0)
                {
                    stringWithBoxIds += boxes[i].id;
                } else
                {
                    stringWithBoxIds += ("-" + boxes[i].id);
                }

            }

            return stringWithBoxIds;
        }
    }
}
