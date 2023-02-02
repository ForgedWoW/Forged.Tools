using Game.DataStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trait_Editor.Models.DB2Records
{
    public sealed class TraitCurrencySourceRecord
    {
        public LocalizedString Requirement;
        public uint Id;
        public int TraitCurrencyID;
        public int Amount;
        public uint QuestID;
        public uint AchievementID;
        public int PlayerLevel;
        public uint TraitNodeEntryID;
        public int OrderIndex;
    }
}
