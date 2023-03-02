// Copyright (c) Forged WoW LLC <https://github.com/ForgedWoW/ForgedCore>
// Licensed under GPL-3.0 license. See <https://github.com/ForgedWoW/ForgedCore/blob/master/LICENSE> for full information.

namespace Forged.Tools.SpellEditor.Models
{
    public sealed class SpellXSpellVisualRecordMod
    {
        public uint Id;
        public byte DifficultyID;
        public uint SpellVisualID;
        public float Probability;
        public int Priority;
        public int SpellIconFileID;
        public int ActiveIconFileID;
        public ushort ViewerUnitConditionID;
        public uint ViewerPlayerConditionID;
        public ushort CasterUnitConditionID;
        public uint CasterPlayerConditionID;
        public uint SpellID;
        public bool KeepRecord = true;
    }
}
