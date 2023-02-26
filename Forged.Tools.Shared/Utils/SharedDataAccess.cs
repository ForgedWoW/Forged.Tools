using Framework.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forged.Tools.Shared.Utils
{
    public static class SharedDataAccess
    {
        public static void UpdateDB2s(string appPath, string db2Path)
        {
            var versionFile = Path.Combine(appPath, "NewVersion.txt");
            var oldVersionFile = Path.Combine(appPath, "CurrentVersion.txt");

            bool newVersion = false;

            DownloadGoogleDriveFile.DriveDownloadFile("1jx6kFQnPR2GDSrLmwinwHGrheeD3SGUM", versionFile);

            if (!File.Exists(oldVersionFile))
            {
                newVersion = true;
                File.Copy(versionFile, oldVersionFile);
            }
            else
            {
                if (!int.TryParse(File.ReadAllText(versionFile).Trim(), out var newVers) ||
                    !int.TryParse(File.ReadAllText(oldVersionFile).Trim(), out var oldVer) ||
                    oldVer < newVers)
                {
                    newVersion = true;
                    File.Copy(versionFile, oldVersionFile);
                }
            }

            File.Delete(versionFile);

            if (newVersion && Directory.Exists(db2Path))
                Directory.Delete(db2Path, true);

            if (Directory.Exists(db2Path))
                return;

            var dbcs = "1Jo-7594VT-X0g0TZLApsq3Eg9bFKZbR_";

            var zipFile = Path.Combine(appPath, "enUS.zip");

            if (File.Exists(zipFile))
                File.Delete(zipFile);

            DownloadGoogleDriveFile.DriveDownloadFile(dbcs, zipFile);

            System.IO.Compression.ZipFile.ExtractToDirectory(zipFile, db2Path);
            Thread.Sleep(1000);
            File.Delete(zipFile);
        }

        public static void UpdateIcons(string appPath, string iconPath)
        {
            if (Directory.Exists(Path.Combine(iconPath, "Interface", "Icons")))
                return;

            var icons = "1pRZ04T67qePO-pLT3fK2gZ8MtIHvhns9";
            var zipFile = Path.Combine(appPath, "icons.zip");

            if (File.Exists(zipFile))
                File.Delete(zipFile);

            DownloadGoogleDriveFile.DriveDownloadFile(icons, zipFile);

            System.IO.Compression.ZipFile.ExtractToDirectory(zipFile, Path.Combine(iconPath, "Interface"));
            Thread.Sleep(1000);
            File.Delete(zipFile);
        }
    }
}
