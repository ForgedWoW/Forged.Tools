// Copyright (c) Forged WoW LLC <https://github.com/ForgedWoW/ForgedCore>
// Licensed under GPL-3.0 license. See <https://github.com/ForgedWoW/ForgedCore/blob/master/LICENSE> for full information.

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
