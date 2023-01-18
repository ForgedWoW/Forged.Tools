﻿using Framework.Constants;
using Framework.Database;
using Framework.Dynamic;
using Game.DataStorage;
using Game.Entities;
using Game.Spells;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Trait_Editor.Models.DB2Records;

namespace Trait_Editor.Utils
{
    public static class DataAccess
    {
        public static DB6Storage<SkillLineRecord> SkillLineStorage;
        public static DB6Storage<SkillLineXTraitTreeRecord> SkillLineXTraitTreeStorage;
        public static DB6Storage<SkillRaceClassInfoRecord> SkillRaceClassInfoStorage;
        public static DB6Storage<TraitSystemRecord> TraitSystemStorage;
        public static DB6Storage<TraitCondRecord> TraitCondStorage;
        public static DB6Storage<TraitCostRecord> TraitCostStorage;
        public static DB6Storage<TraitCurrencyRecord> TraitCurrencyStorage;
        public static DB6Storage<TraitCurrencySourceRecord> TraitCurrencySourceStorage;
        public static DB6Storage<TraitCurrencySourceLocaleRecord> TraitCurrencySourceLocaleStorage;
        public static DB6Storage<TraitDefinitionRecord> TraitDefinitionStorage;
        public static DB6Storage<TraitDefinitionEffectPointsRecord> TraitDefinitionEffectPointsStorage;
        public static DB6Storage<TraitDefinitionLocaleRecord> TraitDefinitionLocaleStorage;
        public static DB6Storage<TraitEdgeRecord> TraitEdgeStorage;
        public static DB6Storage<TraitNodeRecord> TraitNodeStorage;
        public static DB6Storage<TraitNodeEntryRecord> TraitNodeEntryStorage;
        public static DB6Storage<TraitNodeEntryXTraitCondRecord> TraitNodeEntryXTraitCondStorage;
        public static DB6Storage<TraitNodeEntryXTraitCostRecord> TraitNodeEntryXTraitCostStorage;
        public static DB6Storage<TraitNodeGroupRecord> TraitNodeGroupStorage;
        public static DB6Storage<TraitNodeGroupXTraitCondRecord> TraitNodeGroupXTraitCondStorage;
        public static DB6Storage<TraitNodeGroupXTraitCostRecord> TraitNodeGroupXTraitCostStorage;
        public static DB6Storage<TraitNodeGroupXTraitNodeRecord> TraitNodeGroupXTraitNodeStorage;
        public static DB6Storage<TraitNodeXTraitCondRecord> TraitNodeXTraitCondStorage;
        public static DB6Storage<TraitNodeXTraitCostRecord> TraitNodeXTraitCostStorage;
        public static DB6Storage<TraitNodeXTraitNodeEntryRecord> TraitNodeXTraitNodeEntryStorage;
        public static DB6Storage<TraitTreeRecord> TraitTreeStorage;
        public static DB6Storage<TraitTreeLoadoutRecord> TraitTreeLoadoutStorage;
        public static DB6Storage<TraitTreeLoadoutEntryRecord> TraitTreeLoadoutEntryStorage;
        public static DB6Storage<TraitTreeXTraitCostRecord> TraitTreeXTraitCostStorage;
        public static DB6Storage<TraitTreeXTraitCurrencyRecord> TraitTreeXTraitCurrencyStorage;

        public static Dictionary<uint, List<SkillRaceClassInfoRecord>> SkillRaceClassInfoSorted;

        static string _db2Path = string.Empty;
        static BitSet _availableDb2Locales;

        public static string DB_CONNECTION_QUERY = "SELECT ID FROM trait_cond Limit 1;";

        static DataAccess()
        {
            _db2Path = Settings.Default.DB2ParentDir.Replace("{FullTraitEditorPath}", System.Reflection.Assembly.GetEntryAssembly().Location.Replace("\\Trait-Editor.dll", "\\dbc"));
            
            _availableDb2Locales = new((int)Locale.Total);
            foreach (var dir in Directory.GetDirectories(_db2Path).AsSpan())
            {
                Locale locale = Path.GetFileName(dir).ToEnum<Locale>();
                if (SharedConst.IsValidLocale(locale))
                    _availableDb2Locales[(int)locale] = true;
            }
        }

        public static void LoadStores()
        {
            if (!_availableDb2Locales[(int)Locale.enUS])
                return;

            SkillLineStorage = ReadDB2<SkillLineRecord>("SkillLine.db2", HotfixStatements.SEL_SKILL_LINE, HotfixStatements.SEL_SKILL_LINE_LOCALE);
            SkillLineXTraitTreeStorage = ReadDB2<SkillLineXTraitTreeRecord>("SkillLineXTraitTree.db2", HotfixStatements.SEL_SKILL_LINE_X_TRAIT_TREE);
            SkillRaceClassInfoStorage = ReadDB2<SkillRaceClassInfoRecord>("SkillRaceClassInfo.db2", HotfixStatements.SEL_SKILL_RACE_CLASS_INFO);
            TraitSystemStorage = ReadDB2<TraitSystemRecord>("TraitSystem.db2", HotfixStatements.SEL_TRAIT_COND);
            TraitCondStorage = ReadDB2<TraitCondRecord>("TraitCond.db2", HotfixStatements.SEL_TRAIT_COND);
            TraitCostStorage = ReadDB2<TraitCostRecord>("TraitCost.db2", HotfixStatements.SEL_TRAIT_COST);
            TraitCurrencyStorage = ReadDB2<TraitCurrencyRecord>("TraitCurrency.db2", HotfixStatements.SEL_TRAIT_CURRENCY);
            TraitCurrencySourceStorage = ReadDB2<TraitCurrencySourceRecord>("TraitCurrencySource.db2", HotfixStatements.SEL_TRAIT_CURRENCY_SOURCE); //, HotfixStatements.SEL_TRAIT_CURRENCY_SOURCE_LOCALE);
            TraitDefinitionStorage = ReadDB2<TraitDefinitionRecord>("TraitDefinition.db2", HotfixStatements.SEL_TRAIT_DEFINITION); //, HotfixStatements.SEL_TRAIT_DEFINITION_LOCALE);
            TraitDefinitionEffectPointsStorage = ReadDB2<TraitDefinitionEffectPointsRecord>("TraitDefinitionEffectPoints.db2", HotfixStatements.SEL_TRAIT_DEFINITION_EFFECT_POINTS);
            TraitEdgeStorage = ReadDB2<TraitEdgeRecord>("TraitEdge.db2", HotfixStatements.SEL_TRAIT_EDGE);
            TraitNodeStorage = ReadDB2<TraitNodeRecord>("TraitNode.db2", HotfixStatements.SEL_TRAIT_NODE);
            TraitNodeEntryStorage = ReadDB2<TraitNodeEntryRecord>("TraitNodeEntry.db2", HotfixStatements.SEL_TRAIT_NODE_ENTRY);
            TraitNodeEntryXTraitCondStorage = ReadDB2<TraitNodeEntryXTraitCondRecord>("TraitNodeEntryXTraitCond.db2", HotfixStatements.SEL_TRAIT_NODE_ENTRY_X_TRAIT_COND);
            TraitNodeEntryXTraitCostStorage = ReadDB2<TraitNodeEntryXTraitCostRecord>("TraitNodeEntryXTraitCost.db2", HotfixStatements.SEL_TRAIT_NODE_ENTRY_X_TRAIT_COST);
            TraitNodeGroupStorage = ReadDB2<TraitNodeGroupRecord>("TraitNodeGroup.db2", HotfixStatements.SEL_TRAIT_NODE_GROUP);
            TraitNodeGroupXTraitCondStorage = ReadDB2<TraitNodeGroupXTraitCondRecord>("TraitNodeGroupXTraitCond.db2", HotfixStatements.SEL_TRAIT_NODE_GROUP_X_TRAIT_COND);
            TraitNodeGroupXTraitCostStorage = ReadDB2<TraitNodeGroupXTraitCostRecord>("TraitNodeGroupXTraitCost.db2", HotfixStatements.SEL_TRAIT_NODE_GROUP_X_TRAIT_COST);
            TraitNodeGroupXTraitNodeStorage = ReadDB2<TraitNodeGroupXTraitNodeRecord>("TraitNodeGroupXTraitNode.db2", HotfixStatements.SEL_TRAIT_NODE_GROUP_X_TRAIT_NODE);
            TraitNodeXTraitCondStorage = ReadDB2<TraitNodeXTraitCondRecord>("TraitNodeXTraitCond.db2", HotfixStatements.SEL_TRAIT_NODE_X_TRAIT_COND);
            TraitNodeXTraitCostStorage = ReadDB2<TraitNodeXTraitCostRecord>("TraitNodeXTraitCost.db2", HotfixStatements.SEL_TRAIT_NODE_X_TRAIT_COST);
            TraitNodeXTraitNodeEntryStorage = ReadDB2<TraitNodeXTraitNodeEntryRecord>("TraitNodeXTraitNodeEntry.db2", HotfixStatements.SEL_TRAIT_NODE_X_TRAIT_NODE_ENTRY);
            TraitTreeStorage = ReadDB2<TraitTreeRecord>("TraitTree.db2", HotfixStatements.SEL_TRAIT_TREE);
            TraitTreeLoadoutStorage = ReadDB2<TraitTreeLoadoutRecord>("TraitTreeLoadout.db2", HotfixStatements.SEL_TRAIT_TREE_LOADOUT);
            TraitTreeLoadoutEntryStorage = ReadDB2<TraitTreeLoadoutEntryRecord>("TraitTreeLoadoutEntry.db2", HotfixStatements.SEL_TRAIT_TREE_LOADOUT_ENTRY);
            TraitTreeXTraitCostStorage = ReadDB2<TraitTreeXTraitCostRecord>("TraitTreeXTraitCost.db2", HotfixStatements.SEL_TRAIT_TREE_X_TRAIT_COST);
            TraitTreeXTraitCurrencyStorage = ReadDB2<TraitTreeXTraitCurrencyRecord>("TraitTreeXTraitCurrency.db2", HotfixStatements.SEL_TRAIT_TREE_X_TRAIT_CURRENCY);

            SkillRaceClassInfoSorted = new();
            foreach (var entry in SkillRaceClassInfoStorage)
                if (SkillLineStorage.ContainsKey(entry.Value.SkillID))
                {
                    if (!SkillRaceClassInfoSorted.ContainsKey(entry.Value.SkillID))
                        SkillRaceClassInfoSorted.Add(entry.Value.SkillID, new List<SkillRaceClassInfoRecord>());

                    SkillRaceClassInfoSorted[entry.Value.SkillID].Add(entry.Value);
                }
        }

        public static IOrderedEnumerable<KeyValuePair<uint, T>> ReadDB2Ordered<T>(string fileName, HotfixStatements preparedStatement, HotfixStatements preparedStatementLocale = 0) where T : new()
        {
            uint dummy = 0;
            return DBReader.Read<T>(_availableDb2Locales, $"{_db2Path}/enUS/", fileName, preparedStatement, preparedStatementLocale, ref dummy).OrderBy(x => x.Key);
        }

        public static DB6Storage<T> ReadDB2<T>(string fileName, HotfixStatements preparedStatement, HotfixStatements preparedStatementLocale = 0) where T : new()
        {
            uint dummy = 0;
            return DBReader.Read<T>(_availableDb2Locales, $"{_db2Path}/enUS/", fileName, preparedStatement, preparedStatementLocale, ref dummy);
        }

        /// <summary>
        /// Adds the current build number as a new parameter before executing
        /// </summary>
        /// <param name="stmt"></param>
        public static void UpdateHotfixDB(PreparedStatement stmt)
        {
            stmt.AddValue(stmt.Parameters.Count, Settings.Default.BuildNumber);
            DB.Hotfix.Execute(stmt);
        }

        public static List<T> GetHotfixValues<T>(PreparedStatement stmt)
        {
            var ret = new List<T>();

            stmt.AddValue(stmt.Parameters.Count, Settings.Default.BuildNumber);
            var results = DB.Hotfix.Query(stmt);
            while (results.NextRow())
                ret.Add(results.Read<T>(0));

            return ret;
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
    }
}
