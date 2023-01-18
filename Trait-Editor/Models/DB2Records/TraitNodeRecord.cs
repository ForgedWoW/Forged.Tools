using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trait_Editor.Models.DB2Records
{
    public sealed class TraitNodeRecord
    {
        public uint Id;
        public uint TraitTreeID;
        public int PosX;
        public int PosY;
        public short Type;
        public int Flags;
    }
}
