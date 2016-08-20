using System;
using System.Windows.Forms;

namespace Test
{
    class Program
    {
        private static ContextMenuStrip testMenuStrip;
        private static int tick = 0;

        static void Main(string[] args)
        {
            tick = System.Environment.TickCount;
            testMenuStrip = new ContextMenuStrip();

            testMenuStrip.Items.Clear();

            testMenuStrip.Items.Add(GetMenuItem("test", "this is test"));
            testMenuStrip.Items.Add(GetMenuItem("hello", "this is test"));
            testMenuStrip.Items.Add(GetMenuItem("world", "this is test"));

            testMenuStrip.Items.Add(new ToolStripSeparator());
            testMenuStrip.Items.Add(GetDisabledMenuItem(string.Format("{0} ms", System.Environment.TickCount - tick)));
            testMenuStrip.Show();

            Application.Run();
        }

        static private ToolStripItem GetMenuItem(string tip, string content)
        {
            ToolStripMenuItem menuItem = new ToolStripMenuItem();
            menuItem.Text = tip;
            menuItem.Click += new EventHandler((obj, EventArgs) => KiwiHandler(tip, content));
            return menuItem;
        }

        static private ToolStripItem GetDisabledMenuItem(string tip)
        {
            ToolStripMenuItem menuItem = new ToolStripMenuItem();
            menuItem.Text = tip;
            menuItem.Enabled = false;
            return menuItem;
        }

        static private void KiwiHandler(string tip, string content)
        {
            MessageBox.Show(content, tip);
            Application.Exit();
        }
    }
}
