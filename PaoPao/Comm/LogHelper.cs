using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//[assembly: log4net.Config.XmlConfigurator(Watch = true, ConfigFileExtension = "config")]
namespace PaoPao.Comm
{
    public class LogHelper
    {
        public static readonly ILog infolog = LogManager.GetLogger("infolog");
        public static readonly ILog errorlog = LogManager.GetLogger("errorlog");
        public static void SetConfig()
        {
            log4net.Config.XmlConfigurator.Configure();
        }
        /// <summary>
        /// 设置lognet配置文件
        /// </summary>
        /// <param name="configFile"></param>
        public static void SetConfig(FileInfo configFile)
        {
            log4net.Config.XmlConfigurator.Configure(configFile);
        }
        /// <summary>
        /// 正常日志
        /// </summary>
        /// <param name="info"></param>
        public static void WriteLog(string info)
        {
            if (infolog.IsInfoEnabled)
            {
                infolog.Info(info);
            }
        }
        /// <summary>
        /// 异常日志
        /// </summary>
        /// <param name="info"></param>
        /// <param name="ex"></param>
        public static void WriteErrorLog(string info, Exception ex)
        {
            if (errorlog.IsInfoEnabled)
            {
                errorlog.Info(info, ex);
            }
        }
    }
}
