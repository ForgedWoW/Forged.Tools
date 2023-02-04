using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forged.Tools.Shared.Traits.DB2Records
{
    public sealed class TraitDefinitionEffectPointsRecord
    {
        public uint Id;
        public int TraitDefinitionID;
        public int EffectIndex;
        public int OperationType;
        public uint CurveID;
    }
}
