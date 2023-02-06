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
