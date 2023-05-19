using System;

namespace AOSharp.Common.GameData
{
    public struct Vector2
    {
        public float X;
        public float Y;

        public Vector2(double x, double y)
        {
            X = (float)x;
            Y = (float)y;
        }

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static readonly Vector2 Zero = new Vector2(0, 0);

        public static Vector2 AngleToVector(float angle, float mag = 1f)
        {
            float rads = (float)(Math.PI * angle / 180);
            return new Vector2((float)(mag * Math.Sin(rads)), (float)(mag * Math.Sin(rads)));
        }

        public float DistanceFrom(Vector2 v)
        {
            return (float)Math.Sqrt(Math.Pow(Math.Abs(X - v.X), 2) + Math.Pow(Math.Abs(Y - v.Y), 2));
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }

        public Vector3 ToVector3()
        {
            return new Vector3(X, Y, 0);
        }
    }
}
