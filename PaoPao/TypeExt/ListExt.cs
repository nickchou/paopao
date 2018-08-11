using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaoPao.TypeExt
{
    public static class ListExt
    {
        /// <summary>
        /// 泛型列表根据PageIndex和PageSize拆分出数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="PageIndex">主要下标索引从1开始</param>
        /// <param name="PageSize">要取的数据量大小</param>
        /// <returns></returns>
        public static List<T> GetPageDataByIndex<T>(this List<T> source, int PageIndex, int PageSize)
        {
            //List<T> returnValue = new List<T>();
            //int listCnt = source.Count;
            int index = PageIndex < 1 ? 1 : PageIndex;
            var ranageList = source.Skip((index - 1) * PageSize).Take(PageSize).ToList();
            return ranageList;
        }
    }
}
