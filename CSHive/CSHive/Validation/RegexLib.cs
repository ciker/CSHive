#region copyright info
//------------------------------------------------------------------------------
// <copyright company="ChaosStudio">
//     Copyright (c) 2002-2010 巧思工作室.  All rights reserved.
//     Contact:		Email:atwind@cszi.com , QQ:3329091
//		Link:		 http://www.cszi.com
// </copyright>
//------------------------------------------------------------------------------
#endregion

namespace CS.Validation
{
    /// <summary>
    /// 常用正则表达式库
    /// </summary>
    /// 
    /// <description class = "CS.Utility.RegexLib">
    /// 
    /// </description>
    /// 
    /// <history>
    ///     Create      :	    Atwind, 2008-6-13 21:40:58;
    ///  </history>
    public static class RegexLib
    {
        /// <summary>
        /// 标准用户的账号表达式 英文开头 4 ~ 32
        /// </summary>
        public const string ACCOUNT = "^[A-Za-z][A-Za-z0-9]{3,32}$";

        /// <summary>
        /// 标准用户的正则表达式 英文开头3-16 字符, 中文开头 2-12 字符
        /// </summary>
        public const string NICKNAME = "^[a-z][A-Za-z0-9]{2,16}$|^[\u4e00-\u9fa5][\u4e00-\u9fa5A-Za-z0-9]{1,11}$";

        /// <summary>
        /// 标准密码的正则表示式 8-32 字符
        /// </summary>
        public const string PASSWORD = @"^\S{8,32}$";

        /// <summary>
        /// 标准 Email 的正则表达式
        /// </summary>
        public const string EMAIL = @"^[-a-zA-Z0-9_\.]+\@([0-9A-Za-z][0-9A-Za-z-]+\.)+[A-Za-z]{2,5}$";

        /// <summary>
        /// 标准 QQ 的正则表示式
        /// 5 - 10 位数字
        /// </summary>
        public const string QQ = @"^[1-9]*[1-9][0-9]*$";

        /// <summary>
        /// 手机号码正则表达式
        /// </summary>
        public const string MOBILEPHONE = @"^(13|15|18)[0-9]{9}$";

        /// <summary>
        /// 电话正则表达式(包括国际电话)
        /// </summary>
        public const string TELEPHONE = @"^(?:[\+|(]?\d{1,4}[\)]?[s|-]+?)?(?:[\(]?\d{1,4}[\)|\s|-]?)?\d{3,11}(?:[s|-]+?\d{1,4})?$";

        /// <summary>
        /// 匹配格式：11位手机号码 3-4位区号，7-8位直播号码，1－4位分机号 如：12345678901、1234-12345678-1234
        /// Ref:http://www.cnblogs.com/flyker/archive/2009/02/12/1389435.html
        /// </summary>
        public const string PHONE = @"((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)";

        /// <summary>
        /// 使用 , 分割的 Id 字符串
        /// </summary>
        public const string ID_STRINGS = @"([\d+][,]?)+";

        /// <summary>
        /// 仅中文姓名 2~8
        /// <para>Note:按宪法規定，中國公民姓最長可以有3個中文字，名最長可以有4個中文字，即共7个字。</para>
        /// </summary>
        public const string CHINESE_NAME = @"^[\u4E00-\u9FA5\uF900-\uFA2D]{2,8}$";

        /// <summary>
        /// URL链接
        /// <para>http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)? </para>
        /// </summary>
        public const string URL = @"(http|ftp|https):\/\/[\w]+(.[\w]+)([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])";

        /// <summary>
        /// IPV4验证
        /// </summary>
        public const string IPV4 = @"^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$";

    }
}