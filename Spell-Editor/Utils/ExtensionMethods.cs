using Framework.Constants;
using Framework.Database;
using Framework.Dynamic;
using Game.DataStorage;
using Game.Entities;
using Game.Spells;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spell_Editor.Utils
{
    public static class ExtensionMethods
    {
        public static void AddEnumNames(this ListBox.ObjectCollection listValues, Type enumType)
        {
            foreach (string name in Enum.GetNames(enumType).AsSpan())
                listValues.Add(name);
        }

        public static void AddEnumNames(this ComboBox.ObjectCollection listValues, Type enumType)
        {
            foreach (string name in Enum.GetNames(enumType).AsSpan())
                listValues.Add(name);
        }

        public static void AddSelectedEnum<T>(this ListBox.SelectedObjectCollection listValues, T toMatch)
        {
            foreach (string attr in Enum.GetNames(typeof(T)).AsSpan())
                if ((Convert.ToInt64(toMatch) & Convert.ToInt64((T)Enum.Parse(typeof(T), attr))) != 0)
                    listValues.Add(attr);
        }

        public static Int64 CalculateValue<T>(this ListBox.SelectedObjectCollection listValues)
        {
            Int64 retVal = 0;

            for (var i = listValues.Count - 1; i >= 0; i--)
            {
                var value = Convert.ToInt64((T)Enum.Parse(typeof(T), listValues[i].ToString()));

                if ((retVal & value) == 0)
                    retVal |= value;
            }

            return retVal;
        }

        public static void AddSelectedEnum(this ListBox.SelectedObjectCollection listValues, int toMatch, Type enumType)
        {
            foreach (string attr in Enum.GetNames(enumType).AsSpan())
                if ((toMatch & Convert.ToInt64(Enum.Parse(enumType, attr))) != 0)
                    listValues.Add(attr);
        }

        public static void AddSelectedEnum<T>(this ComboBox cmb, T toMatch)
        {
            foreach (string item in cmb.Items)
            {
                if (item == toMatch.ToString())
                {
                    cmb.SelectedItem = item;
                    return;
                }
            }
        }

        public static Int64 EnumCollectionToInt64<T>(this ListBox.SelectedObjectCollection listValues)
        {
            Int64 ret = 0;

            foreach (string attr in Enum.GetNames(typeof(T)).AsSpan())
                ret |= Convert.ToInt64((T)Enum.Parse(typeof(T), attr));

            return ret;
        }

        public static Image GetImage(this SpellIconRecord iconRecord)
        {
            return Image.FromFile(Settings.Default.IconDir.Replace("{FullSpellEditorPath}", System.Reflection.Assembly.GetEntryAssembly().Location.Replace("Spell-Editor.dll", ""))
                + iconRecord.TextureFilename + ".png");
        }

        public static void MakeNumberBox(this TextBox txtBox)
        {
            txtBox.KeyPress += makeNumberBox_KeyPress;
            txtBox.TextChanged += makeNumberBox_TextChanged;
        }

        private static void makeNumberBox_TextChanged(object? sender, EventArgs e)
        {
            TextBox box = (TextBox)sender;
            if (box.Text.Contains("-") && box.Text.IndexOf('-') != 0)
            {
                int cursor = box.SelectionStart;
                int cursor2 = box.SelectionLength;
                box.Text = "-" + box.Text.Replace("-", "");
                box.SelectionStart = cursor;
                box.SelectionLength = cursor2;
            }
            if (box.Text == string.Empty)
                box.Text = "0";
        }

        private static void makeNumberBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox box = (TextBox)sender;

            if (e.KeyChar == 8)
            {
                if (box.TextLength == 1 || (box.TextLength == 2 && box.Text.StartsWith("-")))
                {
                    box.Text = "0";
                    box.SelectionStart = 1;
                    e.Handled = true;
                    return;
                }
            }

            if (Control.ModifierKeys == Keys.Control)
            {
                if (e.KeyChar == 22)
                {
                    string clipText = Clipboard.GetText();

                    if (!float.TryParse(clipText, out float f))
                        e.Handled = true;
                    else
                    {
                        if (clipText.Contains("."))
                            if (box.Text.Contains("."))
                                if (!box.SelectedText.Contains("."))
                                {
                                    e.Handled = true;
                                    return;
                                }
                        if (clipText.Contains("-"))
                            if (box.Text.Contains("-"))
                                if (!box.SelectedText.Contains("-"))
                                {
                                    e.Handled = true;
                                    return;
                                }
                    }

                    return;
                }
                else if (e.KeyChar == 3)
                    return;
            }

            e.Handled = !char.IsDigit(e.KeyChar) && !(e.KeyChar == 8)
                && !(e.KeyChar == '.' && !box.Text.Contains("."))
                && !(e.KeyChar == '-' && !box.Text.Contains("-"));
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
                        listboxToPopulate.SelectedItems.AddSelectedEnum(subClassMask, typeof(ItemSubClassConsumable));
                    break;
                case ItemClass.Container:
                    listboxToPopulate.Items.AddEnumNames(typeof(ItemSubClassContainer));
                    listboxToPopulate.Items.Remove("Max");
                    listboxToPopulate.SelectedItems.Clear();

                    if (initialLoad || equippedItemClass == itemClass)
                        listboxToPopulate.SelectedItems.AddSelectedEnum(subClassMask, typeof(ItemSubClassContainer));
                    break;
                case ItemClass.Weapon:
                    listboxToPopulate.Items.AddEnumNames(typeof(ItemSubClassWeapon));
                    listboxToPopulate.Items.Remove("Max");
                    listboxToPopulate.SelectedItems.Clear();

                    if (initialLoad || equippedItemClass == itemClass)
                        listboxToPopulate.SelectedItems.AddSelectedEnum(subClassMask, typeof(ItemSubClassWeapon));
                    break;
                case ItemClass.Gem:
                    listboxToPopulate.Items.AddEnumNames(typeof(ItemSubClassGem));
                    listboxToPopulate.Items.Remove("Max");
                    listboxToPopulate.SelectedItems.Clear();

                    if (initialLoad || equippedItemClass == itemClass)
                        listboxToPopulate.SelectedItems.AddSelectedEnum(subClassMask, typeof(ItemSubClassGem));
                    break;
                case ItemClass.Armor:
                    listboxToPopulate.Items.AddEnumNames(typeof(ItemSubClassArmor));
                    listboxToPopulate.Items.Remove("Max");
                    listboxToPopulate.SelectedItems.Clear();

                    if (initialLoad || equippedItemClass == itemClass)
                        listboxToPopulate.SelectedItems.AddSelectedEnum(subClassMask, typeof(ItemSubClassArmor));
                    break;
                case ItemClass.Reagent:
                    listboxToPopulate.Items.AddEnumNames(typeof(ItemSubClassReagent));
                    listboxToPopulate.Items.Remove("Max");
                    listboxToPopulate.SelectedItems.Clear();

                    if (initialLoad || equippedItemClass == itemClass)
                        listboxToPopulate.SelectedItems.AddSelectedEnum(subClassMask, typeof(ItemSubClassReagent));
                    break;
                case ItemClass.Projectile:
                    listboxToPopulate.Items.AddEnumNames(typeof(ItemSubClassProjectile));
                    listboxToPopulate.Items.Remove("Max");
                    listboxToPopulate.SelectedItems.Clear();

                    if (initialLoad || equippedItemClass == itemClass)
                        listboxToPopulate.SelectedItems.AddSelectedEnum(subClassMask, typeof(ItemSubClassProjectile));
                    break;
                case ItemClass.TradeGoods:
                    listboxToPopulate.Items.AddEnumNames(typeof(ItemSubClassTradeGoods));
                    listboxToPopulate.Items.Remove("Max");
                    listboxToPopulate.SelectedItems.Clear();

                    if (initialLoad || equippedItemClass == itemClass)
                        listboxToPopulate.SelectedItems.AddSelectedEnum(subClassMask, typeof(ItemSubClassTradeGoods));
                    break;
                case ItemClass.ItemEnhancement:
                    listboxToPopulate.Items.AddEnumNames(typeof(ItemSubclassItemEnhancement));
                    listboxToPopulate.Items.Remove("Max");
                    listboxToPopulate.SelectedItems.Clear();

                    if (initialLoad || equippedItemClass == itemClass)
                        listboxToPopulate.SelectedItems.AddSelectedEnum(subClassMask, typeof(ItemSubclassItemEnhancement));
                    break;
                case ItemClass.Recipe:
                    listboxToPopulate.Items.AddEnumNames(typeof(ItemSubClassRecipe));
                    listboxToPopulate.Items.Remove("Max");
                    listboxToPopulate.SelectedItems.Clear();

                    if (initialLoad || equippedItemClass == itemClass)
                        listboxToPopulate.SelectedItems.AddSelectedEnum(subClassMask, typeof(ItemSubClassRecipe));
                    break;
                case ItemClass.Money:
                    listboxToPopulate.Items.AddEnumNames(typeof(ItemSubClassMoney));
                    listboxToPopulate.Items.Remove("Max");
                    listboxToPopulate.SelectedItems.Clear();

                    if (initialLoad || equippedItemClass == itemClass)
                        listboxToPopulate.SelectedItems.AddSelectedEnum(subClassMask, typeof(ItemSubClassMoney));
                    break;
                case ItemClass.Quiver:
                    listboxToPopulate.Items.AddEnumNames(typeof(ItemSubClassQuiver));
                    listboxToPopulate.Items.Remove("Max");
                    listboxToPopulate.SelectedItems.Clear();

                    if (initialLoad || equippedItemClass == itemClass)
                        listboxToPopulate.SelectedItems.AddSelectedEnum(subClassMask, typeof(ItemSubClassQuiver));
                    break;
                case ItemClass.Quest:
                    listboxToPopulate.Items.AddEnumNames(typeof(ItemSubClassQuest));
                    listboxToPopulate.Items.Remove("Max");
                    listboxToPopulate.SelectedItems.Clear();

                    if (initialLoad || equippedItemClass == itemClass)
                        listboxToPopulate.SelectedItems.AddSelectedEnum(subClassMask, typeof(ItemSubClassQuest));
                    break;
                case ItemClass.Key:
                    listboxToPopulate.Items.AddEnumNames(typeof(ItemSubClassKey));
                    listboxToPopulate.Items.Remove("Max");
                    listboxToPopulate.SelectedItems.Clear();

                    if (initialLoad || equippedItemClass == itemClass)
                        listboxToPopulate.SelectedItems.AddSelectedEnum(subClassMask, typeof(ItemSubClassKey));
                    break;
                case ItemClass.Permanent:
                    listboxToPopulate.Items.AddEnumNames(typeof(ItemSubClassPermanent));
                    listboxToPopulate.Items.Remove("Max");
                    listboxToPopulate.SelectedItems.Clear();

                    if (initialLoad || equippedItemClass == itemClass)
                        listboxToPopulate.SelectedItems.AddSelectedEnum(subClassMask, typeof(ItemSubClassPermanent));
                    break;
                case ItemClass.Glyph:
                    listboxToPopulate.Items.AddEnumNames(typeof(ItemSubClassGlyph));
                    listboxToPopulate.Items.Remove("Max");
                    listboxToPopulate.SelectedItems.Clear();

                    if (initialLoad || equippedItemClass == itemClass)
                        listboxToPopulate.SelectedItems.AddSelectedEnum(subClassMask, typeof(ItemSubClassGlyph));
                    break;
                case ItemClass.BattlePets:
                    listboxToPopulate.Items.AddEnumNames(typeof(ItemSubclassBattlePet));
                    listboxToPopulate.Items.Remove("Max");
                    listboxToPopulate.SelectedItems.Clear();

                    if (initialLoad || equippedItemClass == itemClass)
                        listboxToPopulate.SelectedItems.AddSelectedEnum(subClassMask, typeof(ItemSubclassBattlePet));
                    break;
                case ItemClass.WowToken:
                    listboxToPopulate.Items.AddEnumNames(typeof(ItemSubclassWowToken));
                    listboxToPopulate.Items.Remove("Max");
                    listboxToPopulate.SelectedItems.Clear();

                    if (initialLoad || equippedItemClass == itemClass)
                        listboxToPopulate.SelectedItems.AddSelectedEnum(subClassMask, typeof(ItemSubclassWowToken));
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
                iconStorage = DataAccess.SpellIconStorage.Where(a => a.Value.TextureFilename.Split('/').Last().Contains(iconSearch)).ToArray();
            else
                iconStorage = DataAccess.SpellIconStorage.ToArray();

            int iconPageLength = Settings.Default.IconPageLength;
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
                    if (spellNames.ContainsKey(i))
                        toAdd.Add(spellNames[i]);

                foreach (var spell in toAdd.OrderBy(a => a.Id).ToArray().AsSpan())
                    listSpells.Items.Add(Helpers.SpellDisplayName(spell));
            }
            else
            {
                var spellNames = CliDB.SpellNameStorage.OrderBy(a => a.Value.Id).ToArray();

                maxSpellSearch = (uint)spellNames.Length;

                if (numCurentMax.Value > maxSpellSearch)
                {
                    decimal range = Helpers.CurrentRange(numCurentMin.Value, numCurentMax.Value);
                    numCurentMax.Value = maxSpellSearch;
                    numCurentMin.Value = numCurentMax.Value - range + 1;
                }

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

        public static uint ReverseLookup<T>(this Dictionary<uint, T> dic, T value)
        {
            foreach (var kvp in dic)
            {
                if (kvp.Value.Equals(value))
                    return kvp.Key;
            }

            return 0;
        }

        public static SpellEffectInfo ToSpellEffectInfo(this TabPage tab, SpellInfo spellInfo, Dictionary<uint, int> radiusMap)
        {
            SpellEffectInfo ret = new SpellEffectInfo(spellInfo);
            ret.EffectIndex = uint.Parse(tab.Text.Replace("Effect ", ""));

            ret.SpellClassMask = new FlagArray128();

            foreach (Control c in tab.Controls)
            {
                if (c.GetType() == typeof(Label))
                    continue;

                switch (c.Tag)
                {
                    // combo boxes
                    case "SpellEffect":
                        ret.Effect = (SpellEffectName)Enum.Parse(typeof(SpellEffectName), (string)((ComboBox)c).SelectedItem);
                        break;
                    case "EffMechanic":
                        ret.Mechanic = (Mechanics)Enum.Parse(typeof(Mechanics), (string)((ComboBox)c).SelectedItem);
                        break;
                    case "TargetA":
                        ret.TargetA = new SpellImplicitTargetInfo((Targets)Enum.Parse(typeof(Targets), (string)((ComboBox)c).SelectedItem));
                        break;
                    case "TargetB":
                        ret.TargetB = new SpellImplicitTargetInfo((Targets)Enum.Parse(typeof(Targets), (string)((ComboBox)c).SelectedItem));
                        break;
                    case "RadiusMin":
                        ret.RadiusEntry = CliDB.SpellRadiusStorage[radiusMap.ReverseLookup(((ComboBox)c).SelectedIndex)];
                        break;
                    case "RadiusMax":
                        ret.MaxRadiusEntry = CliDB.SpellRadiusStorage[radiusMap.ReverseLookup(((ComboBox)c).SelectedIndex)];
                        break;
                    case "ApplyAura":
                        ret.ApplyAuraName = (AuraType)Enum.Parse(typeof(AuraType), (string)((ComboBox)c).SelectedItem);
                        break;

                    // numeric
                    case "BasePoints":
                        ret.BasePoints = (int)((NumericUpDown)c).Value;
                        break;
                    case "AuraTickRate":
                        ret.ApplyAuraPeriod = (uint)((NumericUpDown)c).Value;
                        break;
                    case "ScalingClass":
                        ret.Scaling.Class = (int)((NumericUpDown)c).Value;
                        break;
                    case "ChainTargets":
                        ret.ChainTargets = (int)((NumericUpDown)c).Value;
                        break;
                    case "TriggerSpell":
                        ret.TriggerSpell = (uint)((NumericUpDown)c).Value;
                        break;
                    case "MiscValueA":
                        ret.MiscValue = (int)((NumericUpDown)c).Value;
                        break;
                    case "MiscValueB":
                        ret.MiscValueB = (int)((NumericUpDown)c).Value;
                        break;
                    case "EffItemType":
                        ret.ItemType = (uint)((NumericUpDown)c).Value;
                        break;
                    case "EffTableID":
                        NumericUpDown tblId = (NumericUpDown)c;
                        ret.Id = (uint)tblId.Value;

                        // throw if id = 0 or it is a new effect and exists in the effect cache or spell_effect table
                        if (ret.Id == 0 || (tblId.Enabled && (CliDB.SpellEffectStorage.ContainsKey(ret.Id) || DataAccess.GetHotfixSpellEffectIDs().Contains(ret.Id))))
                            throw new Exception($"The Effect Table ID for spell effect {ret.EffectIndex} already exists.");

                        break;
                    case "ClassMask1":
                        ret.SpellClassMask[0] = (uint)((NumericUpDown)c).Value;
                        break;
                    case "ClassMask2":
                        ret.SpellClassMask[0] = (uint)((NumericUpDown)c).Value;
                        break;
                    case "ClassMask3":
                        ret.SpellClassMask[0] = (uint)((NumericUpDown)c).Value;
                        break;
                    case "ClassMask4":
                        ret.SpellClassMask[0] = (uint)((NumericUpDown)c).Value;
                        break;

                    // text boxes
                    case "Variance":
                        ret.Scaling.Variance = float.Parse(((TextBox)c).Text);
                        break;
                    case "ScalingCoefficient":
                        ret.Scaling.Coefficient = float.Parse(((TextBox)c).Text);
                        break;
                    case "ResourceCoefficient":
                        ret.Scaling.ResourceCoefficient = float.Parse(((TextBox)c).Text);
                        break;
                    case "RealPoints":
                        ret.RealPointsPerLevel = float.Parse(((TextBox)c).Text);
                        break;
                    case "PointsPerResource":
                        ret.PointsPerResource = float.Parse(((TextBox)c).Text);
                        break;
                    case "Amplitude":
                        ret.Amplitude = float.Parse(((TextBox)c).Text);
                        break;
                    case "ChainAmplitude":
                        ret.ChainAmplitude = float.Parse(((TextBox)c).Text);
                        break;
                    case "BonusCoefficient":
                        ret.BonusCoefficient = float.Parse(((TextBox)c).Text);
                        break;
                    case "BonusCoefficientFromAP":
                        ret.BonusCoefficientFromAP = float.Parse(((TextBox)c).Text);
                        break;
                    case "PositionFacing":
                        ret.PositionFacing = float.Parse(((TextBox)c).Text);
                        break;

                    default:
                        break;
                }
            }

            return ret;
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
                        ret.PowerType = (PowerType)Enum.Parse(typeof(PowerType), (string)((ComboBox)c).SelectedItem);
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

        public static bool IsZero(this FlagArray128 flags)
        {
            return flags[0] == 0 && flags[1] == 0 && flags[2] == 0 && flags[3] == 0;
        }

        public static bool IsEqualTo(this FlagArray128 flags, FlagArray128 other)
        {
            return flags[0] == other[0] && flags[1] == other[1] && flags[2] == other[2] && flags[3] == other[3];
        }

        public static bool FlagsMatch(this FlagArray128 flags, FlagArray128 other)
        {
            if (flags.IsZero() || other.IsZero())
                return false;

            var result = flags & other;
            int validCount = 0;

            for (int i = 0; i < 4; i++)
                if (result[i] != 0 || (flags[i] != 0 && other[i] == 0) || (flags[i] == 0 && other[i] == 0))
                    validCount++;

            return validCount == 4;
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
                scale.Id = CliDB.SpellScalingStorage.OrderByDescending(a => a.Key).First().Key + 1;
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
            aurOptions.ProcTypeMask[0] = spellInfo.ProcFlags[0];
            aurOptions.ProcTypeMask[1] = spellInfo.ProcFlags[1];
            aurOptions.ProcCategoryRecovery = spellInfo.ProcCooldown;
            aurOptions.DifficultyID = (byte)spellInfo.Difficulty;
            aurOptions.ProcChance = (byte)spellInfo.ProcChance;
            aurOptions.ProcCharges = (int)spellInfo.ProcCharges;
            aurOptions.SpellProcsPerMinuteID = spellInfo.SpellPPMId;

            if (aurOptions.Id == 0)
            {
                aurOptions.Id = CliDB.SpellAuraOptionsStorage.OrderByDescending(a => a.Key).First().Key + 1;
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
                aura.Id = CliDB.SpellAuraRestrictionsStorage.OrderByDescending(a => a.Key).First().Key + 1;
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
                req.Id = CliDB.SpellCastingRequirementsStorage.OrderByDescending(a => a.Key).First().Key + 1;

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
                cat.Id = CliDB.SpellCastingRequirementsStorage.OrderByDescending(a => a.Key).First().Key + 1;
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
                options.Id = CliDB.SpellClassOptionsStorage.OrderByDescending(a => a.Key).First().Key + 1;
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
                cdr.Id = CliDB.SpellCooldownsStorage.OrderByDescending(a => a.Key).First().Key + 1;
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
                equip.Id = CliDB.SpellEquippedItemsStorage.OrderByDescending(a => a.Key).First().Key + 1;
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
                sir.Id = CliDB.SpellEquippedItemsStorage.OrderByDescending(a => a.Key).First().Key + 1;
                spellInfo.SpellInterruptsId = sir.Id;
            }

            return sir;
        }

        public static List<SpellLabelRecord> GetSpellLabelRecords(this SpellInfo spellInfo)
        {
            List<SpellLabelRecord> labels = new List<SpellLabelRecord>();

            foreach (uint label in spellInfo.Labels)
            {
                SpellLabelRecord newLabel = new SpellLabelRecord();
                newLabel.LabelID = label;
                newLabel.SpellID = spellInfo.Id;

                var lbls = CliDB.SpellLabelStorage.Where(a => a.Value.LabelID == newLabel.LabelID && a.Value.SpellID == newLabel.SpellID);

                if (lbls.Count() > 0)
                    newLabel.Id = lbls.Last().Key;

                if (newLabel.Id == 0)
                {
                    var lblStmt = new PreparedStatement(DataAccess.SELECT_LATEST_SPECIFIC_SPELL_LABEL);
                    lblStmt.AddValue(0, newLabel.LabelID);
                    lblStmt.AddValue(1, newLabel.SpellID);
                    var dbLbl = DataAccess.GetHotfixValue(lblStmt);

                    if (dbLbl > 0)
                        newLabel.Id = dbLbl;
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
                sl.Id = CliDB.SpellLevelsStorage.OrderByDescending(a => a.Key).First().Key + 1;
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
                sr.Id = CliDB.SpellReagentsStorage.OrderByDescending(a => a.Key).First().Key + 1;
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
                sr.Id = CliDB.SpellShapeshiftStorage.OrderByDescending(a => a.Key).First().Key + 1;
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
            tr.Targets = (int)spellInfo.Targets;
            tr.Width = spellInfo.Width;
            tr.SpellID = spellInfo.Id;

            if (tr.Id == 0)
            {
                tr.Id = CliDB.SpellTargetRestrictionsStorage.OrderByDescending(a => a.Key).First().Key + 1;
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
                tr.Id = CliDB.SpellTotemsStorage.OrderByDescending(a => a.Key).First().Key + 1;
                spellInfo.TotemRecordID = tr.Id;
            }

            return tr;
        }
    }
}
