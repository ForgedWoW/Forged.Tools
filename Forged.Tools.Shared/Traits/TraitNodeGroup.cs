using Game.DataStorage;

namespace Forged.Tools.Shared.Traits
{
    public sealed class TraitNodeGroup
    {
        public TraitNodeGroupRecord Data = new();
        public List<TraitCondRecord> Conditions = new();
        public List<TraitCostRecord> Costs = new();
        public List<TraitNode> Nodes = new();
    }
}
