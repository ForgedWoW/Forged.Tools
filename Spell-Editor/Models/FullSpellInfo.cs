using Framework.Constants;
using Framework.Database;
using Framework.Dynamic;
using Game.DataStorage;
using Game.Entities;
using Game.Spells;
using Spell_Editor.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spell_Editor.Models
{
    public sealed class FullSpellInfo
    {
        public SpellInfo SpellInfo;
        public SpellRecord SpellDescriptions;

        public FullSpellInfo()
        {
            this.SpellInfo = new SpellInfo();
            SpellDescriptions = new SpellRecord();
        }
        public FullSpellInfo(SpellInfo spellInfo, SpellRecord spell)
        {
            this.SpellInfo = spellInfo;
            SpellDescriptions = spell;
        }

        public void Save()
        {
            // Spell_Name
            SpellNameRecord spellNameRecord = new SpellNameRecord();
            spellNameRecord.Name = SpellInfo.SpellName;
            spellNameRecord.Id = SpellInfo.Id;

            var nameStmt = new PreparedStatement(DataAccess.UPDATE_SPELL_NAME);
            nameStmt.AddValue(0, spellNameRecord.Id);
            nameStmt.AddValue(1, spellNameRecord.Name[Locale.enUS]);

            DataAccess.UpdateHotfixDB(nameStmt);
            CliDB.SpellNameStorage[spellNameRecord.Id] = spellNameRecord;

            // spell
            SpellRecord spellRecord = new SpellRecord();
            spellRecord.Id = SpellInfo.Id;
            spellRecord.NameSubtext_lang = SpellDescriptions.NameSubtext_lang;
            spellRecord.Description_lang = SpellDescriptions.Description_lang;
            spellRecord.AuraDescription_lang = SpellDescriptions.AuraDescription_lang;

            var spellStmt = new PreparedStatement(DataAccess.UPDATE_SPELL);
            spellStmt.AddValue(0, spellRecord.Id);
            spellStmt.AddValue(1, spellRecord.NameSubtext_lang);
            spellStmt.AddValue(2, spellRecord.Description_lang);
            spellStmt.AddValue(3, spellRecord.AuraDescription_lang);

            DataAccess.UpdateHotfixDB(spellStmt);
            DataAccess.SpellStorage[spellRecord.Id] = spellRecord;

            // spell_effect
            var idsSeStmt = new PreparedStatement(DataAccess.SELECT_IDS_BUILD_SPELL_EFFECTS);
            idsSeStmt.AddValue(0, SpellInfo.Id);
            List<uint> seIds = DataAccess.GetHotfixValues<uint>(idsSeStmt);

            foreach (uint id in seIds)
                CliDB.SpellEffectStorage.Remove(id);

            var cleanSeStmt = new PreparedStatement(DataAccess.DELETE_BUILD_SPELL_EFFECTS);
            cleanSeStmt.AddValue(0, SpellInfo.Id);
            DataAccess.UpdateHotfixDB(cleanSeStmt);

            foreach (var spellEffect in SpellInfo.GetEffects())
            {
                var spellEffectRec = spellEffect.ToSpellEffectRecord();
                var seStmt = new PreparedStatement(DataAccess.UPDATE_SPELL_EFFECT);
                seStmt.AddValue(0, spellEffectRec.Id);
                seStmt.AddValue(1, spellEffectRec.EffectAura);
                seStmt.AddValue(2, spellEffectRec.DifficultyID);
                seStmt.AddValue(3, spellEffectRec.EffectIndex);
                seStmt.AddValue(4, spellEffectRec.Effect);
                seStmt.AddValue(5, spellEffectRec.EffectAmplitude);
                seStmt.AddValue(6, (float)spellEffectRec.EffectAttributes);
                seStmt.AddValue(7, spellEffectRec.EffectAuraPeriod);
                seStmt.AddValue(8, spellEffectRec.EffectBonusCoefficient);
                seStmt.AddValue(9, spellEffectRec.EffectChainAmplitude);
                seStmt.AddValue(10, spellEffectRec.EffectChainTargets);
                seStmt.AddValue(11, spellEffectRec.EffectItemType);
                seStmt.AddValue(12, spellEffectRec.EffectMechanic);
                seStmt.AddValue(13, spellEffectRec.EffectPointsPerResource);
                seStmt.AddValue(14, spellEffectRec.EffectPosFacing);
                seStmt.AddValue(15, spellEffectRec.EffectRealPointsPerLevel);
                seStmt.AddValue(16, spellEffectRec.EffectTriggerSpell);
                seStmt.AddValue(17, spellEffectRec.BonusCoefficientFromAP);
                seStmt.AddValue(18, spellEffectRec.PvpMultiplier);
                seStmt.AddValue(19, spellEffectRec.Coefficient);
                seStmt.AddValue(20, spellEffectRec.Variance);
                seStmt.AddValue(21, spellEffectRec.ResourceCoefficient);
                seStmt.AddValue(22, spellEffectRec.GroupSizeBasePointsCoefficient);
                seStmt.AddValue(23, spellEffectRec.EffectBasePoints);
                seStmt.AddValue(24, spellEffectRec.ScalingClass);
                seStmt.AddValue(25, spellEffectRec.EffectMiscValue[0]);
                seStmt.AddValue(26, spellEffectRec.EffectMiscValue[1]);
                seStmt.AddValue(27, spellEffectRec.EffectRadiusIndex[0]);
                seStmt.AddValue(28, spellEffectRec.EffectRadiusIndex[1]);
                seStmt.AddValue(29, spellEffectRec.EffectSpellClassMask[0]);
                seStmt.AddValue(30, spellEffectRec.EffectSpellClassMask[1]);
                seStmt.AddValue(31, spellEffectRec.EffectSpellClassMask[2]);
                seStmt.AddValue(32, spellEffectRec.EffectSpellClassMask[3]);
                seStmt.AddValue(33, spellEffectRec.ImplicitTarget[0]);
                seStmt.AddValue(34, spellEffectRec.ImplicitTarget[1]);
                seStmt.AddValue(35, spellEffectRec.SpellID);
                DataAccess.UpdateHotfixDB(seStmt);

                CliDB.SpellEffectStorage[spellEffectRec.Id] = spellEffectRec;
            }

            // Spell_Misc
            SpellMiscRecord misc = SpellInfo.GetSpellMiscRecord();

            var miscStmt = new PreparedStatement(DataAccess.UPDATE_SPELL_MISC);
            miscStmt.AddValue(0, misc.Id);
            miscStmt.AddValue(1, misc.Attributes[0]);
            miscStmt.AddValue(2, misc.Attributes[1]);
            miscStmt.AddValue(3, misc.Attributes[2]);
            miscStmt.AddValue(4, misc.Attributes[3]);
            miscStmt.AddValue(5, misc.Attributes[4]);
            miscStmt.AddValue(6, misc.Attributes[5]);
            miscStmt.AddValue(7, misc.Attributes[6]);
            miscStmt.AddValue(8, misc.Attributes[7]);
            miscStmt.AddValue(9, misc.Attributes[8]);
            miscStmt.AddValue(10, misc.Attributes[9]);
            miscStmt.AddValue(11, misc.Attributes[10]);
            miscStmt.AddValue(12, misc.Attributes[11]);
            miscStmt.AddValue(13, misc.Attributes[12]);
            miscStmt.AddValue(14, misc.Attributes[13]);
            miscStmt.AddValue(15, misc.Attributes[14]);
            miscStmt.AddValue(16, misc.DifficultyID);
            miscStmt.AddValue(17, misc.CastingTimeIndex);
            miscStmt.AddValue(18, misc.DurationIndex);
            miscStmt.AddValue(19, misc.RangeIndex);
            miscStmt.AddValue(20, misc.SchoolMask);
            miscStmt.AddValue(21, misc.Speed);
            miscStmt.AddValue(22, misc.LaunchDelay);
            miscStmt.AddValue(23, misc.MinDuration);
            miscStmt.AddValue(24, misc.SpellIconFileDataID);
            miscStmt.AddValue(25, misc.ActiveIconFileDataID);
            miscStmt.AddValue(26, misc.ContentTuningID);
            miscStmt.AddValue(27, misc.ShowFutureSpellPlayerConditionID);
            miscStmt.AddValue(28, misc.SpellVisualScript);
            miscStmt.AddValue(29, misc.ActiveSpellVisualScript);
            miscStmt.AddValue(30, misc.SpellID);
            DataAccess.UpdateHotfixDB(miscStmt);

            CliDB.SpellMiscStorage[misc.Id] = misc;

            // Spell_Scaling
            SpellScalingRecord scale = SpellInfo.GetSpellScalingRecord();
            var scalingStmt = new PreparedStatement(DataAccess.UPDATE_SPELL_SCALING);
            scalingStmt.AddValue(0, scale.Id);
            scalingStmt.AddValue(1, scale.SpellID);
            scalingStmt.AddValue(2, scale.MinScalingLevel);
            scalingStmt.AddValue(3, scale.MaxScalingLevel);
            scalingStmt.AddValue(4, scale.ScalesFromItemLevel);
            DataAccess.UpdateHotfixDB(scalingStmt);

            CliDB.SpellScalingStorage[scale.Id] = scale;

            // Spell_Aura_Options
            SpellAuraOptionsRecord aurOptions = SpellInfo.GetSpellAuraOptionsRecord();
            var auraOptionsStmt = new PreparedStatement(DataAccess.UPDATE_SPELL_AURA_OPTIONS);
            auraOptionsStmt.AddValue(0, aurOptions.Id);
            auraOptionsStmt.AddValue(1, aurOptions.DifficultyID);
            auraOptionsStmt.AddValue(2, aurOptions.CumulativeAura);
            auraOptionsStmt.AddValue(3, aurOptions.ProcCategoryRecovery);
            auraOptionsStmt.AddValue(4, aurOptions.ProcChance);
            auraOptionsStmt.AddValue(5, aurOptions.ProcCharges);
            auraOptionsStmt.AddValue(6, aurOptions.SpellProcsPerMinuteID);
            auraOptionsStmt.AddValue(7, aurOptions.ProcTypeMask[0]);
            auraOptionsStmt.AddValue(8, aurOptions.ProcTypeMask[1]);
            auraOptionsStmt.AddValue(9, aurOptions.SpellID);
            DataAccess.UpdateHotfixDB(auraOptionsStmt);

            CliDB.SpellAuraOptionsStorage[aurOptions.Id] = aurOptions;

            // Spell_Aura_Restrictions
            SpellAuraRestrictionsRecord aurRestrictions = SpellInfo.GetSpellAuraRestrictionsRecord();
            var aurRestrictionsStmt = new PreparedStatement(DataAccess.UPDATE_SPELL_AURA_RESTRICTIONS);
            aurRestrictionsStmt.AddValue(0, aurRestrictions.Id);
            aurRestrictionsStmt.AddValue(1, aurRestrictions.DifficultyID);
            aurRestrictionsStmt.AddValue(2, aurRestrictions.CasterAuraState);
            aurRestrictionsStmt.AddValue(3, aurRestrictions.TargetAuraState);
            aurRestrictionsStmt.AddValue(4, aurRestrictions.ExcludeCasterAuraState);
            aurRestrictionsStmt.AddValue(5, aurRestrictions.ExcludeTargetAuraState);
            aurRestrictionsStmt.AddValue(6, aurRestrictions.CasterAuraSpell);
            aurRestrictionsStmt.AddValue(7, aurRestrictions.TargetAuraSpell);
            aurRestrictionsStmt.AddValue(8, aurRestrictions.ExcludeCasterAuraSpell);
            aurRestrictionsStmt.AddValue(9, aurRestrictions.ExcludeTargetAuraSpell);
            aurRestrictionsStmt.AddValue(10, aurRestrictions.SpellID);
            DataAccess.UpdateHotfixDB(aurRestrictionsStmt);

            CliDB.SpellAuraRestrictionsStorage[aurRestrictions.Id] = aurRestrictions;

            // SpellCastingRequirementsEntry
            SpellCastingRequirementsRecord castreq = SpellInfo.GetSpellCastingRequirementsRecord();
            var castReqStmt = new PreparedStatement(DataAccess.UPDATE_SPELL_CASTING_REQUIREMENTS);
            castReqStmt.AddValue(0, castreq.Id);
            castReqStmt.AddValue(1, castreq.SpellID);
            castReqStmt.AddValue(2, castreq.FacingCasterFlags);
            castReqStmt.AddValue(3, castreq.MinFactionID);
            castReqStmt.AddValue(4, castreq.MinReputation);
            castReqStmt.AddValue(5, castreq.RequiredAreasID);
            castReqStmt.AddValue(6, castreq.RequiredAuraVision);
            castReqStmt.AddValue(7, castreq.RequiresSpellFocus);
            DataAccess.UpdateHotfixDB(castReqStmt);

            CliDB.SpellCastingRequirementsStorage[castreq.Id] = castreq;

            // SpellCategoriesEntry
            SpellCategoriesRecord categorie = SpellInfo.GetSpellCategoriesRecord();
            var catStmt = new PreparedStatement(DataAccess.UPDATE_SPELL_CATEGORIES);
            catStmt.AddValue(0, categorie.Id);
            catStmt.AddValue(1, categorie.DifficultyID);
            catStmt.AddValue(2, categorie.Category);
            catStmt.AddValue(3, categorie.DefenseType);
            catStmt.AddValue(4, categorie.DispelType);
            catStmt.AddValue(5, categorie.Mechanic);
            catStmt.AddValue(6, categorie.PreventionType);
            catStmt.AddValue(7, categorie.StartRecoveryCategory);
            catStmt.AddValue(8, categorie.ChargeCategory);
            catStmt.AddValue(9, categorie.SpellID);
            DataAccess.UpdateHotfixDB(catStmt);

            CliDB.SpellCategoriesStorage[categorie.Id] = categorie;

            // SpellClassOptionsRecord
            SpellClassOptionsRecord coRecord = SpellInfo.GetSpellClassOptionsRecord();
            var coStmt = new PreparedStatement(DataAccess.UPDATE_SPELL_CLASS_OPTIONS);
            coStmt.AddValue(0, coRecord.Id);
            coStmt.AddValue(1, coRecord.SpellID);
            coStmt.AddValue(2, coRecord.ModalNextSpell);
            coStmt.AddValue(3, coRecord.SpellClassSet);
            coStmt.AddValue(4, coRecord.SpellClassMask[0]);
            coStmt.AddValue(5, coRecord.SpellClassMask[1]);
            coStmt.AddValue(6, coRecord.SpellClassMask[2]);
            coStmt.AddValue(7, coRecord.SpellClassMask[3]);

            DataAccess.UpdateHotfixDB(coStmt);
            CliDB.SpellClassOptionsStorage[coRecord.Id] = coRecord;

            // SpellCooldownsEntry
            SpellCooldownsRecord cdr = SpellInfo.GetSpellCooldownsRecord();
            var cdStmt = new PreparedStatement(DataAccess.UPDATE_SPELL_COOLDOWNS);
            cdStmt.AddValue(0, cdr.Id);
            cdStmt.AddValue(1, cdr.DifficultyID);
            cdStmt.AddValue(2, cdr.CategoryRecoveryTime);
            cdStmt.AddValue(3, cdr.RecoveryTime);
            cdStmt.AddValue(4, cdr.StartRecoveryTime);
            cdStmt.AddValue(5, cdr.SpellID);

            DataAccess.UpdateHotfixDB(cdStmt);
            CliDB.SpellCooldownsStorage[cdr.Id] = cdr;

            // SpellEquippedItemsEntry
            SpellEquippedItemsRecord equipped = SpellInfo.GetSpellEquippedItemsRecord();
            var equipStmt = new PreparedStatement(DataAccess.UPDATE_SPELL_EQUIPPED_ITEMS);
            equipStmt.AddValue(0, equipped.Id);
            equipStmt.AddValue(1, equipped.SpellID);
            equipStmt.AddValue(2, equipped.EquippedItemClass);
            equipStmt.AddValue(3, equipped.EquippedItemInvTypes);
            equipStmt.AddValue(4, equipped.EquippedItemSubclass);

            DataAccess.UpdateHotfixDB(equipStmt);
            CliDB.SpellEquippedItemsStorage[equipped.Id] = equipped;

            // SpellInterruptsEntry
            SpellInterruptsRecord interrupt = SpellInfo.GetSpellInterruptsRecord();
            var interruptStmt = new PreparedStatement(DataAccess.UPDATE_SPELL_INTERRUPTS);
            interruptStmt.AddValue(0, interrupt.Id);
            interruptStmt.AddValue(1, interrupt.DifficultyID);
            interruptStmt.AddValue(2, interrupt.InterruptFlags);
            interruptStmt.AddValue(3, interrupt.AuraInterruptFlags[0]);
            interruptStmt.AddValue(4, interrupt.AuraInterruptFlags[1]);
            interruptStmt.AddValue(5, interrupt.ChannelInterruptFlags[0]);
            interruptStmt.AddValue(6, interrupt.ChannelInterruptFlags[1]);
            interruptStmt.AddValue(7, interrupt.SpellID);

            DataAccess.UpdateHotfixDB(interruptStmt);
            CliDB.SpellInterruptsStorage[interrupt.Id] = interrupt;

            // Spell_Label
            List<SpellLabelRecord> labels = SpellInfo.GetSpellLabelRecords();

            var idsLblStmt = new PreparedStatement(DataAccess.SELECT_IDS_BUILD_SPELL_LABEL);
            idsLblStmt.AddValue(0, SpellInfo.Id);
            List<uint> lblIds = DataAccess.GetHotfixValues<uint>(idsLblStmt);

            foreach (uint id in lblIds)
                CliDB.SpellLabelStorage.Remove(id);

            var cleanLblStmt = new PreparedStatement(DataAccess.DELETE_BUILD_SPELL_LABEL);
            cleanLblStmt.AddValue(0, SpellInfo.Id);
            DataAccess.UpdateHotfixDB(cleanLblStmt);

            foreach (var label in labels)
            {
                var lblStmt = new PreparedStatement(DataAccess.UPDATE_SPELL_LABEL);
                lblStmt.AddValue(0, label.Id);
                lblStmt.AddValue(1, label.LabelID);
                lblStmt.AddValue(2, label.SpellID);

                DataAccess.UpdateHotfixDB(lblStmt);
                CliDB.SpellLabelStorage[label.Id] = label;
            }

            // SpellLevelsEntry
            SpellLevelsRecord levels = SpellInfo.GetSpellLevelsRecord();
            var levelsStmt = new PreparedStatement(DataAccess.UPDATE_SPELL_LEVELS);
            levelsStmt.AddValue(0, levels.Id);
            levelsStmt.AddValue(1, levels.DifficultyID);
            levelsStmt.AddValue(2, levels.MaxLevel);
            levelsStmt.AddValue(3, levels.MaxPassiveAuraLevel);
            levelsStmt.AddValue(4, levels.BaseLevel);
            levelsStmt.AddValue(5, levels.SpellLevel);
            levelsStmt.AddValue(6, levels.SpellID);

            DataAccess.UpdateHotfixDB(levelsStmt);
            CliDB.SpellLevelsStorage[levels.Id] = levels;

            uint curMaxPwr = 0;
            foreach (var power in SpellInfo.PowerCosts)
            {
                if (power.Id == 0)
                {
                    if (curMaxPwr == 0)
                        curMaxPwr = Helpers.SelectGreater(CliDB.SpellPowerStorage.OrderByDescending(a => a.Key).First().Key, DataAccess.GetLatestID("spell_power"));

                    curMaxPwr++;
                    power.Id = curMaxPwr;
                }

                var pwrStmt = new PreparedStatement(DataAccess.UPDATE_SPELL_POWER);
                pwrStmt.AddValue(0, power.Id);
                pwrStmt.AddValue(1, power.OrderIndex);
                pwrStmt.AddValue(2, power.ManaCost);
                pwrStmt.AddValue(3, power.ManaCostPerLevel);
                pwrStmt.AddValue(4, power.ManaPerSecond);
                pwrStmt.AddValue(5, power.PowerDisplayID);
                pwrStmt.AddValue(6, power.AltPowerBarID);
                pwrStmt.AddValue(7, power.PowerCostPct);
                pwrStmt.AddValue(8, power.PowerCostMaxPct);
                pwrStmt.AddValue(9, power.PowerPctPerSecond);
                pwrStmt.AddValue(10, (sbyte)power.PowerType);
                pwrStmt.AddValue(11, power.RequiredAuraSpellID);
                pwrStmt.AddValue(12, power.OptionalCost);
                pwrStmt.AddValue(13, power.SpellID);

                DataAccess.UpdateHotfixDB(pwrStmt);
                CliDB.SpellPowerStorage[power.Id] = power;
            }

            // SpellReagentsEntry
            SpellReagentsRecord reagents = SpellInfo.GetSpellReagentsRecord();
            var reagentsStmt = new PreparedStatement(DataAccess.UPDATE_SPELL_REAGENTS);
            reagentsStmt.AddValue(0, reagents.Id);
            reagentsStmt.AddValue(1, reagents.SpellID);
            reagentsStmt.AddValue(2, reagents.Reagent[0]);
            reagentsStmt.AddValue(3, reagents.Reagent[1]);
            reagentsStmt.AddValue(4, reagents.Reagent[2]);
            reagentsStmt.AddValue(5, reagents.Reagent[3]);
            reagentsStmt.AddValue(6, reagents.Reagent[4]);
            reagentsStmt.AddValue(7, reagents.Reagent[5]);
            reagentsStmt.AddValue(8, reagents.Reagent[6]);
            reagentsStmt.AddValue(9, reagents.Reagent[7]);
            reagentsStmt.AddValue(10, reagents.ReagentCount[0]);
            reagentsStmt.AddValue(11, reagents.ReagentCount[1]);
            reagentsStmt.AddValue(12, reagents.ReagentCount[2]);
            reagentsStmt.AddValue(13, reagents.ReagentCount[3]);
            reagentsStmt.AddValue(14, reagents.ReagentCount[4]);
            reagentsStmt.AddValue(15, reagents.ReagentCount[5]);
            reagentsStmt.AddValue(16, reagents.ReagentCount[6]);
            reagentsStmt.AddValue(17, reagents.ReagentCount[7]);

            DataAccess.UpdateHotfixDB(reagentsStmt);
            CliDB.SpellReagentsStorage[reagents.Id] = reagents;

            // SpellReagentsCurrency
            var idsCurrencyStmt = new PreparedStatement(DataAccess.SELECT_IDS_BUILD_SPELL_REAGENTS_CURRENCY);
            idsCurrencyStmt.AddValue(0, SpellInfo.Id);
            List<uint> currIds = DataAccess.GetHotfixValues<uint>(idsCurrencyStmt);

            foreach (uint id in currIds)
                CliDB.SpellReagentsCurrencyStorage.Remove(id);

            var cleanCurrencyStmt = new PreparedStatement(DataAccess.DELETE_BUILD_SPELL_REAGENTS_CURRENCY);
            cleanCurrencyStmt.AddValue(0, SpellInfo.Id);
            DataAccess.UpdateHotfixDB(cleanCurrencyStmt);

            foreach (SpellReagentsCurrencyRecord currency in SpellInfo.ReagentsCurrency)
            {
                currency.SpellID = (int)SpellInfo.Id;

                var currencyStmt = new PreparedStatement(DataAccess.UPDATE_SPELL_REAGENTS_CURRENCY);
                currencyStmt.AddValue(0, currency.Id);
                currencyStmt.AddValue(1, currency.SpellID);
                currencyStmt.AddValue(2, currency.CurrencyTypesID);
                currencyStmt.AddValue(3, currency.CurrencyCount);

                DataAccess.UpdateHotfixDB(currencyStmt);
                CliDB.SpellReagentsCurrencyStorage[currency.Id] = currency;
            }

            // SpellShapeshiftEntry
            SpellShapeshiftRecord shapeshift = SpellInfo.GetSpellShapeshiftRecord();
            var shapeshiftStmt = new PreparedStatement(DataAccess.UPDATE_SPELL_SHAPESHIFT);
            shapeshiftStmt.AddValue(0, shapeshift.Id);
            shapeshiftStmt.AddValue(1, shapeshift.SpellID);
            shapeshiftStmt.AddValue(2, shapeshift.StanceBarOrder);
            shapeshiftStmt.AddValue(3, shapeshift.ShapeshiftExclude[0]);
            shapeshiftStmt.AddValue(4, shapeshift.ShapeshiftExclude[1]);
            shapeshiftStmt.AddValue(5, shapeshift.ShapeshiftMask[0]);
            shapeshiftStmt.AddValue(6, shapeshift.ShapeshiftMask[1]);

            DataAccess.UpdateHotfixDB(shapeshiftStmt);
            CliDB.SpellShapeshiftStorage[shapeshift.Id] = shapeshift;

            // SpellTargetRestrictionsEntry
            SpellTargetRestrictionsRecord target = SpellInfo.GetSpellTargetRestrictionsRecord();
            var targetStmt = new PreparedStatement(DataAccess.UPDATE_SPELL_TARGET_RESTRICTIONS);
            targetStmt.AddValue(0, target.Id);
            targetStmt.AddValue(1, target.DifficultyID);
            targetStmt.AddValue(2, target.ConeDegrees);
            targetStmt.AddValue(3, target.MaxTargets);
            targetStmt.AddValue(4, target.MaxTargetLevel);
            targetStmt.AddValue(5, target.TargetCreatureType);
            targetStmt.AddValue(6, target.Targets);
            targetStmt.AddValue(7, target.Width);
            targetStmt.AddValue(8, target.SpellID);

            DataAccess.UpdateHotfixDB(targetStmt);
            CliDB.SpellTargetRestrictionsStorage[target.Id] = target;

            // SpellTotemsEntry
            SpellTotemsRecord totem = SpellInfo.GetSpellTotemsRecord();
            var totemStmt = new PreparedStatement(DataAccess.UPDATE_SPELL_TOTEMS);
            totemStmt.AddValue(0, totem.Id);
            totemStmt.AddValue(1, totem.SpellID);
            totemStmt.AddValue(2, totem.RequiredTotemCategoryID[0]);
            totemStmt.AddValue(3, totem.RequiredTotemCategoryID[1]);
            totemStmt.AddValue(4, totem.Totem[0]);
            totemStmt.AddValue(5, totem.Totem[1]);

            DataAccess.UpdateHotfixDB(totemStmt);
            CliDB.SpellTotemsStorage[totem.Id] = totem;

            // Visuals
            foreach (var visual in SpellInfo.GetSpellVisuals())
            {
                var visualStmt = new PreparedStatement(DataAccess.UPDATE_SPELL_X_SPELL_VISUAL);
                visualStmt.AddValue(0, visual.Id);
                visualStmt.AddValue(1, visual.DifficultyID);
                visualStmt.AddValue(2, visual.SpellVisualID);
                visualStmt.AddValue(3, visual.Probability);
                visualStmt.AddValue(4, visual.Priority);
                visualStmt.AddValue(5, visual.SpellIconFileID);
                visualStmt.AddValue(6, visual.ActiveIconFileID);
                visualStmt.AddValue(7, visual.ViewerUnitConditionID);
                visualStmt.AddValue(8, visual.ViewerPlayerConditionID);
                visualStmt.AddValue(9, visual.CasterUnitConditionID);
                visualStmt.AddValue(10, visual.CasterPlayerConditionID);
                visualStmt.AddValue(11, visual.SpellID);

                DataAccess.UpdateHotfixDB(visualStmt);
                CliDB.SpellXSpellVisualStorage[visual.Id] = visual;
            }

            SpellManager.Instance.UpsertSpellInfo(SpellInfo);
        }

        public void Delete()
        {
            // Spell_Name
            var nameStmt = new PreparedStatement(DataAccess.DELETE_SPELL_NAME);
            nameStmt.AddValue(0, SpellInfo.Id);
            DataAccess.UpdateHotfixDB(nameStmt);
            CliDB.SpellNameStorage.Remove(SpellInfo.Id);

            // spell
            var spellStmt = new PreparedStatement(DataAccess.DELETE_SPELL);
            spellStmt.AddValue(0, SpellInfo.Id);
            DataAccess.UpdateHotfixDB(spellStmt);
            DataAccess.SpellStorage.Remove(SpellInfo.Id);

            // spell_effect
            var seStmt = new PreparedStatement(DataAccess.DELETE_SPELL_EFFECT);
            seStmt.AddValue(0, SpellInfo.Id);
            DataAccess.UpdateHotfixDB(seStmt);

            foreach (var spellEffect in SpellInfo.GetEffects())
                if (spellEffect.Id > 0)
                    CliDB.SpellEffectStorage.Remove(spellEffect.Id);

            // Spell_Misc
            var miscStmt = new PreparedStatement(DataAccess.DELETE_SPELL_MISC);
            miscStmt.AddValue(0, SpellInfo.Id);
            DataAccess.UpdateHotfixDB(miscStmt);
            if (SpellInfo.SpellMiscId > 0)
                CliDB.SpellMiscStorage.Remove(SpellInfo.SpellMiscId);

            // Spell_Scaling
            var scalingStmt = new PreparedStatement(DataAccess.DELETE_SPELL_SCALING);
            scalingStmt.AddValue(0, SpellInfo.Id);
            DataAccess.UpdateHotfixDB(scalingStmt);
            if (SpellInfo.Scaling.Id > 0)
                CliDB.SpellScalingStorage.Remove(SpellInfo.Scaling.Id);

            // Spell_Aura_Options
            var auraOptionsStmt = new PreparedStatement(DataAccess.DELETE_SPELL_AURA_OPTIONS);
            auraOptionsStmt.AddValue(0, SpellInfo.Id);
            DataAccess.UpdateHotfixDB(auraOptionsStmt);
            if (SpellInfo.AuraOptionsId > 0)
                CliDB.SpellAuraOptionsStorage.Remove(SpellInfo.AuraOptionsId);

            // Spell_Aura_Restrictions
            var aurRestrictionsStmt = new PreparedStatement(DataAccess.DELETE_SPELL_AURA_RESTRICTIONS);
            aurRestrictionsStmt.AddValue(0, SpellInfo.Id);
            DataAccess.UpdateHotfixDB(aurRestrictionsStmt);
            if (SpellInfo.AuraRestrictionsId > 0)
                CliDB.SpellAuraRestrictionsStorage.Remove(SpellInfo.AuraRestrictionsId);

            // SpellCastingRequirementsEntry
            var castReqStmt = new PreparedStatement(DataAccess.DELETE_SPELL_CASTING_REQUIREMENTS);
            castReqStmt.AddValue(0, SpellInfo.Id);
            DataAccess.UpdateHotfixDB(castReqStmt);
            if (SpellInfo.SpellCastingRequirements != null && SpellInfo.SpellCastingRequirements.Id > 0)
                CliDB.SpellCastingRequirementsStorage.Remove(SpellInfo.SpellCastingRequirements.Id);

            // SpellCategoriesEntry
            SpellCategoriesRecord categorie = SpellInfo.GetSpellCategoriesRecord();
            var catStmt = new PreparedStatement(DataAccess.DELETE_SPELL_CATEGORIES);
            catStmt.AddValue(0, SpellInfo.Id);
            DataAccess.UpdateHotfixDB(catStmt);
            if (SpellInfo.SpellCategoriesId > 0)
                CliDB.SpellCategoriesStorage.Remove(SpellInfo.SpellCategoriesId);

            // SpellClassOptionsRecord
            SpellClassOptionsRecord coRecord = SpellInfo.GetSpellClassOptionsRecord();
            var coStmt = new PreparedStatement(DataAccess.DELETE_SPELL_CLASS_OPTIONS);
            coStmt.AddValue(0, SpellInfo.Id);
            DataAccess.UpdateHotfixDB(coStmt);
            if (SpellInfo.ClassOptionsId > 0)
                CliDB.SpellClassOptionsStorage.Remove(SpellInfo.ClassOptionsId);

            // SpellCooldownsEntry
            SpellCooldownsRecord cdr = SpellInfo.GetSpellCooldownsRecord();
            var cdStmt = new PreparedStatement(DataAccess.DELETE_SPELL_COOLDOWNS);
            cdStmt.AddValue(0, SpellInfo.Id);
            DataAccess.UpdateHotfixDB(cdStmt);
            if (SpellInfo.SpellCooldownsId > 0)
                CliDB.SpellCooldownsStorage.Remove(SpellInfo.SpellCooldownsId);

            // SpellEquippedItemsEntry
            SpellEquippedItemsRecord equipped = SpellInfo.GetSpellEquippedItemsRecord();
            var equipStmt = new PreparedStatement(DataAccess.DELETE_SPELL_EQUIPPED_ITEMS);
            equipStmt.AddValue(0, SpellInfo.Id);
            DataAccess.UpdateHotfixDB(equipStmt);
            if (SpellInfo.SpellEquippedItemsId > 0)
                CliDB.SpellEquippedItemsStorage.Remove(SpellInfo.SpellEquippedItemsId);

            // SpellInterruptsEntry
            SpellInterruptsRecord interrupt = SpellInfo.GetSpellInterruptsRecord();
            var interruptStmt = new PreparedStatement(DataAccess.UPDATE_SPELL_INTERRUPTS);
            interruptStmt.AddValue(0, SpellInfo.Id);
            DataAccess.UpdateHotfixDB(interruptStmt);
            if (SpellInfo.SpellInterruptsId > 0)
                CliDB.SpellInterruptsStorage.Remove(SpellInfo.SpellInterruptsId);

            List<SpellLabelRecord> labels = SpellInfo.GetSpellLabelRecords();
            var lblStmt = new PreparedStatement(DataAccess.DELETE_SPELL_LABEL);
            lblStmt.AddValue(0, SpellInfo.Id);
            DataAccess.UpdateHotfixDB(lblStmt);

            foreach (var label in labels)
                if (label.Id > 0)
                    CliDB.SpellLabelStorage.Remove(label.Id);

            // SpellLevelsEntry
            SpellLevelsRecord levels = SpellInfo.GetSpellLevelsRecord();
            var levelsStmt = new PreparedStatement(DataAccess.DELETE_SPELL_LEVELS);
            levelsStmt.AddValue(0, SpellInfo.Id);
            DataAccess.UpdateHotfixDB(levelsStmt);
            if (SpellInfo.SpellLevelsId > 0)
                CliDB.SpellLevelsStorage.Remove(SpellInfo.SpellLevelsId);

            var pwrStmt = new PreparedStatement(DataAccess.DELETE_SPELL_POWER);
            pwrStmt.AddValue(0, SpellInfo.Id);
            DataAccess.UpdateHotfixDB(pwrStmt);

            foreach (var power in SpellInfo.PowerCosts)
                if (power != null && power.Id > 0)
                    CliDB.SpellPowerStorage.Remove(power.Id);

            // SpellReagentsEntry
            SpellReagentsRecord reagents = SpellInfo.GetSpellReagentsRecord();
            var reagentsStmt = new PreparedStatement(DataAccess.DELETE_SPELL_REAGENTS);
            reagentsStmt.AddValue(0, SpellInfo.Id);
            DataAccess.UpdateHotfixDB(reagentsStmt);
            if (SpellInfo.SpellReagentsId > 0)
                CliDB.SpellReagentsStorage.Remove(SpellInfo.SpellReagentsId);

            // SpellReagentsCurrency
            var currencyStmt = new PreparedStatement(DataAccess.DELETE_SPELL_REAGENTS_CURRENCY);
            currencyStmt.AddValue(0, SpellInfo.Id);
            DataAccess.UpdateHotfixDB(currencyStmt);
            foreach (SpellReagentsCurrencyRecord currency in SpellInfo.ReagentsCurrency)
                if (currency.Id > 0)
                    CliDB.SpellReagentsCurrencyStorage.Remove(currency.Id);

            // SpellShapeshiftEntry
            SpellShapeshiftRecord shapeshift = SpellInfo.GetSpellShapeshiftRecord();
            var shapeshiftStmt = new PreparedStatement(DataAccess.DELETE_SPELL_SHAPESHIFT);
            shapeshiftStmt.AddValue(0, SpellInfo.Id);
            DataAccess.UpdateHotfixDB(shapeshiftStmt);
            if (SpellInfo.ShapeshiftRecordId > 0)
                CliDB.SpellShapeshiftStorage.Remove(SpellInfo.ShapeshiftRecordId);

            // SpellTargetRestrictionsEntry
            SpellTargetRestrictionsRecord target = SpellInfo.GetSpellTargetRestrictionsRecord();
            var targetStmt = new PreparedStatement(DataAccess.DELETE_SPELL_TARGET_RESTRICTIONS);
            targetStmt.AddValue(0, SpellInfo.Id);
            DataAccess.UpdateHotfixDB(targetStmt);
            if (SpellInfo.TargetRestrictionsId > 0)
                CliDB.SpellTargetRestrictionsStorage.Remove(SpellInfo.TargetRestrictionsId);

            // SpellTotemsEntry
            SpellTotemsRecord totem = SpellInfo.GetSpellTotemsRecord();
            var totemStmt = new PreparedStatement(DataAccess.DELETE_SPELL_TOTEMS);
            totemStmt.AddValue(0, SpellInfo.Id);
            DataAccess.UpdateHotfixDB(totemStmt);
            if (SpellInfo.TotemRecordID > 0)
                CliDB.SpellTotemsStorage.Remove(SpellInfo.TotemRecordID);

            // Visuals
            var visualStmt = new PreparedStatement(DataAccess.DELETE_SPELL_X_SPELL_VISUAL);
            visualStmt.AddValue(0, SpellInfo.Id);
            DataAccess.UpdateHotfixDB(visualStmt);
            foreach (var visual in SpellInfo.GetSpellVisuals())
                if (visual.Id > 0)
                    CliDB.SpellXSpellVisualStorage.Remove(visual.Id);

            SpellManager.Instance.UpsertSpellInfo(SpellInfo);
        }

        public FullSpellInfo Copy(uint newSpellId)
        {
            FullSpellInfo ret = new();
            try
            {
                ret.SpellInfo.Id = newSpellId;
                ret.SpellInfo.Difficulty = SpellInfo.Difficulty;

                // spell
                ret.SpellInfo.SpellName = new LocalizedString();
                ret.SpellInfo.SpellName[Locale.enUS] = SpellInfo.SpellName[Locale.enUS];
                ret.SpellDescriptions.Id = 0;
                ret.SpellDescriptions.NameSubtext_lang = SpellDescriptions.NameSubtext_lang;
                ret.SpellDescriptions.Description_lang = SpellDescriptions.Description_lang;
                ret.SpellDescriptions.AuraDescription_lang = SpellDescriptions.AuraDescription_lang;

                // spell_misc
                ret.SpellInfo.SpellMiscId = SpellInfo.SpellMiscId;
                ret.SpellInfo.Attributes = SpellInfo.Attributes;
                ret.SpellInfo.AttributesEx = SpellInfo.AttributesEx;
                ret.SpellInfo.AttributesEx2 = SpellInfo.AttributesEx2;
                ret.SpellInfo.AttributesEx3 = SpellInfo.AttributesEx3;
                ret.SpellInfo.AttributesEx4 = SpellInfo.AttributesEx4;
                ret.SpellInfo.AttributesEx5 = SpellInfo.AttributesEx5;
                ret.SpellInfo.AttributesEx6 = SpellInfo.AttributesEx6;
                ret.SpellInfo.AttributesEx7 = SpellInfo.AttributesEx7;
                ret.SpellInfo.AttributesEx8 = SpellInfo.AttributesEx8;
                ret.SpellInfo.AttributesEx9 = SpellInfo.AttributesEx9;
                ret.SpellInfo.AttributesEx10 = SpellInfo.AttributesEx10;
                ret.SpellInfo.AttributesEx11 = SpellInfo.AttributesEx11;
                ret.SpellInfo.AttributesEx12 = SpellInfo.AttributesEx12;
                ret.SpellInfo.AttributesEx13 = SpellInfo.AttributesEx13;
                ret.SpellInfo.AttributesEx14 = SpellInfo.AttributesEx14;

                ret.SpellInfo.CastTimeEntry = SpellInfo.CastTimeEntry;
                ret.SpellInfo.DurationEntry = SpellInfo.DurationEntry;
                ret.SpellInfo.RangeEntry = SpellInfo.RangeEntry;
                ret.SpellInfo.Speed = SpellInfo.Speed;
                ret.SpellInfo.LaunchDelay = SpellInfo.LaunchDelay;
                ret.SpellInfo.SchoolMask = SpellInfo.SchoolMask;
                ret.SpellInfo.IconFileDataId = SpellInfo.IconFileDataId;
                ret.SpellInfo.ActiveIconFileDataId = SpellInfo.ActiveIconFileDataId;

                ret.SpellInfo.ContentTuningId = SpellInfo.ContentTuningId;
                ret.SpellInfo.ShowFutureSpellPlayerConditionID = SpellInfo.ShowFutureSpellPlayerConditionID;

                // SpellScalingEntry
                ret.SpellInfo.Scaling.Id = 0;
                ret.SpellInfo.Scaling.MinScalingLevel = SpellInfo.Scaling.MinScalingLevel;
                ret.SpellInfo.Scaling.MaxScalingLevel = SpellInfo.Scaling.MaxScalingLevel;
                ret.SpellInfo.Scaling.ScalesFromItemLevel = SpellInfo.Scaling.ScalesFromItemLevel;

                // SpellAuraOptionsEntry
                ret.SpellInfo.AuraOptionsId = 0;
                if (SpellInfo.ProcFlags != null)
                    ret.SpellInfo.ProcFlags = new ProcFlagsInit(new int[] { SpellInfo.ProcFlags[0], SpellInfo.ProcFlags[1] });

                ret.SpellInfo.ProcChance = SpellInfo.ProcChance;
                ret.SpellInfo.ProcCharges = SpellInfo.ProcCharges;
                ret.SpellInfo.ProcCooldown = SpellInfo.ProcCooldown;
                ret.SpellInfo.StackAmount = SpellInfo.StackAmount;
                ret.SpellInfo.ProcBasePPM = SpellInfo.ProcBasePPM;
                ret.SpellInfo.SpellPPMId = SpellInfo.SpellPPMId;

                // SpellAuraRestrictionsEntry
                ret.SpellInfo.AuraRestrictionsId = 0;
                ret.SpellInfo.CasterAuraState = SpellInfo.CasterAuraState;
                ret.SpellInfo.TargetAuraState = SpellInfo.TargetAuraState;
                ret.SpellInfo.ExcludeCasterAuraState = SpellInfo.ExcludeCasterAuraState;
                ret.SpellInfo.ExcludeTargetAuraState = SpellInfo.ExcludeTargetAuraState;
                ret.SpellInfo.CasterAuraSpell = SpellInfo.CasterAuraSpell;
                ret.SpellInfo.TargetAuraSpell = SpellInfo.TargetAuraSpell;
                ret.SpellInfo.ExcludeCasterAuraSpell = SpellInfo.ExcludeCasterAuraSpell;
                ret.SpellInfo.ExcludeTargetAuraSpell = SpellInfo.ExcludeTargetAuraSpell;

                // SpellCastingRequirementsEntry
                ret.SpellInfo.SpellCastingRequirements = SpellInfo.SpellCastingRequirements.Copy();
                ret.SpellInfo.SpellCastingRequirements.Id = CliDB.SpellCastingRequirementsStorage.OrderByDescending(a => a.Key).First().Key + 1;
                ret.SpellInfo.SpellCastingRequirements.SpellID = newSpellId;
                ret.SpellInfo.RequiresSpellFocus = SpellInfo.RequiresSpellFocus;
                ret.SpellInfo.FacingCasterFlags = SpellInfo.FacingCasterFlags;
                ret.SpellInfo.RequiredAreasID = SpellInfo.RequiredAreasID;

                // SpellCategoriesEntry
                ret.SpellInfo.SpellCategoriesId = 0;
                ret.SpellInfo.CategoryId = SpellInfo.CategoryId;
                ret.SpellInfo.Dispel = SpellInfo.Dispel;
                ret.SpellInfo.Mechanic = SpellInfo.Mechanic;
                ret.SpellInfo.StartRecoveryCategory = SpellInfo.StartRecoveryCategory;
                ret.SpellInfo.DmgClass = SpellInfo.DmgClass;
                ret.SpellInfo.PreventionType = SpellInfo.PreventionType;
                ret.SpellInfo.ChargeCategoryId = SpellInfo.ChargeCategoryId;

                // SpellClassOptionsEntry
                ret.SpellInfo.ClassOptionsId = 0;
                ret.SpellInfo.ModalNextSpell = SpellInfo.ModalNextSpell;
                ret.SpellInfo.SpellFamilyName = SpellInfo.SpellFamilyName;
                ret.SpellInfo.SpellFamilyFlags = new FlagArray128(SpellInfo.SpellFamilyFlags[0], SpellInfo.SpellFamilyFlags[1], SpellInfo.SpellFamilyFlags[2], SpellInfo.SpellFamilyFlags[3]);

                // SpellCooldownsEntry
                ret.SpellInfo.SpellCooldownsId = 0;
                ret.SpellInfo.RecoveryTime = SpellInfo.RecoveryTime;
                ret.SpellInfo.CategoryRecoveryTime = SpellInfo.CategoryRecoveryTime;
                ret.SpellInfo.StartRecoveryTime = SpellInfo.StartRecoveryTime;

                // SpellEquippedItemsEntry
                ret.SpellInfo.SpellEquippedItemsId = 0;
                ret.SpellInfo.EquippedItemClass = SpellInfo.EquippedItemClass;
                ret.SpellInfo.EquippedItemSubClassMask = SpellInfo.EquippedItemSubClassMask;
                ret.SpellInfo.EquippedItemInventoryTypeMask = SpellInfo.EquippedItemInventoryTypeMask;
                
                // SpellInterruptsEntry
                ret.SpellInfo.SpellInterruptsId = 0;
                ret.SpellInfo.InterruptFlags = SpellInfo.InterruptFlags;
                ret.SpellInfo.AuraInterruptFlags = SpellInfo.AuraInterruptFlags;
                ret.SpellInfo.AuraInterruptFlags2 = SpellInfo.AuraInterruptFlags2;
                ret.SpellInfo.ChannelInterruptFlags = SpellInfo.ChannelInterruptFlags;
                ret.SpellInfo.ChannelInterruptFlags2 = SpellInfo.ChannelInterruptFlags2;

                foreach (uint label in SpellInfo.Labels)
                    ret.SpellInfo.Labels.Add(label);

                // SpellLevelsEntry
                ret.SpellInfo.SpellLevelsId = 0;
                ret.SpellInfo.MaxLevel = SpellInfo.MaxLevel;
                ret.SpellInfo.BaseLevel = SpellInfo.BaseLevel;
                ret.SpellInfo.SpellLevel = SpellInfo.SpellLevel;
                ret.SpellInfo.MaxPassiveAuraLevel = SpellInfo.MaxPassiveAuraLevel;

                // SpellPowerEntry
                uint powerId = CliDB.SpellPowerStorage.OrderByDescending(a => a.Key).First().Key + 1;
                for (int i = 0; i < 4; i++)
                {
                    var powerToCopy = ret.SpellInfo.PowerCosts[i];

                    if (powerToCopy != null)
                    {
                        var newPower = powerToCopy.Copy();
                        newPower.SpellID = newSpellId;
                        newPower.Id = powerId;
                        powerId++;
                        ret.SpellInfo.PowerCosts[i] = newPower;
                    }
                }

                // SpellReagentsEntry
                ret.SpellInfo.SpellReagentsId = 0;
                ret.SpellInfo.Reagent[0] = SpellInfo.Reagent[0];
                ret.SpellInfo.Reagent[1] = SpellInfo.Reagent[1];
                ret.SpellInfo.Reagent[2] = SpellInfo.Reagent[2];
                ret.SpellInfo.Reagent[3] = SpellInfo.Reagent[3];
                ret.SpellInfo.Reagent[4] = SpellInfo.Reagent[4];
                ret.SpellInfo.Reagent[5] = SpellInfo.Reagent[5];
                ret.SpellInfo.Reagent[6] = SpellInfo.Reagent[6];
                ret.SpellInfo.Reagent[7] = SpellInfo.Reagent[7];
                ret.SpellInfo.ReagentCount[0] = SpellInfo.ReagentCount[0];
                ret.SpellInfo.ReagentCount[1] = SpellInfo.ReagentCount[1];
                ret.SpellInfo.ReagentCount[2] = SpellInfo.ReagentCount[2];
                ret.SpellInfo.ReagentCount[3] = SpellInfo.ReagentCount[3];
                ret.SpellInfo.ReagentCount[4] = SpellInfo.ReagentCount[4];
                ret.SpellInfo.ReagentCount[5] = SpellInfo.ReagentCount[5];
                ret.SpellInfo.ReagentCount[6] = SpellInfo.ReagentCount[6];
                ret.SpellInfo.ReagentCount[7] = SpellInfo.ReagentCount[7];

                uint currencyId = CliDB.SpellReagentsCurrencyStorage.OrderByDescending(a => a.Key).First().Key + 1;
                foreach (var dirtyCur in SpellInfo.ReagentsCurrency)
                {
                    var cur = dirtyCur.Copy();
                    cur.SpellID = (int)newSpellId;
                    cur.Id = currencyId;
                    currencyId++;
                    ret.SpellInfo.ReagentsCurrency.Add(cur);
                }

                // SpellShapeshiftEntry
                ret.SpellInfo.ShapeshiftRecordId = 0;
                ret.SpellInfo.Stances = SpellInfo.Stances;
                ret.SpellInfo.StancesNot = SpellInfo.StancesNot;
                ret.SpellInfo.StanceBarOrder = SpellInfo.StanceBarOrder;

                // SpellTargetRestrictionsEntry
                ret.SpellInfo.TargetRestrictionsId = 0;
                ret.SpellInfo.Targets = SpellInfo.Targets;
                ret.SpellInfo.ConeAngle = SpellInfo.ConeAngle;
                ret.SpellInfo.Width = SpellInfo.Width;
                ret.SpellInfo.TargetCreatureType = SpellInfo.TargetCreatureType;
                ret.SpellInfo.MaxAffectedTargets = SpellInfo.MaxAffectedTargets;
                ret.SpellInfo.MaxTargetLevel = SpellInfo.MaxTargetLevel;

                // SpellTotemsEntry
                ret.SpellInfo.TotemRecordID = 0;
                ret.SpellInfo.Totem[0] = SpellInfo.Totem[0];
                ret.SpellInfo.Totem[1] = SpellInfo.Totem[1];
                ret.SpellInfo.TotemCategory[0] = SpellInfo.TotemCategory[0];
                ret.SpellInfo.TotemCategory[1] = SpellInfo.TotemCategory[1];

                // Visuals
                uint visualId = CliDB.SpellReagentsCurrencyStorage.OrderByDescending(a => a.Key).First().Key + 1;
                var visuals = ret.SpellInfo.GetSpellVisuals();
                foreach (var dirtyVis in SpellInfo.GetSpellVisuals())
                {
                    var newVis = dirtyVis.Copy();
                    newVis.SpellID = newSpellId;
                    newVis.Id = visualId;
                    visualId++;
                    visuals.Add(newVis);
                }

                // spell effects
                uint spellEffectId = CliDB.SpellReagentsCurrencyStorage.OrderByDescending(a => a.Key).First().Key + 1;
                var effects = ret.SpellInfo.GetEffects();
                foreach (var spellInfo in SpellInfo.GetEffects())
                {
                    var newVis = spellInfo.Copy(SpellInfo);
                    spellInfo.Id = spellEffectId;
                    spellEffectId++;
                    effects.Add(spellInfo);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"There was an error while validating your spell.{Environment.NewLine}{ex.Message}{Environment.NewLine}{ex.StackTrace}", "Error");
                return null;
            }

            return ret;
        }
    }
}
