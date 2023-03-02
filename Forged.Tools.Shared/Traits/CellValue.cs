// Copyright (c) Forged WoW LLC <https://github.com/ForgedWoW/ForgedCore>
// Licensed under GPL-3.0 license. See <https://github.com/ForgedWoW/ForgedCore/blob/master/LICENSE> for full information.

namespace Forged.Tools.Shared.Traits
{
    public sealed class CellValue
    {
        public string Display { get; set; } = string.Empty;
        public TraitNode TraitNode { get; set; }
        public uint SpellID { get; set; } = 0;

        public override string ToString()
        {
            return Display;
        }

        public void Clear()
        {
            Display = string.Empty;
            TraitNode = null;
            SpellID = 0;
        }
    }
}
