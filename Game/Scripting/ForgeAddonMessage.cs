using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Constants;
using Game.Entities;

namespace Game.Scripting
{
    public struct ForgeAddonMessage
    {
        public Player Player { get; set; }
        public ForgeTopic Topic { get; set; }
        public string Message { get; set; }
    }
}
