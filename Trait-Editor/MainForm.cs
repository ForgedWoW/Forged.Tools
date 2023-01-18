using Trait_Editor.Utils;

namespace Trait_Editor
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            DataAccess.LoadStores();
            TraitManager.Load();
        }
    }
}