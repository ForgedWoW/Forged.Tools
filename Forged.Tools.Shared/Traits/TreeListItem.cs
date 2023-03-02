// Copyright (c) Forged WoW LLC <https://github.com/ForgedWoW/ForgedCore>
// Licensed under GPL-3.0 license. See <https://github.com/ForgedWoW/ForgedCore/blob/master/LICENSE> for full information.

using Framework.Constants;

namespace Forged.Tools.Shared.Traits
{
    public sealed class TreeListItem
    {
        public string Description { get; set; }
        public Class Class { get; set; }
        public SpecID SpecID { get; set; }
        public int TreeID { get; set; }
    }
}
