using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trait_Editor.Models.DB2Records;

namespace Trait_Editor.Models
{
    public sealed class TraitNodeGroup
    {
        public TraitNodeGroupRecord Data = new();
        public List<TraitCondRecord> Conditions = new();
        public List<TraitCostRecord> Costs = new();
        public List<TraitNode> Nodes = new();
    }
}
