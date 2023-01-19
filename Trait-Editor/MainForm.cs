using Framework.Constants;
using System.Data;
using Trait_Editor.Models;
using Trait_Editor.Utils;

namespace Trait_Editor
{
    public partial class MainForm : Form
    {
        public uint SelectedTree = 0;
        public DataGridViewTextBoxCell SelectedCell;

        private DataGridView traitGrid;
        private ContainerControl gridContainer;

        public MainForm()
        {
            InitializeComponent();

            DataAccess.LoadStores();
            TraitManager.Load();

            BuildGrid();
            traitGrid.ReadOnly = true;
            PopulateTrees();
            listTrees.SelectedIndexChanged += ListTrees_SelectedIndexChanged;
            
            FormClosed += MainForm_FormClosed;
        }

        private void MainForm_FormClosed(object? sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void ListTrees_SelectedIndexChanged(object? sender, EventArgs e)
        {
            ClearCells();
            SelectedTree = uint.Parse(((string)listTrees.SelectedItem).Split("-").Last());

            foreach(var node in TraitManager.TraitTrees[SelectedTree].Nodes)
            {
                var coord = new Coordinate(node.Data.PosX, node.Data.PosY);
                var cell = GetCell(coord);

                if (cell != null)
                {
                    // DataAccess.SkillLineStorage for info
                    cell.Value = "Thing";
                }
            }
        }

        private void PopulateTrees()
        {
            foreach (var tree in TraitManager.TraitTrees)
            {
                listTrees.Items.Add($"{tree.Value.ConfigType}-{tree.Value.Data.Id}");
                //tree.Value.ConfigType
            }
        }

        private void TraitGrid_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (sender != null)
            {
                SelectedCell = (DataGridViewTextBoxCell)((DataGridView)sender).CurrentCell;
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

            traitGrid = new DataGridView();
            traitGrid.Parent = gridContainer;
            ((System.ComponentModel.ISupportInitialize)(traitGrid)).BeginInit();
            SuspendLayout();
            traitGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            traitGrid.Location = new Point(15, 15);
            traitGrid.Name = "myNewGrid";
            traitGrid.Size = new Size(2000, 1200);
            traitGrid.TabIndex = 0;
            traitGrid.ColumnHeadersVisible = true;
            traitGrid.RowHeadersVisible = true;
            traitGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            traitGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            traitGrid.CellDoubleClick += TraitGrid_CellDoubleClick;
            ((System.ComponentModel.ISupportInitialize)(traitGrid)).EndInit();
            ResumeLayout(false);
            traitGrid.Visible = true;

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

            traitGrid.Columns.AddRange(cols.OrderBy(a => a.Tag).ToArray());

            while (curY < 7000)
            {
                var row = new DataGridViewRow() { Tag = curY };

                curX = 0;
                while (curX < 16900)
                {
                    row.Cells.Add(new DataGridViewTextBoxCell() 
                    { 
                        Tag = new Coordinate(curX, curY),
                        Style = new DataGridViewCellStyle { WrapMode = DataGridViewTriState.True }
                    });

                    curX += 300;
                }

                traitGrid.Rows.Add(row);

                curY += 300;
            }
        }

        private void ClearCells()
        {
            foreach (DataGridViewRow row in traitGrid.Rows)
            {
                foreach (DataGridViewTextBoxCell cell in row.Cells)
                {
                    cell.Value = string.Empty;
                }
            }
        }

        private DataGridViewTextBoxCell GetCell(Coordinate coord)
        {
            foreach (DataGridViewRow row in traitGrid.Rows)
            {
                foreach (DataGridViewTextBoxCell cell in row.Cells)
                {
                    if (((Coordinate)cell.Tag).Equals(coord))
                        return cell;
                }
            }

            return null;
        }
    }
}