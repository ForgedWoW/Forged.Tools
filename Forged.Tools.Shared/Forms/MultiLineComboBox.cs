// Copyright (c) Forged WoW LLC <https://github.com/ForgedWoW/ForgedCore>
// Licensed under GPL-3.0 license. See <https://github.com/ForgedWoW/ForgedCore/blob/master/LICENSE> for full information.

using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Forged.Tools.Shared.Forms
{
    public partial class MultiLineComboBox : ComboBox
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;        // x position of upper-left corner 
            public int Top;         // y position of upper-left corner 
            public int Right;       // x position of lower-right corner 
            public int Bottom;      // y position of lower-right corner 
        }

        public const int SWP_NOZORDER = 0x0004;
        public const int SWP_NOACTIVATE = 0x0010;
        public const int SWP_FRAMECHANGED = 0x0020;
        public const int SWP_NOOWNERZORDER = 0x0200;

        public const int WM_CTLCOLORLISTBOX = 0x0134;

        public uint SelectedItemId { get { return (uint)((DataRowView)SelectedItem)["id"]; } }
        public string ListItemDisplayMember { get; set; }

        private int _hwndDropDown = 0;

        internal List<int> ItemHeights = new List<int>();

        public MultiLineComboBox() : base()
        {
            ValueMember = "id";
            ListItemDisplayMember = "display";
            DisplayMember = "name";
            DropDownStyle = ComboBoxStyle.DropDownList;
        }

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == WM_CTLCOLORLISTBOX)
            {
                if (_hwndDropDown == 0)
                {
                    _hwndDropDown = m.LParam.ToInt32();

                    RECT r;
                    GetWindowRect((IntPtr)_hwndDropDown, out r);

                    int newHeight = 0;
                    int n = (Items.Count > MaxDropDownItems) ? MaxDropDownItems : Items.Count;
                    for (int i = 0; i < n; i++)
                    {
                        newHeight += ItemHeights[i];
                    }
                    newHeight += 5; //to stop scrollbars showing

                    SetWindowPos((IntPtr)_hwndDropDown, IntPtr.Zero,
                        r.Left,
                                 r.Top,
                                 DropDownWidth,
                                 newHeight,
                                 SWP_FRAMECHANGED |
                                     SWP_NOACTIVATE |
                                     SWP_NOZORDER |
                                     SWP_NOOWNERZORDER);
                }
            }

            base.WndProc(ref m);
        }

        protected override void OnDropDownClosed(EventArgs e)
        {
            _hwndDropDown = 0;
            base.OnDropDownClosed(e);
        }

        public MultiLineComboBox InitializeComboBox(DataTable items)
        {
            DrawMode = DrawMode.OwnerDrawVariable;
            DrawItem += new DrawItemEventHandler(this_DrawItem);
            MeasureItem += new MeasureItemEventHandler(this_MeasureItem);

            DataSource = new BindingSource(items, null);
            return this;
        }

        private void this_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            MultiLineComboBox cbox = (MultiLineComboBox)sender;
            DataRowView item = (DataRowView)cbox.Items[e.Index];
            string txt = item[ListItemDisplayMember].ToString();

            int height = Convert.ToInt32(e.Graphics.MeasureString(txt, cbox.Font).Height);

            e.ItemHeight = height + 4;
            e.ItemWidth = cbox.DropDownWidth;

            cbox.ItemHeights.Add(e.ItemHeight);
        }

        private void this_DrawItem(object sender, DrawItemEventArgs e)
        {
            MultiLineComboBox cbox = (MultiLineComboBox)sender;
            DataRowView item = (DataRowView)cbox.Items[e.Index];
            string txt = item[ListItemDisplayMember].ToString();

            e.DrawBackground();
            e.Graphics.DrawString(txt, cbox.Font, System.Drawing.Brushes.Black, new RectangleF(e.Bounds.X + 2, e.Bounds.Y + 2, e.Bounds.Width, e.Bounds.Height));
            e.Graphics.DrawLine(new Pen(Color.LightGray), e.Bounds.X, e.Bounds.Top + e.Bounds.Height - 1, e.Bounds.Width, e.Bounds.Top + e.Bounds.Height - 1);
            e.DrawFocusRectangle();
        }

        public void SetSelectedById(uint id)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (id == (uint)((DataRowView)Items[i])["id"])
                {
                    SelectedIndex = i;
                    return;
                }
            }
        }

        /// <summary>
        /// Contains 3 columns; (uint) id, (string) name, and (string) display
        /// </summary>
        public static DataTable GeneratteDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] { new DataColumn("id", typeof(uint)), new DataColumn("name"), new DataColumn("display") });
            return dt;
        }
    }
}
