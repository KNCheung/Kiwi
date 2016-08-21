using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiwi
{
    class CopynPaste
    {
        public static IDataObject GetContent()
        {
            IDataObject data = null, temp;
            temp = Clipboard.GetDataObject();
            SendKeys.SendWait("^c");
            while (data == null)
                data = Clipboard.GetDataObject();
            Clipboard.SetDataObject(temp, true);
            return data;
        }
    }
}
