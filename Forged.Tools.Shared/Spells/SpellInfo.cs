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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Forged.Tools.Shared.DataStorage;
using Forged.Tools.Shared.Entities;
using Framework.Constants;
using Framework.Dynamic;
using Game.DataStorage;

namespace Forged.Tools.Shared.Spells
{
    public sealed class SpellInfo
    {
        public SpellInfo()
        {
            Id = 0;
            Difficulty = Difficulty.None;

            SpellName = new LocalizedString();
            SpellName[Locale.enUS] = string.Empty;

            Attributes = 0;
            AttributesEx = 0;
            AttributesEx2 = 0;
            AttributesEx3 = 0;
            AttributesEx4 = 0;
            AttributesEx5 = 0;
            AttributesEx6 = 0;
            AttributesEx7 = 0;
            AttributesEx8 = 0;
            AttributesEx9 = 0;
            AttributesEx10 = 0;
            AttributesEx11 = 0;
            AttributesEx12 = 0;
            AttributesEx13 = 0;
            AttributesEx14 = 0;
            CastTimeEntry = new SpellCastTimesRecord();
            DurationEntry = new SpellDurationRecord();
            RangeEntry = new SpellRangeRecord();
            Speed = 0;
            LaunchDelay = 0;
            SchoolMask = SpellSchoolMask.None;
            IconFileDataId = 0;
            ActiveIconFileDataId = 0;
            ContentTuningId = 0;
            ShowFutureSpellPlayerConditionID = 0;
            SpellMiscId = 0;

            // SpellScalingEntry
            Scaling.MinScalingLevel = 0;
            Scaling.MaxScalingLevel = 0;
            Scaling.ScalesFromItemLevel = 0;

            // SpellAuraOptionsEntry
            ProcFlags = new ProcFlagsInit(new int[] { 0, 0 });
            ProcChance = 0;
            ProcCharges = 0;
            ProcCooldown = 0;
            StackAmount = 0;
            ProcBasePPM = 0;

            // SpellAuraRestrictionsEntry
            CasterAuraState = AuraStateType.None;
            TargetAuraState = AuraStateType.None;
            ExcludeCasterAuraState = AuraStateType.None;
            ExcludeTargetAuraState = AuraStateType.None;
            CasterAuraSpell = 0;
            TargetAuraSpell = 0;
            ExcludeCasterAuraSpell = 0;
            ExcludeTargetAuraSpell = 0;

            // SpellCastingRequirementsEntry
            RequiresSpellFocus = 0;
            FacingCasterFlags = 0;
            RequiredAreasID = -1;
            SpellCastingRequirements = new SpellCastingRequirementsRecord();

            // SpellCategoriesEntry
            CategoryId = 0;
            Dispel = DispelType.None;
            Mechanic = Mechanics.None;
            StartRecoveryCategory = 0;
            DmgClass = SpellDmgClass.None;
            PreventionType = SpellPreventionType.None;
            ChargeCategoryId = 0;

            // SpellClassOptionsEntry
            ClassOptionsId = 0;
            ModalNextSpell = 0;
            SpellFamilyName = SpellFamilyNames.Generic;
            SpellFamilyFlags = new FlagArray128();

            // SpellCooldownsEntry
            RecoveryTime = 0;
            CategoryRecoveryTime = 0;
            StartRecoveryTime = 0;

            // SpellEquippedItemsEntry
            EquippedItemClass = ItemClass.None;
            EquippedItemSubClassMask = 0;
            EquippedItemInventoryTypeMask = 0;

            // SpellInterruptsEntry
            InterruptFlags = 0;
            AuraInterruptFlags = 0;
            AuraInterruptFlags2 = 0;
            ChannelInterruptFlags = 0;
            ChannelInterruptFlags2 = 0;

            // SpellLevelsEntry
            MaxLevel = 0;
            BaseLevel = 0;
            SpellLevel = 0;

            // SpellPowerEntry

            // SpellReagentsEntry

            // SpellShapeshiftEntry
            Stances = 0;
            StancesNot = 0;

            // SpellTargetRestrictionsEntry
            Targets = 0;
            ConeAngle = 0;
            Width = 0;
            TargetCreatureType = 0;
            MaxAffectedTargets = 0;
            MaxTargetLevel = 0;

            // SpellTotemsEntry

            // Visuals

            _spellSpecific = SpellSpecificType.Normal;
            _auraState = AuraStateType.None;
        }

        public SpellInfo(SpellNameRecord spellName, Difficulty difficulty, SpellInfoLoadHelper data, BaseDataAccess dataAccess)
        {
            Id = spellName.Id;
            Difficulty = difficulty;

            foreach (SpellEffectRecord spellEffect in data.Effects)
            {
                if (spellEffect == null)
                    continue;

                _effects.EnsureWritableListIndex(spellEffect.EffectIndex, new SpellEffectInfo(this));
                _effects[spellEffect.EffectIndex] = new SpellEffectInfo(this, spellEffect);
            }

            // Correct EffectIndex for blank effects
            for (int i = 0; i < _effects.Count; ++i)
                _effects[i].EffectIndex = (uint)i;

            SpellName = spellName.Name;

            SpellMiscRecord _misc = data.Misc;
            if (_misc != null)
            {
                SpellMiscId = _misc.Id;
                Attributes = (SpellAttr0)_misc.Attributes[0];
                AttributesEx = (SpellAttr1)_misc.Attributes[1];
                AttributesEx2 = (SpellAttr2)_misc.Attributes[2];
                AttributesEx3 = (SpellAttr3)_misc.Attributes[3];
                AttributesEx4 = (SpellAttr4)_misc.Attributes[4];
                AttributesEx5 = (SpellAttr5)_misc.Attributes[5];
                AttributesEx6 = (SpellAttr6)_misc.Attributes[6];
                AttributesEx7 = (SpellAttr7)_misc.Attributes[7];
                AttributesEx8 = (SpellAttr8)_misc.Attributes[8];
                AttributesEx9 = (SpellAttr9)_misc.Attributes[9];
                AttributesEx10 = (SpellAttr10)_misc.Attributes[10];
                AttributesEx11 = (SpellAttr11)_misc.Attributes[11];
                AttributesEx12 = (SpellAttr12)_misc.Attributes[12];
                AttributesEx13 = (SpellAttr13)_misc.Attributes[13];
                AttributesEx14 = (SpellAttr14)_misc.Attributes[14];
                CastTimeEntry = CliDB.SpellCastTimesStorage.LookupByKey(_misc.CastingTimeIndex);
                DurationEntry = CliDB.SpellDurationStorage.LookupByKey(_misc.DurationIndex);
                RangeEntry = CliDB.SpellRangeStorage.LookupByKey(_misc.RangeIndex);
                Speed = _misc.Speed;
                LaunchDelay = _misc.LaunchDelay;
                SchoolMask = (SpellSchoolMask)_misc.SchoolMask;
                IconFileDataId = _misc.SpellIconFileDataID;
                ActiveIconFileDataId = _misc.ActiveIconFileDataID;
                ContentTuningId = _misc.ContentTuningID;
                ShowFutureSpellPlayerConditionID = (uint)_misc.ShowFutureSpellPlayerConditionID;
            }

            // SpellScalingEntry
            SpellScalingRecord _scaling = data.Scaling;
            if (_scaling != null)
            {
                Scaling.Id = _scaling.Id;
                Scaling.MinScalingLevel = _scaling.MinScalingLevel;
                Scaling.MaxScalingLevel = _scaling.MaxScalingLevel;
                Scaling.ScalesFromItemLevel = _scaling.ScalesFromItemLevel;
            }

            // SpellAuraOptionsEntry
            SpellAuraOptionsRecord _options = data.AuraOptions;
            if (_options != null)
            {
                AuraOptionsId = _options.Id;
                ProcFlags = new ProcFlagsInit(_options.ProcTypeMask);
                ProcChance = _options.ProcChance;
                ProcCharges = (uint)_options.ProcCharges;
                ProcCooldown = _options.ProcCategoryRecovery;
                StackAmount = _options.CumulativeAura;
                SpellPPMId = _options.SpellProcsPerMinuteID;

                SpellProcsPerMinuteRecord _ppm = CliDB.SpellProcsPerMinuteStorage.LookupByKey(SpellPPMId);
                if (_ppm != null)
                {
                    ProcBasePPM = _ppm.BaseProcRate;
                    ProcPPMMods = dataAccess.SpellProcsPerMinuteMods.LookupByKey(_ppm.Id);
                }
            }

            // SpellAuraRestrictionsEntry
            SpellAuraRestrictionsRecord _aura = data.AuraRestrictions;
            if (_aura != null)
            {
                AuraRestrictionsId = _aura.Id;
                CasterAuraState = (AuraStateType)_aura.CasterAuraState;
                TargetAuraState = (AuraStateType)_aura.TargetAuraState;
                ExcludeCasterAuraState = (AuraStateType)_aura.ExcludeCasterAuraState;
                ExcludeTargetAuraState = (AuraStateType)_aura.ExcludeTargetAuraState;
                CasterAuraSpell = _aura.CasterAuraSpell;
                TargetAuraSpell = _aura.TargetAuraSpell;
                ExcludeCasterAuraSpell = _aura.ExcludeCasterAuraSpell;
                ExcludeTargetAuraSpell = _aura.ExcludeTargetAuraSpell;
            }

            RequiredAreasID = -1;
            // SpellCastingRequirementsEntry
            SpellCastingRequirementsRecord _castreq = data.CastingRequirements;
            if (_castreq != null)
            {
                SpellCastingRequirements = _castreq;
                RequiresSpellFocus = _castreq.RequiresSpellFocus;
                FacingCasterFlags = _castreq.FacingCasterFlags;
                RequiredAreasID = _castreq.RequiredAreasID;
            }
            else
                SpellCastingRequirements = new SpellCastingRequirementsRecord();

            // SpellCategoriesEntry
            SpellCategoriesRecord _categorie = data.Categories;
            if (_categorie != null)
            {
                SpellCategoriesId = _categorie.Id;
                CategoryId = _categorie.Category;
                Dispel = (DispelType)_categorie.DispelType;
                Mechanic = (Mechanics)_categorie.Mechanic;
                StartRecoveryCategory = _categorie.StartRecoveryCategory;
                DmgClass = (SpellDmgClass)_categorie.DefenseType;
                PreventionType = (SpellPreventionType)_categorie.PreventionType;
                ChargeCategoryId = _categorie.ChargeCategory;
            }

            // SpellClassOptionsEntry
            SpellFamilyFlags = new FlagArray128();
            SpellClassOptionsRecord _class = data.ClassOptions;
            if (_class != null)
            {
                ClassOptionsId = _class.Id;
                ModalNextSpell = _class.ModalNextSpell;
                SpellFamilyName = (SpellFamilyNames)_class.SpellClassSet;
                SpellFamilyFlags = _class.SpellClassMask;
            }

            // SpellCooldownsEntry
            SpellCooldownsRecord _cooldowns = data.Cooldowns;
            if (_cooldowns != null)
            {
                SpellCooldownsId = _cooldowns.Id;
                RecoveryTime = _cooldowns.RecoveryTime;
                CategoryRecoveryTime = _cooldowns.CategoryRecoveryTime;
                StartRecoveryTime = _cooldowns.StartRecoveryTime;
            }

            EquippedItemClass = ItemClass.None;
            EquippedItemSubClassMask = 0;
            EquippedItemInventoryTypeMask = 0;
            // SpellEquippedItemsEntry
            SpellEquippedItemsRecord _equipped = data.EquippedItems;
            if (_equipped != null)
            {
                SpellEquippedItemsId = _equipped.Id;
                EquippedItemClass = (ItemClass)_equipped.EquippedItemClass;
                EquippedItemSubClassMask = _equipped.EquippedItemSubclass;
                EquippedItemInventoryTypeMask = _equipped.EquippedItemInvTypes;
            }

            // SpellInterruptsEntry
            SpellInterruptsRecord _interrupt = data.Interrupts;
            if (_interrupt != null)
            {
                SpellInterruptsId = _interrupt.Id;
                InterruptFlags = (SpellInterruptFlags)_interrupt.InterruptFlags;
                AuraInterruptFlags = (SpellAuraInterruptFlags)_interrupt.AuraInterruptFlags[0];
                AuraInterruptFlags2 = (SpellAuraInterruptFlags2)_interrupt.AuraInterruptFlags[1];
                ChannelInterruptFlags = (SpellAuraInterruptFlags)_interrupt.ChannelInterruptFlags[0];
                ChannelInterruptFlags2 = (SpellAuraInterruptFlags2)_interrupt.ChannelInterruptFlags[1];
            }

            foreach (var label in data.Labels)
                Labels.Add(label.LabelID);

            // SpellLevelsEntry
            SpellLevelsRecord _levels = data.Levels;
            if (_levels != null)
            {
                SpellLevelsId = _levels.Id;
                MaxLevel = _levels.MaxLevel;
                BaseLevel = _levels.BaseLevel;
                SpellLevel = _levels.SpellLevel;
                MaxPassiveAuraLevel = _levels.MaxPassiveAuraLevel;
            }

            // SpellPowerEntry
            PowerCosts = data.Powers;

            // SpellReagentsEntry
            SpellReagentsRecord _reagents = data.Reagents;
            if (_reagents != null)
            {
                SpellReagentsId = _reagents.Id;
                for (var i = 0; i < SpellConst.MaxReagents; ++i)
                {
                    Reagent[i] = _reagents.Reagent[i];
                    ReagentCount[i] = _reagents.ReagentCount[i];
                }
            }

            ReagentsCurrency = data.ReagentsCurrency;

            // SpellShapeshiftEntry
            SpellShapeshiftRecord _shapeshift = data.Shapeshift;
            if (_shapeshift != null)
            {
                ShapeshiftRecordId = _shapeshift.Id;
                Stances = MathFunctions.MakePair64(_shapeshift.ShapeshiftMask[0], _shapeshift.ShapeshiftMask[1]);
                StancesNot = MathFunctions.MakePair64(_shapeshift.ShapeshiftExclude[0], _shapeshift.ShapeshiftExclude[1]);
                StanceBarOrder = _shapeshift.StanceBarOrder;
            }

            // SpellTargetRestrictionsEntry
            SpellTargetRestrictionsRecord _target = data.TargetRestrictions;
            if (_target != null)
            {
                TargetRestrictionsId = _target.Id;
                Targets = (SpellCastTargetFlags)_target.Targets;
                ConeAngle = _target.ConeDegrees;
                Width = _target.Width;
                TargetCreatureType = _target.TargetCreatureType;
                MaxAffectedTargets = _target.MaxTargets;
                MaxTargetLevel = _target.MaxTargetLevel;
            }

            // SpellTotemsEntry
            SpellTotemsRecord _totem = data.Totems;
            if (_totem != null)
            {
                TotemRecordID = _totem.Id;
                for (var i = 0; i < 2; ++i)
                {
                    TotemCategory[i] = _totem.RequiredTotemCategoryID[i];
                    Totem[i] = _totem.Totem[i];
                }
            }

            _visuals = data.Visuals;

            _spellSpecific = SpellSpecificType.Normal;
            _auraState = AuraStateType.None;
        }

        public bool HasLabel(uint labelId)
        {
            return Labels.Contains(labelId);
        }

        public static SpellCastTargetFlags GetTargetFlagMask(SpellTargetObjectTypes objType)
        {
            switch (objType)
            {
                case SpellTargetObjectTypes.Dest:
                    return SpellCastTargetFlags.DestLocation;
                case SpellTargetObjectTypes.UnitAndDest:
                    return SpellCastTargetFlags.DestLocation | SpellCastTargetFlags.Unit;
                case SpellTargetObjectTypes.CorpseAlly:
                    return SpellCastTargetFlags.CorpseAlly;
                case SpellTargetObjectTypes.CorpseEnemy:
                    return SpellCastTargetFlags.CorpseEnemy;
                case SpellTargetObjectTypes.Corpse:
                    return SpellCastTargetFlags.CorpseAlly | SpellCastTargetFlags.CorpseEnemy;
                case SpellTargetObjectTypes.Unit:
                    return SpellCastTargetFlags.Unit;
                case SpellTargetObjectTypes.Gobj:
                    return SpellCastTargetFlags.Gameobject;
                case SpellTargetObjectTypes.GobjItem:
                    return SpellCastTargetFlags.GameobjectItem;
                case SpellTargetObjectTypes.Item:
                    return SpellCastTargetFlags.Item;
                case SpellTargetObjectTypes.Src:
                    return SpellCastTargetFlags.SourceLocation;
                default:
                    return SpellCastTargetFlags.None;
            }
        }

        public uint GetCategory()
        {
            return CategoryId;
        }

        public List<SpellEffectInfo> GetEffects() { return _effects; }

        public SpellEffectInfo GetEffect(uint index) { return _effects[(int)index]; }

        public bool HasTargetType(Targets target)
        {
            foreach (var effectInfo in _effects)
                if (effectInfo.TargetA.GetTarget() == target || effectInfo.TargetB.GetTarget() == target)
                    return true;

            return false;
        }

        public List<SpellXSpellVisualRecord> GetSpellVisuals()
        {
            return _visuals;
        }

        public bool HasAttribute(SpellAttr0 attribute) { return Convert.ToBoolean(Attributes & attribute); }
        public bool HasAttribute(SpellAttr1 attribute) { return Convert.ToBoolean(AttributesEx & attribute); }
        public bool HasAttribute(SpellAttr2 attribute) { return Convert.ToBoolean(AttributesEx2 & attribute); }
        public bool HasAttribute(SpellAttr3 attribute) { return Convert.ToBoolean(AttributesEx3 & attribute); }
        public bool HasAttribute(SpellAttr4 attribute) { return Convert.ToBoolean(AttributesEx4 & attribute); }
        public bool HasAttribute(SpellAttr5 attribute) { return Convert.ToBoolean(AttributesEx5 & attribute); }
        public bool HasAttribute(SpellAttr6 attribute) { return Convert.ToBoolean(AttributesEx6 & attribute); }
        public bool HasAttribute(SpellAttr7 attribute) { return Convert.ToBoolean(AttributesEx7 & attribute); }
        public bool HasAttribute(SpellAttr8 attribute) { return Convert.ToBoolean(AttributesEx8 & attribute); }
        public bool HasAttribute(SpellAttr9 attribute) { return Convert.ToBoolean(AttributesEx9 & attribute); }
        public bool HasAttribute(SpellAttr10 attribute) { return Convert.ToBoolean(AttributesEx10 & attribute); }
        public bool HasAttribute(SpellAttr11 attribute) { return Convert.ToBoolean(AttributesEx11 & attribute); }
        public bool HasAttribute(SpellAttr12 attribute) { return Convert.ToBoolean(AttributesEx12 & attribute); }
        public bool HasAttribute(SpellAttr13 attribute) { return Convert.ToBoolean(AttributesEx13 & attribute); }
        public bool HasAttribute(SpellAttr14 attribute) { return Convert.ToBoolean(AttributesEx14 & attribute); }
        public bool HasAttribute(SpellCustomAttributes attribute) { return Convert.ToBoolean(AttributesCu & attribute); }
        public bool HasAnyAuraInterruptFlag() { return AuraInterruptFlags != SpellAuraInterruptFlags.None || AuraInterruptFlags2 != SpellAuraInterruptFlags2.None; }
        public bool HasAuraInterruptFlag(SpellAuraInterruptFlags flag) { return AuraInterruptFlags.HasAnyFlag(flag); }
        public bool HasAuraInterruptFlag(SpellAuraInterruptFlags2 flag) { return AuraInterruptFlags2.HasAnyFlag(flag); }

        public bool HasChannelInterruptFlag(SpellAuraInterruptFlags flag) { return ChannelInterruptFlags.HasAnyFlag(flag); }
        public bool HasChannelInterruptFlag(SpellAuraInterruptFlags2 flag) { return ChannelInterruptFlags2.HasAnyFlag(flag); }

        #region Fields
        public uint Id { get; set; }
        public Difficulty Difficulty { get; set; }
        public uint CategoryId { get; set; }
        public uint SpellMiscId { get; set; }
        public uint AuraOptionsId { get; set; }
        public ushort SpellPPMId { get; set; }
        public uint AuraRestrictionsId { get; set; }
        public uint SpellCategoriesId { get; set; }
        public uint SpellCooldownsId { get; set; }
        public uint SpellEquippedItemsId { get; set; }
        public uint SpellInterruptsId { get; set; }
        public uint SpellLevelsId { get; set; }
        public uint SpellReagentsId { get; set; }
        public uint ShapeshiftRecordId { get; set; }
        public uint TargetRestrictionsId { get; set; }
        public uint TotemRecordID { get; set; }
        public SpellCastingRequirementsRecord SpellCastingRequirements { get; set; }
        public DispelType Dispel { get; set; }
        public Mechanics Mechanic { get; set; }
        public SpellAttr0 Attributes { get; set; }
        public SpellAttr1 AttributesEx { get; set; }
        public SpellAttr2 AttributesEx2 { get; set; }
        public SpellAttr3 AttributesEx3 { get; set; }
        public SpellAttr4 AttributesEx4 { get; set; }
        public SpellAttr5 AttributesEx5 { get; set; }
        public SpellAttr6 AttributesEx6 { get; set; }
        public SpellAttr7 AttributesEx7 { get; set; }
        public SpellAttr8 AttributesEx8 { get; set; }
        public SpellAttr9 AttributesEx9 { get; set; }
        public SpellAttr10 AttributesEx10 { get; set; }
        public SpellAttr11 AttributesEx11 { get; set; }
        public SpellAttr12 AttributesEx12 { get; set; }
        public SpellAttr13 AttributesEx13 { get; set; }
        public SpellAttr14 AttributesEx14 { get; set; }
        public SpellCustomAttributes AttributesCu { get; set; }
        public BitSet NegativeEffects { get; set; } = new BitSet(32);
        public ulong Stances { get; set; }
        public ulong StancesNot { get; set; }
        public SpellCastTargetFlags Targets { get; set; }
        public uint TargetCreatureType { get; set; }
        public uint RequiresSpellFocus { get; set; }
        public uint FacingCasterFlags { get; set; }
        public AuraStateType CasterAuraState { get; set; }
        public AuraStateType TargetAuraState { get; set; }
        public AuraStateType ExcludeCasterAuraState { get; set; }
        public AuraStateType ExcludeTargetAuraState { get; set; }
        public uint CasterAuraSpell { get; set; }
        public uint TargetAuraSpell { get; set; }
        public uint ExcludeCasterAuraSpell { get; set; }
        public uint ExcludeTargetAuraSpell { get; set; }
        public SpellCastTimesRecord CastTimeEntry { get; set; }
        public uint RecoveryTime { get; set; }
        public uint CategoryRecoveryTime { get; set; }
        public uint StartRecoveryCategory { get; set; }
        public uint StartRecoveryTime { get; set; }
        public SpellInterruptFlags InterruptFlags { get; set; }
        public SpellAuraInterruptFlags AuraInterruptFlags { get; set; }
        public SpellAuraInterruptFlags2 AuraInterruptFlags2 { get; set; }
        public SpellAuraInterruptFlags ChannelInterruptFlags { get; set; }
        public SpellAuraInterruptFlags2 ChannelInterruptFlags2 { get; set; }
        public ProcFlagsInit ProcFlags { get; set; }
        public uint ProcChance { get; set; }
        public uint ProcCharges { get; set; }
        public uint ProcCooldown { get; set; }
        public float ProcBasePPM { get; set; }
        public List<SpellProcsPerMinuteModRecord> ProcPPMMods = new();
        public uint MaxLevel { get; set; }
        public uint BaseLevel { get; set; }
        public uint SpellLevel { get; set; }
        public SpellDurationRecord DurationEntry { get; set; }
        public SpellPowerRecord[] PowerCosts = new SpellPowerRecord[SpellConst.MaxPowersPerSpell];
        public SpellRangeRecord RangeEntry { get; set; }
        public float Speed { get; set; }
        public float LaunchDelay { get; set; }
        public uint StackAmount { get; set; }
        public uint[] Totem = new uint[SpellConst.MaxTotems];
        public uint[] TotemCategory = new uint[SpellConst.MaxTotems];
        public int[] Reagent = new int[SpellConst.MaxReagents];
        public uint[] ReagentCount = new uint[SpellConst.MaxReagents];
        public List<SpellReagentsCurrencyRecord> ReagentsCurrency = new();
        public ItemClass EquippedItemClass { get; set; }
        public int EquippedItemSubClassMask { get; set; }
        public int EquippedItemInventoryTypeMask { get; set; }
        public uint IconFileDataId { get; set; }
        public uint ActiveIconFileDataId { get; set; }
        public uint ContentTuningId { get; set; }
        public uint ShowFutureSpellPlayerConditionID { get; set; }
        public LocalizedString SpellName { get; set; }
        public float ConeAngle { get; set; }
        public float Width { get; set; }
        public uint MaxTargetLevel { get; set; }
        public uint MaxAffectedTargets { get; set; }
        public uint ClassOptionsId { get; set; }
        public uint ModalNextSpell { get; set; }
        public SpellFamilyNames SpellFamilyName { get; set; }
        public FlagArray128 SpellFamilyFlags { get; set; }
        public SpellDmgClass DmgClass { get; set; }
        public SpellPreventionType PreventionType { get; set; }
        public int RequiredAreasID { get; set; }
        public SpellSchoolMask SchoolMask { get; set; }
        public uint ChargeCategoryId;
        public List<uint> Labels = new();

        // SpellScalingEntry
        public ScalingInfo Scaling;
        public uint ExplicitTargetMask { get; set; }

        List<SpellEffectInfo> _effects = new();
        List<SpellXSpellVisualRecord> _visuals = new();
        SpellSpecificType _spellSpecific;
        AuraStateType _auraState;
        public byte MaxPassiveAuraLevel { get; set; }
        public sbyte StanceBarOrder { get; set; }

        SpellDiminishInfo _diminishInfo;
        uint _allowedMechanicMask;
        #endregion

        public struct ScalingInfo
        {
            public uint Id;
            public uint MinScalingLevel;
            public uint MaxScalingLevel;
            public uint ScalesFromItemLevel;
        }
    }

    public class SpellEffectInfo
    {
        public SpellEffectInfo(SpellInfo spellInfo, SpellEffectRecord effect = null)
        {
            _spellInfo = spellInfo;
            if (effect != null)
            {
                Id = effect.Id;
                EffectIndex = (uint)effect.EffectIndex;
                Effect = (SpellEffectName)effect.Effect;
                ApplyAuraName = (AuraType)effect.EffectAura;
                ApplyAuraPeriod = effect.EffectAuraPeriod;
                BasePoints = (int)effect.EffectBasePoints;
                RealPointsPerLevel = effect.EffectRealPointsPerLevel;
                PointsPerResource = effect.EffectPointsPerResource;
                Amplitude = effect.EffectAmplitude;
                ChainAmplitude = effect.EffectChainAmplitude;
                BonusCoefficient = effect.EffectBonusCoefficient;
                MiscValue = effect.EffectMiscValue[0];
                MiscValueB = effect.EffectMiscValue[1];
                Mechanic = (Mechanics)effect.EffectMechanic;
                PositionFacing = effect.EffectPosFacing;
                TargetA = new SpellImplicitTargetInfo((Targets)effect.ImplicitTarget[0]);
                TargetB = new SpellImplicitTargetInfo((Targets)effect.ImplicitTarget[1]);
                RadiusEntry = CliDB.SpellRadiusStorage.LookupByKey(effect.EffectRadiusIndex[0]);
                MaxRadiusEntry = CliDB.SpellRadiusStorage.LookupByKey(effect.EffectRadiusIndex[1]);
                ChainTargets = effect.EffectChainTargets;
                ItemType = effect.EffectItemType;
                TriggerSpell = effect.EffectTriggerSpell;
                SpellClassMask = effect.EffectSpellClassMask;
                BonusCoefficientFromAP = effect.BonusCoefficientFromAP;
                Scaling.Class = effect.ScalingClass;
                Scaling.Coefficient = effect.Coefficient;
                Scaling.Variance = effect.Variance;
                Scaling.ResourceCoefficient = effect.ResourceCoefficient;
                EffectAttributes = effect.EffectAttributes;
            }

            _immunityInfo = new ImmunityInfo();
        }

        public bool IsEffect()
        {
            return Effect != 0;
        }

        public bool IsEffect(SpellEffectName effectName)
        {
            return Effect == effectName;
        }

        public bool IsAura()
        {
            return (IsUnitOwnedAuraEffect() || Effect == SpellEffectName.PersistentAreaAura) && ApplyAuraName != 0;
        }

        public bool IsAura(AuraType aura)
        {
            return IsAura() && ApplyAuraName == aura;
        }

        public bool IsTargetingArea()
        {
            return TargetA.IsArea() || TargetB.IsArea();
        }

        public bool IsAreaAuraEffect()
        {
            if (Effect == SpellEffectName.ApplyAreaAuraParty ||
                Effect == SpellEffectName.ApplyAreaAuraRaid ||
                Effect == SpellEffectName.ApplyAreaAuraFriend ||
                Effect == SpellEffectName.ApplyAreaAuraEnemy ||
                Effect == SpellEffectName.ApplyAreaAuraPet ||
                Effect == SpellEffectName.ApplyAreaAuraOwner ||
                Effect == SpellEffectName.ApplyAreaAuraSummons ||
                Effect == SpellEffectName.ApplyAreaAuraPartyNonrandom)
                return true;
            return false;
        }

        public bool IsUnitOwnedAuraEffect()
        {
            return IsAreaAuraEffect() || Effect == SpellEffectName.ApplyAura || Effect == SpellEffectName.ApplyAuraOnPet;
        }

        public bool HasRadius()
        {
            return RadiusEntry != null;
        }

        public bool HasMaxRadius()
        {
            return MaxRadiusEntry != null;
        }

        public SpellCastTargetFlags GetProvidedTargetMask()
        {
            return SpellInfo.GetTargetFlagMask(TargetA.GetObjectType()) | SpellInfo.GetTargetFlagMask(TargetB.GetObjectType());
        }

        public SpellCastTargetFlags GetMissingTargetMask(bool srcSet = false, bool dstSet = false, SpellCastTargetFlags mask = 0)
        {
            var effImplicitTargetMask = SpellInfo.GetTargetFlagMask(GetUsedTargetObjectType());
            SpellCastTargetFlags providedTargetMask = GetProvidedTargetMask() | mask;

            // remove all flags covered by effect target mask
            if (Convert.ToBoolean(providedTargetMask & SpellCastTargetFlags.UnitMask))
                effImplicitTargetMask &= ~SpellCastTargetFlags.UnitMask;
            if (Convert.ToBoolean(providedTargetMask & SpellCastTargetFlags.CorpseMask))
                effImplicitTargetMask &= ~(SpellCastTargetFlags.UnitMask | SpellCastTargetFlags.CorpseMask);
            if (Convert.ToBoolean(providedTargetMask & SpellCastTargetFlags.GameobjectItem))
                effImplicitTargetMask &= ~(SpellCastTargetFlags.GameobjectItem | SpellCastTargetFlags.Gameobject | SpellCastTargetFlags.Item);
            if (Convert.ToBoolean(providedTargetMask & SpellCastTargetFlags.Gameobject))
                effImplicitTargetMask &= ~(SpellCastTargetFlags.Gameobject | SpellCastTargetFlags.GameobjectItem);
            if (Convert.ToBoolean(providedTargetMask & SpellCastTargetFlags.Item))
                effImplicitTargetMask &= ~(SpellCastTargetFlags.Item | SpellCastTargetFlags.GameobjectItem);
            if (dstSet || Convert.ToBoolean(providedTargetMask & SpellCastTargetFlags.DestLocation))
                effImplicitTargetMask &= ~SpellCastTargetFlags.DestLocation;
            if (srcSet || Convert.ToBoolean(providedTargetMask & SpellCastTargetFlags.SourceLocation))
                effImplicitTargetMask &= ~SpellCastTargetFlags.SourceLocation;

            return effImplicitTargetMask;
        }

        public SpellEffectImplicitTargetTypes GetImplicitTargetType()
        {
            return _data[(int)Effect].ImplicitTargetType;
        }

        public SpellTargetObjectTypes GetUsedTargetObjectType()
        {
            return _data[(int)Effect].UsedTargetObjectType;
        }

        ExpectedStatType GetScalingExpectedStat()
        {
            switch (Effect)
            {
                case SpellEffectName.SchoolDamage:
                case SpellEffectName.EnvironmentalDamage:
                case SpellEffectName.HealthLeech:
                case SpellEffectName.WeaponDamageNoSchool:
                case SpellEffectName.WeaponDamage:
                    return ExpectedStatType.CreatureSpellDamage;
                case SpellEffectName.Heal:
                case SpellEffectName.HealMechanical:
                    return ExpectedStatType.PlayerHealth;
                case SpellEffectName.Energize:
                case SpellEffectName.PowerBurn:
                    if (MiscValue == (int)PowerType.Mana)
                        return ExpectedStatType.PlayerMana;
                    return ExpectedStatType.None;
                case SpellEffectName.PowerDrain:
                    return ExpectedStatType.PlayerMana;
                case SpellEffectName.ApplyAura:
                case SpellEffectName.PersistentAreaAura:
                case SpellEffectName.ApplyAreaAuraParty:
                case SpellEffectName.ApplyAreaAuraRaid:
                case SpellEffectName.ApplyAreaAuraPet:
                case SpellEffectName.ApplyAreaAuraFriend:
                case SpellEffectName.ApplyAreaAuraEnemy:
                case SpellEffectName.ApplyAreaAuraOwner:
                case SpellEffectName.ApplyAuraOnPet:
                case SpellEffectName.ApplyAreaAuraSummons:
                case SpellEffectName.ApplyAreaAuraPartyNonrandom:
                    switch (ApplyAuraName)
                    {
                        case AuraType.PeriodicDamage:
                        case AuraType.ModDamageDone:
                        case AuraType.DamageShield:
                        case AuraType.ProcTriggerDamage:
                        case AuraType.PeriodicLeech:
                        case AuraType.ModDamageDoneCreature:
                        case AuraType.PeriodicHealthFunnel:
                        case AuraType.ModMeleeAttackPowerVersus:
                        case AuraType.ModRangedAttackPowerVersus:
                        case AuraType.ModFlatSpellDamageVersus:
                            return ExpectedStatType.CreatureSpellDamage;
                        case AuraType.PeriodicHeal:
                        case AuraType.ModDamageTaken:
                        case AuraType.ModIncreaseHealth:
                        case AuraType.SchoolAbsorb:
                        case AuraType.ModRegen:
                        case AuraType.ManaShield:
                        case AuraType.ModHealing:
                        case AuraType.ModHealingDone:
                        case AuraType.ModHealthRegenInCombat:
                        case AuraType.ModMaxHealth:
                        case AuraType.ModIncreaseHealth2:
                        case AuraType.SchoolHealAbsorb:
                            return ExpectedStatType.PlayerHealth;
                        case AuraType.PeriodicManaLeech:
                            return ExpectedStatType.PlayerMana;
                        case AuraType.ModStat:
                        case AuraType.ModAttackPower:
                        case AuraType.ModRangedAttackPower:
                            return ExpectedStatType.PlayerPrimaryStat;
                        case AuraType.ModRating:
                            return ExpectedStatType.PlayerSecondaryStat;
                        case AuraType.ModResistance:
                        case AuraType.ModBaseResistance:
                        case AuraType.ModTargetResistance:
                        case AuraType.ModBonusArmor:
                            return ExpectedStatType.ArmorConstant;
                        case AuraType.PeriodicEnergize:
                        case AuraType.ModIncreaseEnergy:
                        case AuraType.ModPowerCostSchool:
                        case AuraType.ModPowerRegen:
                        case AuraType.PowerBurn:
                        case AuraType.ModMaxPower:
                            if (MiscValue == (int)PowerType.Mana)
                                return ExpectedStatType.PlayerMana;
                            return ExpectedStatType.None;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
            return ExpectedStatType.None;
        }

        public ImmunityInfo GetImmunityInfo() { return _immunityInfo; }

        public class StaticData
        {
            public StaticData(SpellEffectImplicitTargetTypes implicittarget, SpellTargetObjectTypes usedtarget)
            {
                ImplicitTargetType = implicittarget;
                UsedTargetObjectType = usedtarget;
            }

            public SpellEffectImplicitTargetTypes ImplicitTargetType; // defines what target can be added to effect target list if there's no valid target type provided for effect
            public SpellTargetObjectTypes UsedTargetObjectType; // defines valid target object type for spell effect
        }

        static StaticData[] _data = new StaticData[(int)SpellEffectName.TotalSpellEffects]
        {
            // implicit target type           used target object type
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.None), // 0
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 1 SPELL_EFFECT_INSTAKILL
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 2 SPELL_EFFECT_SCHOOL_DAMAGE
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.None), // 3 SPELL_EFFECT_DUMMY
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.None), // 4 SPELL_EFFECT_PORTAL_TELEPORT
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Item), // 5 SPELL_EFFECT_5
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 6 SPELL_EFFECT_APPLY_AURA
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 7 SPELL_EFFECT_ENVIRONMENTAL_DAMAGE
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 8 SPELL_EFFECT_POWER_DRAIN
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 9 SPELL_EFFECT_HEALTH_LEECH
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 10 SPELL_EFFECT_HEAL
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 11 SPELL_EFFECT_BIND
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.None), // 12 SPELL_EFFECT_PORTAL
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.UnitAndDest), // 13 SPELL_EFFECT_TELEPORT_TO_RETURN_POINT
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 14 SPELL_EFFECT_INCREASE_CURRENCY_CAP
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.UnitAndDest), // 15 SPELL_EFFECT_TELEPORT_WITH_SPELL_VISUAL_KIT_LOADING_SCREEN
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 16 SPELL_EFFECT_QUEST_COMPLETE
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 17 SPELL_EFFECT_WEAPON_DAMAGE_NOSCHOOL
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.CorpseAlly), // 18 SPELL_EFFECT_RESURRECT
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 19 SPELL_EFFECT_ADD_EXTRA_ATTACKS
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.Unit), // 20 SPELL_EFFECT_DODGE
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.Unit), // 21 SPELL_EFFECT_EVADE
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.Unit), // 22 SPELL_EFFECT_PARRY
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.Unit), // 23 SPELL_EFFECT_BLOCK
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 24 SPELL_EFFECT_CREATE_ITEM
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.Unit), // 25 SPELL_EFFECT_WEAPON
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.Unit), // 26 SPELL_EFFECT_DEFENSE
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Dest), // 27 SPELL_EFFECT_PERSISTENT_AREA_AURA
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Dest), // 28 SPELL_EFFECT_SUMMON
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.UnitAndDest), // 29 SPELL_EFFECT_LEAP
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.Unit), // 30 SPELL_EFFECT_ENERGIZE
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 31 SPELL_EFFECT_WEAPON_PERCENT_DAMAGE
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.None), // 32 SPELL_EFFECT_TRIGGER_MISSILE
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.GobjItem), // 33 SPELL_EFFECT_OPEN_LOCK
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.Unit), // 34 SPELL_EFFECT_SUMMON_CHANGE_ITEM
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 35 SPELL_EFFECT_APPLY_AREA_AURA_PARTY
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 36 SPELL_EFFECT_LEARN_SPELL
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.Unit), // 37 SPELL_EFFECT_SPELL_DEFENSE
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 38 SPELL_EFFECT_DISPEL
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.Unit), // 39 SPELL_EFFECT_LANGUAGE
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 40 SPELL_EFFECT_DUAL_WIELD
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 41 SPELL_EFFECT_JUMP
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.Dest), // 42 SPELL_EFFECT_JUMP_DEST
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.UnitAndDest), // 43 SPELL_EFFECT_TELEPORT_UNITS_FACE_CASTER
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 44 SPELL_EFFECT_SKILL_STEP
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 45 SPELL_EFFECT_ADD_HONOR
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.Unit), // 46 SPELL_EFFECT_SPAWN
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.Unit), // 47 SPELL_EFFECT_TRADE_SKILL
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.Unit), // 48 SPELL_EFFECT_STEALTH
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.Unit), // 49 SPELL_EFFECT_DETECT
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Dest), // 50 SPELL_EFFECT_TRANS_DOOR
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.Unit), // 51 SPELL_EFFECT_FORCE_CRITICAL_HIT
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 52 SPELL_EFFECT_SET_MAX_BATTLE_PET_COUNT
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Item), // 53 SPELL_EFFECT_ENCHANT_ITEM
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Item), // 54 SPELL_EFFECT_ENCHANT_ITEM_TEMPORARY
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 55 SPELL_EFFECT_TAMECREATURE
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Dest), // 56 SPELL_EFFECT_SUMMON_PET
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 57 SPELL_EFFECT_LEARN_PET_SPELL
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 58 SPELL_EFFECT_WEAPON_DAMAGE
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 59 SPELL_EFFECT_CREATE_RANDOM_ITEM
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.Unit), // 60 SPELL_EFFECT_PROFICIENCY
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.None), // 61 SPELL_EFFECT_SEND_EVENT
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 62 SPELL_EFFECT_POWER_BURN
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 63 SPELL_EFFECT_THREAT
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.None), // 64 SPELL_EFFECT_TRIGGER_SPELL
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 65 SPELL_EFFECT_APPLY_AREA_AURA_RAID
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 66 SPELL_EFFECT_RECHARGE_ITEM
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 67 SPELL_EFFECT_HEAL_MAX_HEALTH
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 68 SPELL_EFFECT_INTERRUPT_CAST
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.UnitAndDest), // 69 SPELL_EFFECT_DISTRACT
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 70 SPELL_EFFECT_COMPLETE_AND_REWARD_WORLD_QUEST
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 71 SPELL_EFFECT_PICKPOCKET
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Dest), // 72 SPELL_EFFECT_ADD_FARSIGHT
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 73 SPELL_EFFECT_UNTRAIN_TALENTS
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.Unit), // 74 SPELL_EFFECT_APPLY_GLYPH
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 75 SPELL_EFFECT_HEAL_MECHANICAL
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Dest), // 76 SPELL_EFFECT_SUMMON_OBJECT_WILD
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.None), // 77 SPELL_EFFECT_SCRIPT_EFFECT
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.Unit), // 78 SPELL_EFFECT_ATTACK
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.Unit), // 79 SPELL_EFFECT_SANCTUARY
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 80 SPELL_EFFECT_MODIFY_FOLLOWER_ITEM_LEVEL
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 81 SPELL_EFFECT_PUSH_ABILITY_TO_ACTION_BAR
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 82 SPELL_EFFECT_BIND_SIGHT
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.UnitAndDest), // 83 SPELL_EFFECT_DUEL
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.Unit), // 84 SPELL_EFFECT_STUCK
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.None), // 85 SPELL_EFFECT_SUMMON_PLAYER
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Gobj), // 86 SPELL_EFFECT_ACTIVATE_OBJECT
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Gobj), // 87 SPELL_EFFECT_GAMEOBJECT_DAMAGE
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Gobj), // 88 SPELL_EFFECT_GAMEOBJECT_REPAIR
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Gobj), // 89 SPELL_EFFECT_GAMEOBJECT_SET_DESTRUCTION_STATE
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 90 SPELL_EFFECT_KILL_CREDIT
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 91 SPELL_EFFECT_THREAT_ALL
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 92 SPELL_EFFECT_ENCHANT_HELD_ITEM
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.Unit), // 93 SPELL_EFFECT_FORCE_DESELECT
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.Unit), // 94 SPELL_EFFECT_SELF_RESURRECT
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 95 SPELL_EFFECT_SKINNING
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 96 SPELL_EFFECT_CHARGE
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.Unit), // 97 SPELL_EFFECT_CAST_BUTTON
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 98 SPELL_EFFECT_KNOCK_BACK
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Item), // 99 SPELL_EFFECT_DISENCHANT
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 100 SPELL_EFFECT_INEBRIATE
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Item), // 101 SPELL_EFFECT_FEED_PET
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 102 SPELL_EFFECT_DISMISS_PET
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 103 SPELL_EFFECT_REPUTATION
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Dest), // 104 SPELL_EFFECT_SUMMON_OBJECT_SLOT1
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 105 SPELL_EFFECT_SURVEY
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Dest), // 106 SPELL_EFFECT_CHANGE_RAID_MARKER
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 107 SPELL_EFFECT_SHOW_CORPSE_LOOT
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 108 SPELL_EFFECT_DISPEL_MECHANIC
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Dest), // 109 SPELL_EFFECT_RESURRECT_PET
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.Unit), // 110 SPELL_EFFECT_DESTROY_ALL_TOTEMS
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 111 SPELL_EFFECT_DURABILITY_DAMAGE
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 112 SPELL_EFFECT_112
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 113 SPELL_EFFECT_CANCEL_CONVERSATION
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 114 SPELL_EFFECT_ATTACK_ME
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 115 SPELL_EFFECT_DURABILITY_DAMAGE_PCT
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.CorpseEnemy), // 116 SPELL_EFFECT_SKIN_PLAYER_CORPSE
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 117 SPELL_EFFECT_SPIRIT_HEAL
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.Unit), // 118 SPELL_EFFECT_SKILL
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 119 SPELL_EFFECT_APPLY_AREA_AURA_PET
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 120 SPELL_EFFECT_TELEPORT_GRAVEYARD
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 121 SPELL_EFFECT_NORMALIZED_WEAPON_DMG
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.None), // 122 SPELL_EFFECT_122
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 123 SPELL_EFFECT_SEND_TAXI
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 124 SPELL_EFFECT_PULL_TOWARDS
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 125 SPELL_EFFECT_MODIFY_THREAT_PERCENT
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 126 SPELL_EFFECT_STEAL_BENEFICIAL_BUFF
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Item), // 127 SPELL_EFFECT_PROSPECTING
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 128 SPELL_EFFECT_APPLY_AREA_AURA_FRIEND
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 129 SPELL_EFFECT_APPLY_AREA_AURA_ENEMY
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 130 SPELL_EFFECT_REDIRECT_THREAT
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.Unit), // 131 SPELL_EFFECT_PLAY_SOUND
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 132 SPELL_EFFECT_PLAY_MUSIC
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 133 SPELL_EFFECT_UNLEARN_SPECIALIZATION
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.Unit), // 134 SPELL_EFFECT_KILL_CREDIT2
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Dest), // 135 SPELL_EFFECT_CALL_PET
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 136 SPELL_EFFECT_HEAL_PCT
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 137 SPELL_EFFECT_ENERGIZE_PCT
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 138 SPELL_EFFECT_LEAP_BACK
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 139 SPELL_EFFECT_CLEAR_QUEST
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 140 SPELL_EFFECT_FORCE_CAST
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 141 SPELL_EFFECT_FORCE_CAST_WITH_VALUE
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 142 SPELL_EFFECT_TRIGGER_SPELL_WITH_VALUE
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 143 SPELL_EFFECT_APPLY_AREA_AURA_OWNER
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.UnitAndDest), // 144 SPELL_EFFECT_KNOCK_BACK_DEST
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.UnitAndDest), // 145 SPELL_EFFECT_PULL_TOWARDS_DEST
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 146 SPELL_EFFECT_RESTORE_GARRISON_TROOP_VITALITY
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 147 SPELL_EFFECT_QUEST_FAIL
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.None), // 148 SPELL_EFFECT_TRIGGER_MISSILE_SPELL_WITH_VALUE
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Dest), // 149 SPELL_EFFECT_CHARGE_DEST
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 150 SPELL_EFFECT_QUEST_START
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.None), // 151 SPELL_EFFECT_TRIGGER_SPELL_2
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.None), // 152 SPELL_EFFECT_SUMMON_RAF_FRIEND
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 153 SPELL_EFFECT_CREATE_TAMED_PET
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 154 SPELL_EFFECT_DISCOVER_TAXI
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.Unit), // 155 SPELL_EFFECT_TITAN_GRIP
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Item), // 156 SPELL_EFFECT_ENCHANT_ITEM_PRISMATIC
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 157 SPELL_EFFECT_CREATE_LOOT
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Item), // 158 SPELL_EFFECT_MILLING
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 159 SPELL_EFFECT_ALLOW_RENAME_PET
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 160 SPELL_EFFECT_160
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 161 SPELL_EFFECT_TALENT_SPEC_COUNT
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 162 SPELL_EFFECT_TALENT_SPEC_SELECT
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Item), // 163 SPELL_EFFECT_OBLITERATE_ITEM
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 164 SPELL_EFFECT_REMOVE_AURA
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 165 SPELL_EFFECT_DAMAGE_FROM_MAX_HEALTH_PCT
            new StaticData(SpellEffectImplicitTargetTypes.Caster,   SpellTargetObjectTypes.Unit), // 166 SPELL_EFFECT_GIVE_CURRENCY
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 167 SPELL_EFFECT_UPDATE_PLAYER_PHASE
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 168 SPELL_EFFECT_ALLOW_CONTROL_PET
            new StaticData(SpellEffectImplicitTargetTypes.Caster,   SpellTargetObjectTypes.Unit), // 169 SPELL_EFFECT_DESTROY_ITEM
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 170 SPELL_EFFECT_UPDATE_ZONE_AURAS_AND_PHASES
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.Dest), // 171 SPELL_EFFECT_SUMMON_PERSONAL_GAMEOBJECT
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.CorpseAlly), // 172 SPELL_EFFECT_RESURRECT_WITH_AURA
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.None), // 173 SPELL_EFFECT_UNLOCK_GUILD_VAULT_TAB
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 174 SPELL_EFFECT_APPLY_AURA_ON_PET
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.None), // 175 SPELL_EFFECT_175
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 176 SPELL_EFFECT_SANCTUARY_2
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 177 SPELL_EFFECT_DESPAWN_PERSISTENT_AREA_AURA
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.None), // 178 SPELL_EFFECT_178
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.Dest), // 179 SPELL_EFFECT_CREATE_AREATRIGGER
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.None), // 180 SPELL_EFFECT_UPDATE_AREATRIGGER
            new StaticData(SpellEffectImplicitTargetTypes.Caster,   SpellTargetObjectTypes.Unit), // 181 SPELL_EFFECT_REMOVE_TALENT
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.None), // 182 SPELL_EFFECT_DESPAWN_AREATRIGGER
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.None), // 183 SPELL_EFFECT_183
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 184 SPELL_EFFECT_REPUTATION_2
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.None), // 185 SPELL_EFFECT_185
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.None), // 186 SPELL_EFFECT_186
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 187 SPELL_EFFECT_RANDOMIZE_ARCHAEOLOGY_DIGSITES
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Dest), // 188 SPELL_EFFECT_SUMMON_STABLED_PET_AS_GUARDIAN
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 189 SPELL_EFFECT_LOOT
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 190 SPELL_EFFECT_CHANGE_PARTY_MEMBERS
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 191 SPELL_EFFECT_TELEPORT_TO_DIGSITE
            new StaticData(SpellEffectImplicitTargetTypes.Caster,   SpellTargetObjectTypes.Unit), // 192 SPELL_EFFECT_UNCAGE_BATTLEPET
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 193 SPELL_EFFECT_START_PET_BATTLE
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.None), // 194 SPELL_EFFECT_194
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.Dest), // 195 SPELL_EFFECT_PLAY_SCENE_SCRIPT_PACKAGE
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.Dest), // 196 SPELL_EFFECT_CREATE_SCENE_OBJECT
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.Dest), // 197 SPELL_EFFECT_CREATE_PERSONAL_SCENE_OBJECT
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.Dest), // 198 SPELL_EFFECT_PLAY_SCENE
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 199 SPELL_EFFECT_DESPAWN_SUMMON
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.None), // 200 SPELL_EFFECT_HEAL_BATTLEPET_PCT
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.None), // 201 SPELL_EFFECT_ENABLE_BATTLE_PETS
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 202 SPELL_EFFECT_APPLY_AREA_AURA_SUMMONS
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 203 SPELL_EFFECT_REMOVE_AURA_2
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 204 SPELL_EFFECT_CHANGE_BATTLEPET_QUALITY
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 205 SPELL_EFFECT_LAUNCH_QUEST_CHOICE
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Item), // 206 SPELL_EFFECT_ALTER_ITEM
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 207 SPELL_EFFECT_LAUNCH_QUEST_TASK
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 208 SPELL_EFFECT_SET_REPUTATION
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.None), // 209 SPELL_EFFECT_209
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 210 SPELL_EFFECT_LEARN_GARRISON_BUILDING
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 211 SPELL_EFFECT_LEARN_GARRISON_SPECIALIZATION
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 212 SPELL_EFFECT_REMOVE_AURA_BY_SPELL_LABEL
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.Dest), // 213 SPELL_EFFECT_JUMP_DEST_2
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 214 SPELL_EFFECT_CREATE_GARRISON
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 215 SPELL_EFFECT_UPGRADE_CHARACTER_SPELLS
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 216 SPELL_EFFECT_CREATE_SHIPMENT
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 217 SPELL_EFFECT_UPGRADE_GARRISON
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.None), // 218 SPELL_EFFECT_218
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.Dest), // 219 SPELL_EFFECT_CREATE_CONVERSATION
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 220 SPELL_EFFECT_ADD_GARRISON_FOLLOWER
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 221 SPELL_EFFECT_ADD_GARRISON_MISSION
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 222 SPELL_EFFECT_CREATE_HEIRLOOM_ITEM
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Item), // 223 SPELL_EFFECT_CHANGE_ITEM_BONUSES
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 224 SPELL_EFFECT_ACTIVATE_GARRISON_BUILDING
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 225 SPELL_EFFECT_GRANT_BATTLEPET_LEVEL
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 226 SPELL_EFFECT_TRIGGER_ACTION_SET
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 227 SPELL_EFFECT_TELEPORT_TO_LFG_DUNGEON
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.None), // 228 SPELL_EFFECT_228
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.None), // 229 SPELL_EFFECT_SET_FOLLOWER_QUALITY
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 230 SPELL_EFFECT_230
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 231 SPELL_EFFECT_INCREASE_FOLLOWER_EXPERIENCE
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 232 SPELL_EFFECT_REMOVE_PHASE
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.None), // 233 SPELL_EFFECT_RANDOMIZE_FOLLOWER_ABILITIES
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.Dest), // 234 SPELL_EFFECT_234
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.None), // 235 SPELL_EFFECT_235
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 236 SPELL_EFFECT_GIVE_EXPERIENCE
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 237 SPELL_EFFECT_GIVE_RESTED_EXPERIENCE_BONUS
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 238 SPELL_EFFECT_INCREASE_SKILL
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 239 SPELL_EFFECT_END_GARRISON_BUILDING_CONSTRUCTION
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 240 SPELL_EFFECT_GIVE_ARTIFACT_POWER
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.None), // 241 SPELL_EFFECT_241
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 242 SPELL_EFFECT_GIVE_ARTIFACT_POWER_NO_BONUS
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Item), // 243 SPELL_EFFECT_APPLY_ENCHANT_ILLUSION
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 244 SPELL_EFFECT_LEARN_FOLLOWER_ABILITY
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.None), // 245 SPELL_EFFECT_UPGRADE_HEIRLOOM
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.None), // 246 SPELL_EFFECT_FINISH_GARRISON_MISSION
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.None), // 247 SPELL_EFFECT_ADD_GARRISON_MISSION_SET
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.None), // 248 SPELL_EFFECT_FINISH_SHIPMENT
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 249 SPELL_EFFECT_FORCE_EQUIP_ITEM
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 250 SPELL_EFFECT_TAKE_SCREENSHOT
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 251 SPELL_EFFECT_SET_GARRISON_CACHE_SIZE
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.UnitAndDest), // 252 SPELL_EFFECT_TELEPORT_UNITS
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 253 SPELL_EFFECT_GIVE_HONOR
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.Dest), // 254 SPELL_EFFECT_JUMP_CHARGE
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 255 SPELL_EFFECT_LEARN_TRANSMOG_SET
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.None), // 256 SPELL_EFFECT_256
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.None), // 257 SPELL_EFFECT_257
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Item), // 258 SPELL_EFFECT_MODIFY_KEYSTONE
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Item), // 259 SPELL_EFFECT_RESPEC_AZERITE_EMPOWERED_ITEM
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.None), // 260 SPELL_EFFECT_SUMMON_STABLED_PET
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Item), // 261 SPELL_EFFECT_SCRAP_ITEM
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.None), // 262 SPELL_EFFECT_262
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Item), // 263 SPELL_EFFECT_REPAIR_ITEM
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Item), // 264 SPELL_EFFECT_REMOVE_GEM
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 265 SPELL_EFFECT_LEARN_AZERITE_ESSENCE_POWER
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Item), // 266 SPELL_EFFECT_SET_ITEM_BONUS_LIST_GROUP_ENTRY
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 267 SPELL_EFFECT_CREATE_PRIVATE_CONVERSATION
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 268 SPELL_EFFECT_APPLY_MOUNT_EQUIPMENT
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Item), // 269 SPELL_EFFECT_INCREASE_ITEM_BONUS_LIST_GROUP_STEP
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 270 SPELL_EFFECT_270
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 271 SPELL_EFFECT_APPLY_AREA_AURA_PARTY_NONRANDOM
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 272 SPELL_EFFECT_SET_COVENANT
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Item), // 273 SPELL_EFFECT_CRAFT_RUNEFORGE_LEGENDARY
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.None), // 274 SPELL_EFFECT_274
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.None), // 275 SPELL_EFFECT_275
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 276 SPELL_EFFECT_LEARN_TRANSMOG_ILLUSION
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 277 SPELL_EFFECT_SET_CHROMIE_TIME
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.None), // 278 SPELL_EFFECT_278
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 279 SPELL_EFFECT_LEARN_GARR_TALENT
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.None), // 280 SPELL_EFFECT_280
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 281 SPELL_EFFECT_LEARN_SOULBIND_CONDUIT
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 282 SPELL_EFFECT_CONVERT_ITEMS_TO_CURRENCY
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 283 SPELL_EFFECT_COMPLETE_CAMPAIGN
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 284 SPELL_EFFECT_SEND_CHAT_MESSAGE
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 285 SPELL_EFFECT_MODIFY_KEYSTONE_2
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 286 SPELL_EFFECT_GRANT_BATTLEPET_EXPERIENCE
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.None), // 287 SPELL_EFFECT_SET_GARRISON_FOLLOWER_LEVEL
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.None), // 288 SPELL_EFFECT_288
            new StaticData(SpellEffectImplicitTargetTypes.Explicit, SpellTargetObjectTypes.Unit), // 289 SPELL_EFFECT_289
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.None), // 241 SPELL_EFFECT_290
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.None), // 241 SPELL_EFFECT_291
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.None), // 241 SPELL_EFFECT_292
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.None), // 241 SPELL_EFFECT_293
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.None), // 241 SPELL_EFFECT_294
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.None), // 241 SPELL_EFFECT_295
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.None), // 241 SPELL_EFFECT_296
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.None), // 241 SPELL_EFFECT_297
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.None), // 241 SPELL_EFFECT_298
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.None), // 241 SPELL_EFFECT_299
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.None), // 241 SPELL_EFFECT_300
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.None), // 241 SPELL_EFFECT_301
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.None), // 241 SPELL_EFFECT_302
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.None), // 241 SPELL_EFFECT_303
            new StaticData(SpellEffectImplicitTargetTypes.None,     SpellTargetObjectTypes.None) // 241 SPELL_EFFECT_304
        };

        #region Fields
        SpellInfo _spellInfo;
        public uint EffectIndex;

        public uint Id = 0;
        public SpellEffectName Effect;
        public AuraType ApplyAuraName;
        public uint ApplyAuraPeriod;
        public float BasePoints;
        public float RealPointsPerLevel;
        public float PointsPerResource;
        public float Amplitude;
        public float ChainAmplitude;
        public float BonusCoefficient;
        public int MiscValue;
        public int MiscValueB;
        public Mechanics Mechanic;
        public float PositionFacing;
        public SpellImplicitTargetInfo TargetA = new();
        public SpellImplicitTargetInfo TargetB = new();
        public SpellRadiusRecord RadiusEntry;
        public SpellRadiusRecord MaxRadiusEntry;
        public int ChainTargets;
        public uint ItemType;
        public uint TriggerSpell;
        public FlagArray128 SpellClassMask;
        public float BonusCoefficientFromAP;
        public SpellEffectAttributes EffectAttributes;
        public ScalingInfo Scaling;

        ImmunityInfo _immunityInfo;
        #endregion

        public struct ScalingInfo
        {
            public int Class;
            public float Coefficient;
            public float Variance;
            public float ResourceCoefficient;
        }

        public SpellEffectRecord ToSpellEffectRecord()
        {
            SpellEffectRecord ret = new SpellEffectRecord();

            ret.Id = Id;
            ret.SpellID = _spellInfo.Id;
            ret.BonusCoefficientFromAP = BonusCoefficientFromAP;
            ret.Coefficient = Scaling.Coefficient;
            ret.DifficultyID = (uint)_spellInfo.Difficulty;
            ret.Effect = (uint)Effect;
            ret.EffectAmplitude = Amplitude;
            ret.EffectAttributes = EffectAttributes;
            ret.EffectAura = (short)ApplyAuraName;
            ret.EffectAuraPeriod = ApplyAuraPeriod;
            ret.EffectBasePoints = BasePoints;
            ret.EffectBonusCoefficient = BonusCoefficient;
            ret.EffectChainAmplitude = ChainAmplitude;
            ret.EffectChainTargets = ChainTargets;
            ret.EffectIndex = (int)EffectIndex;
            ret.EffectMechanic = (int)Mechanic;
            ret.EffectMiscValue[0] = MiscValue;
            ret.EffectMiscValue[1] = MiscValueB;
            ret.EffectPointsPerResource = PointsPerResource;
            ret.EffectPosFacing = PositionFacing;
            ret.EffectRadiusIndex[0] = RadiusEntry.Id;
            ret.EffectRadiusIndex[1] = MaxRadiusEntry.Id;
            ret.EffectRealPointsPerLevel = RealPointsPerLevel;
            ret.EffectSpellClassMask = SpellClassMask;
            ret.EffectTriggerSpell = TriggerSpell;
            ret.ImplicitTarget[0] = (short)TargetA.GetTarget();
            ret.ImplicitTarget[1] = (short)TargetB.GetTarget();
            ret.ResourceCoefficient = Scaling.ResourceCoefficient;
            ret.ScalingClass = Scaling.Class;
            ret.Variance = Scaling.Variance;
            ret.EffectItemType = ItemType;
            ret.PvpMultiplier = 0;
            ret.GroupSizeBasePointsCoefficient = 0;

            return ret;
        }

        public SpellEffectInfo Copy(SpellInfo spellInfo)
        {
            SpellEffectInfo ret = new SpellEffectInfo(spellInfo);

            ret.Id = Id;
            ret.BonusCoefficientFromAP = BonusCoefficientFromAP;
            ret.Scaling.Coefficient = Scaling.Coefficient;
            ret.Effect = Effect;
            ret.Amplitude = Amplitude;
            ret.EffectAttributes = EffectAttributes;
            ret.ApplyAuraName = ApplyAuraName;
            ret.ApplyAuraPeriod = ApplyAuraPeriod;
            ret.BasePoints = BasePoints;
            ret.BonusCoefficient = BonusCoefficient;
            ret.ChainAmplitude = ChainAmplitude;
            ret.ChainTargets = ChainTargets;
            ret.EffectIndex = EffectIndex;
            ret.Mechanic = Mechanic;
            ret.MiscValue = MiscValue;
            ret.MiscValueB = MiscValueB;
            ret.PointsPerResource = PointsPerResource;
            ret.PositionFacing = PositionFacing;
            ret.RadiusEntry = RadiusEntry;
            ret.MaxRadiusEntry = MaxRadiusEntry;
            ret.RealPointsPerLevel = RealPointsPerLevel;
            ret.SpellClassMask = SpellClassMask;
            ret.TriggerSpell = TriggerSpell;
            ret.TargetA = new SpellImplicitTargetInfo(TargetA.GetTarget());
            ret.TargetB = new SpellImplicitTargetInfo(TargetB.GetTarget());
            ret.Scaling.ResourceCoefficient = Scaling.ResourceCoefficient;
            ret.Scaling.Class = Scaling.Class;
            ret.Scaling.Variance = Scaling.Variance;
            ret.ItemType = ItemType;

            return ret;
        }
    }

    public class SpellImplicitTargetInfo
    {
        public SpellImplicitTargetInfo(Targets target = 0)
        {
            _target = target;
        }

        public bool IsArea()
        {
            return GetSelectionCategory() == SpellTargetSelectionCategories.Area || GetSelectionCategory() == SpellTargetSelectionCategories.Cone;
        }

        public SpellTargetSelectionCategories GetSelectionCategory()
        {
            return _data[(int)_target].SelectionCategory;
        }

        public SpellTargetReferenceTypes GetReferenceType()
        {
            return _data[(int)_target].ReferenceType;
        }

        public SpellTargetObjectTypes GetObjectType()
        {
            return _data[(int)_target].ObjectType;
        }

        public SpellTargetCheckTypes GetCheckType()
        {
            return _data[(int)_target].SelectionCheckType;
        }

        SpellTargetDirectionTypes GetDirectionType()
        {
            return _data[(int)_target].DirectionType;
        }

        public Targets GetTarget()
        {
            return _target;
        }

        Targets _target;

        public struct StaticData
        {
            public StaticData(SpellTargetObjectTypes obj, SpellTargetReferenceTypes reference,
                SpellTargetSelectionCategories selection, SpellTargetCheckTypes selectionCheck, SpellTargetDirectionTypes direction)
            {
                ObjectType = obj;
                ReferenceType = reference;
                SelectionCategory = selection;
                SelectionCheckType = selectionCheck;
                DirectionType = direction;
            }
            public SpellTargetObjectTypes ObjectType;    // type of object returned by target type
            public SpellTargetReferenceTypes ReferenceType; // defines which object is used as a reference when selecting target
            public SpellTargetSelectionCategories SelectionCategory;
            public SpellTargetCheckTypes SelectionCheckType; // defines selection criteria
            public SpellTargetDirectionTypes DirectionType; // direction for cone and dest targets
        }

        static StaticData[] _data = new StaticData[(int)Targets.TotalSpellTargets]
        {
            new StaticData(SpellTargetObjectTypes.None,         SpellTargetReferenceTypes.None,   SpellTargetSelectionCategories.Nyi,     SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 0
            new StaticData(SpellTargetObjectTypes.Unit,         SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 1 TARGET_UNIT_CASTER
            new StaticData(SpellTargetObjectTypes.Unit,         SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Nearby,  SpellTargetCheckTypes.Enemy,    SpellTargetDirectionTypes.None),        // 2 TARGET_UNIT_NEARBY_ENEMY
            new StaticData(SpellTargetObjectTypes.Unit,         SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Nearby,  SpellTargetCheckTypes.Ally,     SpellTargetDirectionTypes.None),        // 3 TARGET_UNIT_NEARBY_ALLY
            new StaticData(SpellTargetObjectTypes.Unit,         SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Nearby,  SpellTargetCheckTypes.Party,    SpellTargetDirectionTypes.None),        // 4 TARGET_UNIT_NEARBY_PARTY
            new StaticData(SpellTargetObjectTypes.Unit,         SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 5 TARGET_UNIT_PET
            new StaticData(SpellTargetObjectTypes.Unit,         SpellTargetReferenceTypes.Target, SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Enemy,    SpellTargetDirectionTypes.None),        // 6 TARGET_UNIT_TARGET_ENEMY
            new StaticData(SpellTargetObjectTypes.Unit,         SpellTargetReferenceTypes.Src,    SpellTargetSelectionCategories.Area,    SpellTargetCheckTypes.Entry,    SpellTargetDirectionTypes.None),        // 7 TARGET_UNIT_SRC_AREA_ENTRY
            new StaticData(SpellTargetObjectTypes.Unit,         SpellTargetReferenceTypes.Dest,   SpellTargetSelectionCategories.Area,    SpellTargetCheckTypes.Entry,    SpellTargetDirectionTypes.None),        // 8 TARGET_UNIT_DEST_AREA_ENTRY
            new StaticData(SpellTargetObjectTypes.Dest,         SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 9 TARGET_DEST_HOME
            new StaticData(SpellTargetObjectTypes.None,         SpellTargetReferenceTypes.None,   SpellTargetSelectionCategories.Nyi,     SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 10
            new StaticData(SpellTargetObjectTypes.Unit,         SpellTargetReferenceTypes.Src,    SpellTargetSelectionCategories.Nyi,     SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 11 TARGET_UNIT_SRC_AREA_UNK_11
            new StaticData(SpellTargetObjectTypes.None,         SpellTargetReferenceTypes.None,   SpellTargetSelectionCategories.Nyi,     SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 12
            new StaticData(SpellTargetObjectTypes.None,         SpellTargetReferenceTypes.None,   SpellTargetSelectionCategories.Nyi,     SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 13
            new StaticData(SpellTargetObjectTypes.None,         SpellTargetReferenceTypes.None,   SpellTargetSelectionCategories.Nyi,     SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 14
            new StaticData(SpellTargetObjectTypes.Unit,         SpellTargetReferenceTypes.Src,    SpellTargetSelectionCategories.Area,    SpellTargetCheckTypes.Enemy,    SpellTargetDirectionTypes.None),        // 15 TARGET_UNIT_SRC_AREA_ENEMY
            new StaticData(SpellTargetObjectTypes.Unit,         SpellTargetReferenceTypes.Dest,   SpellTargetSelectionCategories.Area,    SpellTargetCheckTypes.Enemy,    SpellTargetDirectionTypes.None),        // 16 TARGET_UNIT_DEST_AREA_ENEMY
            new StaticData(SpellTargetObjectTypes.Dest,         SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 17 TARGET_DEST_DB
            new StaticData(SpellTargetObjectTypes.Dest,         SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 18 TARGET_DEST_CASTER
            new StaticData(SpellTargetObjectTypes.None,         SpellTargetReferenceTypes.None,   SpellTargetSelectionCategories.Nyi,     SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 19
            new StaticData(SpellTargetObjectTypes.Unit,         SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Area,    SpellTargetCheckTypes.Party,    SpellTargetDirectionTypes.None),        // 20 TARGET_UNIT_CASTER_AREA_PARTY
            new StaticData(SpellTargetObjectTypes.Unit,         SpellTargetReferenceTypes.Target, SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Ally,     SpellTargetDirectionTypes.None),        // 21 TARGET_UNIT_TARGET_ALLY
            new StaticData(SpellTargetObjectTypes.Src,          SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 22 TARGET_SRC_CASTER
            new StaticData(SpellTargetObjectTypes.Gobj,         SpellTargetReferenceTypes.Target, SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 23 TARGET_GAMEOBJECT_TARGET
            new StaticData(SpellTargetObjectTypes.Unit,         SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Cone,    SpellTargetCheckTypes.Enemy,    SpellTargetDirectionTypes.Front),       // 24 TARGET_UNIT_CONE_ENEMY_24
            new StaticData(SpellTargetObjectTypes.Unit,         SpellTargetReferenceTypes.Target, SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 25 TARGET_UNIT_TARGET_ANY
            new StaticData(SpellTargetObjectTypes.GobjItem,     SpellTargetReferenceTypes.Target, SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 26 TARGET_GAMEOBJECT_ITEM_TARGET
            new StaticData(SpellTargetObjectTypes.Unit,         SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 27 TARGET_UNIT_MASTER
            new StaticData(SpellTargetObjectTypes.Dest,         SpellTargetReferenceTypes.Dest,   SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Enemy,    SpellTargetDirectionTypes.None),        // 28 TARGET_DEST_DYNOBJ_ENEMY
            new StaticData(SpellTargetObjectTypes.Dest,         SpellTargetReferenceTypes.Dest,   SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Ally,     SpellTargetDirectionTypes.None),        // 29 TARGET_DEST_DYNOBJ_ALLY
            new StaticData(SpellTargetObjectTypes.Unit,         SpellTargetReferenceTypes.Src,    SpellTargetSelectionCategories.Area,    SpellTargetCheckTypes.Ally,     SpellTargetDirectionTypes.None),        // 30 TARGET_UNIT_SRC_AREA_ALLY
            new StaticData(SpellTargetObjectTypes.Unit,         SpellTargetReferenceTypes.Dest,   SpellTargetSelectionCategories.Area,    SpellTargetCheckTypes.Ally,     SpellTargetDirectionTypes.None),        // 31 TARGET_UNIT_DEST_AREA_ALLY
            new StaticData(SpellTargetObjectTypes.Dest,         SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.FrontLeft),   // 32 TARGET_DEST_CASTER_SUMMON
            new StaticData(SpellTargetObjectTypes.Unit,         SpellTargetReferenceTypes.Src,    SpellTargetSelectionCategories.Area,    SpellTargetCheckTypes.Party,    SpellTargetDirectionTypes.None),        // 33 TARGET_UNIT_SRC_AREA_PARTY
            new StaticData(SpellTargetObjectTypes.Unit,         SpellTargetReferenceTypes.Dest,   SpellTargetSelectionCategories.Area,    SpellTargetCheckTypes.Party,    SpellTargetDirectionTypes.None),        // 34 TARGET_UNIT_DEST_AREA_PARTY
            new StaticData(SpellTargetObjectTypes.Unit,         SpellTargetReferenceTypes.Target, SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Party,    SpellTargetDirectionTypes.None),        // 35 TARGET_UNIT_TARGET_PARTY
            new StaticData(SpellTargetObjectTypes.Dest,         SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Nyi,     SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 36 TARGET_DEST_CASTER_UNK_36
            new StaticData(SpellTargetObjectTypes.Unit,         SpellTargetReferenceTypes.Last,   SpellTargetSelectionCategories.Area,    SpellTargetCheckTypes.Party,    SpellTargetDirectionTypes.None),        // 37 TARGET_UNIT_LASTTARGET_AREA_PARTY
            new StaticData(SpellTargetObjectTypes.Unit,         SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Nearby,  SpellTargetCheckTypes.Entry,    SpellTargetDirectionTypes.None),        // 38 TARGET_UNIT_NEARBY_ENTRY
            new StaticData(SpellTargetObjectTypes.Dest,         SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 39 TARGET_DEST_CASTER_FISHING
            new StaticData(SpellTargetObjectTypes.Gobj,         SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Nearby,  SpellTargetCheckTypes.Entry,    SpellTargetDirectionTypes.None),        // 40 TARGET_GAMEOBJECT_NEARBY_ENTRY
            new StaticData(SpellTargetObjectTypes.Dest,         SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.FrontRight),  // 41 TARGET_DEST_CASTER_FRONT_RIGHT
            new StaticData(SpellTargetObjectTypes.Dest,         SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.BackRight),   // 42 TARGET_DEST_CASTER_BACK_RIGHT
            new StaticData(SpellTargetObjectTypes.Dest,         SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.BackLeft),    // 43 TARGET_DEST_CASTER_BACK_LEFT
            new StaticData(SpellTargetObjectTypes.Dest,         SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.FrontLeft),   // 44 TARGET_DEST_CASTER_FRONT_LEFT
            new StaticData(SpellTargetObjectTypes.Unit,         SpellTargetReferenceTypes.Target, SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Ally,     SpellTargetDirectionTypes.None),        // 45 TARGET_UNIT_TARGET_CHAINHEAL_ALLY
            new StaticData(SpellTargetObjectTypes.Dest,         SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Nearby,  SpellTargetCheckTypes.Entry,    SpellTargetDirectionTypes.None),        // 46 TARGET_DEST_NEARBY_ENTRY
            new StaticData(SpellTargetObjectTypes.Dest,         SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.Front),       // 47 TARGET_DEST_CASTER_FRONT
            new StaticData(SpellTargetObjectTypes.Dest,         SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.Back),        // 48 TARGET_DEST_CASTER_BACK
            new StaticData(SpellTargetObjectTypes.Dest,         SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.Right),       // 49 TARGET_DEST_CASTER_RIGHT
            new StaticData(SpellTargetObjectTypes.Dest,         SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.Left),        // 50 TARGET_DEST_CASTER_LEFT
            new StaticData(SpellTargetObjectTypes.Gobj,         SpellTargetReferenceTypes.Src,    SpellTargetSelectionCategories.Area,    SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 51 TARGET_GAMEOBJECT_SRC_AREA
            new StaticData(SpellTargetObjectTypes.Gobj,         SpellTargetReferenceTypes.Dest,   SpellTargetSelectionCategories.Area,    SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 52 TARGET_GAMEOBJECT_DEST_AREA
            new StaticData(SpellTargetObjectTypes.Dest,         SpellTargetReferenceTypes.Target, SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Enemy,    SpellTargetDirectionTypes.None),        // 53 TARGET_DEST_TARGET_ENEMY
            new StaticData(SpellTargetObjectTypes.Unit,         SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Cone,    SpellTargetCheckTypes.Enemy,    SpellTargetDirectionTypes.Front),       // 54 TARGET_UNIT_CONE_180_DEG_ENEMY
            new StaticData(SpellTargetObjectTypes.Dest,         SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 55 TARGET_DEST_CASTER_FRONT_LEAP
            new StaticData(SpellTargetObjectTypes.Unit,         SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Area,    SpellTargetCheckTypes.Raid,     SpellTargetDirectionTypes.None),        // 56 TARGET_UNIT_CASTER_AREA_RAID
            new StaticData(SpellTargetObjectTypes.Unit,         SpellTargetReferenceTypes.Target, SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Raid,     SpellTargetDirectionTypes.None),        // 57 TARGET_UNIT_TARGET_RAID
            new StaticData(SpellTargetObjectTypes.Unit,         SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Nearby,  SpellTargetCheckTypes.Raid,     SpellTargetDirectionTypes.None),        // 58 TARGET_UNIT_NEARBY_RAID
            new StaticData(SpellTargetObjectTypes.Unit,         SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Cone,    SpellTargetCheckTypes.Ally,     SpellTargetDirectionTypes.Front),       // 59 TARGET_UNIT_CONE_ALLY
            new StaticData(SpellTargetObjectTypes.Unit,         SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Cone,    SpellTargetCheckTypes.Entry,    SpellTargetDirectionTypes.Front),       // 60 TARGET_UNIT_CONE_ENTRY
            new StaticData(SpellTargetObjectTypes.Unit,         SpellTargetReferenceTypes.Target, SpellTargetSelectionCategories.Area,    SpellTargetCheckTypes.RaidClass,SpellTargetDirectionTypes.None),        // 61 TARGET_UNIT_TARGET_AREA_RAID_CLASS
            new StaticData(SpellTargetObjectTypes.Dest,         SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 62 TARGET_DEST_CASTER_GROUND
            new StaticData(SpellTargetObjectTypes.Dest,         SpellTargetReferenceTypes.Target, SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 63 TARGET_DEST_TARGET_ANY
            new StaticData(SpellTargetObjectTypes.Dest,         SpellTargetReferenceTypes.Target, SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.Front),       // 64 TARGET_DEST_TARGET_FRONT
            new StaticData(SpellTargetObjectTypes.Dest,         SpellTargetReferenceTypes.Target, SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.Back),        // 65 TARGET_DEST_TARGET_BACK
            new StaticData(SpellTargetObjectTypes.Dest,         SpellTargetReferenceTypes.Target, SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.Right),       // 66 TARGET_DEST_TARGET_RIGHT
            new StaticData(SpellTargetObjectTypes.Dest,         SpellTargetReferenceTypes.Target, SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.Left),        // 67 TARGET_DEST_TARGET_LEFT
            new StaticData(SpellTargetObjectTypes.Dest,         SpellTargetReferenceTypes.Target, SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.FrontRight),  // 68 TARGET_DEST_TARGET_FRONT_RIGHT
            new StaticData(SpellTargetObjectTypes.Dest,         SpellTargetReferenceTypes.Target, SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.BackRight),   // 69 TARGET_DEST_TARGET_BACK_RIGHT
            new StaticData(SpellTargetObjectTypes.Dest,         SpellTargetReferenceTypes.Target, SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.BackLeft),    // 70 TARGET_DEST_TARGET_BACK_LEFT
            new StaticData(SpellTargetObjectTypes.Dest,         SpellTargetReferenceTypes.Target, SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.FrontLeft),   // 71 TARGET_DEST_TARGET_FRONT_LEFT
            new StaticData(SpellTargetObjectTypes.Dest,         SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.Random),      // 72 TARGET_DEST_CASTER_RANDOM
            new StaticData(SpellTargetObjectTypes.Dest,         SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.Random),      // 73 TARGET_DEST_CASTER_RADIUS
            new StaticData(SpellTargetObjectTypes.Dest,         SpellTargetReferenceTypes.Target, SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.Random),      // 74 TARGET_DEST_TARGET_RANDOM
            new StaticData(SpellTargetObjectTypes.Dest,         SpellTargetReferenceTypes.Target, SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.Random),      // 75 TARGET_DEST_TARGET_RADIUS
            new StaticData(SpellTargetObjectTypes.Dest,         SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Channel, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 76 TARGET_DEST_CHANNEL_TARGET
            new StaticData(SpellTargetObjectTypes.Unit,         SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Channel, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 77 TARGET_UNIT_CHANNEL_TARGET
            new StaticData(SpellTargetObjectTypes.Dest,         SpellTargetReferenceTypes.Dest,   SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.Front),       // 78 TARGET_DEST_DEST_FRONT
            new StaticData(SpellTargetObjectTypes.Dest,         SpellTargetReferenceTypes.Dest,   SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.Back),        // 79 TARGET_DEST_DEST_BACK
            new StaticData(SpellTargetObjectTypes.Dest,         SpellTargetReferenceTypes.Dest,   SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.Right),       // 80 TARGET_DEST_DEST_RIGHT
            new StaticData(SpellTargetObjectTypes.Dest,         SpellTargetReferenceTypes.Dest,   SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.Left),        // 81 TARGET_DEST_DEST_LEFT
            new StaticData(SpellTargetObjectTypes.Dest,         SpellTargetReferenceTypes.Dest,   SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.FrontRight),  // 82 TARGET_DEST_DEST_FRONT_RIGHT
            new StaticData(SpellTargetObjectTypes.Dest,         SpellTargetReferenceTypes.Dest,   SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.BackRight),   // 83 TARGET_DEST_DEST_BACK_RIGHT
            new StaticData(SpellTargetObjectTypes.Dest,         SpellTargetReferenceTypes.Dest,   SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.BackLeft),    // 84 TARGET_DEST_DEST_BACK_LEFT
            new StaticData(SpellTargetObjectTypes.Dest,         SpellTargetReferenceTypes.Dest,   SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.FrontLeft),   // 85 TARGET_DEST_DEST_FRONT_LEFT
            new StaticData(SpellTargetObjectTypes.Dest,         SpellTargetReferenceTypes.Dest,   SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.Random),      // 86 TARGET_DEST_DEST_RANDOM
            new StaticData(SpellTargetObjectTypes.Dest,         SpellTargetReferenceTypes.Dest,   SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 87 TARGET_DEST_DEST
            new StaticData(SpellTargetObjectTypes.Dest,         SpellTargetReferenceTypes.Dest,   SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 88 TARGET_DEST_DYNOBJ_NONE
            new StaticData(SpellTargetObjectTypes.Dest,         SpellTargetReferenceTypes.Dest,   SpellTargetSelectionCategories.Traj   , SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 89 TARGET_DEST_TRAJ
            new StaticData(SpellTargetObjectTypes.Unit,         SpellTargetReferenceTypes.Target, SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 90 TARGET_UNIT_TARGET_MINIPET
            new StaticData(SpellTargetObjectTypes.Dest,         SpellTargetReferenceTypes.Dest,   SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.Random),      // 91 TARGET_DEST_DEST_RADIUS
            new StaticData(SpellTargetObjectTypes.Unit,         SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 92 TARGET_UNIT_SUMMONER
            new StaticData(SpellTargetObjectTypes.Corpse,       SpellTargetReferenceTypes.Src,    SpellTargetSelectionCategories.Area,    SpellTargetCheckTypes.Enemy,    SpellTargetDirectionTypes.None),        // 93 TARGET_CORPSE_SRC_AREA_ENEMY
            new StaticData(SpellTargetObjectTypes.Unit,         SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 94 TARGET_UNIT_VEHICLE
            new StaticData(SpellTargetObjectTypes.Unit,         SpellTargetReferenceTypes.Target, SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Passenger,SpellTargetDirectionTypes.None),        // 95 TARGET_UNIT_TARGET_PASSENGER
            new StaticData(SpellTargetObjectTypes.Unit,         SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 96 TARGET_UNIT_PASSENGER_0
            new StaticData(SpellTargetObjectTypes.Unit,         SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 97 TARGET_UNIT_PASSENGER_1
            new StaticData(SpellTargetObjectTypes.Unit,         SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 98 TARGET_UNIT_PASSENGER_2
            new StaticData(SpellTargetObjectTypes.Unit,         SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 99 TARGET_UNIT_PASSENGER_3
            new StaticData(SpellTargetObjectTypes.Unit,         SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 100 TARGET_UNIT_PASSENGER_4
            new StaticData(SpellTargetObjectTypes.Unit,         SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 101 TARGET_UNIT_PASSENGER_5
            new StaticData(SpellTargetObjectTypes.Unit,         SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 102 TARGET_UNIT_PASSENGER_6
            new StaticData(SpellTargetObjectTypes.Unit,         SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 103 TARGET_UNIT_PASSENGER_7
            new StaticData(SpellTargetObjectTypes.Unit,         SpellTargetReferenceTypes.Dest,   SpellTargetSelectionCategories.Cone,    SpellTargetCheckTypes.Enemy,    SpellTargetDirectionTypes.Front),       // 104 TARGET_UNIT_CONE_CASTER_TO_DEST_ENEMY
            new StaticData(SpellTargetObjectTypes.Unit,         SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Area,    SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 105 TARGET_UNIT_CASTER_AND_PASSENGERS
            new StaticData(SpellTargetObjectTypes.Dest,         SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Channel, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 106 TARGET_DEST_CHANNEL_CASTER
            new StaticData(SpellTargetObjectTypes.Dest,         SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Nearby,  SpellTargetCheckTypes.Entry,    SpellTargetDirectionTypes.None),        // 107 TARGET_DEST_NEARBY_ENTRY_2
            new StaticData(SpellTargetObjectTypes.Gobj,         SpellTargetReferenceTypes.Dest,   SpellTargetSelectionCategories.Cone,    SpellTargetCheckTypes.Enemy,    SpellTargetDirectionTypes.Front),       // 108 TARGET_GAMEOBJECT_CONE_CASTER_TO_DEST_ENEMY
            new StaticData(SpellTargetObjectTypes.Gobj,         SpellTargetReferenceTypes.Dest,   SpellTargetSelectionCategories.Cone,    SpellTargetCheckTypes.Ally,     SpellTargetDirectionTypes.Front),       // 109 TARGET_GAMEOBJECT_CONE_CASTER_TO_DEST_ALLY
            new StaticData(SpellTargetObjectTypes.Unit,         SpellTargetReferenceTypes.Dest,   SpellTargetSelectionCategories.Cone,    SpellTargetCheckTypes.Entry  ,  SpellTargetDirectionTypes.Front),       // 110 TARGET_UNIT_CONE_CASTER_TO_DEST_ENTRY
            new StaticData(SpellTargetObjectTypes.None,         SpellTargetReferenceTypes.None,   SpellTargetSelectionCategories.Nyi,     SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 111
            new StaticData(SpellTargetObjectTypes.Dest,         SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 112
            new StaticData(SpellTargetObjectTypes.Dest,         SpellTargetReferenceTypes.Target, SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 113
            new StaticData(SpellTargetObjectTypes.None,         SpellTargetReferenceTypes.None,   SpellTargetSelectionCategories.Nyi,     SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 114
            new StaticData(SpellTargetObjectTypes.Unit,         SpellTargetReferenceTypes.Src,    SpellTargetSelectionCategories.Area,    SpellTargetCheckTypes.Enemy,    SpellTargetDirectionTypes.None),        // 115 TARGET_UNIT_SRC_AREA_FURTHEST_ENEMY
            new StaticData(SpellTargetObjectTypes.UnitAndDest,  SpellTargetReferenceTypes.Last,   SpellTargetSelectionCategories.Area,    SpellTargetCheckTypes.Enemy,    SpellTargetDirectionTypes.None),        // 116 TARGET_UNIT_AND_DEST_LAST_ENEMY
            new StaticData(SpellTargetObjectTypes.None,         SpellTargetReferenceTypes.None,   SpellTargetSelectionCategories.Nyi,     SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 117
            new StaticData(SpellTargetObjectTypes.Unit,         SpellTargetReferenceTypes.Target, SpellTargetSelectionCategories.Area,    SpellTargetCheckTypes.Raid,     SpellTargetDirectionTypes.None),        // 118 TARGET_UNIT_TARGET_ALLY_OR_RAID
            new StaticData(SpellTargetObjectTypes.Corpse,       SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Area,    SpellTargetCheckTypes.Raid,     SpellTargetDirectionTypes.None),        // 119 TARGET_CORPSE_SRC_AREA_RAID
            new StaticData(SpellTargetObjectTypes.Unit,         SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Area,    SpellTargetCheckTypes.Summoned, SpellTargetDirectionTypes.None),        // 120 TARGET_UNIT_SELF_AND_SUMMONS
            new StaticData(SpellTargetObjectTypes.Corpse,       SpellTargetReferenceTypes.Target, SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Ally,     SpellTargetDirectionTypes.None),        // 121 TARGET_CORPSE_TARGET_ALLY
            new StaticData(SpellTargetObjectTypes.Unit,         SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Area,    SpellTargetCheckTypes.Threat,   SpellTargetDirectionTypes.None),        // 122 TARGET_UNIT_AREA_THREAT_LIST
            new StaticData(SpellTargetObjectTypes.Unit,         SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Area,    SpellTargetCheckTypes.Tap,      SpellTargetDirectionTypes.None),        // 123 TARGET_UNIT_AREA_TAP_LIST
            new StaticData(SpellTargetObjectTypes.Unit,         SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 124 TARGET_UNIT_TARGET_TAP_LIST
            new StaticData(SpellTargetObjectTypes.Dest,         SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 125 TARGET_DEST_CASTER_GROUND_2
            new StaticData(SpellTargetObjectTypes.Unit,         SpellTargetReferenceTypes.None,   SpellTargetSelectionCategories.Nyi,     SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 126 TARGET_UNIT_CASTER_AREA_ENEMY_CLUMP
            new StaticData(SpellTargetObjectTypes.Dest,         SpellTargetReferenceTypes.None,   SpellTargetSelectionCategories.Nyi,     SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 127 TARGET_DEST_CASTER_ENEMY_CLUMP_CENTROID
            new StaticData(SpellTargetObjectTypes.Unit,         SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Cone,    SpellTargetCheckTypes.Ally,     SpellTargetDirectionTypes.Front),       // 128 TARGET_UNIT_RECT_CASTER_ALLY
            new StaticData(SpellTargetObjectTypes.Unit,         SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Cone,    SpellTargetCheckTypes.Entry,    SpellTargetDirectionTypes.Front),       // 129 TARGET_UNIT_RECT_CASTER_ENEMY
            new StaticData(SpellTargetObjectTypes.Unit,         SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Cone,    SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.Front),       // 130 TARGET_UNIT_RECT_CASTER
            new StaticData(SpellTargetObjectTypes.Dest,         SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 131 TARGET_DEST_SUMMONER
            new StaticData(SpellTargetObjectTypes.Dest,         SpellTargetReferenceTypes.Target, SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Ally,     SpellTargetDirectionTypes.None),        // 132 TARGET_DEST_TARGET_ALLY
            new StaticData(SpellTargetObjectTypes.Unit,         SpellTargetReferenceTypes.Dest,   SpellTargetSelectionCategories.Line,    SpellTargetCheckTypes.Ally,     SpellTargetDirectionTypes.None),        // 133 TARGET_UNIT_LINE_CASTER_TO_DEST_ALLY
            new StaticData(SpellTargetObjectTypes.Unit,         SpellTargetReferenceTypes.Dest,   SpellTargetSelectionCategories.Line,    SpellTargetCheckTypes.Enemy,    SpellTargetDirectionTypes.None),        // 134 TARGET_UNIT_LINE_CASTER_TO_DEST_ENEMY
            new StaticData(SpellTargetObjectTypes.Unit,         SpellTargetReferenceTypes.Dest,   SpellTargetSelectionCategories.Line,    SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 135 TARGET_UNIT_LINE_CASTER_TO_DEST
            new StaticData(SpellTargetObjectTypes.Unit,         SpellTargetReferenceTypes.Dest,   SpellTargetSelectionCategories.Cone,    SpellTargetCheckTypes.Ally,     SpellTargetDirectionTypes.Front),       // 136 TARGET_UNIT_CONE_CASTER_TO_DEST_ALLY
            new StaticData(SpellTargetObjectTypes.Dest,         SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 137 TARGET_DEST_CASTER_MOVEMENT_DIRECTION
            new StaticData(SpellTargetObjectTypes.Dest,         SpellTargetReferenceTypes.Dest,   SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 138 TARGET_DEST_DEST_GROUND
            new StaticData(SpellTargetObjectTypes.None,         SpellTargetReferenceTypes.None,   SpellTargetSelectionCategories.Nyi,     SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 139
            new StaticData(SpellTargetObjectTypes.Dest,         SpellTargetReferenceTypes.None,   SpellTargetSelectionCategories.Nyi,     SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 140 TARGET_DEST_CASTER_CLUMP_CENTROID
            new StaticData(SpellTargetObjectTypes.None,         SpellTargetReferenceTypes.None,   SpellTargetSelectionCategories.Nyi,     SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 141
            new StaticData(SpellTargetObjectTypes.None,         SpellTargetReferenceTypes.None,   SpellTargetSelectionCategories.Nyi,     SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 142
            new StaticData(SpellTargetObjectTypes.None,         SpellTargetReferenceTypes.None,   SpellTargetSelectionCategories.Nyi,     SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 143
            new StaticData(SpellTargetObjectTypes.None,         SpellTargetReferenceTypes.None,   SpellTargetSelectionCategories.Nyi,     SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 144
            new StaticData(SpellTargetObjectTypes.None,         SpellTargetReferenceTypes.None,   SpellTargetSelectionCategories.Nyi,     SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 145
            new StaticData(SpellTargetObjectTypes.None,         SpellTargetReferenceTypes.None,   SpellTargetSelectionCategories.Nyi,     SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 146
            new StaticData(SpellTargetObjectTypes.None,         SpellTargetReferenceTypes.None,   SpellTargetSelectionCategories.Nyi,     SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 147
            new StaticData(SpellTargetObjectTypes.None,         SpellTargetReferenceTypes.None,   SpellTargetSelectionCategories.Nyi,     SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 148
            new StaticData(SpellTargetObjectTypes.Dest,         SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.Random),      // 149
            new StaticData(SpellTargetObjectTypes.Unit,         SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Default, SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 150 TARGET_UNIT_OWN_CRITTER
            new StaticData(SpellTargetObjectTypes.Unit,         SpellTargetReferenceTypes.Caster, SpellTargetSelectionCategories.Area,    SpellTargetCheckTypes.Enemy,    SpellTargetDirectionTypes.None),        // 151
            new StaticData(SpellTargetObjectTypes.None,         SpellTargetReferenceTypes.None,   SpellTargetSelectionCategories.Nyi,     SpellTargetCheckTypes.Default,  SpellTargetDirectionTypes.None),        // 152
        };
    }

    public class SpellPowerCost
    {
        public PowerType Power;
        public int Amount;
    }

    class SpellDiminishInfo
    {
        public DiminishingGroup DiminishGroup = DiminishingGroup.None;
        public DiminishingReturnsType DiminishReturnType = DiminishingReturnsType.None;
        public DiminishingLevels DiminishMaxLevel = DiminishingLevels.Immune;
        public int DiminishDurationLimit;
    }

    public class ImmunityInfo
    {
        public uint SchoolImmuneMask;
        public uint ApplyHarmfulAuraImmuneMask;
        public uint MechanicImmuneMask;
        public uint DispelImmune;
        public uint DamageSchoolMask;

        public List<AuraType> AuraTypeImmune = new();
        public List<SpellEffectName> SpellEffectImmune = new();
    }
}

