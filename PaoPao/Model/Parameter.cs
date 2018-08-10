using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaoPao.Model
{
    /// <summary>
    /// 匹配程序主窗体左边的代替参数
    /// </summary>
    public class Parameter
    {
        /// <summary>
        /// 线路ID
        /// </summary>
        public long Id { set; get; }
        /// <summary>
        /// 参数集合
        /// </summary>
        public string[] Param { set; get; }
    }
}
