using MazeSolvingLogic.Models;

namespace MazeSolvingLogic.Mappers
{
    public static class EnumResolver
    {
        public static string ResolveMoveableDirection(this MoveableDirection direction)
        {
            return direction switch
            {
                MoveableDirection.LEFT => "LEFT",
                MoveableDirection.RIGHT => "RIGHT",
                MoveableDirection.UP => "UP",
                MoveableDirection.DOWN => "DOWN",
                _ => throw new Exception($"unknown direction {direction}"),
            };
        }

        public static string ResolveKeyColor(this KeyColor keyColor)
        {
            return keyColor switch
            {
                KeyColor.RED => "RED",
                KeyColor.GREEN => "GREEN",
                KeyColor.ORANGE => "ORANGE",
                _ => throw new Exception($"unknown key color {keyColor}"),
            };
        }

        public static KeyColor ResolveKeyColor(this string keyColor)
        {
            return keyColor switch
            {
                "RED" => KeyColor.RED,
                "GREEN"=> KeyColor.GREEN,
                "ORANGE" => KeyColor.ORANGE,
                _ => throw new Exception($"unknown key color {keyColor}"),
            };
        }
    }
}
