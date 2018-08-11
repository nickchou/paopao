using PaoPao.Comm;
using PaoPao.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using PaoPao.TypeExt;

namespace PaoPao
{
    public partial class MainForm : Form
    {
        #region 初始化信息
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            lbMsg.Text = "";
            txtIds.Text = "1,1,1\r\n2,2,2\r\n3,3,3\r\n4,4,4\r\n5,5,5\r\n6,6,6\r\n7,7,7\r\n8,8,8\r\n9,9,9\r\n10,10,10\r\n11,11,11\r\n";
            lblCopyMsg.Text = "";
            btnPause.Enabled = false;
            btnAbort.Enabled = false;
        }
        #endregion

        /// <summary>
        /// 线程数量
        /// </summary>
        private int ThreadNum = 1;
        /// <summary>
        /// 每次执行暂停多少毫秒
        /// </summary>
        private int ThreadSleep = 1;
        /// <summary>
        /// 线程信号变量，为false 则线程需要一直等待
        /// </summary>
        private bool ThreadSignal = true;
        /// <summary>
        /// 线程池
        /// </summary>
        List<Thread> ThreadPools = new List<Thread>();
        /// <summary>
        /// 待执行的总数
        /// </summary>
        private int TodoCount = 0;
        /// <summary>
        /// 已经执行的总数
        /// </summary>
        private int DoingCount = 0;

        public delegate void DelegName(List<Parameter> pros);
        /// <summary>
        /// 事件型号量，默认为false=不阻塞，true=阻塞
        /// </summary>
        static EventWaitHandle EventHandle = new EventWaitHandle(false, EventResetMode.ManualReset);

        #region 按钮Click事件

        public void DoIt2(object obj)
        {
            List<Parameter> list = (List<Parameter>)obj;
            for (int i = 0; i < list.Count; i++)
            {
                Parameter p = list[i];
                Thread.Sleep(ThreadSleep);
            }

        }
        /// <summary>
        /// 开始执行跑数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGO_Click(object sender, EventArgs e)
        {
            ThreadPools.Clear();
            ThreadSignal = true; //信号量设置为true
            btnGO.Enabled = false;//开始按钮不可用
            btnPause.Enabled = true;//可暂停
            btnAbort.Enabled = true;//可以终止

            ThreadNum = (int)numDownThread.Value;
            ThreadSleep = (int)numDownMs.Value;
            //获取产品
            List<Parameter> ps = GetParams();
            //给待执行的总数赋值，好计算线程是否执行完毕
            TodoCount = ps.Count;
            //计算每个线程需要跑的数量，遇到小数进1
            int PageSize = (int)Math.Ceiling(ps.Count * 1.0 / ThreadNum);
            //根据数据和线程数构造线程池和数据
            for (int i = 0; i < ThreadNum; i++)
            {
                List<Parameter> para = ps.GetPageDataByIndex(i + 1, PageSize);
                if (para.Count > 0)
                {
                    string thName = $"{i + 1}";
                    Thread thread = new Thread(() => DoIt3(para, thName))
                    {
                        Name = thName
                    };
                    ThreadPools.Add(thread);
                }
                else
                {
                    break;
                }
            }
            //初始化基本信息
            ListBoxHelp.Clear(lbRes);
            lblCopyMsg.Text = "";
            //呈现线程信息
            ListBoxHelp.Add(lbRes, $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}|共匹配{ps.Count}条数据,线程数：{ThreadPools.Count},每个线程：{PageSize}条数据" + "\r\n");
            lbMsg.Text = string.Format("找到{0}批次数据", ps.Count);
            //Start thread
            foreach (var th in ThreadPools)
            {
                th.Start();
            }
            //Thread t = new Thread(new ParameterizedThreadStart(DoIt2));
            //t.Start(ps);

            //Thread thread = new Thread(() => DoIt3(ps, "thread1"));
            //thread.Start();

            //方式二

            //THREAD_NUM = (int)numDownThread.Value;
            //if (THREAD_NUM < 1) THREAD_NUM = 1;
            //THREAD_SLEEP_MS = (int)numDownMs.Value;
            ////清理ListBox
            //ListBoxHelp.Clear(lbRes);
            //lblCopyMsg.Text = "";
            ////获取产品
            //List<Parameter> ps = GetParams();
            //lbMsg.Text = string.Format("找到{0}条数据批次", ps.Count);
            ////委托开始
            //DelegName delegName = new DelegName(DoIt);
            //AsyncCallback callback = new AsyncCallback(CallbackMethod);
            //delegName.BeginInvoke(ps, callback, delegName);
        }
        /// <summary>
        /// 线程暂停
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPause_Click(object sender, EventArgs e)
        {
            EventHandle.WaitOne();
            ListBoxHelp.Add(lbRes, $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}|暂停。" + "\r\n");
            ThreadSignal = false; //信号量设置为false
            btnGO.Enabled = true;//继续按钮可用
            btnGO.Text = "继续";
            btnPause.Enabled = false;//可暂停
            btnAbort.Enabled = true;//可以终止
        }
        /// <summary>
        /// 线程终止
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAbort_Click(object sender, EventArgs e)
        {
            ThreadSignal = false; //信号量设置为false
            btnGO.Enabled = true;//开始按钮不可用
            btnGO.Text = "开始";
            btnPause.Enabled = true;//可暂停
            btnAbort.Enabled = true;//可以终止
        }
        public void CallbackMethod(IAsyncResult result)
        {
        }
        /// <summary>
        /// 复制跑数据的结果页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCopy_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < lbRes.Items.Count; i++)
            {
                sb.AppendFormat("{0}\r\n", lbRes.Items[i].ToString());
            }
            if (sb.Length > 0)
            {
                Clipboard.SetDataObject(sb.ToString());
                lblCopyMsg.ForeColor = Color.Green;
                lblCopyMsg.Text = "复制成功";
            }
            else
            {
                lblCopyMsg.ForeColor = Color.Red;
                lblCopyMsg.Text = "没有数据";
            }
        }
        /// <summary>
        /// 打开日志文件的路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            string logPath = Path.GetDirectoryName(this.GetType().Assembly.Location) + "\\Logs\\InfoLog";
            if (Directory.Exists(logPath))
            {
                Process.Start(logPath);
            }
            else
            {
                MessageBox.Show("未找到指定的日志文件:" + logPath, "error");
                Path.GetDirectoryName(this.GetType().Assembly.Location);
            }
        }
        #endregion

        #region Thread Todo

        public void DoIt3(List<Parameter> pros, string threadName)
        {
            ListBoxHelp.Add(lbRes, $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}|thread-{threadName} start..." + "\r\n");
            string TempUrl = txtURL.Text.Trim();
            for (int i = 0; i < pros.Count; i++)
            {
                EventHandle.WaitOne();
                //while (ThreadSignal)
                //{
                //http
                HttpUtility http = new HttpUtility();
                StringBuilder sb = new StringBuilder();
                StringBuilder sbLog = new StringBuilder();
                //获取参数
                Parameter para = pros[i];
                //计时开始
                Stopwatch sw = new Stopwatch();
                sw.Start();
                sb.AppendFormat("{0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                //根据参数重新匹配http url 
                string url = GenerateFullURL(TempUrl, para.Param);
                //请求http
                string value = "successs"; //http.CreateGet(url);
                sw.Stop();
                sb.AppendFormat("|thread-{0}({1}/{2})serid:{3}", threadName, i + 1, pros.Count, para.Id);
                sb.AppendFormat("|url:{0}", url);
                sb.AppendFormat("|times:{0}s", sw.ElapsedMilliseconds * 1.0 / 1000);
                sbLog.AppendFormat("serid:{0},{1}", para.Id, value); //TextHelper.ClearHtml(value)
                ListBoxHelp.Add(lbRes, sb.ToString() + "\r\n");
                LogHelper.WriteLog(sbLog.ToString());
                //执行完毕后计数
                CalculatedQuantity();
                Thread.Sleep(ThreadSleep);

            }
            ListBoxHelp.Add(lbRes, $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}|thread-{threadName} finished!" + "\r\n");
        }
        /// <summary>
        /// 计算线程执行的数量
        /// </summary>
        public void CalculatedQuantity()
        {
            DoingCount++;
            if (DoingCount == TodoCount)
            {
                ListBoxHelp.Add(lbRes, $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}|全部执行完毕。" + "\r\n");
                ThreadSignal = true; //信号量设置为true
                btnGO.Enabled = true;//开始按钮不可用
                btnGO.Text = "开始";
                btnPause.Enabled = false;//可暂停
                btnAbort.Enabled = false;//可以终止
            }
        }
        public void DoIt(List<Parameter> pros)
        {
            string TempUrl = txtURL.Text.Trim();
            for (int i = 0; i < pros.Count; i++)
            {
                //http
                HttpUtility http = new HttpUtility();
                StringBuilder sb = new StringBuilder();
                StringBuilder sbLog = new StringBuilder();
                //获取参数
                Parameter para = pros[i];
                //计时开始
                Stopwatch sw = new Stopwatch();
                sw.Start();

                string url = GenerateFullURL(TempUrl, para.Param);
                sb.AppendFormat("{0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));

                string value = http.CreateGet(url);
                //string msg = string.Format("line:{0},time:{1},num:{2},value:{3}", para.Id, DateTime.Now.ToString("HH:mm:ss"), i + 1, value);
                //ListBoxHelp.Add(lbRes, msg);
                sw.Stop();
                sb.AppendFormat("|id:{0}", para.Id);
                sb.AppendFormat("|url:{0}", url);
                sb.AppendFormat("|times:{0}s", sw.ElapsedMilliseconds * 1.0 / 1000);
                sbLog.AppendFormat("id:{0},{1}", para.Id, value); //TextHelper.ClearHtml(value)
                ListBoxHelp.Add(lbRes, sb.ToString() + "\r\n");
                LogHelper.WriteLog(sbLog.ToString());
                Thread.Sleep(ThreadSleep);
            }
            //ListBoxHelp.Add(lbRes, string.Format("执行完毕，耗时{0}秒", sw.ElapsedMilliseconds * 1.0 / 1000));
        }

        #endregion

        #region 辅助方法

        public List<Parameter> GetParams()
        {
            List<Parameter> ps = new List<Parameter>();
            //获取参数列表
            string[] paras = Regex.Split(txtIds.Text.Trim(), "\r\n");
            for (int i = 0; i < paras.Length; i++)
            {
                Parameter p = new Parameter()
                {
                    Id = i + 1,
                    Param = Regex.Split(paras[i], ",")
                };
                ps.Add(p);
            }
            return ps;
        }
        /// <summary>
        /// 根据URL和参数生成完整的URL
        /// </summary>
        /// <param name="url">带替换符的URL</param>
        /// <param name="paras">参数数组</param>
        /// <returns></returns>
        public string GenerateFullURL(string url, string[] paras)
        {
            string value = url;
            MatchCollection colls = Regex.Matches(url, @"(\{(\d+)\})");
            foreach (Match m in colls)
            {
                string text = m.Groups[1].Value.Replace("{", @"{").Replace("}", @"}");
                int.TryParse(m.Groups[2].Value, out int index);
                value = value.Replace(text, index < paras.Length ? paras[index] : "");
            }
            return value;
        }
        #endregion
    }
}
