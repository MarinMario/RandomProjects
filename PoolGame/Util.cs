using System;
using Raylib_cs;
using System.Numerics;

namespace PoolGame
{
    static class Util
    {
        public static float ToAngle(this Vector2 vector)
        {
            return MathF.Atan2(vector.Y, vector.X) * 57.295f;
        }

        public static Vector2 ToVector(this float angle)
        {
            angle /= 57.295f;
            return new Vector2(MathF.Sin(angle), MathF.Cos(angle));
        }

        public static Vector2 MoveTowards(this Vector2 vector, Vector2 target, float speed, float margin)
        {
            var result = vector;
            var dir = target != vector ? Vector2.Normalize(target - vector) * speed : Vector2.Zero;

            if (Math.Abs(target.X - vector.X) > margin)
                result.X += dir.X;
            if (Math.Abs(target.Y - vector.Y) > margin)
                result.Y += dir.Y;

            return result;
        }

        public static Vector2 SafeNormalize(this Vector2 vector)
        {
            if (vector != Vector2.Zero)
                return Vector2.Normalize(vector);
            return Vector2.Zero;
        }
    }
}
