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
using PaoPao.Winform;

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
            //窗体控件自适应
            FormAutoSize.X = this.Width;
            FormAutoSize.Y = this.Height;
            FormAutoSize.SetTag(this);
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
        /// <summary>
        /// 窗体控件自动缩放
        /// </summary>
        FormAutoResize FormAutoSize = new FormAutoResize();
        /// <summary>
        /// 事件信号量，默认为true=不阻塞，false=阻塞
        /// </summary>
        static EventWaitHandle EventHandle = new EventWaitHandle(true, EventResetMode.ManualReset);

        #region 按钮Click事件
        /// <summary>
        /// 开始执行跑数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGO_Click(object sender, EventArgs e)
        {
            if (btnGO.Text == "继续")
            {
                EventHandle.Set();
                btnGO.Enabled = false;//继续按钮可用
                btnGO.Text = "继续";
                btnPause.Enabled = true;//可暂停
                btnAbort.Enabled = true;//可以终止
            }
            else
            {
                ThreadPools.Clear();
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
                        Thread thread = new Thread(() => DoIt(para, thName))
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
                ListBoxHelp.Add(lbRes, $"{DateTime.Now.CnTime()}|共匹配{ps.Count}条数据,线程数：{ThreadPools.Count},每个线程：{PageSize}条数据" + "\r\n");
                lbMsg.Text = string.Format("找到{0}批次数据", ps.Count);
                //Start thread
                foreach (var th in ThreadPools)
                {
                    th.Start();
                }
            }
        }
        /// <summary>
        /// 线程暂停
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPause_Click(object sender, EventArgs e)
        {
            //暂停信号量
            EventHandle.Reset();
            ListBoxHelp.Add(lbRes, $"{DateTime.Now.CnTime()}|已暂停" + "\r\n");
            btnGO.Enabled = true;//继续按钮可用
            btnGO.Text = "继续";
            btnPause.Enabled = false;//不可暂停
            btnAbort.Enabled = true;//可终止
        }
        /// <summary>
        /// 线程终止
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAbort_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Thread t in ThreadPools)
                {
                    t.Abort();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                ThreadPools.Clear();
                EventHandle.Set();
                ListBoxHelp.Add(lbRes, $"{DateTime.Now.CnTime()}|已终止线程。" + "\r\n");
                btnGO.Enabled = true;//开始按钮可用
                btnGO.Text = "开始";
                btnPause.Enabled = false;//不可暂停
                btnAbort.Enabled = false;//不可终止
            }
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
        /// <summary>
        /// 窗体控件自适应大小
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Resize(object sender, EventArgs e)
        {
            FormAutoSize.form_Resize(this);
        }
        #endregion

        #region Thread Todo
        /// <summary>
        /// 抓网页数据核心方法
        /// </summary>
        /// <param name="pros"></param>
        /// <param name="threadName"></param>
        public void DoIt(List<Parameter> pros, string threadName)
        {
            ListBoxHelp.Add(lbRes, $"{DateTime.Now.CnTime()}|thread-{threadName} start..." + "\r\n");
            string TempUrl = txtURL.Text.Trim();
            for (int i = 0; i < pros.Count; i++)
            {
                //接收信号量，否则会阻塞达到暂停的目的
                EventHandle.WaitOne();
                //http
                HttpUtility http = new HttpUtility();
                StringBuilder sb = new StringBuilder();
                StringBuilder sbLog = new StringBuilder();
                //获取参数
                Parameter para = pros[i];
                //计时开始
                Stopwatch sw = new Stopwatch();
                sw.Start();
                sb.AppendFormat("{0}", DateTime.Now.CnTime());
                //根据参数重新匹配http url 
                string url = GenerateFullURL(TempUrl, para.Param);
                //请求http
                string value = TextHelper.ClearSpace(http.CreateGet(url));
                sw.Stop();
                sb.AppendFormat("|thread-{0}({1}/{2})serid:{3}|url:{4}", threadName, i + 1, pros.Count, para.Id, url);
                sb.AppendFormat("|times:{0}s", sw.ElapsedMilliseconds * 1.0 / 1000);
                sbLog.AppendFormat("serid:{0},{1}", para.Id, value); //TextHelper.ClearHtml(value)
                ListBoxHelp.Add(lbRes, sb.ToString() + "\r\n");
                LogHelper.WriteLog(sbLog.ToString());
                //最后一次不sleep了
                if (i < pros.Count)
                {
                    Thread.Sleep(ThreadSleep);
                }
                //执行完毕后计数
                CalculatedQuantity();
            }
            ListBoxHelp.Add(lbRes, $"{DateTime.Now.CnTime()}|thread-{threadName} finished!" + "\r\n");
        }
        /// <summary>
        /// 计算线程执行的数量
        /// </summary>
        public void CalculatedQuantity()
        {
            DoingCount++;
            //如果所有线程都已执行完毕，按钮初始化一下
            if (DoingCount == TodoCount)
            {
                Thread.Sleep(100); //等一会再去执行，否则可能会比跑数据线程会稍微快一点出来
                //最后调用控件无法直接访问，需要开启线程
                new Thread(() =>
                {
                    Action<int> action = (data) =>
                    {
                        btnGO.Enabled = true;//开始按钮可用
                        btnGO.Text = "开始";
                        btnPause.Enabled = false;//不可暂停
                        btnAbort.Enabled = false;//不可终止
                        lbRes.Items.Insert(0, $"{DateTime.Now.CnTime()}|全部执行完毕，serid为每条数据唯一索引，明细搜索文本日志" + "\r\n");
                    };
                    Invoke(action, 1);
                }).Start();
            }
        }
        #endregion

        #region 辅助方法
        /// <summary>
        /// 获取左侧列表参数，用于构造http请求参数
        /// </summary>
        /// <returns></returns>
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
