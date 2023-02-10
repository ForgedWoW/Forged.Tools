using Forged.Tools.SpellEditor.Utils;
using Framework.Configuration;
using Framework.Database;

namespace Forged.Tools.SpellEditor
{
    internal static class Program
    {
        public static MainForm mainForm;
        public static DataAccess DataAccess;

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
                Host = ConfigMgr.GetDefaultValue("HotfixDatabaseInfo.Host", "23.116.116.43"),  
                PortOrSocket = ConfigMgr.GetDefaultValue("HotfixDatabaseInfo.Port", "3306"),
                Username = ConfigMgr.GetDefaultValue("HotfixDatabaseInfo.Username", "dev"), 
                Password = ConfigMgr.GetDefaultValue("HotfixDatabaseInfo.Password", "forgeddev!123")
            });
            var result = DB.Hotfix.Query(DataAccess.SELECT_SPELL_EFFECT_IDS);

            if (result == null)
            {
                MessageBox.Show("Unable to connect to the database. Please check your settings.", "Database Error");
                Environment.Exit(0);
            }

            DataAccess = new();

            DB.Hotfix.PreparedStatements();

            Loading.ShowLoadingScreen();
            mainForm = new MainForm();
            Loading.CloseForm();

            Application.Run(mainForm);
        }
    }
}