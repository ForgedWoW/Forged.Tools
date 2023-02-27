using Framework.Constants;
using Framework.Dynamic;
using Game.DataStorage;
using System.Windows.Forms;

namespace Forged.Tools.Shared.Utils
{
    public static class ExtensionMethods
    {
        public static void AddIfDoesntExist<t>(this List<t> list, t toadd)
        {
            if (!list.Contains(toadd))
                list.Add(toadd);
        }

        public static void AddListItem<t, T>(this Dictionary<T, List<t>> dic, T key, t value)
        {
            if (!dic.ContainsKey(key))
                dic.Add(key, new List<t>());

            dic[key].AddIfDoesntExist(value);
        }

        public static LocalizedString DeepCopy(this LocalizedString str)
        {
            LocalizedString ret = new();

            for (int i = 0; i < (int)Locale.Total; i++)
            {
                ret[(Locale)i] = str[(Locale)i];
            }

            return ret;
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

        public static void AddEnumNames(this ListBox.ObjectCollection listValues, System.Type enumType)
        {
            foreach (string name in System.Enum.GetNames(enumType).AsSpan())
                listValues.Add(name);
        }

        public static void AddEnumNames(this ComboBox.ObjectCollection listValues, System.Type enumType)
        {
            foreach (string name in System.Enum.GetNames(enumType).AsSpan())
                listValues.Add(name);
        }

        public static void AddSelectedBitEnum<T>(this ListBox.SelectedObjectCollection listValues, T toMatch)
        {
            foreach (string attr in System.Enum.GetNames(typeof(T)).AsSpan())
                if ((Convert.ToInt64(toMatch) & Convert.ToInt64((T)System.Enum.Parse(typeof(T), attr))) != 0)
                    listValues.Add(attr);
        }

        public static void AddSelectedIntEnum<T>(this ListBox.SelectedObjectCollection listValues, T toMatch)
        {
            foreach (string attr in System.Enum.GetNames(typeof(T)).AsSpan())
                if (Convert.ToInt32(toMatch).ShiftContains(Convert.ToInt32((T)System.Enum.Parse(typeof(T), attr))))
                    listValues.Add(attr);
        }

        public static Int64 CalculateBitValue<T>(this ListBox.SelectedObjectCollection listValues)
        {
            Int64 retVal = 0;

            for (var i = listValues.Count - 1; i >= 0; i--)
            {
                var value = Convert.ToInt64((T)System.Enum.Parse(typeof(T), listValues[i].ToString()));

                if ((retVal & value) == 0)
                    retVal |= value;
            }

            return retVal;
        }

        public static int CalculateIntValue<T>(this ListBox.SelectedObjectCollection listValues)
        {
            int retVal = 0;

            for (var i = listValues.Count - 1; i >= 0; i--)
            {
                var value = Convert.ToInt32((T)System.Enum.Parse(typeof(T), listValues[i].ToString()));

                if (!retVal.ShiftContains(value))
                    retVal |= (1 << value);
            }

            return retVal;
        }

        public static void AddSelectedBitEnum(this ListBox.SelectedObjectCollection listValues, long toMatch, System.Type enumType)
        {
            foreach (string attr in System.Enum.GetNames(enumType).AsSpan())
                if ((toMatch & Convert.ToInt64(System.Enum.Parse(enumType, attr))) != 0)
                    listValues.Add(attr);
        }

        public static void AddSelectedIntEnum(this ListBox.SelectedObjectCollection listValues, int toMatch, System.Type enumType)
        {
            foreach (string attr in System.Enum.GetNames(enumType).AsSpan())
                if (toMatch.ShiftContains(Convert.ToInt32(System.Enum.Parse(enumType, attr))))
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

            foreach (string attr in System.Enum.GetNames(typeof(T)).AsSpan())
                ret |= Convert.ToInt64((T)System.Enum.Parse(typeof(T), attr));

            return ret;
        }

        public static Int64 EnumCollectionToInt<T>(this ListBox.SelectedObjectCollection listValues)
        {
            int ret = 0;

            foreach (string attr in System.Enum.GetNames(typeof(T)).AsSpan())
                ret |= (1 << Convert.ToInt32((T)System.Enum.Parse(typeof(T), attr)));

            return ret;
        }

        public static bool ShiftContains(this int left, int right)
        {
            return (left & (1 << right)) != 0;
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

        public static uint ReverseLookup<T>(this Dictionary<uint, T> dic, T value)
        {
            foreach (var kvp in dic)
            {
                if (kvp.Value.Equals(value))
                    return kvp.Key;
            }

            return 0;
        }
    }
}
