using Game.DataStorage;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

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

        public static Bitmap ResizeImage(this Image image, int width, int height)
        {
            if (image == null)
                return null;

            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        public static Bitmap Half(this Bitmap image)
        {
            if (image == null)
                return null;

            int targetWidth = 25;
            int targetHeight = 50;

            int x = image.Width / 2 - targetWidth;
            int y = image.Height / 2 - targetHeight / 2;

            Rectangle cropArea =
                new Rectangle(x, y, targetWidth, targetHeight);

            return image.Clone(cropArea, image.PixelFormat);
        }
    }
}
