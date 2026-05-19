using System;
using Avalonia.Media;
using RefactorMe.Common;

namespace RefactorMe
{
    class TheDraftsman
    {
        static float x, y;
        static IGraphics graphics;

        public static void Initialize(IGraphics newGraphics)
        {
            graphics = newGraphics;
            // grafika.SmoothingMode = SmoothingMode.None;
            graphics.Clear(Colors.Black);
        }

        public static void SetPosition(float x0, float y0)
        { x = x0; y = y0; }

        public static void MakeIt(Pen aPen, double length, double angle)
        {
            // Делает шаг длиной length в направлении angle и рисует пройденную траекторию
            var x1 = (float)(x + length * Math.Cos(angle));
            var y1 = (float)(y + length * Math.Sin(angle));
            graphics.DrawLine(aPen, x, y, x1, y1);
            x = x1;
            y = y1;
        }

        public static void Change(double length, double angle)
        {
            x = (float)(x + length * Math.Cos(angle));
            y = (float)(y + length * Math.Sin(angle));
        }
    }

    public class ImpossibleSquare
    {
        public static void Draw(int width, int height, double angleOfRotation, IGraphics graphics)
        {
            // GetAngleOfRotation пока не используется, но будет использоваться в будущем
            int minSize = GetAngleOfRotation(width, height, graphics);
            // Рисуем 1-ую сторону
            DrawTheFirstSide(minSize);

            // Рисуем 2-ую сторону
            DrawTheSecondSide(minSize);

            // Рисуем 3-ю сторону
            DrawThirdSide(minSize);

            // Рисуем 4-ую сторону
            DrawFourthSide(minSize);
        }

        private static void DrawFourthSide(int minSize)
        {
            TheDraftsman.MakeIt(new Pen(Brushes.Yellow), minSize * 0.375f, Math.PI / 2);
            TheDraftsman.MakeIt(new Pen(Brushes.Yellow), minSize * 0.04f * Math.Sqrt(2), Math.PI / 2 + Math.PI / 4);
            TheDraftsman.MakeIt(new Pen(Brushes.Yellow), minSize * 0.375f, Math.PI / 2 + Math.PI);
            TheDraftsman.MakeIt(new Pen(Brushes.Yellow), minSize * 0.375f - minSize * 0.04f, Math.PI / 2 + Math.PI / 2);

            TheDraftsman.Change(minSize * 0.04f, Math.PI / 2 - Math.PI);
            TheDraftsman.Change(minSize * 0.04f * Math.Sqrt(2), Math.PI / 2 + 3 * Math.PI / 4);
        }

        private static void DrawThirdSide(int minSize)
        {
            TheDraftsman.MakeIt(new Pen(Brushes.Yellow), minSize * 0.375f, Math.PI);
            TheDraftsman.MakeIt(new Pen(Brushes.Yellow), minSize * 0.04f * Math.Sqrt(2), Math.PI + Math.PI / 4);
            TheDraftsman.MakeIt(new Pen(Brushes.Yellow), minSize * 0.375f, Math.PI + Math.PI);
            TheDraftsman.MakeIt(new Pen(Brushes.Yellow), minSize * 0.375f - minSize * 0.04f, Math.PI + Math.PI / 2);

            TheDraftsman.Change(minSize * 0.04f, Math.PI - Math.PI);
            TheDraftsman.Change(minSize * 0.04f * Math.Sqrt(2), Math.PI + 3 * Math.PI / 4);
        }

        private static void DrawTheSecondSide(int minSize)
        {
            TheDraftsman.MakeIt(new Pen(Brushes.Yellow), minSize * 0.375f, -Math.PI / 2);
            TheDraftsman.MakeIt(new Pen(Brushes.Yellow), minSize * 0.04f * Math.Sqrt(2), -Math.PI / 2 + Math.PI / 4);
            TheDraftsman.MakeIt(new Pen(Brushes.Yellow), minSize * 0.375f, -Math.PI / 2 + Math.PI);
            TheDraftsman.MakeIt(new Pen(Brushes.Yellow), minSize * 0.375f - minSize * 0.04f,
                -Math.PI / 2 + Math.PI / 2);

            TheDraftsman.Change(minSize * 0.04f, -Math.PI / 2 - Math.PI);
            TheDraftsman.Change(minSize * 0.04f * Math.Sqrt(2), -Math.PI / 2 + 3 * Math.PI / 4);
        }

        private static int GetAngleOfRotation(int width, int height, IGraphics graphics)
        {
            TheDraftsman.Initialize(graphics);

            var minSize = Math.Min(width, height);

            var diagonalLength = Math.Sqrt(2) * (minSize * 0.375f + minSize * 0.04f) / 2;
            var x0 = (float)(diagonalLength * Math.Cos(Math.PI / 4 + Math.PI)) + width / 2f;
            var y0 = (float)(diagonalLength * Math.Sin(Math.PI / 4 + Math.PI)) + height / 2f;

            TheDraftsman.SetPosition(x0, y0);
            return minSize;
        }

        private static void DrawTheFirstSide(int minSize)
        {
            TheDraftsman.MakeIt(new Pen(Brushes.Yellow), minSize * 0.375f, 0);
            TheDraftsman.MakeIt(new Pen(Brushes.Yellow), minSize * 0.04f * Math.Sqrt(2), Math.PI / 4);
            TheDraftsman.MakeIt(new Pen(Brushes.Yellow), minSize * 0.375f, Math.PI);
            TheDraftsman.MakeIt(new Pen(Brushes.Yellow), minSize * 0.375f - minSize * 0.04f, Math.PI / 2);

            TheDraftsman.Change(minSize * 0.04f, -Math.PI);
            TheDraftsman.Change(minSize * 0.04f * Math.Sqrt(2), 3 * Math.PI / 4);
        }
    }
}