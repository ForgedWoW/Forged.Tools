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

using Forged.Tools.Shared.DataStorage;
using Forged.Tools.Shared.Spells;
using Framework.Constants;
using Framework.Dynamic;
using Game.DataStorage;

namespace Forged.Tools.Shared.Entities
{
    public sealed class SpellManager : Singleton<SpellManager>
    {
        SpellManager()
        {
            
        }

        public List<uint> GetSpellsRequiringSpellBounds(uint spell_id)
        {
            return mSpellsReqSpell.LookupByKey(spell_id);
        }

        public bool IsSpellRequiringSpell(uint spellid, uint req_spellid)
        {
            var spellsRequiringSpell = GetSpellsRequiringSpellBounds(req_spellid);

            foreach (var spell in spellsRequiringSpell)
            {
                if (spell == spellid)
                    return true;
            }
            return false;
        }

        List<int> GetSpellGroupSpellMapBounds(SpellGroup group_id)
        {
            return mSpellGroupSpell.LookupByKey(group_id);
        }

        public void GetSetOfSpellsInSpellGroup(SpellGroup group_id, out List<int> foundSpells)
        {
            List<SpellGroup> usedGroups = new();
            GetSetOfSpellsInSpellGroup(group_id, out foundSpells, ref usedGroups);
        }

        void GetSetOfSpellsInSpellGroup(SpellGroup group_id, out List<int> foundSpells, ref List<SpellGroup> usedGroups)
        {
            foundSpells = new List<int>();
            if (usedGroups.Find(p => p == group_id) == 0)
                return;

            usedGroups.Add(group_id);

            var groupSpell = GetSpellGroupSpellMapBounds(group_id);
            foreach (var group in groupSpell)
            {
                if (group < 0)
                {
                    SpellGroup currGroup = (SpellGroup)Math.Abs(group);
                    GetSetOfSpellsInSpellGroup(currGroup, out foundSpells, ref usedGroups);
                }
                else
                {
                    foundSpells.Add(group);
                }
            }
        }

        public void UpsertSpellInfo(SpellInfo spellInfo)
        {
            mSpellInfoMap.Remove(spellInfo.Id);
            mSpellInfoMap.Add(spellInfo.Id, spellInfo);
        }

        public SpellInfo GetSpellInfo(uint spellId, Difficulty difficulty)
        {
            var list = mSpellInfoMap.LookupByKey(spellId);

            var index = list.FindIndex(spellInfo => spellInfo.Difficulty == difficulty);
            if (index != -1)
                return list[index];

            DifficultyRecord difficultyEntry = CliDB.DifficultyStorage.LookupByKey(difficulty);
            if (difficultyEntry != null)
            {
                do
                {
                    index = list.FindIndex(spellInfo => spellInfo.Difficulty == (Difficulty)difficultyEntry.FallbackDifficultyID);
                    if (index != -1)
                        return list[index];

                    difficultyEntry = CliDB.DifficultyStorage.LookupByKey(difficultyEntry.FallbackDifficultyID);
                } while (difficultyEntry != null);
            }

            return null;
        }

        List<SpellInfo> _GetSpellInfo(uint spellId)
        {
            return mSpellInfoMap.LookupByKey(spellId);
        }

        #region Loads
        public void LoadSpellInfoStore(BaseDataAccess dataAccess)
        {
            uint oldMSTime = Time.GetMSTime();

            mSpellInfoMap.Clear();
            var loadData = new Dictionary<(uint Id, Difficulty difficulty), SpellInfoLoadHelper>();

            Dictionary<uint, BattlePetSpeciesRecord> battlePetSpeciesByCreature = new();
            foreach (var battlePetSpecies in CliDB.BattlePetSpeciesStorage.Values)
                if (battlePetSpecies.CreatureID != 0)
                    battlePetSpeciesByCreature[battlePetSpecies.CreatureID] = battlePetSpecies;

            SpellInfoLoadHelper GetLoadHelper(uint spellId, uint difficulty)
            {
                var key = (spellId, (Difficulty)difficulty);
                if (!loadData.ContainsKey(key))
                    loadData[key] = new SpellInfoLoadHelper();

                return loadData[key];
            }

            foreach (var effect in CliDB.SpellEffectStorage.Values)
            {
                GetLoadHelper(effect.SpellID, effect.DifficultyID).Effects[effect.EffectIndex] = effect;
            }

            foreach (SpellAuraOptionsRecord auraOptions in CliDB.SpellAuraOptionsStorage.Values)
                GetLoadHelper(auraOptions.SpellID, auraOptions.DifficultyID).AuraOptions = auraOptions;

            foreach (SpellAuraRestrictionsRecord auraRestrictions in CliDB.SpellAuraRestrictionsStorage.Values)
                GetLoadHelper(auraRestrictions.SpellID, auraRestrictions.DifficultyID).AuraRestrictions = auraRestrictions;

            foreach (SpellCastingRequirementsRecord castingRequirements in CliDB.SpellCastingRequirementsStorage.Values)
                GetLoadHelper(castingRequirements.SpellID, 0).CastingRequirements = castingRequirements;

            foreach (SpellCategoriesRecord categories in CliDB.SpellCategoriesStorage.Values)
                GetLoadHelper(categories.SpellID, categories.DifficultyID).Categories = categories;

            foreach (SpellClassOptionsRecord classOptions in CliDB.SpellClassOptionsStorage.Values)
                GetLoadHelper(classOptions.SpellID, 0).ClassOptions = classOptions;

            foreach (SpellCooldownsRecord cooldowns in CliDB.SpellCooldownsStorage.Values)
                GetLoadHelper(cooldowns.SpellID, cooldowns.DifficultyID).Cooldowns = cooldowns;

            foreach (SpellEquippedItemsRecord equippedItems in CliDB.SpellEquippedItemsStorage.Values)
                GetLoadHelper(equippedItems.SpellID, 0).EquippedItems = equippedItems;

            foreach (SpellInterruptsRecord interrupts in CliDB.SpellInterruptsStorage.Values)
                GetLoadHelper(interrupts.SpellID, interrupts.DifficultyID).Interrupts = interrupts;

            foreach (SpellLabelRecord label in CliDB.SpellLabelStorage.Values)
                GetLoadHelper(label.SpellID, 0).Labels.Add(label);

            foreach (SpellLevelsRecord levels in CliDB.SpellLevelsStorage.Values)
                GetLoadHelper(levels.SpellID, levels.DifficultyID).Levels = levels;

            foreach (SpellMiscRecord misc in CliDB.SpellMiscStorage.Values)
                if (misc.DifficultyID == 0)
                    GetLoadHelper(misc.SpellID, misc.DifficultyID).Misc = misc;

            foreach (SpellPowerRecord power in CliDB.SpellPowerStorage.Values)
            {
                uint difficulty = 0;
                byte index = power.OrderIndex;

                SpellPowerDifficultyRecord powerDifficulty = CliDB.SpellPowerDifficultyStorage.LookupByKey(power.Id);
                if (powerDifficulty != null)
                {
                    difficulty = powerDifficulty.DifficultyID;
                    index = powerDifficulty.OrderIndex;
                }

                GetLoadHelper(power.SpellID, difficulty).Powers[index] = power;
            }

            foreach (SpellReagentsRecord reagents in CliDB.SpellReagentsStorage.Values)
                GetLoadHelper(reagents.SpellID, 0).Reagents = reagents;

            foreach (SpellReagentsCurrencyRecord reagentsCurrency in CliDB.SpellReagentsCurrencyStorage.Values)
                GetLoadHelper((uint)reagentsCurrency.SpellID, 0).ReagentsCurrency.Add(reagentsCurrency);

            foreach (SpellScalingRecord scaling in CliDB.SpellScalingStorage.Values)
                GetLoadHelper(scaling.SpellID, 0).Scaling = scaling;

            foreach (SpellShapeshiftRecord shapeshift in CliDB.SpellShapeshiftStorage.Values)
                GetLoadHelper(shapeshift.SpellID, 0).Shapeshift = shapeshift;

            foreach (SpellTargetRestrictionsRecord targetRestrictions in CliDB.SpellTargetRestrictionsStorage.Values)
                GetLoadHelper(targetRestrictions.SpellID, targetRestrictions.DifficultyID).TargetRestrictions = targetRestrictions;

            foreach (SpellTotemsRecord totems in CliDB.SpellTotemsStorage.Values)
                GetLoadHelper(totems.SpellID, 0).Totems = totems;

            foreach (var visual in CliDB.SpellXSpellVisualStorage.Values)
            {
                var visuals = GetLoadHelper(visual.SpellID, visual.DifficultyID).Visuals;
                visuals.Add(visual);
            }

            // sorted with unconditional visuals being last
            foreach (var data in loadData)
                data.Value.Visuals.Sort((left, right) => { return right.CasterPlayerConditionID.CompareTo(left.CasterPlayerConditionID); });

            foreach (var data in loadData)
            {
                SpellNameRecord spellNameEntry = CliDB.SpellNameStorage.LookupByKey(data.Key.Id);
                if (spellNameEntry == null)
                    continue;

                // fill blanks
                DifficultyRecord difficultyEntry = CliDB.DifficultyStorage.LookupByKey(data.Key.difficulty);
                if (difficultyEntry != null)
                {
                    do
                    {
                        SpellInfoLoadHelper fallbackData = loadData.LookupByKey((data.Key.Id, (Difficulty)difficultyEntry.FallbackDifficultyID));
                        if (fallbackData != null)
                        {
                            if (data.Value.AuraOptions == null)
                                data.Value.AuraOptions = fallbackData.AuraOptions;

                            if (data.Value.AuraRestrictions == null)
                                data.Value.AuraRestrictions = fallbackData.AuraRestrictions;

                            if (data.Value.CastingRequirements == null)
                                data.Value.CastingRequirements = fallbackData.CastingRequirements;

                            if (data.Value.Categories == null)
                                data.Value.Categories = fallbackData.Categories;

                            if (data.Value.ClassOptions == null)
                                data.Value.ClassOptions = fallbackData.ClassOptions;

                            if (data.Value.Cooldowns == null)
                                data.Value.Cooldowns = fallbackData.Cooldowns;

                            for (var i = 0; i < data.Value.Effects.Length; ++i)
                                if (data.Value.Effects[i] == null)
                                    data.Value.Effects[i] = fallbackData.Effects[i];

                            if (data.Value.EquippedItems == null)
                                data.Value.EquippedItems = fallbackData.EquippedItems;

                            if (data.Value.Interrupts == null)
                                data.Value.Interrupts = fallbackData.Interrupts;

                            if (data.Value.Labels.Empty())
                                data.Value.Labels = fallbackData.Labels;

                            if (data.Value.Levels == null)
                                data.Value.Levels = fallbackData.Levels;

                            if (data.Value.Misc == null)
                                data.Value.Misc = fallbackData.Misc;

                            for (var i = 0; i < fallbackData.Powers.Length; ++i)
                                if (data.Value.Powers[i] == null)
                                    data.Value.Powers[i] = fallbackData.Powers[i];

                            if (data.Value.Reagents == null)
                                data.Value.Reagents = fallbackData.Reagents;

                            if (data.Value.ReagentsCurrency.Empty())
                                data.Value.ReagentsCurrency = fallbackData.ReagentsCurrency;

                            if (data.Value.Scaling == null)
                                data.Value.Scaling = fallbackData.Scaling;

                            if (data.Value.Shapeshift == null)
                                data.Value.Shapeshift = fallbackData.Shapeshift;

                            if (data.Value.TargetRestrictions == null)
                                data.Value.TargetRestrictions = fallbackData.TargetRestrictions;

                            if (data.Value.Totems == null)
                                data.Value.Totems = fallbackData.Totems;

                            // visuals fall back only to first difficulty that defines any visual
                            // they do not stack all difficulties in fallback chain
                            if (data.Value.Visuals.Empty())
                                data.Value.Visuals = fallbackData.Visuals;
                        }

                        difficultyEntry = CliDB.DifficultyStorage.LookupByKey(difficultyEntry.FallbackDifficultyID);
                    } while (difficultyEntry != null);
                }

                //first key = id, difficulty
                //second key = id


                mSpellInfoMap.Add(spellNameEntry.Id, new SpellInfo(spellNameEntry, data.Key.difficulty, data.Value, dataAccess));
            }

            Log.outInfo(LogFilter.ServerLoading, "Loaded SpellInfo store in {0} ms", Time.GetMSTimeDiffToNow(oldMSTime));
        }

        void ApplySpellFix(int[] spellIds, Action<SpellInfo> fix)
        {
            foreach (uint spellId in spellIds)
            {
                var range = _GetSpellInfo(spellId);
                if (range == null)
                {
                    Log.outError(LogFilter.ServerLoading, $"Spell info correction specified for non-existing spell {spellId}");
                    continue;
                }

                foreach (SpellInfo spellInfo in range)
                    fix(spellInfo);
            }
        }

        void ApplySpellEffectFix(SpellInfo spellInfo, uint effectIndex, Action<SpellEffectInfo> fix)
        {
            if (spellInfo.GetEffects().Count <= effectIndex)
            {
                Log.outError(LogFilter.ServerLoading, $"Spell effect info correction specified for non-existing effect {effectIndex} of spell {spellInfo.Id}");
                return;
            }

            fix(spellInfo.GetEffect(effectIndex));
        }

        #endregion

        bool IsTriggerAura(AuraType type)
        {
            switch (type)
            {
                case AuraType.Dummy:
                case AuraType.PeriodicDummy:
                case AuraType.ModConfuse:
                case AuraType.ModThreat:
                case AuraType.ModStun:
                case AuraType.ModDamageDone:
                case AuraType.ModDamageTaken:
                case AuraType.ModResistance:
                case AuraType.ModStealth:
                case AuraType.ModFear:
                case AuraType.ModRoot:
                case AuraType.Transform:
                case AuraType.ReflectSpells:
                case AuraType.DamageImmunity:
                case AuraType.ProcTriggerSpell:
                case AuraType.ProcTriggerDamage:
                case AuraType.ModCastingSpeedNotStack:
                case AuraType.SchoolAbsorb:
                case AuraType.ModPowerCostSchoolPct:
                case AuraType.ModPowerCostSchool:
                case AuraType.ReflectSpellsSchool:
                case AuraType.MechanicImmunity:
                case AuraType.ModDamagePercentTaken:
                case AuraType.SpellMagnet:
                case AuraType.ModAttackPower:
                case AuraType.ModPowerRegenPercent:
                case AuraType.InterceptMeleeRangedAttacks:
                case AuraType.OverrideClassScripts:
                case AuraType.ModMechanicResistance:
                case AuraType.MeleeAttackPowerAttackerBonus:
                case AuraType.ModMeleeHaste:
                case AuraType.ModMeleeHaste3:
                case AuraType.ModAttackerMeleeHitChance:
                case AuraType.ProcTriggerSpellWithValue:
                case AuraType.ModSchoolMaskDamageFromCaster:
                case AuraType.ModSpellDamageFromCaster:
                case AuraType.AbilityIgnoreAurastate:
                case AuraType.ModInvisibility:
                case AuraType.ForceReaction:
                case AuraType.ModTaunt:
                case AuraType.ModDetaunt:
                case AuraType.ModDamagePercentDone:
                case AuraType.ModAttackPowerPct:
                case AuraType.ModHitChance:
                case AuraType.ModWeaponCritPercent:
                case AuraType.ModBlockPercent:
                case AuraType.ModRoot2:
                    return true;
            }
            return false;
        }
        bool IsAlwaysTriggeredAura(AuraType type)
        {
            switch (type)
            {
                case AuraType.OverrideClassScripts:
                case AuraType.ModStealth:
                case AuraType.ModConfuse:
                case AuraType.ModFear:
                case AuraType.ModRoot:
                case AuraType.ModStun:
                case AuraType.Transform:
                case AuraType.ModInvisibility:
                case AuraType.SpellMagnet:
                case AuraType.SchoolAbsorb:
                case AuraType.ModRoot2:
                    return true;
            }
            return false;
        }
        ProcFlagsSpellType GetSpellTypeMask(AuraType type)
        {
            switch (type)
            {
                case AuraType.ModStealth:
                    return ProcFlagsSpellType.Damage | ProcFlagsSpellType.NoDmgHeal;
                case AuraType.ModConfuse:
                case AuraType.ModFear:
                case AuraType.ModRoot:
                case AuraType.ModRoot2:
                case AuraType.ModStun:
                case AuraType.Transform:
                case AuraType.ModInvisibility:
                    return ProcFlagsSpellType.Damage;
                default:
                    return ProcFlagsSpellType.MaskAll;
            }
        }

        // SpellInfo object management
        public bool HasSpellInfo(uint spellId, Difficulty difficulty)
        {
            var list = mSpellInfoMap.LookupByKey(spellId);
            if (list.Count == 0)
                return false;

            return list.Any(spellInfo => spellInfo.Difficulty == difficulty);
        }

        #region Fields
        MultiMap<uint, uint> mSpellsReqSpell = new();
        MultiMap<uint, uint> mSpellReq = new();
        Dictionary<KeyValuePair<uint, uint>, SpellTargetPosition> mSpellTargetPositions = new();
        MultiMap<uint, SpellGroup> mSpellSpellGroup = new();
        MultiMap<SpellGroup, int> mSpellGroupSpell = new();
        Dictionary<SpellGroup, SpellGroupStackRule> mSpellGroupStack = new();
        MultiMap<SpellGroup, AuraType> mSpellSameEffectStack = new();
        List<ServersideSpellName> mServersideSpellNames = new();
        Dictionary<(uint id, Difficulty difficulty), SpellProcEntry> mSpellProcMap = new();
        Dictionary<uint, SpellThreatEntry> mSpellThreatMap = new();
        Dictionary<uint, PetAura> mSpellPetAuraMap = new();
        MultiMap<int, int> mSpellLinkedMap = new();
        Dictionary<uint, SpellEnchantProcEntry> mSpellEnchantProcEventMap = new();
        MultiMap<uint, SpellArea> mSpellAreaMap = new();
        MultiMap<uint, SpellArea> mSpellAreaForQuestMap = new();
        MultiMap<uint, SpellArea> mSpellAreaForQuestEndMap = new();
        MultiMap<uint, SpellArea> mSpellAreaForAuraMap = new();
        MultiMap<uint, SpellArea> mSpellAreaForAreaMap = new();
        MultiMap<uint, SkillLineAbilityRecord> mSkillLineAbilityMap = new();
        Dictionary<uint, MultiMap<uint, uint>> mPetLevelupSpellMap = new();
        Dictionary<uint, PetDefaultSpellsEntry> mPetDefaultSpellsMap = new();           // only spells not listed in related mPetLevelupSpellMap entry
        MultiMap<uint, SpellInfo> mSpellInfoMap = new();
        Dictionary<Tuple<uint, byte>, uint> mSpellTotemModel = new();

        public MultiMap<uint, uint> PetFamilySpellsStorage = new();
        #endregion
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class AuraEffectHandlerAttribute : Attribute
    {
        public AuraEffectHandlerAttribute(AuraType type)
        {
            AuraType = type;
        }

        public AuraType AuraType { get; set; }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class SpellEffectHandlerAttribute : Attribute
    {
        public SpellEffectHandlerAttribute(SpellEffectName effectName)
        {
            EffectName = effectName;
        }

        public SpellEffectName EffectName { get; set; }
    }

    public class SpellInfoLoadHelper
    {
        public SpellAuraOptionsRecord AuraOptions;
        public SpellAuraRestrictionsRecord AuraRestrictions;
        public SpellCastingRequirementsRecord CastingRequirements;
        public SpellCategoriesRecord Categories;
        public SpellClassOptionsRecord ClassOptions;
        public SpellCooldownsRecord Cooldowns;
        public SpellEffectRecord[] Effects = new SpellEffectRecord[SpellConst.MaxEffects];
        public SpellEquippedItemsRecord EquippedItems;
        public SpellInterruptsRecord Interrupts;
        public List<SpellLabelRecord> Labels = new();
        public SpellLevelsRecord Levels;
        public SpellMiscRecord Misc;
        public SpellPowerRecord[] Powers = new SpellPowerRecord[SpellConst.MaxPowersPerSpell];
        public SpellReagentsRecord Reagents;
        public List<SpellReagentsCurrencyRecord> ReagentsCurrency = new();
        public SpellScalingRecord Scaling;
        public SpellShapeshiftRecord Shapeshift;
        public SpellTargetRestrictionsRecord TargetRestrictions;
        public SpellTotemsRecord Totems;
        public List<SpellXSpellVisualRecord> Visuals = new(); // only to group visuals when parsing sSpellXSpellVisualStore, not for loading
    }

    public class SpellThreatEntry
    {
        public int flatMod;                                    // flat threat-value for this Spell  - default: 0
        public float pctMod;                                     // threat-multiplier for this Spell  - default: 1.0f
        public float apPctMod;                                   // Pct of AP that is added as Threat - default: 0.0f
    }

    public class SpellProcEntry
    {
        public SpellSchoolMask SchoolMask { get; set; }                                 // if nonzero - bitmask for matching proc condition based on spell's school
        public SpellFamilyNames SpellFamilyName { get; set; }                            // if nonzero - for matching proc condition based on candidate spell's SpellFamilyName
        public FlagArray128 SpellFamilyMask { get; set; } = new(4);    // if nonzero - bitmask for matching proc condition based on candidate spell's SpellFamilyFlags
        public ProcFlagsInit ProcFlags { get; set; }                                   // if nonzero - owerwrite procFlags field for given Spell.dbc entry, bitmask for matching proc condition, see enum ProcFlags
        public ProcFlagsSpellType SpellTypeMask { get; set; }                              // if nonzero - bitmask for matching proc condition based on candidate spell's damage/heal effects, see enum ProcFlagsSpellType
        public ProcFlagsSpellPhase SpellPhaseMask { get; set; }                             // if nonzero - bitmask for matching phase of a spellcast on which proc occurs, see enum ProcFlagsSpellPhase
        public ProcFlagsHit HitMask { get; set; }                                    // if nonzero - bitmask for matching proc condition based on hit result, see enum ProcFlagsHit
        public ProcAttributes AttributesMask { get; set; }                             // bitmask, see ProcAttributes
        public uint DisableEffectsMask { get; set; }                            // bitmask
        public float ProcsPerMinute { get; set; }                              // if nonzero - chance to proc is equal to value * aura caster's weapon speed / 60
        public float Chance { get; set; }                                     // if nonzero - owerwrite procChance field for given Spell.dbc entry, defines chance of proc to occur, not used if ProcsPerMinute set
        public uint Cooldown { get; set; }                                   // if nonzero - cooldown in secs for aura proc, applied to aura
        public uint Charges { get; set; }                                   // if nonzero - owerwrite procCharges field for given Spell.dbc entry, defines how many times proc can occur before aura remove, 0 - infinite
    }

    struct ServersideSpellName
    {
        public SpellNameRecord Name;

        public ServersideSpellName(uint id, string name)
        {
            Name = new();
            Name.Name = new LocalizedString();

            Name.Id = id;
            for (Locale i = 0; i < Locale.Total; ++i)
                Name.Name[i] = name;
        }
    }


    public class PetDefaultSpellsEntry
    {
        public uint[] spellid = new uint[4];
    }

    public class SpellArea
    {
        public uint spellId;
        public uint areaId;                                         // zone/subzone/or 0 is not limited to zone
        public uint questStart;                                     // quest start (quest must be active or rewarded for spell apply)
        public uint questEnd;                                       // quest end (quest must not be rewarded for spell apply)
        public int auraSpell;                                       // spell aura must be applied for spell apply)if possitive) and it must not be applied in other case
        public ulong raceMask;                                      // can be applied only to races
        public Gender gender;                                       // can be applied only to gender
        public uint questStartStatus;                               // QuestStatus that quest_start must have in order to keep the spell
        public uint questEndStatus;                                 // QuestStatus that the quest_end must have in order to keep the spell (if the quest_end's status is different than this, the spell will be dropped)
        public SpellAreaFlag flags;                                 // if SPELL_AREA_FLAG_AUTOCAST then auto applied at area enter, in other case just allowed to cast || if SPELL_AREA_FLAG_AUTOREMOVE then auto removed inside area (will allways be removed on leaved even without flag)
    }

    public class PetAura
    {
        public PetAura()
        {
            removeOnChangePet = false;
            damage = 0;
        }

        public PetAura(uint petEntry, uint aura, bool _removeOnChangePet, int _damage)
        {
            removeOnChangePet = _removeOnChangePet;
            damage = _damage;

            auras[petEntry] = aura;
        }

        public uint GetAura(uint petEntry)
        {
            var auraId = auras.LookupByKey(petEntry);
            if (auraId != 0)
                return auraId;

            auraId = auras.LookupByKey(0);
            if (auraId != 0)
                return auraId;

            return 0;
        }

        public void AddAura(uint petEntry, uint aura)
        {
            auras[petEntry] = aura;
        }

        public bool IsRemovedOnChangePet()
        {
            return removeOnChangePet;
        }

        public int GetDamage()
        {
            return damage;
        }

        Dictionary<uint, uint> auras = new();
        bool removeOnChangePet;
        int damage;
    }

    public class SpellEnchantProcEntry
    {
        public float Chance;         // if nonzero - overwrite SpellItemEnchantment value
        public float ProcsPerMinute; // if nonzero - chance to proc is equal to value * aura caster's weapon speed / 60
        public uint HitMask;        // if nonzero - bitmask for matching proc condition based on hit result, see enum ProcFlagsHit
        public EnchantProcAttributes AttributesMask; // bitmask, see EnchantProcAttributes
    }

    public class SpellTargetPosition
    {
        public uint target_mapId;
        public float target_X;
        public float target_Y;
        public float target_Z;
        public float target_Orientation;
    }
}
