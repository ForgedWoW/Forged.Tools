using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HotfixPatchCompiler.Enums;

namespace HotfixPatchCompiler.Models
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
