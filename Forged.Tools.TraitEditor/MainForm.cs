// Copyright (c) Forged WoW LLC <https://github.com/ForgedWoW/ForgedCore>
// Licensed under GPL-3.0 license. See <https://github.com/ForgedWoW/ForgedCore/blob/master/LICENSE> for full information.

using Forged.Tools.TraitEditor.Utils;
using Forged.Tools.Shared.Traits;
using Game.DataStorage;
using Framework.Constants;

namespace Forged.Tools.TraitEditor
{
    public partial class MainForm : Form
    {
        public SpecID SelectedSpec = SpecID.None;
        public PictureBox SelectedCell;

        private ContainerControl _gridContainer;
        private Dictionary<uint, PictureBox> _nodes = new();
        private bool _building = false;

        public MainForm()
        {
            InitializeComponent();

            Program.DataAccess.LoadStores();
            TraitManager.Load();

            BuildGridContainer();
            PopulateTrees();
            listTrees.SelectedIndexChanged += ListTrees_SelectedIndexChanged;

            FormClosed += MainForm_FormClosed;
            _gridContainer.Paint += gridContainer_Paint;
        }

        private void gridContainer_Paint(object? sender, PaintEventArgs e)
        {
            if (_building) 
                return;

            Pen pen = new Pen(Color.White, 2);

            foreach (var cell in _nodes.Values)
            {
                CellValue cellVal = (CellValue)cell.Tag;

                if (cellVal.SpellID == 0)
                    continue;

                var node = TraitManager.TraitNodes[cellVal.TraitNode.Data.Id];

                if (node.ParentNodes.Count == 0)
                    continue;

                var p1 = new Point(cell.Location.X + (cell.Width / 2), cell.Location.Y + (cell.Height / 2));

                foreach (var pnode in node.ParentNodes)
                {
                    var pcell = GetCell(pnode.Item1.Data.Id);

                    if (pcell != null)
                    {
                        var p2 = new Point(pcell.Location.X + (pcell.Width / 2), pcell.Location.Y + (pcell.Height / 2));
                        e.Graphics.DrawLine(pen, p1, p2);
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
            if (listTrees.SelectedItem == null)
                return;

            var selected = (TreeListItem)listTrees.SelectedItem;

            if (SelectedSpec == selected.SpecID)
                return;

            _building = true;
            ClearCells();
            uint spec = (uint)selected.SpecID;
            SelectedSpec = selected.SpecID;

            foreach (var tree in TraitManager.GetTraitTreesBySpecID((int)spec))
                foreach (var node in tree.Nodes)
                {
                    if (_nodes.ContainsKey(node.Data.Id))
                        continue;

                    // load spec nodes by entry
                    foreach (TraitNodeEntry entry in node.Entries)
                        foreach (TraitCondRecord condition in entry.Conditions)
                            if (TraitManager.IsSpecSetMember(condition.SpecSetID, spec))
                                CreateNode(node);

                    // load spec nodes by condition
                    foreach (TraitCondRecord condition in node.Conditions)
                        if (TraitManager.IsSpecSetMember(condition.SpecSetID, spec))
                            foreach (TraitNodeEntry entry in node.Entries)
                                CreateNode(node);

                    // load class and spec nodes by group
                    foreach (TraitNodeGroup group in node.Groups)
                    {
                        foreach (TraitCondRecord condition in group.Conditions)
                            if (TraitManager.IsSpecSetMember(condition.SpecSetID, spec))
                                foreach (TraitNodeEntry entry in node.Entries)
                                    CreateNode(node);

                        // load class nodes
                        foreach (var cost in group.Costs)
                            if (cost.TraitCurrencyID == (int)Enum.Parse<ClassTraitCurrencyID>(selected.Class.ToString()))
                                CreateNode(node);
                    }
                }

            _building = false;
            _gridContainer.Refresh();
        }

        private void CreateNode(TraitNode node)
        {
            if (_nodes.ContainsKey(node.Data.Id))
                return;

            var cell = CreateCell(node.Data.PosX, node.Data.PosY);
            _nodes[node.Data.Id] = cell;

            if (cell != null)
            {
                // DataAccess.SkillLineStorage for info
                AddCellData(cell, node);
            }
        }

        private static void AddCellData(PictureBox cell, TraitNode node)
        {
            if (!string.IsNullOrEmpty(cell.Text))
                return;

            CellValue cellVal = (CellValue)cell.Tag; 

            if (TraitManager.TraitDefinitionByNodeID.ContainsKey(node.Data.Id))
            {
                // entry 1
                if (node.Entries.Count > 0 && CliDB.TraitDefinitionStorage.TryGetValue((uint)node.Entries[0].Data.TraitDefinitionID, out var def))
                {
                    if (def.VisibleSpellID != 0)
                    {
                        cellVal.SpellID = def.VisibleSpellID;

                        if (def.OverrideIcon != 0)
                            cell.BackgroundImage = Program.DataAccess.GetIcon(def.OverrideIcon).ResizeImage(50, 50);
                        else
                            cell.BackgroundImage = Program.DataAccess.GetIcon(def.VisibleSpellID).ResizeImage(50, 50);
                    }
                    else if (def.SpellID != 0)
                    {
                        cellVal.SpellID = def.SpellID;

                        if (def.OverrideIcon != 0)
                            cell.BackgroundImage = Program.DataAccess.GetIcon(def.OverrideIcon).ResizeImage(50, 50);
                        else
                            cell.BackgroundImage = Program.DataAccess.GetIcon(def.SpellID).ResizeImage(50, 50);
                    }
                }

                // entry 2
                if (node.Entries.Count > 1 && CliDB.TraitDefinitionStorage.TryGetValue((uint)node.Entries[1].Data.TraitDefinitionID, out var def2))
                {
                    if (def2.VisibleSpellID != 0)
                    {
                        if (def2.OverrideIcon != 0)
                            cell.Image = Program.DataAccess.GetIcon(def2.OverrideIcon).ResizeImage(50, 50).Half();
                        else
                            cell.Image = Program.DataAccess.GetIcon(def2.VisibleSpellID).ResizeImage(50, 50).Half();
                    }
                    else if (def2.SpellID != 0)
                    {
                        if (def2.OverrideIcon != 0)
                            cell.Image = Program.DataAccess.GetIcon(def2.OverrideIcon).ResizeImage(50, 50).Half();
                        else
                            cell.Image = Program.DataAccess.GetIcon(def2.SpellID).ResizeImage(50, 50).Half();
                    }
                }
            }

            cellVal.TraitNode = node;
        }

        private void PopulateTrees()
        {
            foreach (var spec in TraitManager.TraitTreeLoadoutsByChrSpecialization)
            {
                int id = (int)spec.Key;

                if (!TraitManager.ClassSpecs.TryGetValue(id, out var cls) || spec.Value.Count <= 0)
                    continue;

                var item = new TreeListItem()
                {
                    SpecID = (SpecID)id,
                    Class = cls,
                    TreeID = CliDB.TraitTreeLoadoutStorage[(uint)spec.Value.First().TraitTreeLoadoutID].TraitTreeID
                };
                item.Description = $"{item.SpecID} {item.Class}";

                listTrees.Items.Add(item);
            }
        }

        private void Box_MouseClick(object? sender, MouseEventArgs e)
        {
            if (sender == null)
                return;

            ClearNodeInfo();

            SelectedCell = (PictureBox)sender;
            CellValue val = (CellValue)SelectedCell.Tag;
            lblTraitId.Text = val.TraitNode.Data.Id.ToString();

            // entry 1
            if (val.TraitNode.Entries.Count > 0 && CliDB.TraitDefinitionStorage.TryGetValue((uint)val.TraitNode.Entries[0].Data.TraitDefinitionID, out var def))
            {
                if (def.SpellID != 0)
                {
                    numSpellId1.Value = def.SpellID;
                    lblSpellName1.Text = CliDB.SpellNameStorage.ContainsKey(def.SpellID) ? CliDB.SpellNameStorage[def.SpellID].Name[Locale.enUS] : string.Empty;
                }

                if (def.OverridesSpellID != 0)
                {
                    numOverrideSpellId1.Value = def.OverridesSpellID;
                    lblOverrideSpellName1.Text = CliDB.SpellNameStorage.ContainsKey(def.OverridesSpellID) ? CliDB.SpellNameStorage[def.OverridesSpellID].Name[Locale.enUS] : string.Empty;
                }

                if (def.VisibleSpellID != 0)
                {
                    numVisibleSpellId1.Value = def.VisibleSpellID;
                    lblVisibleSpellName1.Text = CliDB.SpellNameStorage.ContainsKey(def.VisibleSpellID) ? CliDB.SpellNameStorage[def.VisibleSpellID].Name[Locale.enUS] : string.Empty;
                }

                txtOverrideName1.Text = def.OverrideName[Locale.enUS];
                txtOverrideSubtext1.Text = def.OverrideSubtext[Locale.enUS];
                txtOverrideDesc1.Text = def.OverrideDescription[Locale.enUS];
            }

            if (val.TraitNode.Entries.Count > 1 && CliDB.TraitDefinitionStorage.TryGetValue((uint)val.TraitNode.Entries[1].Data.TraitDefinitionID, out var def2))
            {
                if (def2.SpellID != 0)
                {
                    numSpellId2.Value = def2.SpellID;
                    lblSpellName2.Text = CliDB.SpellNameStorage.ContainsKey(def2.SpellID) ? CliDB.SpellNameStorage[def2.SpellID].Name[Locale.enUS] : string.Empty;
                }

                if (def2.OverridesSpellID != 0)
                {
                    numOverrideSpellId2.Value = def2.OverridesSpellID;
                    lblOverrideSpellName2.Text = CliDB.SpellNameStorage.ContainsKey(def2.OverridesSpellID) ? CliDB.SpellNameStorage[def2.OverridesSpellID].Name[Locale.enUS] : string.Empty;
                }

                if (def2.VisibleSpellID != 0)
                {
                    numVisibleSpellId2.Value = def2.VisibleSpellID;
                    lblVisibleSpellName2.Text = CliDB.SpellNameStorage.ContainsKey(def2.VisibleSpellID) ? CliDB.SpellNameStorage[def2.VisibleSpellID].Name[Locale.enUS] : string.Empty;
                }

                txtOverrideName2.Text = def2.OverrideName[Locale.enUS];
                txtOverrideSubtext2.Text = def2.OverrideSubtext[Locale.enUS];
                txtOverrideDesc2.Text = def2.OverrideDescription[Locale.enUS];
            }
        }

        private void ClearNodeInfo()
        {
            lblTraitId.Text = string.Empty;

            // entry 1
            numSpellId1.Value = 0;
            lblSpellName1.Text = string.Empty;
            numOverrideSpellId1.Value = 0;
            lblOverrideSpellName1.Text = string.Empty;
            numVisibleSpellId1.Value = 0;
            lblVisibleSpellName1.Text = string.Empty;
            txtOverrideName1.Text = string.Empty;
            txtOverrideSubtext1.Text = string.Empty;
            txtOverrideDesc1.Text = string.Empty;

            // entry 2
            numSpellId2.Value = 0;
            lblSpellName2.Text = string.Empty;
            numOverrideSpellId2.Value = 0;
            lblOverrideSpellName2.Text = string.Empty;
            numVisibleSpellId2.Value = 0;
            lblVisibleSpellName2.Text = string.Empty;
            txtOverrideName2.Text = string.Empty;
            txtOverrideSubtext2.Text = string.Empty;
            txtOverrideDesc2.Text = string.Empty;
        }

        private void BuildGridContainer()
        {
            _gridContainer = new ContainerControl();
            _gridContainer.Parent = this;
            _gridContainer.Location = new Point(586, 12);
            _gridContainer.Size = new Size(1480, 1250);
            _gridContainer.AutoScroll = true;
            _gridContainer.Visible = true;
            _gridContainer.Enabled = true;
            _gridContainer.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom;
            _gridContainer.MouseDoubleClick += gridContainer_MouseDoubleClick;
        }

        private void gridContainer_MouseDoubleClick(object? sender, MouseEventArgs e)
        {
            if (sender != null) 
            { 
                
            }
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

        private PictureBox GetCell(uint nodeId)
        {
            return _nodes.ContainsKey(nodeId) ? _nodes[nodeId] : null;
        }

        private PictureBox CreateCell(int x, int y)
        {
            PictureBox box = new PictureBox();
            box.Parent = _gridContainer;
            box.Location = new Point(x != 0 ? x / 7 : x, y != 0 ? y / 7 : y); // dont divide xy for size if it is 0
            box.Size = new Size(50, 50);
            box.Tag = new CellValue();
            box.Enabled = true;
            box.MouseClick += Box_MouseClick;
            box.Cursor = Cursors.Arrow;
            box.BackgroundImageLayout = ImageLayout.Stretch;

            return box;
        }
    }
}