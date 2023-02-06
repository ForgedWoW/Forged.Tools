using Game.DataStorage;

namespace Forged.Tools.Shared.Traits
{
    public sealed class TraitNodeEntry
    {
        public TraitNodeEntryRecord Data = new();
        public List<TraitCondRecord> Conditions = new();
        public List<TraitCostRecord> Costs = new();
    }
}
