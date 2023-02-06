using Framework.Constants;
using Game.DataStorage;

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
