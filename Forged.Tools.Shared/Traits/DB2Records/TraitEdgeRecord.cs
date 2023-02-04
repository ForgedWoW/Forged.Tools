using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forged.Tools.Shared.Traits.DB2Records
{
    public sealed class TraitEdgeRecord
    {
        public uint Id;
        public int VisualStyle;
        public uint LeftTraitNodeID;
        public uint RightTraitNodeID;
        public int Type;
    }
}
