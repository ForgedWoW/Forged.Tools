using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trait_Editor.Models
{
    public sealed class Coordinate : IEquatable<Coordinate>
    {
        public int X;
        public int Y;

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool Equals(Coordinate? other)
        {
            return X >= other?.X - 150 && X <= other?.X + 149
                && Y >= other?.Y - 150 && Y <= other?.Y + 149;
        }
    }
}
