// Copyright (c) Forged WoW LLC <https://github.com/ForgedWoW/ForgedCore>
// Licensed under GPL-3.0 license. See <https://github.com/ForgedWoW/ForgedCore/blob/master/LICENSE> for full information.

using Game.DataStorage;
using Framework.Constants;
using Forged.Tools.Shared.Spells;
using Framework.Dynamic;
using Framework.Database;
using Forged.Tools.SpellEditor.Models;
using Framework.Configuration;
using Forged.Tools.Shared.Utils;
using System.Runtime.CompilerServices;

namespace Forged.Tools.SpellEditor.Utils
{
    public static class ExtensionMethods
    {
        public static Image GetImage(this SpellIconRecord iconRecord)
        {
            var path = ConfigMgr.GetDefaultValue("Tools.IconDir", "{FullSpellEditorPath}").Replace("{FullSpellEditorPath}", System.Reflection.Assembly.GetEntryAssembly().Location.Replace("Forged.Tools.SpellEditor.dll", ""))
                + iconRecord.TextureFilename + ".png";

            if (File.Exists(path))
                return Image.FromFile(path);

            return null;
        }

        public static void UpdateItemSubClass(this ListBox listboxToPopulate, ItemClass itemClass, ItemClass equippedItemClass, int subClassMask, bool initialLoad)
        {
            listboxToPopulate.SelectedItems.Clear();
            listboxToPopulate.Items.Clear();

            switch (itemClass)
            {
                case ItemClass.Consumable:
                    listboxToPopulate.Items.AddEnumNames(typeof(ItemSubClassConsumable));
                    listboxToPopulate.Items.Remove("Max");
                    listboxToPopulate.SelectedItems.Clear();

                    if (initialLoad || equippedItemClass == itemClass)
                        listboxToPopulate.SelectedItems.AddSelectedIntEnum(subClassMask, typeof(ItemSubClassConsumable));
                    break;
                case ItemClass.Container:
                    listboxToPopulate.Items.AddEnumNames(typeof(ItemSubClassContainer));
                    listboxToPopulate.Items.Remove("Max");
                    listboxToPopulate.SelectedItems.Clear();

                    if (initialLoad || equippedItemClass == itemClass)
                        listboxToPopulate.SelectedItems.AddSelectedIntEnum(subClassMask, typeof(ItemSubClassContainer));
                    break;
                case ItemClass.Weapon:
                    listboxToPopulate.Items.AddEnumNames(typeof(ItemSubClassWeapon));
                    listboxToPopulate.Items.Remove("Max");
                    listboxToPopulate.SelectedItems.Clear();

                    if (initialLoad || equippedItemClass == itemClass)
                        listboxToPopulate.SelectedItems.AddSelectedIntEnum(subClassMask, typeof(ItemSubClassWeapon));
                    break;
                case ItemClass.Gem:
                    listboxToPopulate.Items.AddEnumNames(typeof(ItemSubClassGem));
                    listboxToPopulate.Items.Remove("Max");
                    listboxToPopulate.SelectedItems.Clear();

                    if (initialLoad || equippedItemClass == itemClass)
                        listboxToPopulate.SelectedItems.AddSelectedIntEnum(subClassMask, typeof(ItemSubClassGem));
                    break;
                case ItemClass.Armor:
                    listboxToPopulate.Items.AddEnumNames(typeof(ItemSubClassArmor));
                    listboxToPopulate.Items.Remove("Max");
                    listboxToPopulate.SelectedItems.Clear();

                    if (initialLoad || equippedItemClass == itemClass)
                        listboxToPopulate.SelectedItems.AddSelectedIntEnum(subClassMask, typeof(ItemSubClassArmor));
                    break;
                case ItemClass.Reagent:
                    listboxToPopulate.Items.AddEnumNames(typeof(ItemSubClassReagent));
                    listboxToPopulate.Items.Remove("Max");
                    listboxToPopulate.SelectedItems.Clear();

                    if (initialLoad || equippedItemClass == itemClass)
                        listboxToPopulate.SelectedItems.AddSelectedIntEnum(subClassMask, typeof(ItemSubClassReagent));
                    break;
                case ItemClass.Projectile:
                    listboxToPopulate.Items.AddEnumNames(typeof(ItemSubClassProjectile));
                    listboxToPopulate.Items.Remove("Max");
                    listboxToPopulate.SelectedItems.Clear();

                    if (initialLoad || equippedItemClass == itemClass)
                        listboxToPopulate.SelectedItems.AddSelectedIntEnum(subClassMask, typeof(ItemSubClassProjectile));
                    break;
                case ItemClass.TradeGoods:
                    listboxToPopulate.Items.AddEnumNames(typeof(ItemSubClassTradeGoods));
                    listboxToPopulate.Items.Remove("Max");
                    listboxToPopulate.SelectedItems.Clear();

                    if (initialLoad || equippedItemClass == itemClass)
                        listboxToPopulate.SelectedItems.AddSelectedIntEnum(subClassMask, typeof(ItemSubClassTradeGoods));
                    break;
                case ItemClass.ItemEnhancement:
                    listboxToPopulate.Items.AddEnumNames(typeof(ItemSubclassItemEnhancement));
                    listboxToPopulate.Items.Remove("Max");
                    listboxToPopulate.SelectedItems.Clear();

                    if (initialLoad || equippedItemClass == itemClass)
                        listboxToPopulate.SelectedItems.AddSelectedIntEnum(subClassMask, typeof(ItemSubclassItemEnhancement));
                    break;
                case ItemClass.Recipe:
                    listboxToPopulate.Items.AddEnumNames(typeof(ItemSubClassRecipe));
                    listboxToPopulate.Items.Remove("Max");
                    listboxToPopulate.SelectedItems.Clear();

                    if (initialLoad || equippedItemClass == itemClass)
                        listboxToPopulate.SelectedItems.AddSelectedIntEnum(subClassMask, typeof(ItemSubClassRecipe));
                    break;
                case ItemClass.Money:
                    listboxToPopulate.Items.AddEnumNames(typeof(ItemSubClassMoney));
                    listboxToPopulate.Items.Remove("Max");
                    listboxToPopulate.SelectedItems.Clear();

                    if (initialLoad || equippedItemClass == itemClass)
                        listboxToPopulate.SelectedItems.AddSelectedIntEnum(subClassMask, typeof(ItemSubClassMoney));
                    break;
                case ItemClass.Quiver:
                    listboxToPopulate.Items.AddEnumNames(typeof(ItemSubClassQuiver));
                    listboxToPopulate.Items.Remove("Max");
                    listboxToPopulate.SelectedItems.Clear();

                    if (initialLoad || equippedItemClass == itemClass)
                        listboxToPopulate.SelectedItems.AddSelectedIntEnum(subClassMask, typeof(ItemSubClassQuiver));
                    break;
                case ItemClass.Quest:
                    listboxToPopulate.Items.AddEnumNames(typeof(ItemSubClassQuest));
                    listboxToPopulate.Items.Remove("Max");
                    listboxToPopulate.SelectedItems.Clear();

                    if (initialLoad || equippedItemClass == itemClass)
                        listboxToPopulate.SelectedItems.AddSelectedIntEnum(subClassMask, typeof(ItemSubClassQuest));
                    break;
                case ItemClass.Key:
                    listboxToPopulate.Items.AddEnumNames(typeof(ItemSubClassKey));
                    listboxToPopulate.Items.Remove("Max");
                    listboxToPopulate.SelectedItems.Clear();

                    if (initialLoad || equippedItemClass == itemClass)
                        listboxToPopulate.SelectedItems.AddSelectedIntEnum(subClassMask, typeof(ItemSubClassKey));
                    break;
                case ItemClass.Permanent:
                    listboxToPopulate.Items.AddEnumNames(typeof(ItemSubClassPermanent));
                    listboxToPopulate.Items.Remove("Max");
                    listboxToPopulate.SelectedItems.Clear();

                    if (initialLoad || equippedItemClass == itemClass)
                        listboxToPopulate.SelectedItems.AddSelectedIntEnum(subClassMask, typeof(ItemSubClassPermanent));
                    break;
                case ItemClass.Glyph:
                    listboxToPopulate.Items.AddEnumNames(typeof(ItemSubClassGlyph));
                    listboxToPopulate.Items.Remove("Max");
                    listboxToPopulate.SelectedItems.Clear();

                    if (initialLoad || equippedItemClass == itemClass)
                        listboxToPopulate.SelectedItems.AddSelectedIntEnum(subClassMask, typeof(ItemSubClassGlyph));
                    break;
                case ItemClass.BattlePets:
                    listboxToPopulate.Items.AddEnumNames(typeof(ItemSubclassBattlePet));
                    listboxToPopulate.Items.Remove("Max");
                    listboxToPopulate.SelectedItems.Clear();

                    if (initialLoad || equippedItemClass == itemClass)
                        listboxToPopulate.SelectedItems.AddSelectedIntEnum(subClassMask, typeof(ItemSubclassBattlePet));
                    break;
                case ItemClass.WowToken:
                    listboxToPopulate.Items.AddEnumNames(typeof(ItemSubclassWowToken));
                    listboxToPopulate.Items.Remove("Max");
                    listboxToPopulate.SelectedItems.Clear();

                    if (initialLoad || equippedItemClass == itemClass)
                        listboxToPopulate.SelectedItems.AddSelectedIntEnum(subClassMask, typeof(ItemSubclassWowToken));
                    break;
                default:
                    break;
            }
        }


        public static void PopulateIconList(this ListView lvIcons, Label pageCount, NumericUpDown page, string iconFolder, string iconSearch)
        {
            var imageList = new ImageList();
            imageList.ImageSize = new Size(40, 40);
            imageList.ColorDepth = ColorDepth.Depth32Bit;
            lvIcons.Items.Clear();
            lvIcons.LargeImageList = imageList;

            var icons = Directory.GetFiles(iconFolder);
            KeyValuePair<uint, SpellIconRecord>[] iconStorage;

            if (!string.IsNullOrEmpty(iconSearch))
                iconStorage = Program.DataAccess.SpellIconStorage.Where(a => a.Value.TextureFilename.Split('/').Last().Contains(iconSearch)).ToArray();
            else
                iconStorage = Program.DataAccess.SpellIconStorage.ToArray();

            int iconPageLength = ConfigMgr.GetDefaultValue("Tools.IconPageLength", 100);
            var maxPages = 1;
            if (iconStorage.Length > iconPageLength)
                maxPages = iconStorage.Length / iconPageLength;

            pageCount.Text = maxPages.ToString();

            if (page.Value > maxPages)
                page.Value = maxPages;

            page.Maximum = maxPages;

            int starting = 0;
            int ending = iconPageLength;

            if (page.Value > 1)
                starting = (int)page.Value * iconPageLength;

            if (iconStorage.Length < iconPageLength || (page.Value > 1 && page.Value == maxPages))
                ending = iconStorage.Length;
            else
                ending = starting + iconPageLength;

            for (int i = starting; i < ending; i++)
            {
                var icon = iconStorage[i];
                imageList.Images.Add(icon.Value.TextureFilename, icon.Value.GetImage());
                var listViewItem = lvIcons.Items.Add($"{icon.Value.TextureFilename.Split('/').Last()} - {icon.Key}");
                listViewItem.ImageKey = icon.Value.TextureFilename;
            }
        }

        public static void PopulateSpellList(this ListBox listSpells, NumericUpDown numCurentMin, NumericUpDown numCurentMax, int currentIndex, string nameSearch, ref uint maxSpellSearch)
        {
            bool searchSpellName = !string.IsNullOrWhiteSpace(nameSearch);

            listSpells.Items.Clear();

            if (!searchSpellName && currentIndex == 0) // search by spell id
            {
                var spellNames = CliDB.SpellNameStorage;
                List<SpellNameRecord> toAdd = new();

                if (maxSpellSearch == 0 || maxSpellSearch == spellNames.Count)
                    maxSpellSearch = spellNames.OrderBy(a => a.Value.Id).Last().Value.Id;

                for (uint i = (uint)numCurentMin.Value; i <= numCurentMax.Value; i++)
                    if (spellNames.TryGetValue(i, out var val))
                        toAdd.Add(val);

                foreach (var spell in toAdd.OrderBy(a => a.Id).ToArray().AsSpan())
                    listSpells.Items.Add(Helpers.SpellDisplayName(spell));
            }
            else
            {
                var spellNames = CliDB.SpellNameStorage.OrderBy(a => a.Value.Id).ToArray();

                maxSpellSearch = (uint)numCurentMax.Value;

                if (searchSpellName) //Search by spell name
                {
                    int count = 1;
                    foreach (var spell in spellNames.AsSpan())
                    {
                        if (count >= numCurentMin.Value && count <= numCurentMax.Value
                            && spell.Value.Name[Locale.enUS].Contains(nameSearch, StringComparison.InvariantCultureIgnoreCase))
                        {
                            listSpells.Items.Add(Helpers.SpellDisplayName(spell.Value));
                            count++;
                        }

                        if (count >= numCurentMax.Value)
                            break;
                    }
                }
                else if (currentIndex == 1) //Search by spell index
                {
                    int count = 1;
                    foreach (var spell in spellNames.AsSpan())
                    {
                        if (count >= numCurentMin.Value && count <= numCurentMax.Value)
                            listSpells.Items.Add(Helpers.SpellDisplayName(spell.Value));

                        count++;

                        if (count >= numCurentMax.Value)
                            break;
                    }
                }
            }
        }

        public static SpellPowerRecord ToSpellPowerRecord(this TabPage tab, uint id)
        {
            SpellPowerRecord ret = new SpellPowerRecord();
            ret.Id = id;

            foreach (Control c in tab.Controls)
            {
                if (c.GetType() == typeof(Label))
                    continue;

                switch (c.Tag)
                {
                    // combo boxes
                    case "PowerType":
                        ret.PowerType = (PowerType)System.Enum.Parse(typeof(PowerType), (string)((ComboBox)c).SelectedItem);
                        break;

                    // numeric
                    case "ManaCost":
                        ret.ManaCost = (int)((NumericUpDown)c).Value;
                        break;
                    case "ManaCostPerLevel":
                        ret.ManaCostPerLevel = (int)((NumericUpDown)c).Value;
                        break;
                    case "ManaPerSecond":
                        ret.ManaPerSecond = (int)((NumericUpDown)c).Value;
                        break;
                    case "PowerDisplayID":
                        ret.PowerDisplayID = (uint)((NumericUpDown)c).Value;
                        break;
                    case "AltPowerBarID":
                        ret.AltPowerBarID = (int)((NumericUpDown)c).Value;
                        break;
                    case "RequiredAuraID":
                        ret.RequiredAuraSpellID = (uint)((NumericUpDown)c).Value;
                        break;
                    case "OptionalCost":
                        ret.OptionalCost = (uint)((NumericUpDown)c).Value;
                        break;

                    // text
                    case "PowerCostPct":
                        ret.PowerCostPct = float.Parse(((TextBox)c).Text);
                        break;
                    case "PowerCostMaxPct":
                        ret.PowerCostMaxPct = float.Parse(((TextBox)c).Text);
                        break;
                    case "PowerPctPerSecond":
                        ret.PowerPctPerSecond = float.Parse(((TextBox)c).Text);
                        break;

                    default:
                        break;
                }
            }

            return ret;
        }

        public static SpellMiscRecord GetSpellMiscRecord(this SpellInfo spellInfo)
        {
            SpellMiscRecord misc = new SpellMiscRecord();
            misc.Id = spellInfo.SpellMiscId;
            misc.Attributes[0] = (int)spellInfo.Attributes;
            misc.Attributes[1] = (int)spellInfo.AttributesEx;
            misc.Attributes[2] = (int)spellInfo.AttributesEx2;
            misc.Attributes[3] = (int)spellInfo.AttributesEx3;
            misc.Attributes[4] = (int)spellInfo.AttributesEx4;
            misc.Attributes[5] = (int)spellInfo.AttributesEx5;
            misc.Attributes[6] = (int)spellInfo.AttributesEx6;
            misc.Attributes[7] = (int)spellInfo.AttributesEx7;
            misc.Attributes[8] = (int)spellInfo.AttributesEx8;
            misc.Attributes[9] = (int)spellInfo.AttributesEx9;
            misc.Attributes[10] = (int)spellInfo.AttributesEx10;
            misc.Attributes[11] = (int)spellInfo.AttributesEx11;
            misc.Attributes[12] = (int)spellInfo.AttributesEx12;
            misc.Attributes[13] = (int)spellInfo.AttributesEx13;
            misc.Attributes[14] = (int)spellInfo.AttributesEx14;
            misc.DifficultyID = (byte)spellInfo.Difficulty;
            misc.CastingTimeIndex = (ushort)spellInfo.CastTimeEntry.Id;
            misc.DurationIndex = (ushort)spellInfo.DurationEntry.Id;
            misc.RangeIndex = (ushort)spellInfo.RangeEntry.Id;
            misc.SchoolMask = (byte)spellInfo.SchoolMask;
            misc.Speed = spellInfo.Speed;
            misc.LaunchDelay = spellInfo.LaunchDelay;
            misc.MinDuration = 0;
            misc.SpellIconFileDataID = spellInfo.IconFileDataId;
            misc.ActiveIconFileDataID = spellInfo.ActiveIconFileDataId;
            misc.ContentTuningID = spellInfo.ContentTuningId;
            misc.ShowFutureSpellPlayerConditionID = (int)spellInfo.ShowFutureSpellPlayerConditionID;
            misc.SpellVisualScript = 0;
            misc.ActiveSpellVisualScript = 0;
            misc.SpellID = spellInfo.Id;
            return misc;
        }

        public static SpellScalingRecord GetSpellScalingRecord(this SpellInfo spellInfo)
        {
            SpellScalingRecord scale = new SpellScalingRecord();
            scale.Id = spellInfo.Scaling.Id;
            scale.MinScalingLevel = spellInfo.Scaling.MinScalingLevel;
            scale.MaxScalingLevel = spellInfo.Scaling.MaxScalingLevel;
            scale.ScalesFromItemLevel = (ushort)spellInfo.Scaling.ScalesFromItemLevel;
            scale.SpellID = spellInfo.Id;

            if (scale.Id == 0)
            {
                scale.Id = SharedDataAccess.GetNewId(CliDB.SpellScalingStorage, "spell_scaling");
                spellInfo.Scaling.Id = scale.Id;
            }

            return scale;
        }

        public static SpellAuraOptionsRecord GetSpellAuraOptionsRecord(this SpellInfo spellInfo)
        {
            SpellAuraOptionsRecord aurOptions = new SpellAuraOptionsRecord();
            aurOptions.Id = spellInfo.AuraOptionsId;
            aurOptions.SpellID = spellInfo.Id;
            aurOptions.CumulativeAura = (ushort)spellInfo.StackAmount;
            aurOptions.ProcTypeMask[0] = spellInfo.ProcFlags.GetProcFlags();
            aurOptions.ProcTypeMask[1] = spellInfo.ProcFlags.GetProcFlags2();
            aurOptions.ProcCategoryRecovery = spellInfo.ProcCooldown;
            aurOptions.DifficultyID = (byte)spellInfo.Difficulty;
            aurOptions.ProcChance = (byte)spellInfo.ProcChance;
            aurOptions.ProcCharges = (int)spellInfo.ProcCharges;
            aurOptions.SpellProcsPerMinuteID = spellInfo.SpellPPMId;

            if (aurOptions.Id == 0)
            {
                aurOptions.Id = SharedDataAccess.GetNewId(CliDB.SpellAuraOptionsStorage, "spell_aura_options");
                spellInfo.AuraOptionsId = aurOptions.Id;
            }

            return aurOptions;
        }

        public static SpellAuraRestrictionsRecord GetSpellAuraRestrictionsRecord(this SpellInfo spellInfo)
        {
            SpellAuraRestrictionsRecord aura = new SpellAuraRestrictionsRecord();
            aura.Id = spellInfo.AuraRestrictionsId;
            aura.DifficultyID = (byte)spellInfo.Difficulty;
            aura.CasterAuraState = (byte)spellInfo.CasterAuraState;
            aura.TargetAuraState = (byte)spellInfo.TargetAuraState;
            aura.ExcludeCasterAuraState = (byte)spellInfo.ExcludeCasterAuraState;
            aura.ExcludeTargetAuraState = (byte)spellInfo.ExcludeTargetAuraState;
            aura.CasterAuraSpell = spellInfo.CasterAuraSpell;
            aura.TargetAuraSpell = spellInfo.TargetAuraSpell;
            aura.ExcludeCasterAuraSpell = spellInfo.ExcludeCasterAuraSpell;
            aura.ExcludeTargetAuraSpell = spellInfo.ExcludeTargetAuraSpell;
            aura.SpellID = spellInfo.Id;

            if (aura.Id == 0)
            {
                aura.Id = SharedDataAccess.GetNewId(CliDB.SpellAuraRestrictionsStorage, "spell_aura_restrictions");
                spellInfo.AuraRestrictionsId = aura.Id;
            }

            return aura;
        }

        public static SpellCastingRequirementsRecord GetSpellCastingRequirementsRecord(this SpellInfo spellInfo)
        {
            SpellCastingRequirementsRecord req = new SpellCastingRequirementsRecord();
            req.Id = spellInfo.SpellCastingRequirements.Id;
            req.SpellID = spellInfo.Id;
            req.FacingCasterFlags = (byte)spellInfo.FacingCasterFlags;
            req.MinFactionID = spellInfo.SpellCastingRequirements.MinFactionID;
            req.MinReputation = spellInfo.SpellCastingRequirements.MinReputation;
            req.RequiredAreasID = (ushort)spellInfo.RequiredAreasID;
            req.RequiredAuraVision = spellInfo.SpellCastingRequirements.RequiredAuraVision;
            req.RequiresSpellFocus = (ushort)spellInfo.RequiresSpellFocus;

            if (req.Id == 0)
            {
                req.Id = SharedDataAccess.GetNewId(CliDB.SpellCastingRequirementsStorage, "spell_casting_requirements");
                spellInfo.SpellCastingRequirements.Id = req.Id;
            }

            spellInfo.SpellCastingRequirements = req;

            return req;
        }

        public static SpellCategoriesRecord GetSpellCategoriesRecord(this SpellInfo spellInfo)
        {
            SpellCategoriesRecord cat = new SpellCategoriesRecord();
            cat.Id = spellInfo.SpellCategoriesId;
            cat.DifficultyID = (byte)spellInfo.Difficulty;
            cat.Category = (ushort)spellInfo.CategoryId;
            cat.DefenseType = (sbyte)spellInfo.DmgClass;
            cat.DispelType = (sbyte)spellInfo.Dispel;
            cat.Mechanic = (sbyte)spellInfo.Mechanic;
            cat.PreventionType = (sbyte)spellInfo.PreventionType;
            cat.StartRecoveryCategory = (ushort)spellInfo.StartRecoveryCategory;
            cat.ChargeCategory = (ushort)spellInfo.ChargeCategoryId;
            cat.SpellID = spellInfo.Id;

            if (cat.Id == 0)
            {
                cat.Id = SharedDataAccess.GetNewId(CliDB.SpellCategoriesStorage, "spell_categories");
                spellInfo.SpellCategoriesId = cat.Id;
            }

            return cat;
        }

        public static SpellClassOptionsRecord GetSpellClassOptionsRecord(this SpellInfo spellInfo)
        {
            SpellClassOptionsRecord options = new SpellClassOptionsRecord();
            options.Id = spellInfo.ClassOptionsId;
            options.SpellClassSet = Convert.ToByte(spellInfo.SpellFamilyName);
            options.SpellClassMask = spellInfo.SpellFamilyFlags;
            options.ModalNextSpell = spellInfo.ModalNextSpell;
            options.SpellID = spellInfo.Id;

            if (options.Id == 0)
            {
                options.Id = SharedDataAccess.GetNewId(CliDB.SpellClassOptionsStorage, "spell_class_options");
                spellInfo.ClassOptionsId = options.Id;
            }

            return options;
        }

        public static SpellCooldownsRecord GetSpellCooldownsRecord(this SpellInfo spellInfo)
        {
            SpellCooldownsRecord cdr = new SpellCooldownsRecord();
            cdr.Id = spellInfo.SpellCooldownsId;
            cdr.DifficultyID = (byte)spellInfo.Difficulty;
            cdr.CategoryRecoveryTime = spellInfo.CategoryRecoveryTime;
            cdr.RecoveryTime = spellInfo.RecoveryTime;
            cdr.StartRecoveryTime = spellInfo.StartRecoveryTime;
            cdr.SpellID = spellInfo.Id;

            if (cdr.Id == 0)
            {
                cdr.Id = SharedDataAccess.GetNewId(CliDB.SpellCooldownsStorage, "spell_cooldowns");
                spellInfo.SpellCooldownsId = cdr.Id;
            }

            return cdr;
        }

        public static SpellEquippedItemsRecord GetSpellEquippedItemsRecord(this SpellInfo spellInfo)
        {
            SpellEquippedItemsRecord equip = new SpellEquippedItemsRecord();
            equip.Id = spellInfo.SpellEquippedItemsId;
            equip.SpellID = spellInfo.Id;
            equip.EquippedItemClass = (sbyte)spellInfo.EquippedItemClass;
            equip.EquippedItemInvTypes = spellInfo.EquippedItemInventoryTypeMask;
            equip.EquippedItemSubclass = spellInfo.EquippedItemSubClassMask;

            if (equip.Id == 0)
            {
                equip.Id = SharedDataAccess.GetNewId(CliDB.SpellEquippedItemsStorage, "spell_equipped_items");
                spellInfo.SpellEquippedItemsId = equip.Id;
            }

            return equip;
        }

        public static SpellInterruptsRecord GetSpellInterruptsRecord(this SpellInfo spellInfo)
        {
            SpellInterruptsRecord sir = new SpellInterruptsRecord();
            sir.Id = spellInfo.SpellInterruptsId;
            sir.DifficultyID = (byte)spellInfo.Difficulty;
            sir.InterruptFlags = (short)spellInfo.InterruptFlags;
            sir.AuraInterruptFlags[0] = (int)spellInfo.AuraInterruptFlags;
            sir.AuraInterruptFlags[1] = (int)spellInfo.AuraInterruptFlags2;
            sir.ChannelInterruptFlags[0] = (int)spellInfo.ChannelInterruptFlags;
            sir.ChannelInterruptFlags[1] = (int)spellInfo.ChannelInterruptFlags2;
            sir.SpellID = spellInfo.Id;

            if (sir.Id == 0)
            {
                sir.Id = SharedDataAccess.GetNewId(CliDB.SpellInterruptsStorage, "spell_interrupts");
                spellInfo.SpellInterruptsId = sir.Id;
            }

            return sir;
        }

        public static List<SpellLabelRecord> GetSpellLabelRecords(this SpellInfo spellInfo)
        {
            List<SpellLabelRecord> labels = new List<SpellLabelRecord>();
            uint curMax = 0;

            foreach (uint label in spellInfo.Labels)
            {
                SpellLabelRecord newLabel = new SpellLabelRecord();
                newLabel.LabelID = label;
                newLabel.SpellID = spellInfo.Id;

                var lbls = CliDB.SpellLabelStorage.Where(a => a.Value.LabelID == newLabel.LabelID && a.Value.SpellID == newLabel.SpellID);

                if (lbls.Count() > 0)
                    newLabel.Id = lbls.Last().Key;
                else
                {
                    var lblStmt = new PreparedStatement(DataAccess.SELECT_LATEST_SPECIFIC_SPELL_LABEL);
                    lblStmt.AddValue(0, newLabel.LabelID);
                    lblStmt.AddValue(1, newLabel.SpellID);
                    var dbLbl = Program.DataAccess.GetHotfixValue(lblStmt);

                    if (dbLbl > 0)
                        newLabel.Id = dbLbl;
                }

                if (newLabel.Id == 0)
                {
                    if (curMax == 0)
                        curMax = SharedDataAccess.GetNewId(CliDB.SpellLabelStorage, "spell_label");

                    curMax++;
                    newLabel.Id = curMax;
                }

                labels.Add(newLabel);
            }

            return labels;
        }

        public static SpellLevelsRecord GetSpellLevelsRecord(this SpellInfo spellInfo)
        {
            SpellLevelsRecord sl = new SpellLevelsRecord();
            sl.Id = spellInfo.SpellLevelsId;
            sl.DifficultyID = (byte)spellInfo.Difficulty;
            sl.MaxLevel = (ushort)spellInfo.MaxLevel;
            sl.MaxPassiveAuraLevel = spellInfo.MaxPassiveAuraLevel;
            sl.BaseLevel = (ushort)spellInfo.BaseLevel;
            sl.SpellLevel = (ushort)spellInfo.MaxLevel;
            sl.SpellID = spellInfo.Id;

            if (sl.Id == 0)
            {
                sl.Id = SharedDataAccess.GetNewId(CliDB.SpellLevelsStorage, "spell_levels");
                spellInfo.SpellLevelsId = sl.Id;
            }

            return sl;
        }

        public static SpellReagentsRecord GetSpellReagentsRecord(this SpellInfo spellInfo)
        {
            SpellReagentsRecord sr = new SpellReagentsRecord();
            sr.Id = spellInfo.SpellReagentsId;
            sr.Reagent = spellInfo.Reagent;
            sr.SpellID = spellInfo.Id;

            for (int i = 0; i < spellInfo.ReagentCount.Length; i++)
                sr.ReagentCount[i] = (ushort)spellInfo.ReagentCount[i];

            if (sr.Id == 0)
            {
                sr.Id = SharedDataAccess.GetNewId(CliDB.SpellReagentsStorage, "spell_reagents");
                spellInfo.SpellReagentsId = sr.Id;
            }

            return sr;
        }

        public static SpellShapeshiftRecord GetSpellShapeshiftRecord(this SpellInfo spellInfo)
        {
            SpellShapeshiftRecord sr = new SpellShapeshiftRecord();
            sr.Id = spellInfo.ShapeshiftRecordId;
            sr.SpellID = spellInfo.Id;
            sr.StanceBarOrder = spellInfo.StanceBarOrder;
            sr.ShapeshiftExclude[0] = MathFunctions.Pair64_LoPart(spellInfo.StancesNot);
            sr.ShapeshiftExclude[1] = MathFunctions.Pair64_HiPart(spellInfo.StancesNot);
            sr.ShapeshiftMask[0] = MathFunctions.Pair64_LoPart(spellInfo.Stances);
            sr.ShapeshiftMask[1] = MathFunctions.Pair64_HiPart(spellInfo.Stances);

            if (sr.Id == 0)
            {
                sr.Id = SharedDataAccess.GetNewId(CliDB.SpellShapeshiftStorage, "spell_shapeshift");
                spellInfo.ShapeshiftRecordId = sr.Id;
            }

            return sr;
        }

        public static SpellTargetRestrictionsRecord GetSpellTargetRestrictionsRecord(this SpellInfo spellInfo)
        {
            SpellTargetRestrictionsRecord tr = new SpellTargetRestrictionsRecord();
            tr.Id = spellInfo.TargetRestrictionsId;
            tr.DifficultyID = (byte)spellInfo.Difficulty;
            tr.ConeDegrees = spellInfo.ConeAngle;
            tr.MaxTargets = (byte)spellInfo.MaxAffectedTargets;
            tr.MaxTargetLevel = spellInfo.MaxTargetLevel;
            tr.TargetCreatureType = (ushort)spellInfo.TargetCreatureType;
            tr.Targets = (uint)spellInfo.Targets;
            tr.Width = spellInfo.Width;
            tr.SpellID = spellInfo.Id;

            if (tr.Id == 0)
            {
                tr.Id = SharedDataAccess.GetNewId(CliDB.SpellTargetRestrictionsStorage, "spell_target_restrictions");
                spellInfo.TargetRestrictionsId = tr.Id;
            }

            return tr;
        }

        public static SpellTotemsRecord GetSpellTotemsRecord(this SpellInfo spellInfo)
        {
            SpellTotemsRecord tr = new SpellTotemsRecord();
            tr.Id = spellInfo.TotemRecordID;
            tr.SpellID = spellInfo.Id;
            tr.RequiredTotemCategoryID[0] = (ushort)spellInfo.TotemCategory[0];
            tr.RequiredTotemCategoryID[1] = (ushort)spellInfo.TotemCategory[1];
            tr.Totem[0] = spellInfo.Totem[0];
            tr.Totem[1] = spellInfo.Totem[1];

            if (tr.Id == 0)
            {
                tr.Id = SharedDataAccess.GetNewId(CliDB.SpellTotemsStorage, "spell_totems");
                spellInfo.TotemRecordID = tr.Id;
            }

            return tr;
        }

        public static SpellCastingRequirementsRecord Copy(this SpellCastingRequirementsRecord obj)
        {
            SpellCastingRequirementsRecord ret = new SpellCastingRequirementsRecord();
            ret.Id = obj.Id;
            ret.SpellID = obj.SpellID;
            ret.FacingCasterFlags = obj.FacingCasterFlags;
            ret.MinFactionID = obj.MinFactionID;
            ret.MinReputation = obj.MinReputation;
            ret.RequiredAreasID = obj.RequiredAreasID;
            ret.RequiredAuraVision = obj.RequiredAuraVision;
            ret.RequiresSpellFocus = obj.RequiresSpellFocus;
            return ret;
        }

        public static SpellPowerRecord Copy(this SpellPowerRecord obj)
        {
            SpellPowerRecord ret = new SpellPowerRecord();
            ret.Id = obj.Id;
            ret.OrderIndex = obj.OrderIndex;
            ret.ManaCost = obj.ManaCost;
            ret.ManaCostPerLevel = obj.ManaCostPerLevel;
            ret.ManaPerSecond = obj.ManaPerSecond;
            ret.PowerDisplayID = obj.PowerDisplayID;
            ret.AltPowerBarID = obj.AltPowerBarID;
            ret.PowerCostPct = obj.PowerCostPct;
            ret.PowerCostMaxPct = obj.PowerCostMaxPct;
            ret.PowerPctPerSecond = obj.PowerPctPerSecond;
            ret.PowerType = obj.PowerType;
            ret.RequiredAuraSpellID = obj.RequiredAuraSpellID;
            ret.OptionalCost = obj.OptionalCost;
            ret.SpellID = obj.SpellID;
            return ret;
        }

        public static SpellReagentsCurrencyRecord Copy(this SpellReagentsCurrencyRecord obj)
        {
            return new SpellReagentsCurrencyRecord()
            {
                Id = obj.Id,
                CurrencyTypesID = obj.CurrencyTypesID,
                CurrencyCount = obj.CurrencyCount,
                SpellID = obj.SpellID
            };
        }

        public static SpellReagentsCurrencyRecordMod Copy(this SpellReagentsCurrencyRecord obj, bool keepRecord)
        {
            return new SpellReagentsCurrencyRecordMod()
            {
                Id = obj.Id,
                CurrencyTypesID = obj.CurrencyTypesID,
                CurrencyCount = obj.CurrencyCount,
                SpellID = obj.SpellID,
                KeepRecord = keepRecord
            };
        }

        public static SpellReagentsCurrencyRecord ToBaseRecord(this SpellReagentsCurrencyRecordMod obj)
        {
            return new SpellReagentsCurrencyRecord()
            {
                Id = obj.Id,
                CurrencyTypesID = obj.CurrencyTypesID,
                CurrencyCount = obj.CurrencyCount,
                SpellID = obj.SpellID
            };
        }

        public static SpellXSpellVisualRecordMod Copy(this SpellXSpellVisualRecord obj, bool keepRecord)
        {
            SpellXSpellVisualRecordMod ret = new SpellXSpellVisualRecordMod();

            ret.Id = obj.Id;
            ret.DifficultyID = obj.DifficultyID;
            ret.SpellVisualID = obj.SpellVisualID;
            ret.Probability = obj.Probability;
            ret.Priority = obj.Priority;
            ret.SpellIconFileID = obj.SpellIconFileID;
            ret.ActiveIconFileID = obj.ActiveIconFileID;
            ret.ViewerUnitConditionID = obj.ViewerUnitConditionID;
            ret.ViewerPlayerConditionID = obj.ViewerPlayerConditionID;
            ret.CasterUnitConditionID = obj.CasterUnitConditionID;
            ret.CasterPlayerConditionID = obj.CasterPlayerConditionID;
            ret.SpellID = obj.SpellID;
            ret.KeepRecord = keepRecord;

            return ret;
        }

        public static SpellXSpellVisualRecord Copy(this SpellXSpellVisualRecord obj)
        {
            SpellXSpellVisualRecord ret = new SpellXSpellVisualRecord();

            ret.Id = obj.Id;
            ret.DifficultyID = obj.DifficultyID;
            ret.SpellVisualID = obj.SpellVisualID;
            ret.Probability = obj.Probability;
            ret.Priority = obj.Priority;
            ret.SpellIconFileID = obj.SpellIconFileID;
            ret.ActiveIconFileID = obj.ActiveIconFileID;
            ret.ViewerUnitConditionID = obj.ViewerUnitConditionID;
            ret.ViewerPlayerConditionID = obj.ViewerPlayerConditionID;
            ret.CasterUnitConditionID = obj.CasterUnitConditionID;
            ret.CasterPlayerConditionID = obj.CasterPlayerConditionID;
            ret.SpellID = obj.SpellID;

            return ret;
        }

        public static SpellXSpellVisualRecord ToBaseRecord(this SpellXSpellVisualRecordMod obj)
        {
            SpellXSpellVisualRecord ret = new SpellXSpellVisualRecord();

            ret.Id = obj.Id;
            ret.DifficultyID = obj.DifficultyID;
            ret.SpellVisualID = obj.SpellVisualID;
            ret.Probability = obj.Probability;
            ret.Priority = obj.Priority;
            ret.SpellIconFileID = obj.SpellIconFileID;
            ret.ActiveIconFileID = obj.ActiveIconFileID;
            ret.ViewerUnitConditionID = obj.ViewerUnitConditionID;
            ret.ViewerPlayerConditionID = obj.ViewerPlayerConditionID;
            ret.CasterUnitConditionID = obj.CasterUnitConditionID;
            ret.CasterPlayerConditionID = obj.CasterPlayerConditionID;
            ret.SpellID = obj.SpellID;

            return ret;
        }

        public static string GetListName(this SpellEffectInfo effInfo)
        {
            return $"Effect {effInfo.EffectIndex} - {effInfo.Effect} - {effInfo.ApplyAuraName}";
        }

        public static float ToFloat(this TextBox bx)
        {
            return float.Parse(bx.Text);
        }

        public static double ToDouble(this TextBox bx)
        {
            return double.Parse(bx.Text);
        }
    }
}
