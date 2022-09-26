using MazeSolvingLogic;
using MazeSolvingLogic.Helpers;
using MazeSolvingLogic.Helpers.Statics;
using MazeSolvingLogic.Http;
using MazeSolvingLogic.Mappers;
using MazeSolvingLogic.Models;

namespace DoolhofFormsApp
{
    public partial class Form1 : Form
    {

        private const int SQUARE_SIZE = 20;
        private static Pen pen;

        private Graphics g;

        private readonly AvisiApiCaller apiCaller;

        public Form1()
        {
            apiCaller = new AvisiApiCaller();

            InitializeComponent();
            Maze.InitiateMaze();
        }
        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {
            g = e.Graphics;

            DrawInitialEmptyMaze();
            InitCall();
        }

        private void DrawInitialEmptyMaze()
        {
            for (int x = 0; x < Maze.MAZE_SIZE; x++)
            {
                for (int y = 0; y < Maze.MAZE_SIZE; y++)
                {
                    // Determine pen color based on info in tile
                    pen = Maze.DetermineSquareColor(Maze.MazeArray[x, y]);

                    // Draw the square
                    g.DrawRectangle(pen, SQUARE_SIZE * x, SQUARE_SIZE * y, SQUARE_SIZE, SQUARE_SIZE);
                }
            }
        }

        public void DrawDiscoveredTile(int x, int y, MoveableDirection[] directionsWithDoor)
        {
            var xLocation = x * SQUARE_SIZE;
            var yLocation = y * SQUARE_SIZE;

            // First fill rectangle with color to indicate that it has been discovered
            g.FillRectangle(new SolidBrush(Color.Blue), xLocation, yLocation, SQUARE_SIZE, SQUARE_SIZE);

            // Add borders to the directions where there is a door
            if (directionsWithDoor.Contains(MoveableDirection.UP))
            {
                g.DrawLine(StaticPens.GetRedPen(), new Point(xLocation, yLocation), new Point(xLocation + SQUARE_SIZE, yLocation));
            }

            if (directionsWithDoor.Contains(MoveableDirection.DOWN))
            {
                g.DrawLine(StaticPens.GetRedPen(), new Point(xLocation, yLocation + SQUARE_SIZE), new Point(xLocation + SQUARE_SIZE, yLocation + SQUARE_SIZE));
            }

            if (directionsWithDoor.Contains(MoveableDirection.LEFT))
            {
                g.DrawLine(StaticPens.GetRedPen(), new Point(xLocation, yLocation), new Point(xLocation, yLocation + SQUARE_SIZE));
            }

            if (directionsWithDoor.Contains(MoveableDirection.RIGHT))
            {
                g.DrawLine(StaticPens.GetRedPen(), new Point(xLocation + SQUARE_SIZE, yLocation), new Point(xLocation + SQUARE_SIZE, yLocation + SQUARE_SIZE));
            }
        }


        public void InitCall()
        {
            // Initiate on point 0,0
            apiCaller.ResetMaze();
            var startTile = apiCaller.GetCurrentPosition();
            Maze.MazeArray[startTile.position.x, startTile.position.y] = ApiResultMapper.MapResponseToMazeTile(startTile);
            DrawDiscoveredTile(startTile.position.x, startTile.position.y, startTile.openDirections.MapMoveableDirectionFromStringToArray());


            var currentTile = Maze.MazeArray[startTile.position.x, startTile.position.y];
            MoveableDirection cameFromDirection = MoveableDirection.UP;


            // Walk through maze
            var canWalkMaze = true;
            while (canWalkMaze)
            {
                if (currentTile.OpenDirections.Count() > 1 || (currentTile.X == 0 && currentTile.Y == 0)) // else dead end
                {
                    // dont go the direction that he came from
                    foreach (var direction in currentTile.OpenDirections)
                    {
                        // if (kwam niet van die richting vandaan)
                        if (cameFromDirection != direction)
                        {
                            // ga dan die direction op
                            var newPosition = apiCaller.MoveInDirection(direction);
                            DrawDiscoveredTile(newPosition.position.x, newPosition.position.y, newPosition.openDirections.MapMoveableDirectionFromStringToArray());
                            Maze.MazeArray[newPosition.position.x, newPosition.position.y] = ApiResultMapper.MapResponseToMazeTile(newPosition);
                            currentTile = Maze.MazeArray[newPosition.position.x, newPosition.position.y];
                            cameFromDirection = DirectionHelper.OppositeDirection(direction);
                        }
                    }
                }
                else
                {
                    // Dead end - now broken...
                    canWalkMaze = false;
                }
            }

        }
    }
}