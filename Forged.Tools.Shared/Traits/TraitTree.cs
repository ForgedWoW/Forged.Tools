using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Forged.Tools.Shared.Traits.DB2Records;

namespace Forged.Tools.Shared.Traits
{
    public sealed class TraitTree
    {
        public TraitTreeRecord Data = new();
        public List<TraitNode> Nodes = new();
        public List<TraitCostRecord> Costs = new();
        public List<TraitCurrencyRecord> Currencies = new();
        public TraitConfigType ConfigType = TraitConfigType.Invalid;
    }
}
