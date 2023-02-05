﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forged.Tools.Shared.Traits.DB2Records
{
    public sealed class TraitCondRecord
    {
        public uint Id;
        public int CondType;
        public uint TraitTreeID;
        public int GrantedRanks;
        public uint QuestID;
        public uint AchievementID;
        public int SpecSetID;
        public uint TraitNodeGroupID;
        public uint TraitNodeID;
        public uint TraitCurrencyID;
        public int SpentAmountRequired;
        public int Flags;
        public int RequiredLevel;
        public uint FreeSharedStringID;
        public uint SpendMoreSharedStringID;
    }
}