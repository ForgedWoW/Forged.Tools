using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trait_Editor.Models
{
    public sealed class CellValue
    {
        public Coordinate Coordinate { get; set; }
        public string Display { get; set; } = string.Empty;

        public bool CompareCoordinate(Coordinate other)
        {
            return this.Coordinate.Equals(other);
        }

        public bool CompareCoordinate(int x, int y)
        {
            return this.Coordinate.Equals(x, y);
        }

        public override string ToString()
        {
            return Display;
        }
    }
}
