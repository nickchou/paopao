using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaoPao.Comm
{
    public class ListBoxHelp
    {
        private delegate void AddListDelegate(ListBox box, string strshow);

        /// <summary>
        /// 在ListBox中插入数据
        /// </summary>
        /// <param name="box"></param>
        /// <param name="obj"></param>
        public static void Add(ListBox box, object obj)
        {
            string msg = "";
            if (obj != null) msg = obj.ToString().Trim();
            if (box.InvokeRequired)
            {
                box.Invoke(new AddListDelegate(Add), box, msg);
            }
            else
            {
                box.Items.Insert(0, msg);
            }
        }
        /// <summary>
        /// 清理ListBox现有的数据
        /// </summary>
        /// <param name="box"></param>
        public static void Clear(ListBox box)
        {
            box.Items.Clear();
        }
    }
}
