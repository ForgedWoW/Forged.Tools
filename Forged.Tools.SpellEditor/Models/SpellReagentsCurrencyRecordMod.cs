// Copyright (c) Forged WoW LLC <https://github.com/ForgedWoW/ForgedCore>
// Licensed under GPL-3.0 license. See <https://github.com/ForgedWoW/ForgedCore/blob/master/LICENSE> for full information.

namespace Forged.Tools.SpellEditor.Models
{
    public sealed class SpellReagentsCurrencyRecordMod
    {
        public uint Id;
        public int SpellID;
        public ushort CurrencyTypesID;
        public ushort CurrencyCount;
        public bool KeepRecord = true;
    }
}
