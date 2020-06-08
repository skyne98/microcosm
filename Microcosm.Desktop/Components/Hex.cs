using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microcosm.Desktop.Components
{
    public class Hex
    {
        public Point Position { get; set; }
        public int Size { get; set; }

        public static Hex FromCubic(Vector3 position, int size)
        {
            var q = (int)position.X;
            var r = (int)position.Z;
            return new Hex() { Position = new Point(q, r), Size = size };
        }

        public Vector2 ToPixel()
        {
            var x = Size * (Math.Sqrt(3) * Position.X + Math.Sqrt(3) / 2f * Position.Y);
            var y = Size * (3d / 2d * Position.Y);

            return new Vector2((float)x, (float)y);
        }

        public Vector3 ToCubic()
        {
            var x = Position.X;
            var z = Position.Y;
            var y = -x - z;

            return new Vector3(x, y, z);
        }

        public float DistanceTo(Hex hex)
        {
            var myCube = ToCubic();
            var otherCube = hex.ToCubic();

            return (Math.Abs(myCube.X - otherCube.X) + Math.Abs(myCube.Y - otherCube.Y) + Math.Abs(myCube.Z - otherCube.Z)) / 2;
        }
    }
}
