using MazeSolvingLogic.Http.Models;
using MazeSolvingLogic.Mappers;
using MazeSolvingLogic.Models;
using System.Text.Json;

namespace MazeSolvingLogic.Http
{
    public class AvisiApiCaller : IAvisiApiCaller
    {
        private readonly HttpClient httpClient;

        public const string API_BASE_ENDPOINT = "https://acc.avisilabs.nl/api/maze";
        private const string API_KEY = "e41b0ea1-6693-403e-9b87-a2ebbc7284cb";

        public AvisiApiCaller()
        {
            this.httpClient = new HttpClient();
        }

        public void ResetMaze()
        {
            var path = $"/reset?secret={API_KEY}";
            var response = httpClient.PostAsync($"{API_BASE_ENDPOINT}{path}", null).Result;

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Could not reset");
            }

            Console.WriteLine("Calling reset maze");
        }

        public MovementResponse? GetCurrentPosition()
        {
            var path = $"/position?secret={API_KEY}";
            var response = httpClient.GetAsync($"{API_BASE_ENDPOINT}{path}").Result;

            return JsonSerializer.Deserialize<MovementResponse>(response.Content.ReadAsStream());     
        }

        public MovementResponse? MoveInDirection(MoveableDirection moveableDirection)
        {
            var path = $"/position/move/{moveableDirection.ResolveMoveableDirection()}?secret={API_KEY}";
            var response = httpClient.PostAsync($"{API_BASE_ENDPOINT}{path}", null).Result;

            return JsonSerializer.Deserialize<MovementResponse>(response.Content.ReadAsStream());
        }

        public InventoryResponse? GetCurrentInventory()
        {
            var path = $"/inventory?secret={API_KEY}";
            var response = httpClient.GetAsync($"{API_BASE_ENDPOINT}{path}").Result;

            return JsonSerializer.Deserialize<InventoryResponse>(response.Content.ReadAsStream());
        }

        public InventoryResponse? TakeKey(string keyType)
        {
            var path = $"/inventory/loot/key/{keyType}?secret={API_KEY}";
            var response = httpClient.PostAsync($"{API_BASE_ENDPOINT}{path}", null).Result;

            return JsonSerializer.Deserialize<InventoryResponse>(response.Content.ReadAsStream());
        }

        public MovementResponse? OpenDoor(KeyColor keyColor, MoveableDirection direction)
        {
            var path = $"/inventory/use/key/{keyColor.ResolveKeyColor()}/{direction.ResolveMoveableDirection()}?secret={API_KEY}";
            var response = httpClient.PostAsync($"{API_BASE_ENDPOINT}{path}", null).Result;

            return JsonSerializer.Deserialize<MovementResponse>(response.Content.ReadAsStream());
        }

        public InventoryResponse? LootFuse()
        {
            var path = $"/inventory/loot/fuse?secret={API_KEY}";
            var response = httpClient.PostAsync($"{API_BASE_ENDPOINT}{path}", null).Result;

            return JsonSerializer.Deserialize<InventoryResponse>(response.Content.ReadAsStream());
        }
    }
}
