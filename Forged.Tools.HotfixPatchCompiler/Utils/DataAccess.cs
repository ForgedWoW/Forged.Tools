﻿// Copyright (c) Forged WoW LLC <https://github.com/ForgedWoW/ForgedCore>
// Licensed under GPL-3.0 license. See <https://github.com/ForgedWoW/ForgedCore/blob/master/LICENSE> for full information.

using Framework.Database;

namespace Forged.Tools.HotfixPatchCompiler.Utils
{
    public static class DataAccess
    {
        public const string INSERT_HOTFIX_DATA = "INSERT INTO `hotfix_data` (`Id`, `UniqueId`, `TableHash`, `RecordId`, `Status`, `VerifiedBuild`) VALUES (@0, @1, @2, @3, @4, @5);";

        static DataAccess()
        {
            
        }

        /// <summary>
        /// returns the first uint value. 0 if not found.
        /// </summary>
        /// <param name="stmt"></param>
        /// <returns></returns>
        public static uint GetHotfixValue(PreparedStatement stmt)
        {
            var result = DB.Hotfix.Query(stmt);

            if (result.IsEmpty())
                return 0;

            return result.Read<uint>(0);
        }

        public static List<T> GetHotfixValues<T>(string query)
        {
            var result = DB.Hotfix.Query(query);
            var ret = new List<T>();

            if (result.IsEmpty())
                return ret;

            ret.Add(result.Read<T>(0));

            while (!result.IsEmpty() && result.NextRow())
                ret.Add(result.Read<T>(0));

            return ret;
        }

        public static List<string> GetHotfixTables()
        {
            return GetHotfixValues<string>("SHOW TABLES;");
        }
    }
}
