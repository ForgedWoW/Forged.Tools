using Framework.Constants;
using Game.DataStorage;

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
    }
}
