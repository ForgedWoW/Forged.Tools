using Framework.Constants;
using Framework.GameMath;
using System.Data;
using System.Xml.Linq;
using Trait_Editor.Models;
using Trait_Editor.Utils;

namespace Trait_Editor
{
    public partial class MainForm : Form
    {
        public uint SelectedTree = 0;
        public TextBox SelectedCell;

        private ContainerControl gridContainer;

        Dictionary<int, List<TextBox>> Rows = new();

        public MainForm()
        {
            InitializeComponent();

            DataAccess.LoadStores();
            TraitManager.Load();

            BuildGrid();
            PopulateTrees();
            listTrees.SelectedIndexChanged += ListTrees_SelectedIndexChanged;
            listTrees.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom;

            FormClosed += MainForm_FormClosed;
        }

        private void MainForm_FormClosed(object? sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void ListTrees_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (listTrees.SelectedItem != null)
            {
                ClearCells();
                var selected = (TreeListItem)listTrees.SelectedItem;
                SelectedTree = selected.TreeID;
                var tree = TraitManager.TraitTrees[SelectedTree];

                foreach (var spec in TraitManager.TraitTreeLoadoutsByChrSpecialization[(uint)selected.SpecID])
                {
                    var node = TraitManager.TraitNodes[spec.SelectedTraitNodeID];
                    var cell = GetCell(node.Data.PosX, node.Data.PosY);

                    if (cell != null)
                    {
                        // DataAccess.SkillLineStorage for info
                        AddCellData(cell, node);
                    }
                }
            }
        }

        private static void AddCellData(TextBox cell, TraitNode node)
        {
            if (string.IsNullOrEmpty(cell.Text))
            {
                string display = "Not Found";
                if (TraitManager.TraitDefinitionByNodeID.ContainsKey(node.Data.Id))
                {
                    var def = TraitManager.TraitDefinitionByNodeID[node.Data.Id];
                    ((CellValue)cell.Tag).SpellID = def.SpellID;
                    display = def.OverrideName[Locale.enUS];

                    if (string.IsNullOrWhiteSpace(display))
                    {
                        display = DataAccess.SpellNameStorage[def.SpellID].Name[Locale.enUS];
                    }
                }

                ((CellValue)cell.Tag).Display = display;
                cell.Text = display;
                cell.BackColor = Color.White;
            }
        }

        private void PopulateTrees()
        {
            foreach (var spec in TraitManager.TraitTreeLoadoutsByChrSpecialization)
            {
                int id = (int)spec.Key;
                var item = new TreeListItem()
                {
                    SpecID = (SpecID)id,
                    Class = TraitManager.ClassSpecs[id],
                    TreeID = DataAccess.TraitTreeLoadoutStorage[spec.Value.First().TraitTreeLoadoutID].TraitTreeID
                };
                item.Description = $"{item.SpecID} {item.Class}";

                listTrees.Items.Add(item);
            }
        }

        private void Box_DoubleClick(object? sender, EventArgs e)
        {
            if (sender != null)
            {
                SelectedCell = (TextBox)sender;
            }
        }

        private void BuildGrid()
        {
            gridContainer = new ContainerControl();
            gridContainer.Parent = this;
            gridContainer.Location = new Point(268, 12);
            gridContainer.Size = new Size(1004, 1237);
            gridContainer.AutoScroll = true;
            gridContainer.Visible = true;
            gridContainer.Enabled = true;
            gridContainer.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom;

            int curX = 0;
            int curY = -300;

            List<DataGridViewColumn> cols = new List<DataGridViewColumn>();

            while (curX < 16900)
            {
                cols.Add(new DataGridViewColumn()
                {
                    Name = curX.ToString(),
                    Tag = curX,
                    CellTemplate = new DataGridViewTextBoxCell()
                });

                curX += 300;
            }

            int rowIndex = 0;
            int columnIndex = 0;

            while (curY < 7000)
            {
                List<TextBox> row = new();
                Rows[rowIndex] = row;

                curX = 0;
                columnIndex = 0;
                while (curX < 16900)
                {
                    TextBox box = new TextBox();
                    box.Multiline = true;
                    box.Parent = gridContainer;
                    box.Location = new Point(12 + (columnIndex * 52), 12 + (rowIndex * 52));
                    box.Size = new Size(50, 50);
                    box.Tag = new CellValue() { Coordinate = new Coordinate(curX, curY) };
                    box.Enabled = true;
                    box.Visible = true;
                    box.ReadOnly= true;
                    box.DoubleClick += Box_DoubleClick;
                    box.BackColor = Color.Gray;
                    box.ForeColor = Color.Black;
                    box.Cursor = Cursors.Arrow;

                    row.Add(box);

                    curX += 300;
                    columnIndex++;
                }

                curY += 300;
                rowIndex++;
            }
        }

        private void ClearCells()
        {
            foreach (var row in Rows)
            {
                foreach (var cell in row.Value)
                {
                    if (cell.Tag != null)
                    {
                        cell.Text = string.Empty;
                        ((CellValue)cell.Tag).Clear();
                        cell.BackColor = Color.Gray;
                    }
                }
            }
        }

        private TextBox GetCell(int x, int y)
        {
            foreach (var row in Rows)
            {
                foreach (var cell in row.Value)
                {
                    if (((CellValue)cell.Tag).CompareCoordinate(x, y))
                        return cell;
                }
            }

            return null;
        }
    }
}