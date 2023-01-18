using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trait_Editor.Models.DB2Records
{
    public sealed class TraitTreeRecord
    {
        public uint Id;
        public uint TraitSystemID;
        public int Unused1000_1;
        public uint FirstTraitNodeID;
        public uint PlayerConditionID;
        public int Flags;
        public float Unused1000_2;
        public float Unused1000_3;
    }
}
