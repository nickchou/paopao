﻿using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace PaoPao.Comm
{
    public class LogHelper
    {
        /// <summary>
        /// 输出日志到Log4Net
        /// </summary>
        /// <param name="t"></param>
        /// <param name="ex"></param>
        #region static void WriteLog(Type t, Exception ex)

        public static void WriteLog(Type t, Exception ex)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Info("Error", ex);
        }

        #endregion

        /// <summary>
        /// 输出日志到Log4Net
        /// </summary>
        /// <param name="t"></param>
        /// <param name="msg"></param>
        #region static void WriteLog(Type t, string msg)

        public static void WriteLog(Type t, string msg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Error(msg);
        }

        #endregion

        public static void WriteLog(string Name, string ex)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(Name);
            log.Error(ex);
        }

        public static void WriteLog(string info)
        {
            ILog logtest = LogManager.GetLogger("ObserverLog");
            logtest.Info(info);
        }

        public static void WriteRecordLog(string info)
        {
            ILog logtest = LogManager.GetLogger("RecordLog");
            logtest.Info(info);
        }
    }
}
