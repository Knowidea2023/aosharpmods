using System;

namespace AOSharp.Common.GameData
{
    public struct IPoint
    {
        public int X;
        public int Y;

        public IPoint(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static readonly IPoint Zero = new IPoint(0, 0);

        public override string ToString()
        {
            return $"({X}, {Y})";
        }

        public Vector2 ToVector2()
        {
            return new Vector2(X, Y);
        }

        public Vector3 ToVector3()
        {
            return new Vector3(X, Y, 0);
        }
    }
}
