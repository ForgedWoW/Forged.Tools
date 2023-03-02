// Copyright (c) Forged WoW LLC <https://github.com/ForgedWoW/ForgedCore>
// Licensed under GPL-3.0 license. See <https://github.com/ForgedWoW/ForgedCore/blob/master/LICENSE> for full information.

using Game.DataStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forged.Tools.Shared.DataStorage
{
    public class BaseDataAccess
    {
        public MultiMap<uint, SpellProcsPerMinuteModRecord> SpellProcsPerMinuteMods = new();

        public BaseDataAccess() { }

        public void LoadStores()
        {

        }
    }
}
