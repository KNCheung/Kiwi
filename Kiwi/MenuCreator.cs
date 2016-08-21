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
        private static XmlDocument config = new XmlDocument();

        internal static ContextMenuStrip Creator(string configFileName)
        {

            config.Load(configFileName);

            XmlNode mainNode = config.SelectSingleNode("/kiwi/menu[@name='main']");
            if (mainNode == null)
                throw new Exception("\"main\" node not found");

            return CreateMenu(mainNode);
        }

        private static ContextMenuStrip CreateMenu(XmlNode menuNode)
        {
            ContextMenuStrip menu = new ContextMenuStrip();
            menu.Text = menuNode.Attributes["name"].Value;
            foreach (XmlNode x in menuNode.ChildNodes)
            {
                menu.Items.Add(GetMenuItem(x));
            }
            return menu;
        }

        private static ToolStripItem GetMenuItem(XmlNode item)
        {
            ToolStripMenuItem menuItem = new ToolStripMenuItem();
            switch (item.Attributes["type"].Value.ToLower())
            {
                case "text":
                    {
                        menuItem.Text = item.Attributes["name"].Value;
                        menuItem.Enabled = false;
                        break;
                    }
                case "seperator":
                    {
                        return new ToolStripSeparator();
                    }
                case "action":
                    {
                        menuItem.Text = item.Attributes["name"].Value;
                        menuItem.Click += (obj, args) => Actions.ActionHandler(item.SelectSingleNode("./action").InnerText);
                        break;
                    }
                case "submenu":
                    {
                        menuItem.DropDown = CreateMenu(item);
                        menuItem.Text = menuItem.DropDown.Text;
                        break;
                    }
                case "menu":
                    {
                        string name = item.Attributes["name"].Value;
                        XmlNode mainNode = config.SelectSingleNode("/kiwi/menu[@name='" + name + "']");
                        if (mainNode == null)
                            throw new Exception("\"" + name + "\" node not found");
                        menuItem.DropDown = CreateMenu(mainNode);
                        menuItem.Text = name;
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
