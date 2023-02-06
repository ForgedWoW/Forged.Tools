using Framework.Constants;
using Game.DataStorage;

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
