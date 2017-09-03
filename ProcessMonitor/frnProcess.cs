using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;

namespace processMonitor
{
    public partial class frmProcess : Form
    {
        classProcesses ps_;
        List<Dictionary<string, string>> htList_;
        int selectedIndex_;
        string selectedItemName_;

        public int SelectedIndex { get => selectedIndex_; }
        public string SelectedItemName { get => selectedItemName_; }

        public frmProcess()
        {
            InitializeComponent();
        }

        private void frmProcess_Load(object sender, EventArgs e)
        {
            List<ColumnHeader> chList = new List<ColumnHeader>();
            ColumnHeader ch = new ColumnHeader();
            ch.Text = "プロセス名";
            ch.Width = 150;
            chList.Add(ch);

            ch = new ColumnHeader();
            ch.Text = "プロセスID";
            ch.Width = 100;
            chList.Add(ch);

            ch = new ColumnHeader();
            ch.Text = "ファイル名";
            ch.Width = 400;
            chList.Add(ch);

            lvProcessList.VirtualMode = true;
            lvProcessList.Columns.AddRange(chList.ToArray());

            ps_ = new classProcesses();
            timer1.Interval = 1000;
            timer1.Start();
        }

        private void frmProcess_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Stop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            htList_ = ps_.GetCurrentValue();
            lvProcessList.VirtualListSize = htList_.Count;
            lvProcessList.Update();
            Debug.WriteLine(string.Format("timer {0}", htList_.Count));
        }

        private void lvProcessList_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            if(e.ItemIndex < 0) { return; }
            if(e.Item == null)
            {
                e.Item = new ListViewItem();
            }
            e.Item.SubItems.Clear();
            Dictionary<string, string> item = htList_[e.ItemIndex];
            e.Item.Text = item["Name"];
            e.Item.SubItems.Add(item["ProcessId"]);
            e.Item.SubItems.Add(item["ExecutablePath"]);
            Debug.WriteLine(string.Format("Retrieve {0}", e.ItemIndex));
        }

        private void lvProcessList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            selectedIndex_ = e.ItemIndex;
            selectedItemName_ = e.Item.Text;

        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
