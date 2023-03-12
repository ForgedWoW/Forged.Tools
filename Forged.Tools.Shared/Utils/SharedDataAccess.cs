// Copyright (c) Forged WoW LLC <https://github.com/ForgedWoW/ForgedCore>
// Licensed under GPL-3.0 license. See <https://github.com/ForgedWoW/ForgedCore/blob/master/LICENSE> for full information.

using Framework.Configuration;
using Framework.Database;
using Game.DataStorage;
using Game.Extendability;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forged.Tools.Shared.Utils
{
    public static class SharedDataAccess
    {
        public const string SELECT_LATEST_ID = "SELECT `ID` FROM `{0}` ORDER BY `ID` DESC LIMIT 1;";

        public static void UpdateDB2s(string appPath, string db2Path)
        {
            var versionFile = Path.Combine(appPath, "NewVersion.txt");
            var oldVersionFile = Path.Combine(appPath, "CurrentVersion.txt");

            bool newVersion = false;

            DownloadGoogleDriveFile.DriveDownloadFile("1jx6kFQnPR2GDSrLmwinwHGrheeD3SGUM", versionFile);

            if (!Directory.Exists(db2Path) || Directory.GetDirectories(db2Path).Count() == 0)
                File.Delete(oldVersionFile);

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
                    File.Copy(versionFile, oldVersionFile, true);
                }
            }

            File.Delete(versionFile);

            if (!newVersion)
                return;

            var dbcs = "1Jo-7594VT-X0g0TZLApsq3Eg9bFKZbR_";

            var zipFile = Path.Combine(appPath, "enUS.zip");

            if (File.Exists(zipFile))
                File.Delete(zipFile);

            DownloadGoogleDriveFile.DriveDownloadFile(dbcs, zipFile);

            System.IO.Compression.ZipFile.ExtractToDirectory(zipFile, db2Path, true);
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

        public static uint GetLatestId<TValue>(Dictionary<uint, TValue> dataSet, string table)
        {
            uint hotfixId = 0;
            uint db2Id = 0;

            var result = DB.Hotfix.Query(string.Format(SELECT_LATEST_ID, table));

            if (!result.IsEmpty())
                hotfixId = result.Read<uint>(0);

            db2Id = dataSet.OrderByDescending(a => a.Key).First().Key;

            return Math.Max(db2Id, hotfixId);
        }

        public static uint GetNewId<TValue>(Dictionary<uint, TValue> dataSet, string table)
        {
            return GetLatestId(dataSet, table) + 1;
        }
    }
}
