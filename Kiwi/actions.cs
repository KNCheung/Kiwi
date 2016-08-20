using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiwi
{
    class Actions
    {
        public static void ActionHandler(string name)
        {
            switch (name.Split(',')[0].ToLower())
            {
                default:
                    {
                        MessageBox.Show(name, "Hello World");
                        break;
                    }
            }
            Application.Exit();
        }
    }
}
