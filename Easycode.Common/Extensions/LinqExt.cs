using System;
using System.Collections.Generic;
using System.Text;

namespace Easycode.Extensions
{
    /// <summary>
    /// Linq 扩展帮助类
    /// </summary>
    public static class LinqExtension
    {
        /// <summary>
        /// Linq DistinctBy 去重方法
        /// <para>可根据对象的字段来过滤重复对象</para>
        /// </summary>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>
       (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                    yield return element;
            }
        }
    }
}
