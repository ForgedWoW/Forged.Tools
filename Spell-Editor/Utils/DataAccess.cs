using Framework.Constants;
using Framework.Database;
using Framework.Dynamic;
using Game.DataStorage;
using Game.Entities;
using Game.Spells;
using Spell_Editor.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Spell_Editor.Utils
{
    public static class DataAccess
    {
        public static DB6Storage<SpellRecord> SpellStorage;
        public static DB6Storage<SpellIconRecord> SpellIconStorage;

        static string _db2Path = string.Empty;
        static BitSet _availableDb2Locales;

        public const string SELECT_SPELL_EFFECT_IDS = "SELECT ID FROM spell_effect;";
        public const string SELECT_LATEST_SPECIFIC_SPELL_LABEL = "SELECT `ID` FROM `spell_label` WHERE `LabelID` = @0 AND `SpellID` = @1 DESC LIMIT 1;";

        public const string UPDATE_SPELL = "REPLACE INTO `spell` (ID,NameSubtext,Description,AuraDescription,VerifiedBuild) VALUES (@0, @1, @2, @3, @4);";
        public const string UPDATE_SPELL_NAME = "REPLACE INTO `spell_name` (ID,Name,VerifiedBuild) VALUES (@0, @1, @2);";
        public const string UPDATE_SPELL_EFFECT = "REPLACE INTO `spell_effect` (ID,EffectAura,DifficultyID,EffectIndex,Effect,EffectAmplitude,EffectAttributes,EffectAuraPeriod,EffectBonusCoefficient,EffectChainAmplitude,EffectChainTargets,EffectItemType,EffectMechanic,EffectPointsPerResource,EffectPosFacing,EffectRealPointsPerLevel,EffectTriggerSpell,BonusCoefficientFromAP,PvpMultiplier,Coefficient,Variance,ResourceCoefficient,GroupSizeBasePointsCoefficient,EffectBasePoints,ScalingClass,EffectMiscValue1,EffectMiscValue2,EffectRadiusIndex1,EffectRadiusIndex2,EffectSpellClassMask1,EffectSpellClassMask2,EffectSpellClassMask3,EffectSpellClassMask4,ImplicitTarget1,ImplicitTarget2,SpellID,VerifiedBuild) VALUES (@0, @1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11, @12, @13, @14, @15, @16, @17, @18, @19, @20, @21, @22, @23, @24, @25, @26, @27, @28, @29, @30, @31, @32, @33, @34, @35, @36);";
        public const string UPDATE_SPELL_CATEGORY = "REPLACE INTO `spell_category` (ID,Name,Flags,UsesPerWeek,MaxCharges,ChargeRecoveryTime,TypeMask,VerifiedBuild) VALUES (@0, @1, @2, @3, @4, @5, @6, @7);";
        public const string UPDATE_SPELL_CLASS_OPTIONS = "REPLACE INTO `spell_class_options` (ID,SpellID,ModalNextSpell,SpellClassSet,SpellClassMask1,SpellClassMask2,SpellClassMask3,SpellClassMask4,VerifiedBuild) VALUES (@0, @1, @2, @3, @4, @5, @6, @7, @8);";
        public const string UPDATE_SPELL_MISC = "REPLACE INTO `spell_misc` (`ID`, `Attributes1`, `Attributes2`, `Attributes3`, `Attributes4`, `Attributes5`, `Attributes6`, `Attributes7`, `Attributes8`, `Attributes9`, `Attributes10`, `Attributes11`, `Attributes12`, `Attributes13`, `Attributes14`, `Attributes15`, `DifficultyID`, `CastingTimeIndex`, `DurationIndex`, `RangeIndex`, `SchoolMask`, `Speed`, `LaunchDelay`, `MinDuration`, `SpellIconFileDataID`, `ActiveIconFileDataID`, `ContentTuningID`, `ShowFutureSpellPlayerConditionID`, `SpellVisualScript`, `ActiveSpellVisualScript`, `SpellID`, `VerifiedBuild`) VALUES (@0, @1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11, @12, @13, @14, @15, @16, @17, @18, @19, @20, @21, @22, @23, @24, @25, @26, @27, @28, @29, @30, @31)";
        public const string UPDATE_SPELL_SCALING = "REPLACE INTO `spell_scaling` (`ID`, `SpellID`, `MinScalingLevel`, `MaxScalingLevel`, `ScalesFromItemLevel`, `VerifiedBuild`) VALUES (@0, @1, @2, @3, @4, @5);";
        public const string UPDATE_SPELL_AURA_OPTIONS = "REPLACE INTO `spell_aura_options` (`ID`, `DifficultyID`, `CumulativeAura`, `ProcCategoryRecovery`, `ProcChance`, `ProcCharges`, `SpellProcsPerMinuteID`, `ProcTypeMask1`, `ProcTypeMask2`, `SpellID`, `VerifiedBuild`) VALUES (@0, @1, @2, @3, @4, @5, @6, @7, @8, @9, @10);";
        public const string UPDATE_SPELL_AURA_PROCS_PER_MINUTE = "REPLACE INTO `spell_procs_per_minute` (`ID`, `BaseProcRate`, `Flags`, `VerifiedBuild`) VALUES(@0, @1, @2, @3);";
        public const string UPDATE_SPELL_AURA_RESTRICTIONS = "REPLACE INTO `spell_aura_restrictions` (`ID`, `DifficultyID`, `CasterAuraState`, `TargetAuraState`, `ExcludeCasterAuraState`, `ExcludeTargetAuraState`, `CasterAuraSpell`, `TargetAuraSpell`, `ExcludeCasterAuraSpell`, `ExcludeTargetAuraSpell`, `SpellID`, `VerifiedBuild`) VALUES(@0, @1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11);";
        public const string UPDATE_SPELL_CASTING_REQUIREMENTS = "REPLACE INTO `spell_casting_requirements` (`ID`, `SpellID`, `FacingCasterFlags`, `MinFactionID`, `MinReputation`, `RequiredAreasID`, `RequiredAuraVision`, `RequiresSpellFocus`, `VerifiedBuild`) VALUES (@0, @1, @2, @3, @4, @5, @6, @7, @8);";
        public const string UPDATE_SPELL_CATEGORIES = "REPLACE INTO `spell_categories` (`ID`, `DifficultyID`, `Category`, `DefenseType`, `DispelType`, `Mechanic`, `PreventionType`, `StartRecoveryCategory`, `ChargeCategory`, `SpellID`, `VerifiedBuild`) VALUES (@0, @1, @2, @3, @4, @5, @6, @7, @8, @9, @10);";
        public const string UPDATE_SPELL_COOLDOWNS = "REPLACE INTO `spell_cooldowns` (`ID`, `DifficultyID`, `CategoryRecoveryTime`, `RecoveryTime`, `StartRecoveryTime`, `SpellID`, `VerifiedBuild`) VALUES (@0, @1, @2, @3, @4, @5, @6);";
        public const string UPDATE_SPELL_EQUIPPED_ITEMS = "REPLACE INTO `spell_equipped_items` (`ID`, `SpellID`, `EquippedItemClass`, `EquippedItemInvTypes`, `EquippedItemSubclass`, `VerifiedBuild`) VALUES (@0, @1, @2, @3, @4, @5);";
        public const string UPDATE_SPELL_INTERRUPTS = "REPLACE INTO `spell_interrupts` (`ID`, `DifficultyID`, `InterruptFlags`, `AuraInterruptFlags1`, `AuraInterruptFlags2`, `ChannelInterruptFlags1`, `ChannelInterruptFlags2`, `SpellID`, `VerifiedBuild`) VALUES (@0, @1, @2, @3, @4, @5, @6, @7, @8);";
        public const string UPDATE_SPELL_LABEL = "REPLACE INTO `spell_label` (`ID`, `LabelID`, `SpellID`, `VerifiedBuild`) VALUES (@0, @1, @2, @3);";
        public const string UPDATE_SPELL_LEVELS = "REPLACE INTO `spell_levels` (`ID`, `DifficultyID`, `MaxLevel`, `MaxPassiveAuraLevel`, `BaseLevel`, `SpellLevel`, `SpellID`, `VerifiedBuild`) VALUES (@0, @1, @2, @3, @4, @5, @6, @7);";
        public const string UPDATE_SPELL_POWER = "REPLACE INTO `spell_power` (`ID`, `OrderIndex`, `ManaCost`, `ManaCostPerLevel`, `ManaPerSecond`, `PowerDisplayID`, `AltPowerBarID`, `PowerCostPct`, `PowerCostMaxPct`, `PowerPctPerSecond`, `PowerType`, `RequiredAuraSpellID`, `OptionalCost`, `SpellID`, `VerifiedBuild`) VALUES (@0, @1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11, @12, @13, @14);";
        public const string UPDATE_SPELL_REAGENTS = "REPLACE INTO `spell_reagents` (`ID`, `SpellID`, `Reagent1`, `Reagent2`, `Reagent3`, `Reagent4`, `Reagent5`, `Reagent6`, `Reagent7`, `Reagent8`, `ReagentCount1`, `ReagentCount2`, `ReagentCount3`, `ReagentCount4`, `ReagentCount5`, `ReagentCount6`, `ReagentCount7`, `ReagentCount8`, `VerifiedBuild`) VALUES (@0, @1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11, @12, @13, @14, @15, @16, @17, @18);";
        public const string UPDATE_SPELL_REAGENTS_CURRENCY = "REPLACE INTO `spell_reagents_currency` (`ID`, `SpellID`, `CurrencyTypesID`, `CurrencyCount`, `VerifiedBuild`) VALUES (@0, @1, @2, @3, @4);";
        public const string UPDATE_SPELL_SHAPESHIFT = "REPLACE INTO `spell_shapeshift` (`ID`, `SpellID`, `StanceBarOrder`, `ShapeshiftExclude1`, `ShapeshiftExclude2`, `ShapeshiftMask1`, `ShapeshiftMask2`, `VerifiedBuild`) VALUES (@0, @1, @2, @3, @4, @5, @6, @7);";
        public const string UPDATE_SPELL_TARGET_RESTRICTIONS = "REPLACE INTO `spell_target_restrictions` (`ID`, `DifficultyID`, `ConeDegrees`, `MaxTargets`, `MaxTargetLevel`, `TargetCreatureType`, `Targets`, `Width`, `SpellID`, `VerifiedBuild`) VALUES (@0, @1, @2, @3, @4, @5, @6, @7, @8, @9);";
        public const string UPDATE_SPELL_TOTEMS = "REPLACE INTO `spell_totems` (`ID`, `SpellID`, `RequiredTotemCategoryID1`, `RequiredTotemCategoryID2`, `Totem1`, `Totem2`, `VerifiedBuild`) VALUES (@0, @1, @2, @3, @4, @5, @6);";
        public const string UPDATE_SPELL_X_SPELL_VISUAL = "REPLACE INTO `spell_x_spell_visual` (`ID`, `DifficultyID`, `SpellVisualID`, `Probability`, `Priority`, `SpellIconFileID`, `ActiveIconFileID`, `ViewerUnitConditionID`, `ViewerPlayerConditionID`, `CasterUnitConditionID`, `CasterPlayerConditionID`, `SpellID`, `VerifiedBuild`) VALUES (@0, @1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11, @12);";

        public const string SELECT_IDS_BUILD_SPELL_EFFECTS = "SELECT `ID` FROM `spell_effect` WHERE `SpellID` = @0 AND `VerifiedBuild` = @1;";
        public const string SELECT_IDS_BUILD_SPELL_LABEL = "SELECT `ID` FROM `spell_label` WHERE `SpellID` = @0 AND `VerifiedBuild` = @1;";
        public const string SELECT_IDS_BUILD_SPELL_REAGENTS_CURRENCY = "SELECT `ID` FROM `spell_reagents_currency` WHERE `SpellID` = @0 AND `VerifiedBuild` = @1;";

        public const string DELETE_BUILD_SPELL_EFFECTS = "DELETE FROM `spell_effect` WHERE `SpellID` = @0 AND `VerifiedBuild` = @1;";
        public const string DELETE_BUILD_SPELL_LABEL = "DELETE FROM `spell_label` WHERE `SpellID` = @0 AND `VerifiedBuild` = @1;";
        public const string DELETE_BUILD_SPELL_REAGENTS_CURRENCY = "DELETE FROM `spell_reagents_currency` WHERE `SpellID` = @0 AND `VerifiedBuild` = @1;";

        public const string DELETE_SPELL = "DELETE FROM `spell` WHERE `ID` = @0 AND `VerifiedBuild` > 44730;";
        public const string DELETE_SPELL_NAME = "DELETE FROM `spell_name` WHERE `ID` = @0 AND `VerifiedBuild` > 44730;";
        public const string DELETE_SPELL_EFFECT = "DELETE FROM `spell_effect` WHERE `SpellID` = @0 AND `VerifiedBuild` > 44730;";
        public const string DELETE_SPELL_CLASS_OPTIONS = "DELETE FROM `spell_class_options` WHERE `SpellID` = @0 AND `VerifiedBuild` > 44730;";
        public const string DELETE_SPELL_MISC = "DELETE FROM `spell_misc` WHERE `SpellID` = @0 AND `VerifiedBuild` > 44730;";
        public const string DELETE_SPELL_SCALING = "DELETE FROM `spell_scaling` WHERE `SpellID` = @0 AND `VerifiedBuild` > 44730;";
        public const string DELETE_SPELL_AURA_OPTIONS = "DELETE FROM `spell_aura_options` WHERE `SpellID` = @0 AND `VerifiedBuild` > 44730;";
        public const string DELETE_SPELL_AURA_RESTRICTIONS = "DELETE FROM `spell_aura_restrictions` WHERE `SpellID` = @0 AND `VerifiedBuild` > 44730;";
        public const string DELETE_SPELL_CASTING_REQUIREMENTS = "DELETE FROM `spell_casting_requirements` WHERE `SpellID` = @0 AND `VerifiedBuild` > 44730;";
        public const string DELETE_SPELL_CATEGORIES = "DELETE FROM `spell_categories` WHERE `SpellID` = @0 AND `VerifiedBuild` > 44730;";
        public const string DELETE_SPELL_COOLDOWNS = "DELETE FROM `spell_cooldowns` WHERE `SpellID` = @0 AND `VerifiedBuild` > 44730;";
        public const string DELETE_SPELL_EQUIPPED_ITEMS = "DELETE FROM `spell_equipped_items` WHERE `SpellID` = @0 AND `VerifiedBuild` > 44730;";
        public const string DELETE_SPELL_INTERRUPTS = "DELETE FROM `spell_interrupts` WHERE `SpellID` = @0 AND `VerifiedBuild` > 44730;";
        public const string DELETE_SPELL_LABEL = "DELETE FROM `spell_label` WHERE `SpellID` = @0 AND `VerifiedBuild` > 44730;";
        public const string DELETE_SPELL_LEVELS = "DELETE FROM `spell_levels` WHERE `SpellID` = @0 AND `VerifiedBuild` > 44730;";
        public const string DELETE_SPELL_POWER = "DELETE FROM `spell_power` WHERE `SpellID` = @0 AND `VerifiedBuild` > 44730;";
        public const string DELETE_SPELL_REAGENTS = "DELETE FROM `spell_reagents` WHERE `SpellID` = @0 AND `VerifiedBuild` > 44730;";
        public const string DELETE_SPELL_REAGENTS_CURRENCY = "DELETE FROM `spell_reagents_currency` WHERE `SpellID` = @0 AND `VerifiedBuild` > 44730;";
        public const string DELETE_SPELL_SHAPESHIFT = "DELETE FROM `spell_shapeshift` WHERE `SpellID` = @0 AND `VerifiedBuild` > 44730;";
        public const string DELETE_SPELL_TARGET_RESTRICTIONS = "DELETE FROM `spell_target_restrictions` WHERE `SpellID` = @0 AND `VerifiedBuild` > 44730;";
        public const string DELETE_SPELL_TOTEMS = "DELETE FROM `spell_totems` WHERE `SpellID` = @0 AND `VerifiedBuild` > 44730;";
        public const string DELETE_SPELL_X_SPELL_VISUAL = "DELETE FROM `spell_x_spell_visual` WHERE `SpellID` = @0 AND `VerifiedBuild` > 44730;";

        public const string SELECT_LATEST_ID = "SELECT `ID` FROM `{0}` ORDER BY `ID` DESC LIMIT 1;";

        static DataAccess()
        {
            _db2Path = Settings.Default.DB2ParentDir.Replace("{FullSpellEditorPath}", System.Reflection.Assembly.GetEntryAssembly().Location.Replace("\\Spell-Editor.dll", "\\dbc"));
            
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

            var results = DB.Hotfix.Query("SELECT * FROM `spell`");

            if (results.IsNull())
                DB.Hotfix.ApplyFile("SpellTable.sql");


            CliDB.BattlePetSpeciesStorage = ReadDB2<BattlePetSpeciesRecord>("BattlePetSpecies.db2", HotfixStatements.SEL_BATTLE_PET_SPECIES, HotfixStatements.SEL_BATTLE_PET_SPECIES_LOCALE);
            CliDB.DifficultyStorage = ReadDB2<DifficultyRecord>("Difficulty.db2", HotfixStatements.SEL_DIFFICULTY, HotfixStatements.SEL_DIFFICULTY_LOCALE);
            CliDB.SpellNameStorage = ReadDB2<SpellNameRecord>("SpellName.db2", HotfixStatements.SEL_SPELL_NAME, HotfixStatements.SEL_SPELL_NAME_LOCALE);
            CliDB.SpellAuraOptionsStorage = ReadDB2<SpellAuraOptionsRecord>("SpellAuraOptions.db2", HotfixStatements.SEL_SPELL_AURA_OPTIONS);
            CliDB.SpellAuraRestrictionsStorage = ReadDB2<SpellAuraRestrictionsRecord>("SpellAuraRestrictions.db2", HotfixStatements.SEL_SPELL_AURA_RESTRICTIONS);
            CliDB.SpellCastTimesStorage = ReadDB2<SpellCastTimesRecord>("SpellCastTimes.db2", HotfixStatements.SEL_SPELL_CAST_TIMES);
            CliDB.SpellCastingRequirementsStorage = ReadDB2<SpellCastingRequirementsRecord>("SpellCastingRequirements.db2", HotfixStatements.SEL_SPELL_CASTING_REQUIREMENTS);
            CliDB.SpellCategoriesStorage = ReadDB2<SpellCategoriesRecord>("SpellCategories.db2", HotfixStatements.SEL_SPELL_CATEGORIES);
            CliDB.SpellCategoryStorage = ReadDB2<SpellCategoryRecord>("SpellCategory.db2", HotfixStatements.SEL_SPELL_CATEGORY, HotfixStatements.SEL_SPELL_CATEGORY_LOCALE);
            CliDB.SpellClassOptionsStorage = ReadDB2<SpellClassOptionsRecord>("SpellClassOptions.db2", HotfixStatements.SEL_SPELL_CLASS_OPTIONS);
            CliDB.SpellCooldownsStorage = ReadDB2<SpellCooldownsRecord>("SpellCooldowns.db2", HotfixStatements.SEL_SPELL_COOLDOWNS);
            CliDB.SpellDurationStorage = ReadDB2<SpellDurationRecord>("SpellDuration.db2", HotfixStatements.SEL_SPELL_DURATION);
            CliDB.SpellEffectStorage = ReadDB2<SpellEffectRecord>("SpellEffect.db2", HotfixStatements.SEL_SPELL_EFFECT);
            CliDB.SpellEquippedItemsStorage = ReadDB2<SpellEquippedItemsRecord>("SpellEquippedItems.db2", HotfixStatements.SEL_SPELL_EQUIPPED_ITEMS);
            CliDB.SpellFocusObjectStorage = ReadDB2<SpellFocusObjectRecord>("SpellFocusObject.db2", HotfixStatements.SEL_SPELL_FOCUS_OBJECT, HotfixStatements.SEL_SPELL_FOCUS_OBJECT_LOCALE);
            CliDB.SpellInterruptsStorage = ReadDB2<SpellInterruptsRecord>("SpellInterrupts.db2", HotfixStatements.SEL_SPELL_INTERRUPTS);
            CliDB.SpellItemEnchantmentStorage = ReadDB2<SpellItemEnchantmentRecord>("SpellItemEnchantment.db2", HotfixStatements.SEL_SPELL_ITEM_ENCHANTMENT, HotfixStatements.SEL_SPELL_ITEM_ENCHANTMENT_LOCALE);
            CliDB.SpellItemEnchantmentConditionStorage = ReadDB2<SpellItemEnchantmentConditionRecord>("SpellItemEnchantmentCondition.db2", HotfixStatements.SEL_SPELL_ITEM_ENCHANTMENT_CONDITION);
            CliDB.SpellLabelStorage = ReadDB2<SpellLabelRecord>("SpellLabel.db2", HotfixStatements.SEL_SPELL_LABEL);
            CliDB.SpellLearnSpellStorage = ReadDB2<SpellLearnSpellRecord>("SpellLearnSpell.db2", HotfixStatements.SEL_SPELL_LEARN_SPELL);
            CliDB.SpellLevelsStorage = ReadDB2<SpellLevelsRecord>("SpellLevels.db2", HotfixStatements.SEL_SPELL_LEVELS);
            CliDB.SpellMiscStorage = ReadDB2<SpellMiscRecord>("SpellMisc.db2", HotfixStatements.SEL_SPELL_MISC);
            CliDB.SpellPowerStorage = ReadDB2<SpellPowerRecord>("SpellPower.db2", HotfixStatements.SEL_SPELL_POWER);
            CliDB.SpellPowerDifficultyStorage = ReadDB2<SpellPowerDifficultyRecord>("SpellPowerDifficulty.db2", HotfixStatements.SEL_SPELL_POWER_DIFFICULTY);
            CliDB.SpellProcsPerMinuteStorage = ReadDB2<SpellProcsPerMinuteRecord>("SpellProcsPerMinute.db2", HotfixStatements.SEL_SPELL_PROCS_PER_MINUTE);
            CliDB.SpellProcsPerMinuteModStorage = ReadDB2<SpellProcsPerMinuteModRecord>("SpellProcsPerMinuteMod.db2", HotfixStatements.SEL_SPELL_PROCS_PER_MINUTE_MOD);
            CliDB.SpellRadiusStorage = ReadDB2<SpellRadiusRecord>("SpellRadius.db2", HotfixStatements.SEL_SPELL_RADIUS);
            CliDB.SpellRadiusStorage.Add(0, new SpellRadiusRecord() { Id = 0, Radius = 0, RadiusMin = 0, RadiusMax = 0, RadiusPerLevel = 0 });
            CliDB.SpellRangeStorage = ReadDB2<SpellRangeRecord>("SpellRange.db2", HotfixStatements.SEL_SPELL_RANGE, HotfixStatements.SEL_SPELL_RANGE_LOCALE);
            CliDB.SpellReagentsStorage = ReadDB2<SpellReagentsRecord>("SpellReagents.db2", HotfixStatements.SEL_SPELL_REAGENTS);
            CliDB.SpellReagentsCurrencyStorage = ReadDB2<SpellReagentsCurrencyRecord>("SpellReagentsCurrency.db2", HotfixStatements.SEL_SPELL_REAGENTS_CURRENCY);
            CliDB.SpellScalingStorage = ReadDB2<SpellScalingRecord>("SpellScaling.db2", HotfixStatements.SEL_SPELL_SCALING);
            CliDB.SpellShapeshiftStorage = ReadDB2<SpellShapeshiftRecord>("SpellShapeshift.db2", HotfixStatements.SEL_SPELL_SHAPESHIFT);
            CliDB.SpellShapeshiftFormStorage = ReadDB2<SpellShapeshiftFormRecord>("SpellShapeshiftForm.db2", HotfixStatements.SEL_SPELL_SHAPESHIFT_FORM, HotfixStatements.SEL_SPELL_SHAPESHIFT_FORM_LOCALE);
            CliDB.SpellTargetRestrictionsStorage = ReadDB2<SpellTargetRestrictionsRecord>("SpellTargetRestrictions.db2", HotfixStatements.SEL_SPELL_TARGET_RESTRICTIONS);
            CliDB.SpellTotemsStorage = ReadDB2<SpellTotemsRecord>("SpellTotems.db2", HotfixStatements.SEL_SPELL_TOTEMS);
            CliDB.SpellVisualStorage = ReadDB2<SpellVisualRecord>("SpellVisual.db2", HotfixStatements.SEL_SPELL_VISUAL);
            CliDB.SpellVisualEffectNameStorage = ReadDB2<SpellVisualEffectNameRecord>("SpellVisualEffectName.db2", HotfixStatements.SEL_SPELL_VISUAL_EFFECT_NAME);
            CliDB.SpellVisualMissileStorage = ReadDB2<SpellVisualMissileRecord>("SpellVisualMissile.db2", HotfixStatements.SEL_SPELL_VISUAL_MISSILE);
            CliDB.SpellVisualKitStorage = ReadDB2<SpellVisualKitRecord>("SpellVisualKit.db2", HotfixStatements.SEL_SPELL_VISUAL_KIT);
            CliDB.SpellXSpellVisualStorage = ReadDB2<SpellXSpellVisualRecord>("SpellXSpellVisual.db2", HotfixStatements.SEL_SPELL_X_SPELL_VISUAL);
            CliDB.SummonPropertiesStorage = ReadDB2<SummonPropertiesRecord>("SummonProperties.db2", HotfixStatements.SEL_SUMMON_PROPERTIES);
            CliDB.TotemCategoryStorage = ReadDB2<TotemCategoryRecord>("TotemCategory.db2", HotfixStatements.SEL_TOTEM_CATEGORY, HotfixStatements.SEL_TOTEM_CATEGORY_LOCALE);
            CliDB.CurrencyTypesStorage = ReadDB2<CurrencyTypesRecord>("CurrencyTypes.db2", HotfixStatements.SEL_CURRENCY_TYPES, HotfixStatements.SEL_CURRENCY_TYPES_LOCALE);
            SpellStorage = ReadDB2<SpellRecord>("Spell.db2", HotfixStatements.SEL_SPELL);

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

        public static FullSpellInfo GetSpellInfo(uint spellId)
        {
            var spellInfo = SpellManager.Instance.GetSpellInfo(spellId, Difficulty.None);

            return new(spellInfo, SpellStorage[spellId]);
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
            var results = DB.Hotfix.Query(SELECT_SPELL_EFFECT_IDS);
            while (results.NextRow())
                ret.Add(results.Read<T>(0));

            return ret;
        }

        public static List<uint> GetHotfixSpellEffectIDs()
        {
            var ret = new List<uint>();

            var results = DB.Hotfix.Query(SELECT_SPELL_EFFECT_IDS);
            while (results.NextRow())
                ret.Add(results.Read<uint>(0));

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

        public static uint GetLatestID(string tableName)
        {
            return GetHotfixValue(new PreparedStatement(string.Format(SELECT_LATEST_ID, tableName)));
        }
    }

    class SpellFamily : IEquatable<SpellFamily>
    {
        public FlagArray128 SpellFamilyFlags;
        public string SpellName;

        public SpellFamily(string name, FlagArray128 flags)
        {
            SpellName = name;
            SpellFamilyFlags = flags;
        }

        public bool Equals(SpellFamily? other)
        {
            return SpellName == other?.SpellName && SpellFamilyFlags.IsEqualTo(other?.SpellFamilyFlags);
        }
    }
}
