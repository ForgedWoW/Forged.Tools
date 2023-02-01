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
        private Dictionary<uint, TextBox> _nodes = new();

        public MainForm()
        {
            InitializeComponent();

            DataAccess.LoadStores();
            TraitManager.Load();

            BuildGridContainer();
            PopulateTrees();
            listTrees.SelectedIndexChanged += ListTrees_SelectedIndexChanged;
            listTrees.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom;

            FormClosed += MainForm_FormClosed;
            _gridContainer.Paint += gridContainer_Paint;
        }

        private void gridContainer_Paint(object? sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.White, 2);

            foreach (var cell in _nodes.Values)
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
                            //var pcell = GetCell(pnode.Key.Data.PosX, pnode.Key.Data.PosY);
                            var pcell = GetCell(pnode.Key.Data.Id);

                            if (pcell != null)
                            {
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

                //var def = DataAccess.TraitDefinitionStorage.Where(a => a.Value.SpellID == 108416).ToList().First().Key;
                //var dpNodeEntry = DataAccess.TraitNodeEntryStorage.Where(a => a.Value.TraitDefinitionID == def).First().Value.Id;
                //var dpNode = TraitManager.TraitNodes[DataAccess.TraitNodeXTraitNodeEntryStorage.Where(a => a.Value.TraitNodeEntryID == dpNodeEntry).First().Value.TraitNodeID];

                foreach (var spec in TraitManager.TraitTreeLoadoutsByChrSpecialization[(uint)selected.SpecID])
                {
                    var node = TraitManager.TraitNodes[spec.SelectedTraitNodeID];
                    var cell = CreateCell(node.Data.PosX, node.Data.PosY);
                    _nodes[node.Data.Id] = cell;

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
        }

        private void Box_DoubleClick(object? sender, EventArgs e)
        {
            if (sender != null)
            {
                SelectedCell = (TextBox)sender;
            }
        }

        private void BuildGridContainer()
        {
            _gridContainer = new ContainerControl();
            _gridContainer.Parent = this;
            _gridContainer.Location = new Point(268, 12);
            _gridContainer.Size = new Size(1004, 1237);
            _gridContainer.AutoScroll = true;
            _gridContainer.Visible = true;
            _gridContainer.Enabled = true;
            _gridContainer.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom;
        }

        private void ClearCells()
        {
            foreach (var node in _nodes)
            {
                node.Value.Dispose();
            }

            _nodes.Clear();
            _gridContainer.Controls.Clear();
        }

        private TextBox GetCell(uint nodeId)
        {
            return _nodes.ContainsKey(nodeId) ? _nodes[nodeId] : null;
        }

        private TextBox CreateCell(int x, int y)
        {
            TextBox box = new TextBox();
            box.Multiline = true;
            box.Parent = _gridContainer;
            box.Location = new Point(x != 0 ? x / 7 : x, y != 0 ? y / 7 : y); // dont divide xy for size if it is 0
            box.Size = new Size(50, 50);
            box.Tag = new CellValue() { Coordinate = new Coordinate(x, y) };
            box.Enabled = true;
            box.Visible = false;
            box.ReadOnly = true;
            box.DoubleClick += Box_DoubleClick;
            box.BackColor = Color.Gray;
            box.ForeColor = Color.Black;
            box.Cursor = Cursors.Arrow;

            return box;
        }
    }
}