using Forged.Tools.Shared.Constants;
using Forged.Tools.Shared.Database;
using Forged.Tools.Shared.Dynamic;
using Forged.Tools.Shared.DataStorage;
using Forged.Tools.Shared.Entities;
using Forged.Tools.Shared.Spells;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Forged.Tools.Shared.Traits.DB2Records;

namespace Forged.Tools.TraitEditor.Utils
{
    public static class DataAccess
    {
        public static Dictionary<uint, List<SkillRaceClassInfoRecord>> SkillRaceClassInfoSorted { get; private set; } = new();
        public static Dictionary<uint, SpellMiscRecord> SpellMiscBySpellID { get; private set; } = new();

        static string _db2Path = string.Empty;
        static BitSet _availableDb2Locales;

        public static string DB_CONNECTION_QUERY = "SELECT ID FROM trait_cond Limit 1;";

        static DataAccess()
        {
            _db2Path = Settings.Default.DB2ParentDir.Replace("{FullTraitEditorPath}", System.Reflection.Assembly.GetEntryAssembly().Location.Replace("\\Forged.Tools.TraitEditor.dll", "\\dbc"));
            
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

            CliDB.ChrSpecializationStorage = ReadDB2<ChrSpecializationRecord>("ChrSpecialization.db2", HotfixStatements.SEL_CHR_SPECIALIZATION, HotfixStatements.SEL_CHR_SPECIALIZATION_LOCALE);
            CliDB.SpecSetMemberStorage = ReadDB2<SpecSetMemberRecord>("SpecSetMember.db2", HotfixStatements.SEL_SPEC_SET_MEMBER);
            CliDB.SkillLineStorage = ReadDB2<SkillLineRecord>("SkillLine.db2", HotfixStatements.SEL_SKILL_LINE, HotfixStatements.SEL_SKILL_LINE_LOCALE);
            CliDB.SkillLineXTraitTreeStorage = ReadDB2<SkillLineXTraitTreeRecord>("SkillLineXTraitTree.db2", HotfixStatements.SEL_SKILL_LINE_X_TRAIT_TREE);
            CliDB.SkillRaceClassInfoStorage = ReadDB2<SkillRaceClassInfoRecord>("SkillRaceClassInfo.db2", HotfixStatements.SEL_SKILL_RACE_CLASS_INFO);
            CliDB.SpellNameStorage = ReadDB2<SpellNameRecord>("SpellName.db2", HotfixStatements.SEL_SPELL_NAME, HotfixStatements.SEL_SPELL_NAME_LOCALE);
            CliDB.TraitSystemStorage = ReadDB2<TraitSystemRecord>("TraitSystem.db2", HotfixStatements.SEL_TRAIT_COND);
            CliDB.TraitCondStorage = ReadDB2<TraitCondRecord>("TraitCond.db2", HotfixStatements.SEL_TRAIT_COND);
            CliDB.TraitCostStorage = ReadDB2<TraitCostRecord>("TraitCost.db2", HotfixStatements.SEL_TRAIT_COST);
            CliDB.TraitCurrencyStorage = ReadDB2<TraitCurrencyRecord>("TraitCurrency.db2", HotfixStatements.SEL_TRAIT_CURRENCY);
            CliDB.TraitCurrencySourceStorage = ReadDB2<TraitCurrencySourceRecord>("TraitCurrencySource.db2", HotfixStatements.SEL_TRAIT_CURRENCY_SOURCE); //, HotfixStatements.SEL_TRAIT_CURRENCY_SOURCE_LOCALE);
            CliDB.TraitDefinitionStorage = ReadDB2<TraitDefinitionRecord>("TraitDefinition.db2", HotfixStatements.SEL_TRAIT_DEFINITION); //, HotfixStatements.SEL_TRAIT_DEFINITION_LOCALE);
            CliDB.TraitDefinitionEffectPointsStorage = ReadDB2<TraitDefinitionEffectPointsRecord>("TraitDefinitionEffectPoints.db2", HotfixStatements.SEL_TRAIT_DEFINITION_EFFECT_POINTS);
            CliDB.TraitEdgeStorage = ReadDB2<TraitEdgeRecord>("TraitEdge.db2", HotfixStatements.SEL_TRAIT_EDGE);
            CliDB.TraitNodeStorage = ReadDB2<TraitNodeRecord>("TraitNode.db2", HotfixStatements.SEL_TRAIT_NODE);
            CliDB.TraitNodeEntryStorage = ReadDB2<TraitNodeEntryRecord>("TraitNodeEntry.db2", HotfixStatements.SEL_TRAIT_NODE_ENTRY);
            CliDB.TraitNodeEntryXTraitCondStorage = ReadDB2<TraitNodeEntryXTraitCondRecord>("TraitNodeEntryXTraitCond.db2", HotfixStatements.SEL_TRAIT_NODE_ENTRY_X_TRAIT_COND);
            CliDB.TraitNodeEntryXTraitCostStorage = ReadDB2<TraitNodeEntryXTraitCostRecord>("TraitNodeEntryXTraitCost.db2", HotfixStatements.SEL_TRAIT_NODE_ENTRY_X_TRAIT_COST);
            CliDB.TraitNodeGroupStorage = ReadDB2<TraitNodeGroupRecord>("TraitNodeGroup.db2", HotfixStatements.SEL_TRAIT_NODE_GROUP);
            CliDB.TraitNodeGroupXTraitCondStorage = ReadDB2<TraitNodeGroupXTraitCondRecord>("TraitNodeGroupXTraitCond.db2", HotfixStatements.SEL_TRAIT_NODE_GROUP_X_TRAIT_COND);
            CliDB.TraitNodeGroupXTraitCostStorage = ReadDB2<TraitNodeGroupXTraitCostRecord>("TraitNodeGroupXTraitCost.db2", HotfixStatements.SEL_TRAIT_NODE_GROUP_X_TRAIT_COST);
            CliDB.TraitNodeGroupXTraitNodeStorage = ReadDB2<TraitNodeGroupXTraitNodeRecord>("TraitNodeGroupXTraitNode.db2", HotfixStatements.SEL_TRAIT_NODE_GROUP_X_TRAIT_NODE);
            CliDB.TraitNodeXTraitCondStorage = ReadDB2<TraitNodeXTraitCondRecord>("TraitNodeXTraitCond.db2", HotfixStatements.SEL_TRAIT_NODE_X_TRAIT_COND);
            CliDB.TraitNodeXTraitCostStorage = ReadDB2<TraitNodeXTraitCostRecord>("TraitNodeXTraitCost.db2", HotfixStatements.SEL_TRAIT_NODE_X_TRAIT_COST);
            CliDB.TraitNodeXTraitNodeEntryStorage = ReadDB2<TraitNodeXTraitNodeEntryRecord>("TraitNodeXTraitNodeEntry.db2", HotfixStatements.SEL_TRAIT_NODE_X_TRAIT_NODE_ENTRY);
            CliDB.TraitTreeStorage = ReadDB2<TraitTreeRecord>("TraitTree.db2", HotfixStatements.SEL_TRAIT_TREE);
            CliDB.TraitTreeLoadoutStorage = ReadDB2<TraitTreeLoadoutRecord>("TraitTreeLoadout.db2", HotfixStatements.SEL_TRAIT_TREE_LOADOUT);
            CliDB.TraitTreeLoadoutEntryStorage = ReadDB2<TraitTreeLoadoutEntryRecord>("TraitTreeLoadoutEntry.db2", HotfixStatements.SEL_TRAIT_TREE_LOADOUT_ENTRY);
            CliDB.TraitTreeXTraitCostStorage = ReadDB2<TraitTreeXTraitCostRecord>("TraitTreeXTraitCost.db2", HotfixStatements.SEL_TRAIT_TREE_X_TRAIT_COST);
            CliDB.TraitTreeXTraitCurrencyStorage = ReadDB2<TraitTreeXTraitCurrencyRecord>("TraitTreeXTraitCurrency.db2", HotfixStatements.SEL_TRAIT_TREE_X_TRAIT_CURRENCY);

            foreach (var entry in CliDB.SkillRaceClassInfoStorage)
                if (CliDB.SkillLineStorage.ContainsKey(entry.Value.SkillID))
                {
                    if (!SkillRaceClassInfoSorted.ContainsKey(entry.Value.SkillID))
                        SkillRaceClassInfoSorted.Add(entry.Value.SkillID, new List<SkillRaceClassInfoRecord>());

                    SkillRaceClassInfoSorted[entry.Value.SkillID].Add(entry.Value);
                }

            // build and pre-order spell icons
            CliDB.SpellIconStorage = new DB6Storage<SpellIconRecord>();
            var ordered = new List<SpellIconRecord>();
            foreach (string line in File.ReadLines("IconIDs.csv"))
            {
                var split = line.Split(',');

                if (uint.TryParse(split[0], out uint iconId))
                    ordered.Add(new SpellIconRecord() { Id = iconId, TextureFilename = split[1] });
            }

            ordered.Sort((id1, id2) => id1.Id.CompareTo(id2.Id));
            foreach (var item in ordered.OrderBy(i => i.Id))
                CliDB.SpellIconStorage.Add(item.Id, item);

            var misc = ReadDB2<SpellMiscRecord>("SpellMisc.db2", HotfixStatements.SEL_SPELL_MISC);
            foreach (var miscrec in misc)
                SpellMiscBySpellID[miscrec.Value.SpellID] = miscrec.Value;
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

        public static Image GetIcon(int iconId)
        {
            if (CliDB.SpellIconStorage.ContainsKey((uint)iconId))
                return CliDB.SpellIconStorage[(uint)iconId].GetImage();

            return null;
        }

        public static Image GetIcon(uint spellId)
        {
            if (SpellMiscBySpellID.ContainsKey(spellId) && CliDB.SpellIconStorage.ContainsKey(SpellMiscBySpellID[spellId].SpellIconFileDataID))
                return CliDB.SpellIconStorage[SpellMiscBySpellID[spellId].SpellIconFileDataID].GetImage();

            return null;
        }
    }
}
