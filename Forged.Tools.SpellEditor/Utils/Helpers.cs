// Copyright (c) Forged WoW LLC <https://github.com/ForgedWoW/ForgedCore>
// Licensed under GPL-3.0 license. See <https://github.com/ForgedWoW/ForgedCore/blob/master/LICENSE> for full information.

using Game.DataStorage;
using Framework.Constants;
using Forged.Tools.Shared.Spells;
using Forged.Tools.Shared.Utils;

namespace Forged.Tools.SpellEditor.Utils
{
    public static class Helpers
    {
        public static int GetItemSubClassValue(ItemClass itemClass, ListBox.SelectedObjectCollection selectedItems)
        {
            int retVal = 0;

            switch (itemClass)
            {
                case ItemClass.Consumable:
                    for (var i = selectedItems.Count - 1; i >= 0; i--)
                    {
                        var value = Convert.ToInt32((ItemSubClassConsumable)Enum.Parse(typeof(ItemSubClassConsumable), selectedItems[i].ToString()));

                        if (!retVal.ShiftContains(value))
                            retVal |= (1 << value);
                    }
                    break;
                case ItemClass.Container:
                    for (var i = selectedItems.Count - 1; i >= 0; i--)
                    {
                        var value = Convert.ToInt32((ItemSubClassContainer)Enum.Parse(typeof(ItemSubClassContainer), selectedItems[i].ToString()));

                        if (!retVal.ShiftContains(value))
                            retVal |= (1 << value);
                    }
                    break;
                case ItemClass.Weapon:
                    for (var i = selectedItems.Count - 1; i >= 0; i--)
                    {
                        var value = Convert.ToInt32((ItemSubClassWeapon)Enum.Parse(typeof(ItemSubClassWeapon), selectedItems[i].ToString()));

                        if (!retVal.ShiftContains(value))
                            retVal |= (1 << value);
                    }
                    break;
                case ItemClass.Gem:
                    for (var i = selectedItems.Count - 1; i >= 0; i--)
                    {
                        var value = Convert.ToInt32((ItemSubClassGem)Enum.Parse(typeof(ItemSubClassGem), selectedItems[i].ToString()));

                        if (!retVal.ShiftContains(value))
                            retVal |= (1 << value);
                    }
                    break;
                case ItemClass.Armor:
                    for (var i = selectedItems.Count - 1; i >= 0; i--)
                    {
                        var value = Convert.ToInt32((ItemSubClassArmor)Enum.Parse(typeof(ItemSubClassArmor), selectedItems[i].ToString()));

                        if (!retVal.ShiftContains(value))
                            retVal |= (1 << value);
                    }
                    break;
                case ItemClass.Reagent:
                    for (var i = selectedItems.Count - 1; i >= 0; i--)
                    {
                        var value = Convert.ToInt32((ItemSubClassReagent)Enum.Parse(typeof(ItemSubClassReagent), selectedItems[i].ToString()));

                        if (!retVal.ShiftContains(value))
                            retVal |= (1 << value);
                    }
                    break;
                case ItemClass.Projectile:
                    for (var i = selectedItems.Count - 1; i >= 0; i--)
                    {
                        var value = Convert.ToInt32((ItemSubClassProjectile)Enum.Parse(typeof(ItemSubClassProjectile), selectedItems[i].ToString()));

                        if (!retVal.ShiftContains(value))
                            retVal |= (1 << value);
                    }
                    break;
                case ItemClass.TradeGoods:
                    for (var i = selectedItems.Count - 1; i >= 0; i--)
                    {
                        var value = Convert.ToInt32((ItemSubClassTradeGoods)Enum.Parse(typeof(ItemSubClassTradeGoods), selectedItems[i].ToString()));

                        if (!retVal.ShiftContains(value))
                            retVal |= (1 << value);
                    }
                    break;
                case ItemClass.ItemEnhancement:
                    for (var i = selectedItems.Count - 1; i >= 0; i--)
                    {
                        var value = Convert.ToInt32((ItemSubclassItemEnhancement)Enum.Parse(typeof(ItemSubclassItemEnhancement), selectedItems[i].ToString()));

                        if (!retVal.ShiftContains(value))
                            retVal |= (1 << value);
                    }
                    break;
                case ItemClass.Recipe:
                    for (var i = selectedItems.Count - 1; i >= 0; i--)
                    {
                        var value = Convert.ToInt32((ItemSubClassRecipe)Enum.Parse(typeof(ItemSubClassRecipe), selectedItems[i].ToString()));

                        if (!retVal.ShiftContains(value))
                            retVal |= (1 << value);
                    }
                    break;
                case ItemClass.Money:
                    for (var i = selectedItems.Count - 1; i >= 0; i--)
                    {
                        var value = Convert.ToInt32((ItemSubClassMoney)Enum.Parse(typeof(ItemSubClassMoney), selectedItems[i].ToString()));

                        if (!retVal.ShiftContains(value))
                            retVal |= (1 << value);
                    }
                    break;
                case ItemClass.Quiver:
                    for (var i = selectedItems.Count - 1; i >= 0; i--)
                    {
                        var value = Convert.ToInt32((ItemSubClassQuiver)Enum.Parse(typeof(ItemSubClassQuiver), selectedItems[i].ToString()));

                        if (!retVal.ShiftContains(value))
                            retVal |= (1 << value);
                    }
                    break;
                case ItemClass.Quest:
                    for (var i = selectedItems.Count - 1; i >= 0; i--)
                    {
                        var value = Convert.ToInt32((ItemSubClassQuest)Enum.Parse(typeof(ItemSubClassQuest), selectedItems[i].ToString()));

                        if (!retVal.ShiftContains(value))
                            retVal |= (1 << value);
                    }
                    break;
                case ItemClass.Key:
                    for (var i = selectedItems.Count - 1; i >= 0; i--)
                    {
                        var value = Convert.ToInt32((ItemSubClassKey)Enum.Parse(typeof(ItemSubClassKey), selectedItems[i].ToString()));

                        if (!retVal.ShiftContains(value))
                            retVal |= (1 << value);
                    }
                    break;
                case ItemClass.Permanent:
                    for (var i = selectedItems.Count - 1; i >= 0; i--)
                    {
                        var value = Convert.ToInt32((ItemSubClassPermanent)Enum.Parse(typeof(ItemSubClassPermanent), selectedItems[i].ToString()));

                        if (!retVal.ShiftContains(value))
                            retVal |= (1 << value);
                    }
                    break;
                case ItemClass.Glyph:
                    for (var i = selectedItems.Count - 1; i >= 0; i--)
                    {
                        var value = Convert.ToInt32((ItemSubClassGlyph)Enum.Parse(typeof(ItemSubClassGlyph), selectedItems[i].ToString()));

                        if (!retVal.ShiftContains(value))
                            retVal |= (1 << value);
                    }
                    break;
                case ItemClass.BattlePets:
                    for (var i = selectedItems.Count - 1; i >= 0; i--)
                    {
                        var value = Convert.ToInt32((ItemSubclassBattlePet)Enum.Parse(typeof(ItemSubclassBattlePet), selectedItems[i].ToString()));

                        if (!retVal.ShiftContains(value))
                            retVal |= (1 << value);
                    }
                    break;
                case ItemClass.WowToken:
                    for (var i = selectedItems.Count - 1; i >= 0; i--)
                    {
                        var value = Convert.ToInt32((ItemSubclassWowToken)Enum.Parse(typeof(ItemSubclassWowToken), selectedItems[i].ToString()));

                        if (!retVal.ShiftContains(value))
                            retVal |= (1 << value);
                    }
                    break;
                default:
                    return 0;
            }

            return retVal;
        }

        public static GroupBox CopyCurveEffect(GroupBox toCopy, int effIndex, bool newEffect = false)
        {
            GroupBox gbNew = new GroupBox();

            gbNew.Name = Guid.NewGuid().ToString().Replace("-", "");
            gbNew.Text = "Effect " + effIndex.ToString();
            gbNew.Size = toCopy.Size;
            gbNew.Visible = true;

            // labels
            foreach (Control control in toCopy.Controls)
            {
                Type controlType = control.GetType();

                if (controlType == typeof(Label))
                {
                    var cToCopy = (Label)control;
                    var newControl = new Label();
                    newControl.Location = cToCopy.Location;
                    newControl.Size = cToCopy.Size;
                    newControl.Text = cToCopy.Text;
                    newControl.Tag = cToCopy.Tag;
                    newControl.Enabled = cToCopy.Enabled;
                    gbNew.Controls.Add(newControl);
                }
                else if (controlType == typeof(TextBox))
                {
                    var cToCopy = (TextBox)control;
                    var newControl = new TextBox();
                    newControl.Location = cToCopy.Location;
                    newControl.Size = cToCopy.Size;
                    newControl.Tag = cToCopy.Tag;
                    newControl.Text = cToCopy.Text;
                    newControl.Enabled = cToCopy.Enabled;
                    newControl.ReadOnly = cToCopy.ReadOnly;

                    newControl.MakeNumberBox();
                    gbNew.Controls.Add(newControl);
                }
            }

            return gbNew;
        }

        public static string SpellDisplayName(SpellNameRecord spell)
        {
            return SpellDisplayName(spell.Id, spell.Name[Locale.enUS]);
        }

        public static string SpellDisplayName(uint id, string name)
        {
            return $"{id} - {name}";
        }

        public static decimal CurrentRange(decimal numCurentMin, decimal numCurentMax)
        {
            return numCurentMax - numCurentMin + 1;
        }
    }
}
