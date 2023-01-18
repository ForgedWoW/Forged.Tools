using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trait_Editor.Models.DB2Records;

namespace Trait_Editor.Models
{
    public sealed class TraitNode
    {
        public TraitNodeRecord Data = new();
        public List<TraitNodeEntry> Entries = new();
        public List<TraitNodeGroup> Groups = new();
        public Dictionary<TraitNode, TraitEdgeType> ParentNodes = new();
        public List<TraitCondRecord> Conditions = new();
        public List<TraitCostRecord> Costs = new();
    }
}
