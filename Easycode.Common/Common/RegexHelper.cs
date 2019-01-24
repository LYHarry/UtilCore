using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Easycode.Common
{
    /// <summary>
    /// 正则表达式帮助类
    /// </summary>
    public class RegexHelper
    {
        #region 正则判断,返回 bool 

        /// <summary>
        /// 是否为闰年
        /// </summary>
        /// <param name="year">待判断年份</param>
        /// <returns>闰年返回 true 否则 false </returns>
        public static bool IsLeapYear(int year)
        {
            return ((year % 4 == 0) && (year % 100 != 0)) || (year % 400 == 0);
        }

        /// <summary>
        /// 是否为电子邮箱地址
        /// </summary>
        /// <param name="email">待判断邮箱地址</param>
        /// <returns></returns>
        public static bool IsEmail(string email)
        {
            return Regex.IsMatch(email, "^[a-z0-9]+([-_.][a-z0-9]+)*@([a-z0-9]+[-.])+[a-z0-9]{2,5}.com$", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 是否为身份证号
        /// </summary>
        /// <param name="card">待判断身份证号</param>
        /// <returns></returns>
        public static bool IsIDCard(string card)
        {
            return Regex.IsMatch(card, @"(^\d{15}$)|(^\d{18}$)|(^\d{17}(\d|X|x)$)", RegexOptions.Singleline);
        }

        /// <summary>
        /// 是否为手机号码
        /// <para>格式：1[3|8|5|7|6|9]\d{9} </para>
        /// </summary>
        /// <param name="phone">待判断手机号</param>
        /// <returns></returns>
        public static bool IsPhone(string phone)
        {
            return Regex.IsMatch(phone, @"^[1][3|8|5|7|6|9]\d{9}$", RegexOptions.Singleline);
        }

        /// <summary>
        /// 是否为全数字
        /// </summary>
        /// <param name="num">待判断字符串</param>
        /// <returns></returns>
        public static bool IsNumber(string num)
        {
            return Regex.IsMatch(num, @"^(\d)*$", RegexOptions.Singleline);
        }

        /// <summary>
        /// 是否为字母加数字的字符串
        /// </summary>
        /// <param name="content">待判断字符串</param>
        /// <returns></returns>
        public static bool IsAlphanumeric(string content)
        {
            return Regex.IsMatch(content, (@"^[a-zA-Z0-9]+$"), RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 是否为小写字母
        /// </summary>
        /// <param name="content">待判断字符串</param>
        /// <returns></returns>
        public static bool IsLowerCase(string content)
        {
            return Regex.IsMatch(content, (@"^[a-z]+$"), RegexOptions.Singleline);
        }

        /// <summary>
        /// 是否为大写字母
        /// </summary>
        /// <param name="content">待判断字符串</param>
        /// <returns></returns>
        public static bool IsUpperCase(string content)
        {
            return Regex.IsMatch(content, (@"^[A-Z]+$"), RegexOptions.Singleline);
        }

        /// <summary>
        /// 是否为全字母字符串
        /// </summary>
        /// <param name="content">待判断字符串</param>
        /// <returns></returns>
        public static bool IsAlphabet(string content)
        {
            return Regex.IsMatch(content, (@"^[a-zA-Z]+$"), RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 是否为汉字
        /// </summary>
        /// <param name="content">待判断字符串</param>
        /// <returns></returns>
        public static bool IsChinaChar(string content)
        {
            return Regex.IsMatch(content, @"^[\u4e00-\u9fa5]+$", RegexOptions.Singleline);
        }

        /// <summary>
        /// 是否为普通常用字符串(汉字,大小字母,数字的字符串)
        /// </summary>
        /// <param name="content">待判断字符串</param>
        /// <returns></returns>
        public static bool IsNormalChar(string content)
        {
            return Regex.IsMatch(content, @"^[\u4e00-\u9fa5a-zA-Z0-9\s]+$", RegexOptions.Singleline);
        }

        /// <summary>
        /// 是否为特殊字符串(除汉字,大小字母,数字的字符串)
        /// </summary>
        /// <param name="content">待判断字符串</param>
        /// <returns></returns>
        public static bool IsSpechars(string content)
        {
            return !IsNormalChar(content);
        }

        /// <summary>
        /// 是否为IP地址
        /// </summary>
        /// <param name="ip">待判断ip地址</param>
        /// <returns></returns>
        public static bool IsIPAddress(string ip)
        {
            return Regex.IsMatch(ip, @"(?:(?:25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d)))\.){3}(?:25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d)))");
        }

        /// <summary>
        /// 是否为base64字符串
        /// </summary>
        /// <param name="content">待判断base64字符串</param>
        /// <returns></returns>
        public static bool IsBase64(string content)
        {
            return Regex.IsMatch(content, @"[A-Za-z0-9\+\/\=]*", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 是否为时间格式
        /// </summary>
        /// <param name="content">待判断字符串</param>
        /// <returns></returns>
        public static bool IsDataTime(string content)
        {
            if (DateTime.TryParse(content, out _))
                return true;
            return false;
        }

        /// <summary>
        /// 是否为Web Url地址(以Http/Https开头)
        /// </summary>
        /// <param name="url">待判断 url 地址</param>
        /// <returns></returns>
        public static bool IsWebURL(string url)
        {
            return Regex.IsMatch(url, @"^(http|https)\://([a-zA-Z0-9\.\-]+(\:[a-zA-Z0-9\.&%\$\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|localhost|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{1,10}))(\:[0-9]+)*(/($|[a-zA-Z0-9\.\,\?\'\\\+&%\$#\=~_\-]+))*$");
        }

        /// <summary>
        /// 是否为物理路径
        /// </summary>
        /// <param name="path">待判断路径</param>
        /// <returns></returns>
        public static bool IsPhysicsUrl(string path)
        {
            return Regex.IsMatch(path, @"([A-Za-z]):\\([\S]*)", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 是否含有SQL关键字
        /// </summary>
        /// <param name="content">待判断字符串</param>
        /// <returns></returns>
        public static bool HasSqlkeyword(string content)
        {
            return Regex.IsMatch(content, @"[select|update|delete|and|union|limit|</script>|iframe|truncate|drop|insert|alert]+", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 是否含有SQL字符
        /// </summary>
        /// <param name="content">要判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool HasSqlChar(string content)
        {
            return Regex.IsMatch(content, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\'|~|$|#|&|+|?|:|=]+", RegexOptions.Singleline);
        }


        #endregion

        #region 正则过滤字符串

        /// <summary>
        /// 移除Html标记
        /// </summary>
        /// <param name="content">待过滤字符串</param>
        /// <returns></returns>
        public static string RemoveHtml(string content)
        {
            return Regex.Replace(content, @"<[^>]*>", string.Empty, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 过滤HTML中的不安全标签
        /// </summary>
        /// <param name="content">待过滤字符串</param>
        /// <returns></returns>
        public static string DealUnSafeHtml(string content)
        {
            content = Regex.Replace(content, "'", "");
            content = Regex.Replace(content, @"(\<|\s+)o([a-z]+\s?=)", "", RegexOptions.IgnoreCase);
            content = Regex.Replace(content, @"(script|form|meta|behavior|style)([\s|:|>])+", "", RegexOptions.IgnoreCase);
            return content;
        }


        /// <summary>
        /// 过滤html,js,css代码
        /// </summary>
        /// <param name="html">待过滤字符串</param>
        /// <returns></returns>
        public static string DealHtmlCode(string html)
        {
            //过滤<script></script>标记
            html = Regex.Replace(html, @"<script[\s\S]+</script *>", "", RegexOptions.IgnoreCase);
            //过滤href=javascript: (<A>) 属性
            html = Regex.Replace(html, @" href *= *[\s\S]*script *:", "", RegexOptions.IgnoreCase);
            //过滤其它控件的on...事件
            html = Regex.Replace(html, @" no[\s\S]*=", " _disibledevent=", RegexOptions.IgnoreCase);
            //过滤iframe
            html = Regex.Replace(html, @"<iframe[\s\S]+</iframe *>", "", RegexOptions.IgnoreCase);
            //过滤frameset
            html = Regex.Replace(html, @"<frameset[\s\S]+</frameset *>", "", RegexOptions.IgnoreCase);
            //过滤frameset
            html = Regex.Replace(html, @"\<img[^\>]+\>", "", RegexOptions.IgnoreCase);
            //过滤p标签
            html = Regex.Replace(html, @"</p>", "", RegexOptions.IgnoreCase);
            //过滤p标签
            html = Regex.Replace(html, @"<p>", "", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"<[^>]*>", "", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"select", "", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"update", "", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"delete", "", RegexOptions.IgnoreCase);
            html = html.Replace(" ", "");
            html = html.Replace("\n", "");
            html = html.Replace("\r", "");
            html = html.Replace("\t", "");
            html = html.Replace("&nbsp;", "");
            html = html.Replace("&quot;", "'");
            html = html.Replace("&ldquo;", "\"");
            html = html.Replace("&;", "");
            html = html.Replace("</strong>", "");
            html = html.Replace("<strong>", "");
            return html;
        }


        /// <summary>
        /// 过滤SQL关键字语句
        /// </summary>
        /// <param name="content">待过滤字符串</param>
        /// <returns></returns>
        public static string DealSqlStr(string content)
        {
            return Regex.Replace(content, @"(insert|or|and|update|delete|like|union|limit|select|script|in|1=1|form|from|style|meta|behavior|drop|create|iframe|declare|char|truncate|exec|master|xp_cmdshell|/add|net user|execute|xp_|sp_|0x)+", "", RegexOptions.IgnoreCase);
        }


        /// <summary>
        /// 处理Sql危险字符
        /// </summary>
        /// <param name="content">待过滤字符串</param>
        /// <returns></returns>
        public static string DealUnSafeSqlStr(string content)
        {
            return Regex.Replace(content, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\'|~|$|#|&|+|?|:]+", "", RegexOptions.IgnoreCase);
        }


        /// <summary>
        /// 去除aspx页面头部的标记
        /// </summary>
        /// <param name="aspxhtml">待过滤aspx页面HTML(包含aspx头部标记的字符串)</param>
        /// <returns></returns>
        public static string RemoveAspxTag(string aspxhtml)
        {
            return Regex.Replace(aspxhtml, "\\<%@.+%>\n*", "");
        }

        /// <summary>
        /// 清理字符串
        /// </summary>
        /// <param name="content">待过滤字符串</param>
        /// <returns></returns>
        public static string CleanChar(string content)
        {
            return Regex.Replace(content, @"[^\w\.@-]", "");
        }

        /// <summary>
        /// 清除字符串中的空格
        /// </summary>
        /// <param name="content">待过滤字符串</param>
        /// <returns></returns>
        public static string CleanSpacing(string content)
        {
            return Regex.Replace(content, @"[\s]+", "");
        }

        /// <summary>
        /// 获取页面上所有的图片地址
        /// </summary>
        /// <param name="content">html页面内容</param>
        /// <returns></returns>
        public static string[] GetHtmlImgUrl(string content)
        {
            Regex reg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);
            MatchCollection matches = reg.Matches(content);
            int i = 0;
            string[] sUrlList = new string[matches.Count];
            foreach (Match match in matches)
            {
                sUrlList[i++] = match.Groups["imgUrl"].Value;
            }
            return sUrlList;
        }

        /// <summary>
        /// 获取页面上所有的链接地址
        /// </summary>
        /// <param name="content">html页面内容</param>
        /// <returns></returns>
        public static string[] GetHtmlUrl(string content)
        {
            Regex reg = new Regex(@"(?is)<a((?!href=)[\s\S])*href=['""]?(?<href>[^'""]*)[^<]*", RegexOptions.IgnoreCase);
            MatchCollection matches = reg.Matches(content);
            int i = 0;
            string[] sUrlList = new string[matches.Count];
            foreach (Match match in matches)
            {
                sUrlList[i++] = match.Groups["href"].Value;
            }
            return sUrlList;
        }

        #endregion
    }
}
