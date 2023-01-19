using Framework.Database;
using Trait_Editor.Utils;

namespace Trait_Editor
{
    internal static class Program
    {
        public static MainForm MainForm;

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
            var result = DB.Hotfix.Query(DataAccess.DB_CONNECTION_QUERY);

            if (result.IsNull())
            {
                MessageBox.Show("Unable to connect to the database. Please check your settings in Trait-Editor.dll.config.", "Database Error");
                Environment.Exit(0);
            }

            DB.Hotfix.PreparedStatements();

            Loading.ShowLoadingScreen();
            MainForm = new MainForm();
            Loading.CloseForm();

            Application.Run(MainForm);
        }
    }
}