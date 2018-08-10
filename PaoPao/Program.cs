using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaoPao
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //log4net
            string path = Application.StartupPath + @"../../../";
            Directory.SetCurrentDirectory(path);
            //设置log4net配置文件
            string strFilePath = System.IO.Directory.GetCurrentDirectory() + @"\log4net.cfg.xml";
            FileInfo file = new FileInfo(strFilePath);
            log4net.Config.XmlConfigurator.Configure(file);
            //程序默认
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
