using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trait_Editor.Models.DB2Records
{
    public sealed class TraitNodeEntryRecord
    {
        public uint Id;
        public uint TraitDefinitionID;
        public int MaxRanks;
        public short NodeEntryType;
    }
}
