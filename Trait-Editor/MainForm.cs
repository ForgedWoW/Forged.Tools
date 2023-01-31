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

        private ContainerControl _gridContainer;
        private Dictionary<int, List<TextBox>> _rows = new();

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
            _gridContainer.Paint += gridContainer_Paint;
        }

        private void gridContainer_Paint(object? sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.White, 2);
            
            foreach (var row in _rows)
            {
                foreach (var cell in row.Value)
                {
                    CellValue cellVal = (CellValue)cell.Tag;

                    if (cellVal.SpellID != 0)
                    {
                        var node = TraitManager.TraitNodes[cellVal.TraitNodeID];

                        if (node.ParentNodes.Count > 0)
                        {
                            var p1 = new Point(cell.Location.X + (cell.Width / 2), cell.Location.Y + (cell.Height / 2));

                            foreach (var pnode in node.ParentNodes)
                            {
                                var pcell = GetCell(pnode.Key.Data.PosX, pnode.Key.Data.PosY);
                                var p2 = new Point(pcell.Location.X + (pcell.Width / 2), pcell.Location.Y + (pcell.Height / 2));

                                e.Graphics.DrawLine(pen, p1, p2);
                            }
                        }
                    }
                }
            }

            pen.Dispose();
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

                var def = DataAccess.TraitDefinitionStorage.Where(a => a.Value.SpellID == 108416).ToList().First().Key;
                var dpNodeEntry = DataAccess.TraitNodeEntryStorage.Where(a => a.Value.TraitDefinitionID == def).First().Value.Id;
                var dpNode = TraitManager.TraitNodes[DataAccess.TraitNodeXTraitNodeEntryStorage.Where(a => a.Value.TraitNodeEntryID == dpNodeEntry).First().Value.TraitNodeID];

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

                _gridContainer.Refresh();
            }
        }

        private static void AddCellData(TextBox cell, TraitNode node)
        {
            if (string.IsNullOrEmpty(cell.Text))
            {
                string display = string.Empty;
                CellValue cellVal = (CellValue)cell.Tag; 
                if (TraitManager.TraitDefinitionByNodeID.ContainsKey(node.Data.Id))
                {
                    var def = TraitManager.TraitDefinitionByNodeID[node.Data.Id];
                    if (def.SpellID != 0)
                    {
                        cellVal.SpellID = def.SpellID;
                        display = def.OverrideName[Locale.enUS];

                        if (string.IsNullOrWhiteSpace(display))
                        {
                            display = DataAccess.SpellNameStorage[def.SpellID].Name[Locale.enUS];
                        }
                    }
                }

                cellVal.Display = display;
                cellVal.TraitNodeID = node.Data.Id;
                cell.Text = display;
                cell.BackColor = Color.White;
                cell.Visible = true;
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

            //foreach (var group in TraitManager.TraitNodesByGroup)
            //{
            //    var item = new TreeListItem()
            //    {
            //        SpecID = 0,
            //        Class = 0,
            //        TreeID = 0
            //    };
            //    item.Description = $"Group: {group.Key}";

            //    listTrees.Items.Add(item);
            //}
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
            _gridContainer = new ContainerControl();
            _gridContainer.Parent = this;
            _gridContainer.Location = new Point(268, 12);
            _gridContainer.Size = new Size(1004, 1237);
            _gridContainer.AutoScroll = true;
            _gridContainer.Visible = true;
            _gridContainer.Enabled = true;
            _gridContainer.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom;

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
                _rows[rowIndex] = row;

                curX = 0;
                columnIndex = 0;
                while (curX < 16900)
                {
                    TextBox box = new TextBox();
                    box.Multiline = true;
                    box.Parent = _gridContainer;
                    box.Location = new Point(12 + (columnIndex * 52), 12 + (rowIndex * 52));
                    box.Size = new Size(50, 50);
                    box.Tag = new CellValue() { Coordinate = new Coordinate(curX, curY) };
                    box.Enabled = true;
                    box.Visible = false;
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
            foreach (var row in _rows)
            {
                foreach (var cell in row.Value)
                {
                    if (cell.Tag != null)
                    {
                        cell.Text = string.Empty;
                        ((CellValue)cell.Tag).Clear();
                        cell.BackColor = Color.Gray;
                        cell.Visible = false;
                    }
                }
            }
        }

        private TextBox GetCell(int x, int y)
        {
            foreach (var row in _rows)
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