using MazeSolvingLogic;
using MazeSolvingLogic.Helpers;
using MazeSolvingLogic.Helpers.Statics;
using MazeSolvingLogic.Http;
using MazeSolvingLogic.Http.Models;
using MazeSolvingLogic.Mappers;
using MazeSolvingLogic.Models;
using System.Runtime.InteropServices;

namespace DoolhofFormsApp2
{
    public partial class Form1 : Form
    {
        private const int SQUARE_SIZE = 10;
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
            base.OnPaint(e);
            SetStyle(ControlStyles.ResizeRedraw, false);
            DrawInitialEmptyMaze();
        }
        
        private void DrawInitialEmptyMaze()
        {
            for (int x = 0; x < Maze.MAZE_SIZE; x++)
            {
                for (int y = 0; y < Maze.MAZE_SIZE; y++)
                {
                    pen = Maze.BlackPen;
                    g.DrawRectangle(pen, SQUARE_SIZE * x, SQUARE_SIZE * y, SQUARE_SIZE, SQUARE_SIZE);
                }
            }
        }

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, Int32 wMsg, bool wParam, Int32 lParam);
        public static void SuspendDrawing(Control parent)
        {
            SendMessage(parent.Handle, 11, false, 0);
        }
        public static void ResumeDrawing(Control parent)
        {
            SendMessage(parent.Handle, 11, true, 0);
            parent.Refresh();
        }
    }
}