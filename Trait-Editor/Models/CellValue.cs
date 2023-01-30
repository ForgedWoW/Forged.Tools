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
        public uint TraitNodeID { get; set; } = 0;
        public uint SpellID { get; set; } = 0;

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

        public void Clear()
        {
            Display = string.Empty;
            TraitNodeID = 0;
            SpellID = 0;
        }
    }
}
