using Game.DataStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forged.Tools.Shared.DataStorage
{
    public class BaseDataAccess
    {
        public MultiMap<uint, SpellProcsPerMinuteModRecord> SpellProcsPerMinuteMods = new();

        public BaseDataAccess() { }

        public void LoadStores()
        {

        }
    }
}
