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

        public void DrawDiscoveredTile(int x, int y, MoveableDirection[] directionsWithDoor, bool tileAlreadyDiscovered, Item? item = null)
        {
            var xLocation = x * SQUARE_SIZE;
            var yLocation = y * SQUARE_SIZE;

            // First fill rectangle with color to indicate that it has been discovered
            if (!tileAlreadyDiscovered) // 1st time discovery
            {
                g.FillRectangle(new SolidBrush(Color.Blue), xLocation, yLocation, SQUARE_SIZE, SQUARE_SIZE);
            } else // 2nd time discovery
            {
                g.FillRectangle(new SolidBrush(Color.DarkBlue), xLocation, yLocation, SQUARE_SIZE, SQUARE_SIZE);
            }

            // Draw key discovered
            if (item != null)
                {
                    if (item.type == "key")
                    {
                    //Pen pen = null;
                    //switch (item.keyType.ResolveKeyColor())
                    //{
                    //    case KeyColor.GREEN:
                    //        pen = new Pen(Color.Green);
                    //        break;
                    //    case KeyColor.RED:
                    //        pen = new Pen(Color.Red);
                    //        break;
                    //    case KeyColor.ORANGE:
                    //        pen = new Pen(Color.Orange);
                    //        break;
                    //}

                        apiCaller.TakeKey(item.keyType);

                        g.DrawEllipse(new Pen(Color.Orange), x * SQUARE_SIZE, y * SQUARE_SIZE, SQUARE_SIZE, SQUARE_SIZE);
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
            DrawDiscoveredTile(startTile.position.x, startTile.position.y, startTile.openDirections.MapMoveableDirectionFromStringToArray(), false);

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
                        IList<MoveableDirection> dirs = new List<MoveableDirection>();
                        foreach(var dir in availableDirections)
                        {
                            switch (dir)
                            {
                                case MoveableDirection.LEFT:
                                    if (!Maze.MazeArray[currentTile.X - 1, currentTile.Y].IsDiscoverd)
                                    {
                                        dirs.Add(dir);
                                    }
                                    break;
                                case MoveableDirection.RIGHT:
                                    if (!Maze.MazeArray[currentTile.X + 1, currentTile.Y].IsDiscoverd)
                                    {
                                        dirs.Add(dir);
                                    }
                                    break;
                                case MoveableDirection.UP:
                                    if (!Maze.MazeArray[currentTile.X, currentTile.Y - 1].IsDiscoverd)
                                    {
                                        dirs.Add(dir);
                                    }
                                    break;
                                case MoveableDirection.DOWN:
                                    if (!Maze.MazeArray[currentTile.X, currentTile.Y + 1].IsDiscoverd)
                                    {
                                        dirs.Add(dir);
                                    }
                                    break;
                            }
                        }
                        Random rnd = new Random();
                        try
                        {
                            int index = rnd.Next(dirs.Count());
                            chosenDirection = dirs[index];
                        }
                        catch (Exception ex)
                        {
                            IList<MoveableDirection> dirs2 = new List<MoveableDirection>();
                            foreach (var dir in availableDirections)
                            {
                                switch (dir)
                                {
                                    case MoveableDirection.LEFT:
                                        if (!Maze.MazeArray[currentTile.X - 1, currentTile.Y].IsDiscoverdTwice)
                                        {
                                            dirs2.Add(dir);
                                        }
                                        break;
                                    case MoveableDirection.RIGHT:
                                        if (!Maze.MazeArray[currentTile.X + 1, currentTile.Y].IsDiscoverdTwice)
                                        {
                                            dirs2.Add(dir);
                                        }
                                        break;
                                    case MoveableDirection.UP:
                                        if (!Maze.MazeArray[currentTile.X, currentTile.Y - 1].IsDiscoverdTwice)
                                        {
                                            dirs2.Add(dir);
                                        }
                                        break;
                                    case MoveableDirection.DOWN:
                                        if (!Maze.MazeArray[currentTile.X, currentTile.Y + 1].IsDiscoverdTwice)
                                        {
                                            dirs2.Add(dir);
                                        }
                                        break;
                                }
                            }
                            int index = rnd.Next(dirs2.Count());
                            chosenDirection = dirs2[index];
                        }
                    } 
                    else // count is 0 --> walk back
                    {
                        break;
                    }
                    if (chosenDirection != null)
                    {
                        var newPosition = apiCaller.MoveInDirection(chosenDirection);

                        DrawDiscoveredTile(newPosition.position.x, newPosition.position.y, newPosition.openDirections.MapMoveableDirectionFromStringToArray(), Maze.MazeArray[newPosition.position.x, newPosition.position.y].IsDiscoverd, newPosition.item);

                        Maze.MazeArray[newPosition.position.x, newPosition.position.y] = ApiResultMapper.MapResponseToMazeTile(newPosition, Maze.MazeArray[newPosition.position.x, newPosition.position.y].IsDiscoverd);
                        currentTile = Maze.MazeArray[newPosition.position.x, newPosition.position.y];
                        cameFromDirection = DirectionHelper.OppositeDirection(chosenDirection);
                    }
                }
                else // Er is nog maar 1 mogelijke deur (dead end)
                {
                    // if nog 1 mogelijke deur loop 1 terug
                    DrawDiscoveredTile(currentTile.X, currentTile.Y, currentTile.OpenDirections.ToArray(), currentTile.IsDiscoverd);
                    currentTile = WalkBackFromDeadEnd(cameFromDirection);
                    DrawDiscoveredTile(currentTile.X, currentTile.Y, currentTile.OpenDirections.ToArray(), currentTile.IsDiscoverd);
                    cameFromDirection = DirectionHelper.OppositeDirection(cameFromDirection);
                    // 
                    // Dead end - now broken...
                    // WalkToTile(juntcion);
                }
            }

        }

        public MazeTile WalkBackFromDeadEnd(MoveableDirection cameFromDirection)
        {
            return ApiResultMapper.MapResponseToMazeTile(apiCaller.MoveInDirection(cameFromDirection), true);
        }

        public bool anyUndiscoveredTiles()
        {

            return false;
        }
    }
}