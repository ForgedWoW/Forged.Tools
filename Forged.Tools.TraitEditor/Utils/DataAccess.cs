using System.Collections;
using Game.DataStorage;
using Framework.Constants;
using Framework.Database;
using Forged.Tools.Shared.DataStorage;

namespace Forged.Tools.TraitEditor.Utils
{
    public class DataAccess : BaseDataAccess
    {
        public DB6Storage<SpellIconRecord> SpellIconStorage;

        public Dictionary<uint, List<SkillRaceClassInfoRecord>> SkillRaceClassInfoSorted { get; private set; } = new();
        public Dictionary<uint, SpellMiscRecord> SpellMiscBySpellID { get; private set; } = new();

        string _db2Path = string.Empty;
        BitSet _availableDb2Locales;

        public const string DB_CONNECTION_QUERY = "SELECT ID FROM trait_cond Limit 1;";

        public DataAccess()
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

        public void LoadStores()
        {
            if (!_availableDb2Locales[(int)Locale.enUS])
                return;

            CliDB.ChrSpecializationStorage = CliDB.ReadDB2<ChrSpecializationRecord>(_availableDb2Locales, _db2Path, Locale.enUS, "ChrSpecialization.db2", HotfixStatements.SEL_CHR_SPECIALIZATION, HotfixStatements.SEL_CHR_SPECIALIZATION_LOCALE);
            CliDB.SpecSetMemberStorage = CliDB.ReadDB2<SpecSetMemberRecord>(_availableDb2Locales, _db2Path, Locale.enUS, "SpecSetMember.db2", HotfixStatements.SEL_SPEC_SET_MEMBER);
            CliDB.SkillLineStorage = CliDB.ReadDB2<SkillLineRecord>(_availableDb2Locales, _db2Path, Locale.enUS, "SkillLine.db2", HotfixStatements.SEL_SKILL_LINE, HotfixStatements.SEL_SKILL_LINE_LOCALE);
            CliDB.SkillLineXTraitTreeStorage = CliDB.ReadDB2<SkillLineXTraitTreeRecord>(_availableDb2Locales, _db2Path, Locale.enUS, "SkillLineXTraitTree.db2", HotfixStatements.SEL_SKILL_LINE_X_TRAIT_TREE);
            CliDB.SkillRaceClassInfoStorage = CliDB.ReadDB2<SkillRaceClassInfoRecord>(_availableDb2Locales, _db2Path, Locale.enUS, "SkillRaceClassInfo.db2", HotfixStatements.SEL_SKILL_RACE_CLASS_INFO);
            CliDB.SpellNameStorage = CliDB.ReadDB2<SpellNameRecord>(_availableDb2Locales, _db2Path, Locale.enUS, "SpellName.db2", HotfixStatements.SEL_SPELL_NAME, HotfixStatements.SEL_SPELL_NAME_LOCALE);
            CliDB.TraitCondStorage = CliDB.ReadDB2<TraitCondRecord>(_availableDb2Locales, _db2Path, Locale.enUS, "TraitCond.db2", HotfixStatements.SEL_TRAIT_COND);
            CliDB.TraitCostStorage = CliDB.ReadDB2<TraitCostRecord>(_availableDb2Locales, _db2Path, Locale.enUS, "TraitCost.db2", HotfixStatements.SEL_TRAIT_COST);
            CliDB.TraitCurrencyStorage = CliDB.ReadDB2<TraitCurrencyRecord>(_availableDb2Locales, _db2Path, Locale.enUS, "TraitCurrency.db2", HotfixStatements.SEL_TRAIT_CURRENCY);
            CliDB.TraitCurrencySourceStorage = CliDB.ReadDB2<TraitCurrencySourceRecord>(_availableDb2Locales, _db2Path, Locale.enUS, "TraitCurrencySource.db2", HotfixStatements.SEL_TRAIT_CURRENCY_SOURCE); //, HotfixStatements.SEL_TRAIT_CURRENCY_SOURCE_LOCALE);
            CliDB.TraitDefinitionStorage = CliDB.ReadDB2<TraitDefinitionRecord>(_availableDb2Locales, _db2Path, Locale.enUS, "TraitDefinition.db2", HotfixStatements.SEL_TRAIT_DEFINITION); //, HotfixStatements.SEL_TRAIT_DEFINITION_LOCALE);
            CliDB.TraitDefinitionEffectPointsStorage = CliDB.ReadDB2<TraitDefinitionEffectPointsRecord>(_availableDb2Locales, _db2Path, Locale.enUS, "TraitDefinitionEffectPoints.db2", HotfixStatements.SEL_TRAIT_DEFINITION_EFFECT_POINTS);
            CliDB.TraitEdgeStorage = CliDB.ReadDB2<TraitEdgeRecord>(_availableDb2Locales, _db2Path, Locale.enUS, "TraitEdge.db2", HotfixStatements.SEL_TRAIT_EDGE);
            CliDB.TraitNodeStorage = CliDB.ReadDB2<TraitNodeRecord>(_availableDb2Locales, _db2Path, Locale.enUS, "TraitNode.db2", HotfixStatements.SEL_TRAIT_NODE);
            CliDB.TraitNodeEntryStorage = CliDB.ReadDB2<TraitNodeEntryRecord>(_availableDb2Locales, _db2Path, Locale.enUS, "TraitNodeEntry.db2", HotfixStatements.SEL_TRAIT_NODE_ENTRY);
            CliDB.TraitNodeEntryXTraitCondStorage = CliDB.ReadDB2<TraitNodeEntryXTraitCondRecord>(_availableDb2Locales, _db2Path, Locale.enUS, "TraitNodeEntryXTraitCond.db2", HotfixStatements.SEL_TRAIT_NODE_ENTRY_X_TRAIT_COND);
            CliDB.TraitNodeEntryXTraitCostStorage = CliDB.ReadDB2<TraitNodeEntryXTraitCostRecord>(_availableDb2Locales, _db2Path, Locale.enUS, "TraitNodeEntryXTraitCost.db2", HotfixStatements.SEL_TRAIT_NODE_ENTRY_X_TRAIT_COST);
            CliDB.TraitNodeGroupStorage = CliDB.ReadDB2<TraitNodeGroupRecord>(_availableDb2Locales, _db2Path, Locale.enUS, "TraitNodeGroup.db2", HotfixStatements.SEL_TRAIT_NODE_GROUP);
            CliDB.TraitNodeGroupXTraitCondStorage = CliDB.ReadDB2<TraitNodeGroupXTraitCondRecord>(_availableDb2Locales, _db2Path, Locale.enUS, "TraitNodeGroupXTraitCond.db2", HotfixStatements.SEL_TRAIT_NODE_GROUP_X_TRAIT_COND);
            CliDB.TraitNodeGroupXTraitCostStorage = CliDB.ReadDB2<TraitNodeGroupXTraitCostRecord>(_availableDb2Locales, _db2Path, Locale.enUS, "TraitNodeGroupXTraitCost.db2", HotfixStatements.SEL_TRAIT_NODE_GROUP_X_TRAIT_COST);
            CliDB.TraitNodeGroupXTraitNodeStorage = CliDB.ReadDB2<TraitNodeGroupXTraitNodeRecord>(_availableDb2Locales, _db2Path, Locale.enUS, "TraitNodeGroupXTraitNode.db2", HotfixStatements.SEL_TRAIT_NODE_GROUP_X_TRAIT_NODE);
            CliDB.TraitNodeXTraitCondStorage = CliDB.ReadDB2<TraitNodeXTraitCondRecord>(_availableDb2Locales, _db2Path, Locale.enUS, "TraitNodeXTraitCond.db2", HotfixStatements.SEL_TRAIT_NODE_X_TRAIT_COND);
            CliDB.TraitNodeXTraitCostStorage = CliDB.ReadDB2<TraitNodeXTraitCostRecord>(_availableDb2Locales, _db2Path, Locale.enUS, "TraitNodeXTraitCost.db2", HotfixStatements.SEL_TRAIT_NODE_X_TRAIT_COST);
            CliDB.TraitNodeXTraitNodeEntryStorage = CliDB.ReadDB2<TraitNodeXTraitNodeEntryRecord>(_availableDb2Locales, _db2Path, Locale.enUS, "TraitNodeXTraitNodeEntry.db2", HotfixStatements.SEL_TRAIT_NODE_X_TRAIT_NODE_ENTRY);
            CliDB.TraitTreeStorage = CliDB.ReadDB2<TraitTreeRecord>(_availableDb2Locales, _db2Path, Locale.enUS, "TraitTree.db2", HotfixStatements.SEL_TRAIT_TREE);
            CliDB.TraitTreeLoadoutStorage = CliDB.ReadDB2<TraitTreeLoadoutRecord>(_availableDb2Locales, _db2Path, Locale.enUS, "TraitTreeLoadout.db2", HotfixStatements.SEL_TRAIT_TREE_LOADOUT);
            CliDB.TraitTreeLoadoutEntryStorage = CliDB.ReadDB2<TraitTreeLoadoutEntryRecord>(_availableDb2Locales, _db2Path, Locale.enUS, "TraitTreeLoadoutEntry.db2", HotfixStatements.SEL_TRAIT_TREE_LOADOUT_ENTRY);
            CliDB.TraitTreeXTraitCostStorage = CliDB.ReadDB2<TraitTreeXTraitCostRecord>(_availableDb2Locales, _db2Path, Locale.enUS, "TraitTreeXTraitCost.db2", HotfixStatements.SEL_TRAIT_TREE_X_TRAIT_COST);
            CliDB.TraitTreeXTraitCurrencyStorage = CliDB.ReadDB2<TraitTreeXTraitCurrencyRecord>(_availableDb2Locales, _db2Path, Locale.enUS, "TraitTreeXTraitCurrency.db2", HotfixStatements.SEL_TRAIT_TREE_X_TRAIT_CURRENCY);

            CliDB.TraitSystemStorage = CliDB.ReadDB2<TraitSystemRecord>(_availableDb2Locales, _db2Path, Locale.enUS, "TraitSystem.db2", HotfixStatements.SEL_TRAIT_SYSTEM);

            foreach (var entry in CliDB.SkillRaceClassInfoStorage)
                if (CliDB.SkillLineStorage.ContainsKey(entry.Value.SkillID))
                {
                    if (!SkillRaceClassInfoSorted.ContainsKey(entry.Value.SkillID))
                        SkillRaceClassInfoSorted.Add(entry.Value.SkillID, new List<SkillRaceClassInfoRecord>());

                    SkillRaceClassInfoSorted[entry.Value.SkillID].Add(entry.Value);
                }

            // build and pre-order spell icons
            SpellIconStorage = new DB6Storage<SpellIconRecord>();
            var ordered = new List<SpellIconRecord>();
            foreach (string line in File.ReadLines("IconIDs.csv"))
            {
                var split = line.Split(',');

                if (uint.TryParse(split[0], out uint iconId))
                    ordered.Add(new SpellIconRecord() { Id = iconId, TextureFilename = split[1] });
            }

            ordered.Sort((id1, id2) => id1.Id.CompareTo(id2.Id));
            foreach (var item in ordered.OrderBy(i => i.Id))
                SpellIconStorage.Add(item.Id, item);

            var misc = CliDB.ReadDB2<SpellMiscRecord>(_availableDb2Locales, _db2Path, Locale.enUS, "SpellMisc.db2", HotfixStatements.SEL_SPELL_MISC);
            foreach (var miscrec in misc)
                SpellMiscBySpellID[miscrec.Value.SpellID] = miscrec.Value;
        }

        /// <summary>
        /// Adds the current build number as a new parameter before executing
        /// </summary>
        /// <param name="stmt"></param>
        public void UpdateHotfixDB(PreparedStatement stmt)
        {
            stmt.AddValue(stmt.Parameters.Count, Settings.Default.BuildNumber);
            DB.Hotfix.Execute(stmt);
        }

        public List<T> GetHotfixValues<T>(PreparedStatement stmt)
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
        public uint GetHotfixValue(PreparedStatement stmt)
        {
            var result = DB.Hotfix.Query(stmt);

            if (result.IsEmpty())
                return 0;

            return result.Read<uint>(0);
        }

        public Image GetIcon(int iconId)
        {
            if (SpellIconStorage.ContainsKey((uint)iconId))
                return SpellIconStorage[(uint)iconId].GetImage();

            return null;
        }

        public Image GetIcon(uint spellId)
        {
            if (SpellMiscBySpellID.ContainsKey(spellId) && SpellIconStorage.ContainsKey(SpellMiscBySpellID[spellId].SpellIconFileDataID))
                return SpellIconStorage[SpellMiscBySpellID[spellId].SpellIconFileDataID].GetImage();

            return null;
        }
    }
}
