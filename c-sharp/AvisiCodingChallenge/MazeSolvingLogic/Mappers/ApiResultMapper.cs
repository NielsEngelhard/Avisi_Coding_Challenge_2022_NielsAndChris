using MazeSolvingLogic.Http.Models;
using MazeSolvingLogic.Models;

namespace MazeSolvingLogic.Mappers
{
    public static class ApiResultMapper
    {
        public static MoveableDirection[] MapMoveableDirectionFromStringToArray(this string[] directions)
        {
            var response = new List<MoveableDirection>();

            if (directions.Contains("RIGHT"))
            {
                response.Add(MoveableDirection.RIGHT);
            }

            if (directions.Contains("LEFT"))
            {
                response.Add(MoveableDirection.LEFT);
            }

            if (directions.Contains("UP"))
            {
                response.Add(MoveableDirection.UP);
            }

            if (directions.Contains("DOWN"))
            {
                response.Add(MoveableDirection.DOWN);
            }

            return response.ToArray();
        }

        public static MazeTile MapResponseToMazeTile(MovementResponse response, bool isDiscoveredTwice = false)
        {
            if ((response.item != null) && response.item.type != "key")
            {
                throw new Exception($"something unknown on this tile: {response.item.type}");
            }
            if (isDiscoveredTwice)
            {
                return new MazeTile(response.position.x, response.position.y)
                {
                    IsDiscoverd = true,
                    IsDiscoverdTwice = true,
                    Item = response.item?.keyType,
                    OpenDirections = response.openDirections.MapStringArrayToMoveableDirections()
                };
            }
            else
            {
                return new MazeTile(response.position.x, response.position.y)
                {
                    IsDiscoverd = true,
                    Item = response.item?.keyType,
                    OpenDirections = response.openDirections.MapStringArrayToMoveableDirections()
                };
            }
        }

        public static IList<MoveableDirection> MapStringArrayToMoveableDirections(this string[] directionsStringArray)
        {
            var directionList = new List<MoveableDirection>();

            foreach(var direction in directionsStringArray)
            {
                if (direction == "UP")
                {
                    directionList.Add(MoveableDirection.UP);
                }

                if (direction == "DOWN")
                {
                    directionList.Add(MoveableDirection.DOWN);
                }

                if (direction == "LEFT")
                {
                    directionList.Add(MoveableDirection.LEFT);
                }

                if (direction == "RIGHT")
                {
                    directionList.Add(MoveableDirection.RIGHT);
                }
            }

            return directionList;
        }
    }
}
