using System.Drawing;

namespace MazeSolvingLogic.Helpers.Statics
{
    public static class StaticPens
    {

        private static Pen GreenPen = new Pen(Color.Green);
        public static Pen GetGreenPen() => GreenPen;

        private static Pen BlackPen = new Pen(Color.Black);
        public static Pen GetBlackPen() => BlackPen;

        private static Pen RedPen = new Pen(Color.Red);
        public static Pen GetRedPen() => RedPen;

        private static Pen ThickBlackPen = new Pen(Color.Black);
        public static Pen GetThickBlackPen()
        {
            ThickBlackPen.Width = 10;
            return ThickBlackPen;
        }

    }
}
