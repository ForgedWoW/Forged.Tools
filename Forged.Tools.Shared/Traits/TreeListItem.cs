using Forged.Tools.Shared.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forged.Tools.Shared.Traits
{
    public sealed class TreeListItem
    {
        public string Description { get; set; }
        public Class Class { get; set; }
        public SpecID SpecID { get; set; }
        public uint TreeID { get; set; }
    }
}
