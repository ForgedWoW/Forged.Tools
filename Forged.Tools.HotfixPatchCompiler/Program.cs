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

            DB.Hotfix.Initialize(new MySqlConnectionInfo()
            {
                Database = Settings.Default.HotfixDatabaseName,
                Host = Settings.Default.HotfixDatabaseHost,
                PortOrSocket = Settings.Default.HotfixDatabasePort,
                Username = Settings.Default.HotfixDatabaseUser,
                Password = Settings.Default.HotfixDatabasePassword
            });
            var result = DB.Hotfix.Query("SELECT ID FROM spell_effect LIMIT 1;");

            if (result == null)
            {
                MessageBox.Show("Unable to connect to the database. Please check your settings in Forged.Tools.SpellEditor.dll.config.", "Database Error");
                Environment.Exit(0);
            }

            Application.Run(new Form1());
        }
    }
}