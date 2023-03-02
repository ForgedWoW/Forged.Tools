// Copyright (c) Forged WoW LLC <https://github.com/ForgedWoW/ForgedCore>
// Licensed under GPL-3.0 license. See <https://github.com/ForgedWoW/ForgedCore/blob/master/LICENSE> for full information.

using static Forged.Tools.HotfixPatchCompiler.Enums;

namespace Forged.Tools.HotfixPatchCompiler.Models
{
    public class HotfixDataRecord
    {
        public int Id { get; set; }
        public uint UniqueId { get; set; }
        public uint TableHash { get; set; }
        public int RecordId { get; set; }
        public Status Status { get; set; }
        public int VerifiedBuild { get; set; }
    }
}
