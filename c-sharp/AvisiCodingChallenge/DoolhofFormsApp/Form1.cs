using MazeSolvingLogic;
using MazeSolvingLogic.Helpers;
using MazeSolvingLogic.Helpers.Statics;
using MazeSolvingLogic.Http;
using MazeSolvingLogic.Http.Models;
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

        public void DrawDiscoveredTile(int x, int y, MoveableDirection[] directionsWithDoor, Item? item = null)
        {
            var xLocation = x * SQUARE_SIZE;
            var yLocation = y * SQUARE_SIZE;

            // First fill rectangle with color to indicate that it has been discovered
            g.FillRectangle(new SolidBrush(Color.Blue), xLocation, yLocation, SQUARE_SIZE, SQUARE_SIZE);

            // Draw key discovered
            if (item != null)
            {
                if (item.type == "key")
                {
                    Pen pen = null;
                    switch (item.keyType.ResolveKeyColor())
                    {
                        case KeyColor.GREEN:
                            pen = new Pen(Color.Green);
                            break;
                        case KeyColor.RED:
                            pen = new Pen(Color.Red);
                            break;
                        case KeyColor.ORANGE:
                            pen = new Pen(Color.Orange);
                            break;
                    }

                    g.DrawEllipse(pen, x* SQUARE_SIZE, y* SQUARE_SIZE, SQUARE_SIZE, SQUARE_SIZE);
                }
            }

            // Add borders to the directions where there is a door
            if (directionsWithDoor.Contains(MoveableDirection.UP))
            {
                if (Maze.MazeArray[x,y-1].IsDiscoverd == false)
                {
                    g.FillRectangle(new SolidBrush(Color.LawnGreen), xLocation, yLocation - SQUARE_SIZE, SQUARE_SIZE, SQUARE_SIZE);
                }
            } else // Draw wall because you cant go here
            {
                g.DrawLine(StaticPens.GetRedPen(), new Point(xLocation, yLocation), new Point(xLocation + SQUARE_SIZE, yLocation));
            }

            if (directionsWithDoor.Contains(MoveableDirection.DOWN))
            {
                if (Maze.MazeArray[x, y+1].IsDiscoverd == false)
                {
                    g.FillRectangle(new SolidBrush(Color.LawnGreen), xLocation, yLocation + SQUARE_SIZE, SQUARE_SIZE, SQUARE_SIZE);
                }
            } else// Draw wall because you cant go here
            {
                g.DrawLine(StaticPens.GetRedPen(), new Point(xLocation, yLocation + SQUARE_SIZE), new Point(xLocation + SQUARE_SIZE, yLocation + SQUARE_SIZE));
            }

            if (directionsWithDoor.Contains(MoveableDirection.LEFT))
            {
                if (Maze.MazeArray[x-1, y].IsDiscoverd == false)
                {
                    g.FillRectangle(new SolidBrush(Color.LawnGreen), xLocation - SQUARE_SIZE, yLocation, SQUARE_SIZE, SQUARE_SIZE);
                }
            } else // Draw wall because you cant go here
            {
                g.DrawLine(StaticPens.GetRedPen(), new Point(xLocation, yLocation), new Point(xLocation, yLocation + SQUARE_SIZE));
            }

            if (directionsWithDoor.Contains(MoveableDirection.RIGHT))
            {
                if (Maze.MazeArray[x+1, y].IsDiscoverd == false)
                {
                    g.FillRectangle(new SolidBrush(Color.LawnGreen), xLocation + SQUARE_SIZE, yLocation, SQUARE_SIZE, SQUARE_SIZE);
                } 
            } else
            { // Draw wall because you cant go here
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
            while (true)
            {
                if (currentTile.OpenDirections.Count() > 1 || (currentTile.X == 0 && currentTile.Y == 0)) // else dead end
                {
                    var availableDirections = currentTile.OpenDirections;
                    availableDirections.Remove(cameFromDirection);
                    MoveableDirection chosenDirection;

                    if (availableDirections.Count() == 1)
                    {
                        chosenDirection = availableDirections[0];
                    } 
                    else if (availableDirections.Count() > 1) // junction found --> choose random direction
                    {
                        Random rnd = new Random();
                        int index = rnd.Next(availableDirections.Count());
                        chosenDirection = availableDirections[index];
                    } 
                    else // count is 0 --> walk back
                    {
                        break;
                    }
                    if (chosenDirection != null)
                    {
                        var newPosition = apiCaller.MoveInDirection(chosenDirection);

                        try
                        {
                            DrawDiscoveredTile(newPosition.position.x, newPosition.position.y, newPosition.openDirections.MapMoveableDirectionFromStringToArray(), newPosition.item);
                        }
                        catch (Exception ex)
                        {
                            break;
                        }
                        Maze.MazeArray[newPosition.position.x, newPosition.position.y] = ApiResultMapper.MapResponseToMazeTile(newPosition);
                        currentTile = Maze.MazeArray[newPosition.position.x, newPosition.position.y];
                        cameFromDirection = DirectionHelper.OppositeDirection(chosenDirection);
                    }
                }
                else
                {
                    // Dead end - now broken...
                    break;
                    // WalkToTile(juntcion);
                }
            }

        }

        public void WalkToTile(int x, int y)
        {
            // TODO implement
        }
    }
}