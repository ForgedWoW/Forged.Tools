using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Forged.Tools.Shared.Traits.DB2Records;

namespace Forged.Tools.Shared.Traits
{
    public sealed class TraitNodeEntry
    {
        public TraitNodeEntryRecord Data = new();
        public List<TraitCondRecord> Conditions = new();
        public List<TraitCostRecord> Costs = new();
    }
}
