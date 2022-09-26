using MazeSolvingLogic.Models;

namespace MazeSolvingLogic.Helpers
{
    public static class DirectionHelper
    {
        public static bool CameFromOppositeDirection(MoveableDirection first, MoveableDirection second)
        {
            if (first == MoveableDirection.UP)
            {
                return second == MoveableDirection.DOWN;
            }

            if (first == MoveableDirection.DOWN)
            {
                return second == MoveableDirection.UP;
            }

            if (first == MoveableDirection.LEFT)
            {
                return second == MoveableDirection.RIGHT;
            }

            if (first == MoveableDirection.RIGHT)
            {
                return second == MoveableDirection.LEFT;
            }

            throw new Exception("This cant happen");
        }

        public static MoveableDirection OppositeDirection(MoveableDirection direction)
        {
            if (direction == MoveableDirection.UP)
            {
                return MoveableDirection.DOWN;
            }

            if (direction == MoveableDirection.DOWN)
            {
                return MoveableDirection.UP;
            }

            if (direction == MoveableDirection.LEFT)
            {
                return MoveableDirection.RIGHT;
            }

            if (direction == MoveableDirection.RIGHT)
            {
                return MoveableDirection.LEFT;
            }

            throw new Exception("This cant happen");
        }
    }
}
