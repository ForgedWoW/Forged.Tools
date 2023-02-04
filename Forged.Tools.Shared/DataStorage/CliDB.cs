/*
 * Copyright (C) 2012-2020 CypherCore <http://github.com/CypherCore>
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using Forged.Tools.Shared.Constants;
using Forged.Tools.Shared.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.IO;
using Forged.Tools.Shared.Traits.DB2Records;

namespace Forged.Tools.Shared.DataStorage
{
    public class CliDB
    {
        #region Main Collections
        public static DB6Storage<AchievementRecord> AchievementStorage;
        public static DB6Storage<AchievementCategoryRecord> AchievementCategoryStorage;
        public static DB6Storage<AdventureJournalRecord> AdventureJournalStorage;
        public static DB6Storage<AdventureMapPOIRecord> AdventureMapPOIStorage;
        public static DB6Storage<AnimationDataRecord> AnimationDataStorage;
        public static DB6Storage<AnimKitRecord> AnimKitStorage;
        public static DB6Storage<AreaGroupMemberRecord> AreaGroupMemberStorage;
        public static DB6Storage<AreaTableRecord> AreaTableStorage;
        public static DB6Storage<AreaTriggerRecord> AreaTriggerStorage;
        public static DB6Storage<ArmorLocationRecord> ArmorLocationStorage;
        public static DB6Storage<ArtifactRecord> ArtifactStorage;
        public static DB6Storage<ArtifactAppearanceRecord> ArtifactAppearanceStorage;
        public static DB6Storage<ArtifactAppearanceSetRecord> ArtifactAppearanceSetStorage;
        public static DB6Storage<ArtifactCategoryRecord> ArtifactCategoryStorage;
        public static DB6Storage<ArtifactPowerRecord> ArtifactPowerStorage;
        public static DB6Storage<ArtifactPowerLinkRecord> ArtifactPowerLinkStorage;
        public static DB6Storage<ArtifactPowerPickerRecord> ArtifactPowerPickerStorage;
        public static DB6Storage<ArtifactPowerRankRecord> ArtifactPowerRankStorage;
        //public static DB6Storage<ArtifactQuestXPRecord> ArtifactQuestXPStorage;
        public static DB6Storage<ArtifactTierRecord> ArtifactTierStorage;
        public static DB6Storage<ArtifactUnlockRecord> ArtifactUnlockStorage;
        public static DB6Storage<AuctionHouseRecord> AuctionHouseStorage;
        public static DB6Storage<AzeriteEmpoweredItemRecord> AzeriteEmpoweredItemStorage;
        public static DB6Storage<AzeriteEssenceRecord> AzeriteEssenceStorage;
        public static DB6Storage<AzeriteEssencePowerRecord> AzeriteEssencePowerStorage;
        public static DB6Storage<AzeriteItemRecord> AzeriteItemStorage;
        public static DB6Storage<AzeriteItemMilestonePowerRecord> AzeriteItemMilestonePowerStorage;
        public static DB6Storage<AzeriteKnowledgeMultiplierRecord> AzeriteKnowledgeMultiplierStorage;
        public static DB6Storage<AzeriteLevelInfoRecord> AzeriteLevelInfoStorage;
        public static DB6Storage<AzeritePowerRecord> AzeritePowerStorage;
        public static DB6Storage<AzeritePowerSetMemberRecord> AzeritePowerSetMemberStorage;
        public static DB6Storage<AzeriteTierUnlockRecord> AzeriteTierUnlockStorage;
        public static DB6Storage<AzeriteTierUnlockSetRecord> AzeriteTierUnlockSetStorage;
        public static DB6Storage<AzeriteUnlockMappingRecord> AzeriteUnlockMappingStorage;
        public static DB6Storage<BankBagSlotPricesRecord> BankBagSlotPricesStorage;
        public static DB6Storage<BannedAddonsRecord> BannedAddOnsStorage;
        public static DB6Storage<BarberShopStyleRecord> BarberShopStyleStorage;
        public static DB6Storage<BattlePetBreedQualityRecord> BattlePetBreedQualityStorage;
        public static DB6Storage<BattlePetBreedStateRecord> BattlePetBreedStateStorage;
        public static DB6Storage<BattlePetSpeciesRecord> BattlePetSpeciesStorage;
        public static DB6Storage<BattlePetSpeciesStateRecord> BattlePetSpeciesStateStorage;
        public static DB6Storage<BattlemasterListRecord> BattlemasterListStorage;
        public static DB6Storage<BroadcastTextRecord> BroadcastTextStorage;
        public static DB6Storage<BroadcastTextDurationRecord> BroadcastTextDurationStorage;
        public static DB6Storage<Cfg_RegionsRecord> CfgRegionsStorage;
        public static DB6Storage<CharTitlesRecord> CharTitlesStorage;
        public static DB6Storage<CharacterLoadoutRecord> CharacterLoadoutStorage;
        public static DB6Storage<CharacterLoadoutItemRecord> CharacterLoadoutItemStorage;
        public static DB6Storage<ChatChannelsRecord> ChatChannelsStorage;
        public static DB6Storage<ChrClassUIDisplayRecord> ChrClassUIDisplayStorage;
        public static DB6Storage<ChrClassesRecord> ChrClassesStorage;
        public static DB6Storage<ChrClassesXPowerTypesRecord> ChrClassesXPowerTypesStorage;
        public static DB6Storage<ChrCustomizationChoiceRecord> ChrCustomizationChoiceStorage;
        public static DB6Storage<ChrCustomizationDisplayInfoRecord> ChrCustomizationDisplayInfoStorage;
        public static DB6Storage<ChrCustomizationElementRecord> ChrCustomizationElementStorage;
        public static DB6Storage<ChrCustomizationReqRecord> ChrCustomizationReqStorage;
        public static DB6Storage<ChrCustomizationReqChoiceRecord> ChrCustomizationReqChoiceStorage;
        public static DB6Storage<ChrModelRecord> ChrModelStorage;
        public static DB6Storage<ChrRaceXChrModelRecord> ChrRaceXChrModelStorage;
        public static DB6Storage<ChrCustomizationOptionRecord> ChrCustomizationOptionStorage;
        public static DB6Storage<ChrRacesRecord> ChrRacesStorage;
        public static DB6Storage<ChrSpecializationRecord> ChrSpecializationStorage;
        public static DB6Storage<CinematicCameraRecord> CinematicCameraStorage;
        public static DB6Storage<CinematicSequencesRecord> CinematicSequencesStorage;
        public static DB6Storage<ContentTuningRecord> ContentTuningStorage;
        public static DB6Storage<ContentTuningXExpectedRecord> ContentTuningXExpectedStorage;
        public static DB6Storage<ConversationLineRecord> ConversationLineStorage;
        public static DB6Storage<CorruptionEffectsRecord> CorruptionEffectsStorage;
        public static DB6Storage<CreatureDisplayInfoRecord> CreatureDisplayInfoStorage;
        public static DB6Storage<CreatureDisplayInfoExtraRecord> CreatureDisplayInfoExtraStorage;
        public static DB6Storage<CreatureFamilyRecord> CreatureFamilyStorage;
        public static DB6Storage<CreatureModelDataRecord> CreatureModelDataStorage;
        public static DB6Storage<CreatureTypeRecord> CreatureTypeStorage;
        public static DB6Storage<CriteriaRecord> CriteriaStorage;
        public static DB6Storage<CriteriaTreeRecord> CriteriaTreeStorage;
        public static DB6Storage<CurrencyContainerRecord> CurrencyContainerStorage;
        public static DB6Storage<CurrencyTypesRecord> CurrencyTypesStorage;
        public static DB6Storage<CurveRecord> CurveStorage;
        public static DB6Storage<CurvePointRecord> CurvePointStorage;
        public static DB6Storage<DestructibleModelDataRecord> DestructibleModelDataStorage;
        public static DB6Storage<DifficultyRecord> DifficultyStorage;
        public static DB6Storage<DungeonEncounterRecord> DungeonEncounterStorage;
        public static DB6Storage<DurabilityCostsRecord> DurabilityCostsStorage;
        public static DB6Storage<DurabilityQualityRecord> DurabilityQualityStorage;
        public static DB6Storage<EmotesRecord> EmotesStorage;
        public static DB6Storage<EmotesTextRecord> EmotesTextStorage;
        public static DB6Storage<EmotesTextSoundRecord> EmotesTextSoundStorage;
        public static DB6Storage<ExpectedStatRecord> ExpectedStatStorage;
        public static DB6Storage<ExpectedStatModRecord> ExpectedStatModStorage;
        public static DB6Storage<FactionRecord> FactionStorage;
        public static DB6Storage<FactionTemplateRecord> FactionTemplateStorage;
        public static DB6Storage<FriendshipRepReactionRecord> FriendshipRepReactionStorage;
        public static DB6Storage<FriendshipReputationRecord> FriendshipReputationStorage;
        public static DB6Storage<GameObjectArtKitRecord> GameObjectArtKitStorage;
        public static DB6Storage<GameObjectDisplayInfoRecord> GameObjectDisplayInfoStorage;
        public static DB6Storage<GameObjectsRecord> GameObjectsStorage;
        public static DB6Storage<GarrAbilityRecord> GarrAbilityStorage;
        public static DB6Storage<GarrBuildingRecord> GarrBuildingStorage;
        public static DB6Storage<GarrBuildingPlotInstRecord> GarrBuildingPlotInstStorage;
        public static DB6Storage<GarrClassSpecRecord> GarrClassSpecStorage;
        public static DB6Storage<GarrFollowerRecord> GarrFollowerStorage;
        public static DB6Storage<GarrFollowerXAbilityRecord> GarrFollowerXAbilityStorage;
        public static DB6Storage<GarrMissionRecord> GarrMissionStorage;
        public static DB6Storage<GarrPlotBuildingRecord> GarrPlotBuildingStorage;
        public static DB6Storage<GarrPlotRecord> GarrPlotStorage;
        public static DB6Storage<GarrPlotInstanceRecord> GarrPlotInstanceStorage;
        public static DB6Storage<GarrSiteLevelRecord> GarrSiteLevelStorage;
        public static DB6Storage<GarrSiteLevelPlotInstRecord> GarrSiteLevelPlotInstStorage;
        public static DB6Storage<GemPropertiesRecord> GemPropertiesStorage;
        public static DB6Storage<GlobalCurveRecord> GlobalCurveStorage;
        public static DB6Storage<GlyphBindableSpellRecord> GlyphBindableSpellStorage;
        public static DB6Storage<GlyphPropertiesRecord> GlyphPropertiesStorage;
        public static DB6Storage<GlyphRequiredSpecRecord> GlyphRequiredSpecStorage;
        public static DB6Storage<GuildColorBackgroundRecord> GuildColorBackgroundStorage;
        public static DB6Storage<GuildColorBorderRecord> GuildColorBorderStorage;
        public static DB6Storage<GuildColorEmblemRecord> GuildColorEmblemStorage;
        public static DB6Storage<GuildPerkSpellsRecord> GuildPerkSpellsStorage;
        public static DB6Storage<HeirloomRecord> HeirloomStorage;
        public static DB6Storage<HolidaysRecord> HolidaysStorage;
        public static DB6Storage<ImportPriceArmorRecord> ImportPriceArmorStorage;
        public static DB6Storage<ImportPriceQualityRecord> ImportPriceQualityStorage;
        public static DB6Storage<ImportPriceShieldRecord> ImportPriceShieldStorage;
        public static DB6Storage<ImportPriceWeaponRecord> ImportPriceWeaponStorage;
        public static DB6Storage<ItemAppearanceRecord> ItemAppearanceStorage;
        public static DB6Storage<ItemArmorQualityRecord> ItemArmorQualityStorage;
        public static DB6Storage<ItemArmorShieldRecord> ItemArmorShieldStorage;
        public static DB6Storage<ItemArmorTotalRecord> ItemArmorTotalStorage;
        //public static DB6Storage<ItemBagFamilyRecord> ItemBagFamilyStorage;
        public static DB6Storage<ItemBonusRecord> ItemBonusStorage;
        public static DB6Storage<ItemBonusListLevelDeltaRecord> ItemBonusListLevelDeltaStorage;
        public static DB6Storage<ItemBonusTreeNodeRecord> ItemBonusTreeNodeStorage;
        public static DB6Storage<ItemClassRecord> ItemClassStorage;
        public static DB6Storage<ItemChildEquipmentRecord> ItemChildEquipmentStorage;
        public static DB6Storage<ItemCurrencyCostRecord> ItemCurrencyCostStorage;
        public static DB6Storage<ItemDamageRecord> ItemDamageAmmoStorage;
        public static DB6Storage<ItemDamageRecord> ItemDamageOneHandStorage;
        public static DB6Storage<ItemDamageRecord> ItemDamageOneHandCasterStorage;
        public static DB6Storage<ItemDamageRecord> ItemDamageTwoHandStorage;
        public static DB6Storage<ItemDamageRecord> ItemDamageTwoHandCasterStorage;
        public static DB6Storage<ItemDisenchantLootRecord> ItemDisenchantLootStorage;
        public static DB6Storage<ItemEffectRecord> ItemEffectStorage;
        public static DB6Storage<ItemRecord> ItemStorage;
        public static DB6Storage<ItemExtendedCostRecord> ItemExtendedCostStorage;
        public static DB6Storage<ItemLevelSelectorRecord> ItemLevelSelectorStorage;
        public static DB6Storage<ItemLevelSelectorQualityRecord> ItemLevelSelectorQualityStorage;
        public static DB6Storage<ItemLevelSelectorQualitySetRecord> ItemLevelSelectorQualitySetStorage;
        public static DB6Storage<ItemLimitCategoryRecord> ItemLimitCategoryStorage;
        public static DB6Storage<ItemLimitCategoryConditionRecord> ItemLimitCategoryConditionStorage;
        public static DB6Storage<ItemModifiedAppearanceRecord> ItemModifiedAppearanceStorage;
        public static DB6Storage<ItemModifiedAppearanceExtraRecord> ItemModifiedAppearanceExtraStorage;
        public static DB6Storage<ItemNameDescriptionRecord> ItemNameDescriptionStorage;
        public static DB6Storage<ItemPriceBaseRecord> ItemPriceBaseStorage;
        public static DB6Storage<ItemSearchNameRecord> ItemSearchNameStorage;
        public static DB6Storage<ItemSetRecord> ItemSetStorage;
        public static DB6Storage<ItemSetSpellRecord> ItemSetSpellStorage;
        public static DB6Storage<ItemSparseRecord> ItemSparseStorage;
        public static DB6Storage<ItemSpecRecord> ItemSpecStorage;
        public static DB6Storage<ItemSpecOverrideRecord> ItemSpecOverrideStorage;
        public static DB6Storage<ItemXBonusTreeRecord> ItemXBonusTreeStorage;
        public static DB6Storage<ItemXItemEffectRecord> ItemXItemEffectStorage;
        public static DB6Storage<JournalEncounterRecord> JournalEncounterStorage;
        public static DB6Storage<JournalEncounterSectionRecord> JournalEncounterSectionStorage;
        public static DB6Storage<JournalInstanceRecord> JournalInstanceStorage;
        public static DB6Storage<JournalTierRecord> JournalTierStorage;
        //public static DB6Storage<KeyChainRecord> KeyChainStorage;
        public static DB6Storage<KeystoneAffixRecord> KeystoneAffixStorage;
        public static DB6Storage<LanguageWordsRecord> LanguageWordsStorage;
        public static DB6Storage<LanguagesRecord> LanguagesStorage;
        public static DB6Storage<LFGDungeonsRecord> LFGDungeonsStorage;
        public static DB6Storage<LightRecord> LightStorage;
        public static DB6Storage<LiquidTypeRecord> LiquidTypeStorage;
        public static DB6Storage<LockRecord> LockStorage;
        public static DB6Storage<MailTemplateRecord> MailTemplateStorage;
        public static DB6Storage<MapRecord> MapStorage;
        public static DB6Storage<MapChallengeModeRecord> MapChallengeModeStorage;
        public static DB6Storage<MapDifficultyRecord> MapDifficultyStorage;
        public static DB6Storage<MapDifficultyXConditionRecord> MapDifficultyXConditionStorage;
        public static DB6Storage<MawPowerRecord> MawPowerStorage;
        public static DB6Storage<ModifierTreeRecord> ModifierTreeStorage;
        public static DB6Storage<MountCapabilityRecord> MountCapabilityStorage;
        public static DB6Storage<MountRecord> MountStorage;
        public static DB6Storage<MountTypeXCapabilityRecord> MountTypeXCapabilityStorage;
        public static DB6Storage<MountXDisplayRecord> MountXDisplayStorage;
        public static DB6Storage<MovieRecord> MovieStorage;
        public static DB6Storage<NameGenRecord> NameGenStorage;
        public static DB6Storage<NamesProfanityRecord> NamesProfanityStorage;
        public static DB6Storage<NamesReservedRecord> NamesReservedStorage;
        public static DB6Storage<NamesReservedLocaleRecord> NamesReservedLocaleStorage;
        public static DB6Storage<NumTalentsAtLevelRecord> NumTalentsAtLevelStorage;
        public static DB6Storage<OverrideSpellDataRecord> OverrideSpellDataStorage;
        public static DB6Storage<ParagonReputationRecord> ParagonReputationStorage;
        public static DB6Storage<PhaseRecord> PhaseStorage;
        public static DB6Storage<PhaseXPhaseGroupRecord> PhaseXPhaseGroupStorage;
        public static DB6Storage<PlayerConditionRecord> PlayerConditionStorage;
        public static DB6Storage<PowerDisplayRecord> PowerDisplayStorage;
        public static DB6Storage<PowerTypeRecord> PowerTypeStorage;
        public static DB6Storage<PrestigeLevelInfoRecord> PrestigeLevelInfoStorage;
        public static DB6Storage<PvpDifficultyRecord> PvpDifficultyStorage;
        public static DB6Storage<PvpItemRecord> PvpItemStorage;
        public static DB6Storage<PvpTalentRecord> PvpTalentStorage;
        public static DB6Storage<PvpTalentCategoryRecord> PvpTalentCategoryStorage;
        public static DB6Storage<PvpTalentSlotUnlockRecord> PvpTalentSlotUnlockStorage;
        public static DB6Storage<PvpTierRecord> PvpTierStorage;
        public static DB6Storage<QuestFactionRewardRecord> QuestFactionRewardStorage;
        public static DB6Storage<QuestInfoRecord> QuestInfoStorage;
        public static DB6Storage<QuestLineXQuestRecord> QuestLineXQuestStorage;
        public static DB6Storage<QuestMoneyRewardRecord> QuestMoneyRewardStorage;
        public static DB6Storage<QuestPackageItemRecord> QuestPackageItemStorage;
        public static DB6Storage<QuestSortRecord> QuestSortStorage;
        public static DB6Storage<QuestV2Record> QuestV2Storage;
        public static DB6Storage<QuestXPRecord> QuestXPStorage;
        public static DB6Storage<RandPropPointsRecord> RandPropPointsStorage;
        public static DB6Storage<RewardPackRecord> RewardPackStorage;
        public static DB6Storage<RewardPackXCurrencyTypeRecord> RewardPackXCurrencyTypeStorage;
        public static DB6Storage<RewardPackXItemRecord> RewardPackXItemStorage;
        public static DB6Storage<ScenarioRecord> ScenarioStorage;
        public static DB6Storage<ScenarioStepRecord> ScenarioStepStorage;
        public static DB6Storage<SceneScriptRecord> SceneScriptStorage;
        public static DB6Storage<SceneScriptGlobalTextRecord> SceneScriptGlobalTextStorage;
        public static DB6Storage<SceneScriptPackageRecord> SceneScriptPackageStorage;
        public static DB6Storage<SceneScriptTextRecord> SceneScriptTextStorage;
        public static DB6Storage<SkillLineRecord> SkillLineStorage;
        public static DB6Storage<SkillLineAbilityRecord> SkillLineAbilityStorage;
        public static DB6Storage<SkillRaceClassInfoRecord> SkillRaceClassInfoStorage;
        public static DB6Storage<SoulbindConduitRankRecord> SoulbindConduitRankStorage;
        public static DB6Storage<SoundKitRecord> SoundKitStorage;
        public static DB6Storage<SpecializationSpellsRecord> SpecializationSpellsStorage;
        public static DB6Storage<SpecSetMemberRecord> SpecSetMemberStorage;
        public static DB6Storage<SpellAuraOptionsRecord> SpellAuraOptionsStorage;
        public static DB6Storage<SpellAuraRestrictionsRecord> SpellAuraRestrictionsStorage;
        public static DB6Storage<SpellCastTimesRecord> SpellCastTimesStorage;
        public static DB6Storage<SpellCastingRequirementsRecord> SpellCastingRequirementsStorage;
        public static DB6Storage<SpellCategoriesRecord> SpellCategoriesStorage;
        public static DB6Storage<SpellCategoryRecord> SpellCategoryStorage;
        public static DB6Storage<SpellClassOptionsRecord> SpellClassOptionsStorage;
        public static DB6Storage<SpellCooldownsRecord> SpellCooldownsStorage;
        public static DB6Storage<SpellDurationRecord> SpellDurationStorage;
        public static DB6Storage<SpellEffectRecord> SpellEffectStorage;
        public static DB6Storage<SpellEquippedItemsRecord> SpellEquippedItemsStorage;
        public static DB6Storage<SpellFocusObjectRecord> SpellFocusObjectStorage;
        public static DB6Storage<SpellInterruptsRecord> SpellInterruptsStorage;
        public static DB6Storage<SpellItemEnchantmentRecord> SpellItemEnchantmentStorage;
        public static DB6Storage<SpellItemEnchantmentConditionRecord> SpellItemEnchantmentConditionStorage;
        public static DB6Storage<SpellLabelRecord> SpellLabelStorage;
        public static DB6Storage<SpellLearnSpellRecord> SpellLearnSpellStorage;
        public static DB6Storage<SpellLevelsRecord> SpellLevelsStorage;
        public static DB6Storage<SpellMiscRecord> SpellMiscStorage;
        public static DB6Storage<SpellNameRecord> SpellNameStorage;
        public static DB6Storage<SpellPowerRecord> SpellPowerStorage;
        public static DB6Storage<SpellPowerDifficultyRecord> SpellPowerDifficultyStorage;
        public static DB6Storage<SpellProcsPerMinuteRecord> SpellProcsPerMinuteStorage;
        public static DB6Storage<SpellProcsPerMinuteModRecord> SpellProcsPerMinuteModStorage;
        public static DB6Storage<SpellRadiusRecord> SpellRadiusStorage;
        public static DB6Storage<SpellRangeRecord> SpellRangeStorage;
        public static DB6Storage<SpellReagentsRecord> SpellReagentsStorage;
        public static DB6Storage<SpellReagentsCurrencyRecord> SpellReagentsCurrencyStorage;
        public static DB6Storage<SpellScalingRecord> SpellScalingStorage;
        public static DB6Storage<SpellShapeshiftRecord> SpellShapeshiftStorage;
        public static DB6Storage<SpellShapeshiftFormRecord> SpellShapeshiftFormStorage;
        public static DB6Storage<SpellTargetRestrictionsRecord> SpellTargetRestrictionsStorage;
        public static DB6Storage<SpellTotemsRecord> SpellTotemsStorage;
        public static DB6Storage<SpellVisualRecord> SpellVisualStorage;
        public static DB6Storage<SpellVisualEffectNameRecord> SpellVisualEffectNameStorage;
        public static DB6Storage<SpellVisualMissileRecord> SpellVisualMissileStorage;
        public static DB6Storage<SpellVisualKitRecord> SpellVisualKitStorage;
        public static DB6Storage<SpellXSpellVisualRecord> SpellXSpellVisualStorage;
        public static DB6Storage<SummonPropertiesRecord> SummonPropertiesStorage;
        public static DB6Storage<TactKeyRecord> TactKeyStorage;
        public static DB6Storage<TalentRecord> TalentStorage;
        public static DB6Storage<TaxiNodesRecord> TaxiNodesStorage;
        public static DB6Storage<TaxiPathRecord> TaxiPathStorage;
        public static DB6Storage<TaxiPathNodeRecord> TaxiPathNodeStorage;
        public static DB6Storage<TotemCategoryRecord> TotemCategoryStorage;
        public static DB6Storage<ToyRecord> ToyStorage;
        public static DB6Storage<TransmogHolidayRecord> TransmogHolidayStorage;
        public static DB6Storage<TransmogIllusionRecord> TransmogIllusionStorage;
        public static DB6Storage<TransmogSetRecord> TransmogSetStorage;
        public static DB6Storage<TransmogSetGroupRecord> TransmogSetGroupStorage;
        public static DB6Storage<TransmogSetItemRecord> TransmogSetItemStorage;
        public static DB6Storage<TransportAnimationRecord> TransportAnimationStorage;
        public static DB6Storage<TransportRotationRecord> TransportRotationStorage;
        public static DB6Storage<UiMapRecord> UiMapStorage;
        public static DB6Storage<UiMapAssignmentRecord> UiMapAssignmentStorage;
        public static DB6Storage<UiMapLinkRecord> UiMapLinkStorage;
        public static DB6Storage<UiMapXMapArtRecord> UiMapXMapArtStorage;
        public static DB6Storage<UISplashScreenRecord> UISplashScreenStorage;
        public static DB6Storage<UnitConditionRecord> UnitConditionStorage;
        public static DB6Storage<UnitPowerBarRecord> UnitPowerBarStorage;
        public static DB6Storage<VehicleRecord> VehicleStorage;
        public static DB6Storage<VehicleSeatRecord> VehicleSeatStorage;
        public static DB6Storage<WMOAreaTableRecord> WMOAreaTableStorage;
        public static DB6Storage<WorldEffectRecord> WorldEffectStorage;
        public static DB6Storage<WorldMapOverlayRecord> WorldMapOverlayStorage;
        public static DB6Storage<WorldStateExpressionRecord> WorldStateExpressionStorage;
        public static DB6Storage<SpellRecord> SpellStorage;
        public static DB6Storage<SpellIconRecord> SpellIconStorage;
        public static DB6Storage<SpellEmpowerRecord> SpellEmpowerStorage;
        public static DB6Storage<SpellEmpowerStageRecord> SpellEmpowerStageStorage;
        public static DB6Storage<SpellReplacementRecord> SpellReplacementStorage;
        public static DB6Storage<SkillLineXTraitTreeRecord> SkillLineXTraitTreeStorage;
        public static DB6Storage<TraitSystemRecord> TraitSystemStorage;
        public static DB6Storage<TraitCondRecord> TraitCondStorage;
        public static DB6Storage<TraitCostRecord> TraitCostStorage;
        public static DB6Storage<TraitCurrencyRecord> TraitCurrencyStorage;
        public static DB6Storage<TraitCurrencySourceRecord> TraitCurrencySourceStorage;
        public static DB6Storage<TraitDefinitionRecord> TraitDefinitionStorage;
        public static DB6Storage<TraitDefinitionEffectPointsRecord> TraitDefinitionEffectPointsStorage;
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
        #endregion

        #region GameTables
        public static GameTable<GtArtifactKnowledgeMultiplierRecord> ArtifactKnowledgeMultiplierGameTable;
        public static GameTable<GtArtifactLevelXPRecord> ArtifactLevelXPGameTable;
        public static GameTable<GtBarberShopCostBaseRecord> BarberShopCostBaseGameTable;
        public static GameTable<GtBaseMPRecord> BaseMPGameTable;
        public static GameTable<GtBattlePetXPRecord> BattlePetXPGameTable;
        public static GameTable<GtCombatRatingsRecord> CombatRatingsGameTable;
        public static GameTable<GtGenericMultByILvlRecord> CombatRatingsMultByILvlGameTable;
        public static GameTable<GtHpPerStaRecord> HpPerStaGameTable;
        public static GameTable<GtItemSocketCostPerLevelRecord> ItemSocketCostPerLevelGameTable;
        public static GameTable<GtNpcManaCostScalerRecord> NpcManaCostScalerGameTable;
        public static GameTable<GtSpellScalingRecord> SpellScalingGameTable;
        public static GameTable<GtGenericMultByILvlRecord> StaminaMultByILvlGameTable;
        public static GameTable<GtXpRecord> XpGameTable;
        public static GameTable<GtRegenHPPerSptEntry> HPRegenPerSpiritTable;
        public static GameTable<GtRegenMPPerSptEntry> MPRegenPerSpiritTable;
        public static GameTable<GtCombatRatingsEntry> RatioCombatRatingsTable;
        public static GameTable<GtChanceToMeleeCritBaseEntry> BaseMeleeCritChanceTable;
        public static GameTable<GtChanceToMeleeCritEntry> MeleeCritChanceTable;
        public static GameTable<GtChanceToSpellCritBaseEntry> BaseSpellCritChanceTable;
        public static GameTable<GtChanceToSpellCritEntry> SpellCritChanceTable;
        public static GameTable<GtOCTClassCombatRatingScalarEntry> OCTClassCombatRatingScalarTable;
        public static GameTable<GtOCTRegenHPEntry> OCTRegenHPTable;
        #endregion

        #region Taxi Collections
        public static byte[] TaxiNodesMask;
        public static byte[] OldContinentsNodesMask;
        public static byte[] HordeTaxiNodesMask;
        public static byte[] AllianceTaxiNodesMask;
        public static Dictionary<uint, Dictionary<uint, TaxiPathBySourceAndDestination>> TaxiPathSetBySource = new();
        public static Dictionary<uint, TaxiPathNodeRecord[]> TaxiPathNodesByPath = new();
        #endregion

        #region Helper Methods
        public static float GetGameTableColumnForClass(dynamic row, Class class_)
        {
            switch (class_)
            {
                case Class.Warrior:
                    return row.Warrior;
                case Class.Paladin:
                    return row.Paladin;
                case Class.Hunter:
                    return row.Hunter;
                case Class.Rogue:
                    return row.Rogue;
                case Class.Priest:
                    return row.Priest;
                case Class.Deathknight:
                    return row.DeathKnight;
                case Class.Shaman:
                    return row.Shaman;
                case Class.Mage:
                    return row.Mage;
                case Class.Warlock:
                    return row.Warlock;
                case Class.Monk:
                    return row.Monk;
                case Class.Druid:
                    return row.Druid;
                case Class.DemonHunter:
                    return row.DemonHunter;
                default:
                    break;
            }

            return 0.0f;
        }

        public static float GetSpellScalingColumnForClass(GtSpellScalingRecord row, int class_)
        {
            switch (class_)
            {
                case (int)Class.Warrior:
                    return row.Warrior;
                case (int)Class.Paladin:
                    return row.Paladin;
                case (int)Class.Hunter:
                    return row.Hunter;
                case (int)Class.Rogue:
                    return row.Rogue;
                case (int)Class.Priest:
                    return row.Priest;
                case (int)Class.Deathknight:
                    return row.DeathKnight;
                case (int)Class.Shaman:
                    return row.Shaman;
                case (int)Class.Mage:
                    return row.Mage;
                case (int)Class.Warlock:
                    return row.Warlock;
                case (int)Class.Monk:
                    return row.Monk;
                case (int)Class.Druid:
                    return row.Druid;
                case (int)Class.DemonHunter:
                    return row.DemonHunter;
                case -1:
                case -7:
                    return row.Item;
                case -2:
                    return row.Consumable;
                case -3:
                    return row.Gem1;
                case -4:
                    return row.Gem2;
                case -5:
                    return row.Gem3;
                case -6:
                    return row.Health;
                case -8:
                    return row.DamageReplaceStat;
                case -9:
                    return row.DamageSecondary;
                default:
                    break;
            }

            return 0.0f;
        }

        public static float GetBattlePetXPPerLevel(GtBattlePetXPRecord row)
        {
            return row.Wins * row.Xp;
        }
        
        public static float GetIlvlStatMultiplier(GtGenericMultByILvlRecord row, InventoryType invType)
        {
            switch (invType)
            {
                case InventoryType.Neck:
                case InventoryType.Finger:
                    return row.JewelryMultiplier;
                case InventoryType.Trinket:
                    return row.TrinketMultiplier;
                case InventoryType.Weapon:
                case InventoryType.Shield:
                case InventoryType.Ranged:
                case InventoryType.Weapon2Hand:
                case InventoryType.WeaponMainhand:
                case InventoryType.WeaponOffhand:
                case InventoryType.Holdable:
                case InventoryType.RangedRight:
                case InventoryType.Thrown:
                case InventoryType.Relic:
                    return row.WeaponMultiplier;
                default:
                    return row.ArmorMultiplier;
            }
        }

        public static List<SpellProcsPerMinuteModRecord> GetSpellProcsPerMinuteMods(uint ppmId)
        {
            List<SpellProcsPerMinuteModRecord> ret = new();

            foreach(var mod in SpellProcsPerMinuteModStorage)
                if (mod.Value.SpellProcsPerMinuteID == ppmId)
                    ret.Add(mod.Value);

            return ret;
        }
        #endregion
    }
}
