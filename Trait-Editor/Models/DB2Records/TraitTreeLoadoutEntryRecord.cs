using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trait_Editor.Models.DB2Records
{
    public sealed class TraitTreeLoadoutEntryRecord
    {
        public uint Id;
        public int TraitTreeLoadoutID;
        public uint SelectedTraitNodeID;
        public uint SelectedTraitNodeEntryID;
        public int NumPoints;
        public int OrderIndex;
    }
}
