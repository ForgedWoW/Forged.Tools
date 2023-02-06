using Game.DataStorage;

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
