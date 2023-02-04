using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Forged.Tools.Shared.Traits.DB2Records;

namespace Forged.Tools.Shared.Traits
{
    public sealed class TraitNode
    {
        public TraitNodeRecord Data = new();
        public List<TraitNodeEntry> Entries = new();
        public List<TraitNodeGroup> Groups = new();
        public List<TraitCondRecord> Conditions = new();
        public List<TraitCostRecord> Costs = new();
        public List<Tuple<TraitNode, TraitEdgeType>> ParentNodes = new();
    }
}
