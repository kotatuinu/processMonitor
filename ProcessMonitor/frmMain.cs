using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace processMonitor
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void プロセス選択PToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProcess frmProcess = new frmProcess();
            frmProcess.ShowDialog();
            string selectedItemName = frmProcess.SelectedItemName;
            Debug.WriteLine(string.Format("{0}", selectedItemName));
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            //Bitmapの作成
            Bitmap bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                Screen.PrimaryScreen.Bounds.Height);
            //Graphicsの作成
            Graphics g = Graphics.FromImage(bmp);
            //画面全体をコピーする
            g.CopyFromScreen(new Point(0, 0), new Point(0, 0), bmp.Size);
            //解放
            g.Dispose();

            //表示
            pictureBox1.Image = bmp;

        }

        private void btnCapture2_Click(object sender, EventArgs e)
        {
            Bitmap bmp = CaptureDisplay.CaptureActiveWindow();
            //表示
            pictureBox1.Image = bmp;
        }
    }
}
