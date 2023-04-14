// Copyright (c) Forged WoW LLC <https://github.com/ForgedWoW/ForgedCore>
// Licensed under GPL-3.0 license. See <https://github.com/ForgedWoW/ForgedCore/blob/master/LICENSE> for full information.

using Framework.Constants;
using Framework.Database;
using Game.DataStorage;
using System.Collections;

namespace Forged.Tools.Shared.Utils
{
    public static class SharedHelpers
    {
        public static DB6Storage<T> ReadDB2<T>(BitSet availableDb2Locales, string db2Path, Locale defaultLocale, string fileName, HotfixStatements preparedStatement, HotfixStatements preparedStatementLocale = 0) where T : new()
        {
            Log.outInfo(LogFilter.Server, "Loading {0}", fileName);
            DB6Storage<T> storage = new();
            storage.LoadData($"{db2Path}/{defaultLocale}/{fileName}", fileName);
            storage.LoadHotfixData(availableDb2Locales, preparedStatement, preparedStatementLocale);

            Global.DB2Mgr.AddDB2(storage.GetTableHash(), storage);
            return storage;
        }

        public static string ToSql(this PreparedStatement stmnt)
        {
            var ret = stmnt.CommandText;

            foreach (var parameter in stmnt.Parameters)
            {
                if (parameter.Value.GetType() == typeof(string))
                    ret = ret.Replace(@"@" + parameter.Key.ToString(), $"`{parameter.Value.ToString()}`");
                else
                    ret = ret.Replace(@"@" + parameter.Key.ToString(), parameter.Value.ToString());
            }

            if (!ret.EndsWith(";"))
                ret += ";";

            return ret;
        }
    }
}
