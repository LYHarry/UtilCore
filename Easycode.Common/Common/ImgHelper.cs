using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Easycode.Common
{
    /// <summary>
    /// 图片帮助类
    /// </summary>
    public class ImgHelper
    {
        /// <summary>
        /// 充许的图片扩展名称
        /// </summary>
        public static string AllowExt { get; set; } = "gif|png|jpeg|jpg|bmp";

        /// <summary>
        /// 检测图片是否损坏(是否能正常预览)  
        /// </summary>
        /// <param name="imgPath">图片路径</param>
        /// <returns>是否损坏</returns>
        public static bool IsDamageImg(string imgPath)
        {
            var ext = FileHelper.Fi.GetExtName(imgPath, false);
            if (!FileHelper.Fi.IsSpecifyType(ext, AllowExt.Split('|')))
                throw new Exception($"暂不支持此{ext}类型文件");
            using (StreamReader sr = new StreamReader(imgPath, Encoding.UTF8))
            {
                string strContent = sr.ReadToEnd().ToLower();
                //过滤字符串，包含该字符表示图片损坏
                string filterStr = "request|<script|.getfolder|.createfolder|.deletefolder|.createdirectory|.deletedirectory|.saveas|wscript.shell|script.encode|server.|.createobject|execute|activexobject|language=";
                foreach (string item in filterStr.Split('|'))
                {
                    if (strContent.Contains(item))
                        return true;
                }
            }
            return false;
        }
    }
}
