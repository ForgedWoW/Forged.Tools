using Forged.Tools.Shared.Constants;
using Forged.Tools.Shared.Database;
using Forged.Tools.Shared.Dynamic;
using Forged.Tools.Shared.DataStorage;
using Forged.Tools.Shared.Entities;
using Forged.Tools.Shared.Spells;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forged.Tools.TraitEditor.Utils
{
    public static class ExtensionMethods
    {
        public static Image GetImage(this SpellIconRecord iconRecord)
        {
            var path = Settings.Default.IconDir.Replace("{FullTraitEditorPath}", System.Reflection.Assembly.GetEntryAssembly().Location.Replace("Forged.Tools.TraitEditor.dll", ""))
                + iconRecord.TextureFilename + ".png";

            if (File.Exists(path))
                return Image.FromFile(path);

            return null;
        }
    }
}
