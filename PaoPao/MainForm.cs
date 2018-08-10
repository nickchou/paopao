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
            txtIds.Text = "1,2,3\r\n4,5,6";
            lblCopyMsg.Text = "";
        }
        #endregion

        /// <summary>
        /// 线程数量
        /// </summary>
        private int THREAD_NUM = 1;
        /// <summary>
        /// 每次执行暂停多少毫秒
        /// </summary>
        private int THREAD_SLEEP_MS = 1;

        public delegate void DelegName(List<Parameter> pros);

        #region 按钮Click事件
        /// <summary>
        /// 开始执行跑数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGO_Click(object sender, EventArgs e)
        {
            THREAD_NUM = (int)numDownThread.Value;
            if (THREAD_NUM < 1) THREAD_NUM = 1;
            THREAD_SLEEP_MS = (int)numDownMs.Value;
            //清理ListBox
            ListBoxHelp.Clear(lbRes);
            lblCopyMsg.Text = "";
            //获取产品
            List<Parameter> ps = GetParams();
            lbMsg.Text = string.Format("找到{0}条数据批次", ps.Count);
            //委托开始
            DelegName delegName = new DelegName(DoIt);
            AsyncCallback callback = new AsyncCallback(CallbackMethod);
            delegName.BeginInvoke(ps, callback, delegName);
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
            string logPath = Path.GetDirectoryName(this.GetType().Assembly.Location) + "\\logs\\RecordLog1";

            try
            {
                Process.Start(logPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("未找到指定的日志文件:" + logPath, "error");
                Path.GetDirectoryName(this.GetType().Assembly.Location);
            }
        }
        #endregion

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

                string value = "";
                value = http.CreateGet(url);
                //string msg = string.Format("line:{0},time:{1},num:{2},value:{3}", para.Id, DateTime.Now.ToString("HH:mm:ss"), i + 1, value);
                //ListBoxHelp.Add(lbRes, msg);
                sw.Stop();
                sb.AppendFormat("|id:{0}", para.Id);
                sb.AppendFormat("|url:{0}", url);
                sb.AppendFormat("|times:{0}s", sw.ElapsedMilliseconds * 1.0 / 1000);
                sbLog.AppendFormat("id:{0}\r\n{1}", para.Id, value);
                ListBoxHelp.Add(lbRes, sb.ToString() + "\r\n");
                LogHelper.WriteRecordLog(sbLog.ToString());
                Thread.Sleep(THREAD_SLEEP_MS);
            }

            //ListBoxHelp.Add(lbRes, string.Format("执行完毕，耗时{0}秒", sw.ElapsedMilliseconds * 1.0 / 1000));
        }
        public List<Parameter> GetParams()
        {
            List<Parameter> ps = new List<Parameter>();
            //获取参数列表
            string[] paras = Regex.Split(txtIds.Text, "\r\n");
            for (int i = 0; i < paras.Length; i++)
            {
                Parameter p = new Parameter()
                {
                    Id = i,
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
                int index = 0;
                int.TryParse(m.Groups[2].Value, out index);
                value = value.Replace(text, index < paras.Length ? paras[index] : "");
            }
            return value;
        }
    }
}
