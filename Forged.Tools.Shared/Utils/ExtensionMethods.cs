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
    }
}
