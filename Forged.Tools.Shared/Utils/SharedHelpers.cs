// Copyright (c) Forged WoW LLC <https://github.com/ForgedWoW/ForgedCore>
// Licensed under GPL-3.0 license. See <https://github.com/ForgedWoW/ForgedCore/blob/master/LICENSE> for full information.

using Framework.Constants;
using Framework.Database;
using Game.DataStorage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forged.Tools.Shared.Utils
{
    public static class SharedHelpers
    {
        public static DB6Storage<T> ReadDB2<T>(BitSet availableDb2Locales, string db2Path, Locale defaultLocale, string fileName, HotfixStatements preparedStatement, HotfixStatements preparedStatementLocale = 0) where T : new()
        {
            DB6Storage<T> storage = new();
            storage.LoadData($"{db2Path}/{defaultLocale}/{fileName}", fileName);
            storage.LoadHotfixData(availableDb2Locales, preparedStatement, preparedStatementLocale);

            Global.DB2Mgr.AddDB2(storage.GetTableHash(), storage);
            return storage;
        }
    }
}
