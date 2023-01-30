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

namespace Trait_Editor.Utils
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
