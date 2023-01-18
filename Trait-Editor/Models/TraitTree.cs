using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trait_Editor.Models.DB2Records;

namespace Trait_Editor.Models
{
    [Serializable]
    public sealed class TraitTree
    {
        public TraitTreeRecord Data = new();
        public List<TraitNode> Nodes = new();
        public List<TraitCostRecord> Costs = new();
        public List<TraitCurrencyRecord> Currencies = new();
        public TraitConfigType ConfigType = TraitConfigType.Invalid;
    }
}
