using System;
using System.Windows.Forms;
using System.Xml;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiwi
{
    class MenuCreator
    {
        internal static ContextMenuStrip Creator(string configFileName)
        {
            ContextMenuStrip menu = new ContextMenuStrip();
            XmlDocument config = new XmlDocument();

            config.Load(configFileName);

            XmlNode mainNode = null;
            foreach (XmlNode x in config.SelectNodes("/kiwi/menu"))
                if (x.Attributes["name"].Value.ToLower() == "main")
                {
                    mainNode = x;
                    break;
                }
            if (mainNode == null)
                throw new Exception("\"main\" node not found");
            foreach (XmlNode x in mainNode.ChildNodes)
            {
                menu.Items.Add(GetMenuItem(x));
            }

            return menu;
        }

        static private ToolStripItem GetMenuItem(XmlNode item)
        {
            ToolStripMenuItem menuItem = new ToolStripMenuItem();
            Console.WriteLine(item.Attributes["type"].Value);
            switch (item.Attributes["type"].Value.ToLower())
            {
                case "text":
                    {
                        menuItem.Text = item.SelectSingleNode("./text").InnerText;
                        menuItem.Enabled = false;
                        break;
                    }
                case "seperator":
                    {
                        return new ToolStripSeparator();
                    }
                case "action":
                    {
                        menuItem.Text = item.SelectSingleNode("./text").InnerText;
                        menuItem.Click += (obj, args) => Actions.ActionHandler(item.SelectSingleNode("./action").InnerText);
                        break;
                    }
            }
            return menuItem;
        }

        static internal ToolStripItem GetDisabledMenuItem(string tip)
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
