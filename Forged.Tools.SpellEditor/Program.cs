using Forged.Tools.Shared.Database;
using Forged.Tools.SpellEditor.Utils;

namespace Forged.Tools.SpellEditor
{
    internal static class Program
    {
        public static MainForm mainForm;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            DB.Hotfix.Initialize(new MySqlConnectionInfo() 
            { 
                Database = Settings.Default.HotfixDatabaseName, 
                Host = Settings.Default.HotfixDatabaseHost, 
                Port = Settings.Default.HotfixDatabasePort,
                Username = Settings.Default.HotfixDatabaseUser, 
                Password = Settings.Default.HotfixDatabasePassword
            });
            var result = DB.Hotfix.Query(DataAccess.SELECT_SPELL_EFFECT_IDS);

            if (result.IsNull())
            {
                MessageBox.Show("Unable to connect to the database. Please check your settings in Forged.Tools.SpellEditor.dll.config.", "Database Error");
                Environment.Exit(0);
            }

            DB.Hotfix.PreparedStatements();

            Loading.ShowLoadingScreen();
            mainForm = new MainForm();
            Loading.CloseForm();

            Application.Run(mainForm);
        }
    }
}