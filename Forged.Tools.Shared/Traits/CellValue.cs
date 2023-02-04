using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forged.Tools.Shared.Traits
{
    public sealed class CellValue
    {
        public string Display { get; set; } = string.Empty;
        public TraitNode TraitNode { get; set; }
        public uint SpellID { get; set; } = 0;

        public override string ToString()
        {
            return Display;
        }

        public void Clear()
        {
            Display = string.Empty;
            TraitNode = null;
            SpellID = 0;
        }
    }
}
