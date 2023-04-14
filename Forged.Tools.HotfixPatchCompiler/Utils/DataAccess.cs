// Copyright (c) Forged WoW LLC <https://github.com/ForgedWoW/ForgedCore>
// Licensed under GPL-3.0 license. See <https://github.com/ForgedWoW/ForgedCore/blob/master/LICENSE> for full information.

using Framework.Configuration;
using Framework.Database;
using Game.DataStorage;
using System.Text;
using static Game.AI.SmartAction;

namespace Forged.Tools.HotfixPatchCompiler.Utils
{
    public static class DataAccess
    {
        private const uint WDC3FmtSig = 0x33434457; // WDC3

        public const string INSERT_HOTFIX_DATA = "INSERT INTO `hotfix_data` (`Id`, `UniqueId`, `TableHash`, `RecordId`, `Status`, `VerifiedBuild`) VALUES (@0, @1, @2, @3, @4, @5);";
        public static readonly Dictionary<string, uint> TableHashes = new();

        static DataAccess()
        {
            string path = Directory.GetCurrentDirectory();
            string db2Path = ConfigMgr.GetDefaultValue("DataDir", Path.Combine(path, "Data"));

            if (!db2Path.EndsWith("\\dbc") || !db2Path.EndsWith("/dbc"))
                db2Path += "/dbc";

            foreach (var file in Directory.GetFiles(Path.Combine(db2Path, "enUS")))
            {
                if (!file.EndsWith(".db2", StringComparison.InvariantCultureIgnoreCase))
                    continue;

                using (var stream = new FileStream(file, FileMode.Open))
                {

                    using (var reader = new BinaryReader(stream, Encoding.UTF8))
                    {
                        var signature = reader.ReadUInt32();

                        if (signature != WDC3FmtSig)
                            continue;

                        var dump = reader.ReadUInt32(); // RecordCount
                        dump = reader.ReadUInt32();         // FieldCount
                        dump = reader.ReadUInt32();         // RecordSize
                        dump = reader.ReadUInt32();         // StringTableSize

                        var tableHash = reader.ReadUInt32();

                        TableHashes.Add(Path.GetFileNameWithoutExtension(file).ToLower(), tableHash);
                    }
                }
            }
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
