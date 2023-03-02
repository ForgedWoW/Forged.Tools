// Copyright (c) Forged WoW LLC <https://github.com/ForgedWoW/ForgedCore>
// Licensed under GPL-3.0 license. See <https://github.com/ForgedWoW/ForgedCore/blob/master/LICENSE> for full information.

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
