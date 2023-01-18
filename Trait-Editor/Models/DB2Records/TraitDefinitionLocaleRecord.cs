using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trait_Editor.Models.DB2Records
{
    public sealed class TraitDefinitionLocaleRecord
    {
        public uint Id;
        public string locale;
        public string OverrideName_lang;
        public string OverrideSubtext_lang;
        public string OverrideDescription_lang;
    }
}
