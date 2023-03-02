// Copyright (c) Forged WoW LLC <https://github.com/ForgedWoW/ForgedCore>
// Licensed under GPL-3.0 license. See <https://github.com/ForgedWoW/ForgedCore/blob/master/LICENSE> for full information.

using Framework.Configuration;
using Framework.Database;

namespace Forged.Tools.HotfixPatchCompiler
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            if (!ConfigMgr.Load("WorldServer.conf"))
                MessageBox.Show("Copy your WorldServer.conf to the spell editor directory, using dev db settings.", "Error Loading", MessageBoxButtons.OK);

            DB.Hotfix.Initialize(new MySqlConnectionInfo()
            {
                Database = ConfigMgr.GetDefaultValue("HotfixDatabaseInfo.Database", "hotfixes"),
                Host = ConfigMgr.GetDefaultValue("HotfixDatabaseInfo.Host", ""),
                PortOrSocket = ConfigMgr.GetDefaultValue("HotfixDatabaseInfo.Port", "3306"),
                Username = ConfigMgr.GetDefaultValue("HotfixDatabaseInfo.Username", ""),
                Password = ConfigMgr.GetDefaultValue("HotfixDatabaseInfo.Password", "")
            });
            var result = DB.Hotfix.Query("SHOW TABLES;");

            if (result.IsEmpty())
            {
                MessageBox.Show("Unable to connect to the database. Please check your settings.", "Database Error");
                Environment.Exit(0);
            }

            Application.Run(new Form1());
        }
    }
}
