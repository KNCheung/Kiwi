using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiwi
{
    class Program
    {
        static void Main(string[] args)
        {
            ContextMenuStrip menu = null;
            int tick = System.Environment.TickCount;

            try
            {
                menu = MenuCreator.Creator("config.xml");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "EXCEPTION");
                return;
            }

            menu.Items.Add(new ToolStripSeparator());
            menu.Items.Add(MenuCreator.GetDisabledMenuItem(string.Format("{0} ms", System.Environment.TickCount - tick)));

            menu.Show(System.Windows.Forms.Cursor.Position);
            Application.Run();
        }
    }
}
