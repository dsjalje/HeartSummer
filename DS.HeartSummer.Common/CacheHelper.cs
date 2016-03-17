using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace DS.HeartSummer.Common
{
    public class CacheHelper
    {
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object Get(string key)
        {
            Cache cache = HttpRuntime.Cache;
            return cache[key];
        }
        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Set(string key, object value)
        {
            Cache cache = HttpRuntime.Cache;
            cache[key] = value;
        }
        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="key"></param>
        public static void Remove(string key)
        {
            Cache cache = HttpRuntime.Cache;
            cache.Remove(key);
        }
        /// <summary>
        /// 存在否
        /// </summary>
        /// <param name="key"></param>
        /// <returns>存在则返回true</returns>
        public static bool Exist(string key)
        {
            Cache cache = HttpRuntime.Cache;
            if (cache[key] == null)
            {
                return false;
            }
            else
            {
                return true;

            }
        }
    }
}
