using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace XnaID
{
    public static class VecUtil
    {
        private static Vector2 vecZeroAng = new Vector2(0, 1);



        public static Vector2 GetNormUP() { return new Vector2(0, -1); }
        public static Vector2 GetNormLeft() { return new Vector2(-1, 0); }
        public static Vector2 GetNormRight() { return new Vector2( 1, 0); }
        public static Vector2 GetNormDown() { return new Vector2(0, 1); }

        public static Vector2 GetNormAng(float angle)
        {
            Vector2 v = GetNormDown();
            Rotate((double)angle, ref v);
            return v;
        }


        public static float GetAngle(ref Vector2 v)
        {
            return GetAngle(ref v, ref vecZeroAng);
        }


        public static float GetAngle(ref Vector2 v1, ref Vector2 v2)
        {
            float dotProd = Vector2.Dot(v1, v2);
            dotProd /= (v1.Length() * v2.Length());

            dotProd = (float)Math.Acos(dotProd);
            if (v1.X > 0)
                dotProd *= -1;

            return dotProd;
        }


        public static void Rotate(double angle, ref Vector2 v)
        {
            //Precalculations
            float sinAng = (float)Math.Sin(angle);
            float cosAng = (float)Math.Cos(angle);

            //direction
            v = new Vector2(
                v.X * cosAng - v.Y * sinAng,
                v.X * sinAng + v.Y * cosAng);
        }


        public static void SetRotation(double angle, ref Vector2 v)
        {
            float length = v.Length();

            v.X = 0;
            v.Y = length;

            Rotate(angle, ref v);
        }


        public static void SetRotationNormal(double angle, ref Vector2 v)
        {
            v.X = 0;
            v.Y = 1;

            Rotate(angle, ref v);
            v.Normalize();
        }



    }
}
