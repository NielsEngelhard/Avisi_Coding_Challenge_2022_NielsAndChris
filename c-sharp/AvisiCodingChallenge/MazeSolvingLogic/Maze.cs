using MazeSolvingLogic.Models;
using System.Drawing;

namespace MazeSolvingLogic
{
    public static class Maze
    {

        public const int MAZE_SIZE = 100;


        public static MazeTile[,] MazeArray { get; set; }

        public static void InitiateMaze()
        {
            MazeArray = new MazeTile[MAZE_SIZE,MAZE_SIZE];

            for (int x = 0; x < MazeArray.GetLength(0); x++)
            {
                for (int y = 0; y < MazeArray.GetLength(1); y++)
                {
                    MazeArray[x, y] = new MazeTile(x, y);
                }
            }
        }

        public static Pen GreenPen = new Pen(Color.Blue);
        public static Pen BlackPen = new Pen(Color.Black);
        public static Pen DetermineSquareColor(MazeTile tile)
        {
            if (tile.IsDiscoverd)
            {
                return GreenPen;
            }


            return BlackPen;
        }

    }
}