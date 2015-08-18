#region copyright info
//------------------------------------------------------------------------------
// <copyright company="ChaosStudio">
//     Copyright (c) 2002-2010 ��˼������.  All rights reserved.
//     Contact:		Email:atwind@cszi.com , QQ:3329091
//		Link:		 http://www.cszi.com
// </copyright>
//------------------------------------------------------------------------------
#endregion

namespace CS.Validation
{
    /// <summary>
    /// ����������ʽ��
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
        /// ��׼�û����˺ű��ʽ Ӣ�Ŀ�ͷ 4 ~ 32
        /// </summary>
        public const string ACCOUNT = "^[A-Za-z][A-Za-z0-9]{3,32}$";

        /// <summary>
        /// ��׼�û���������ʽ Ӣ�Ŀ�ͷ3-16 �ַ�, ���Ŀ�ͷ 2-12 �ַ�
        /// </summary>
        public const string NICKNAME = "^[a-z][A-Za-z0-9]{2,16}$|^[\u4e00-\u9fa5][\u4e00-\u9fa5A-Za-z0-9]{1,11}$";

        /// <summary>
        /// ��׼����������ʾʽ 8-32 �ַ�
        /// </summary>
        public const string PASSWORD = @"^\S{8,32}$";

        /// <summary>
        /// ��׼ Email ��������ʽ
        /// </summary>
        public const string EMAIL = @"^[-a-zA-Z0-9_\.]+\@([0-9A-Za-z][0-9A-Za-z-]+\.)+[A-Za-z]{2,5}$";

        /// <summary>
        /// ��׼ QQ �������ʾʽ
        /// 5 - 10 λ����
        /// </summary>
        public const string QQ = @"^[1-9]*[1-9][0-9]*$";

        /// <summary>
        /// �ֻ�����������ʽ
        /// </summary>
        public const string MOBILEPHONE = @"^(13|15|18)[0-9]{9}$";

        /// <summary>
        /// �绰������ʽ(�������ʵ绰)
        /// </summary>
        public const string TELEPHONE = @"^(?:[\+|(]?\d{1,4}[\)]?[s|-]+?)?(?:[\(]?\d{1,4}[\)|\s|-]?)?\d{3,11}(?:[s|-]+?\d{1,4})?$";

        /// <summary>
        /// ƥ���ʽ��11λ�ֻ����� 3-4λ���ţ�7-8λֱ�����룬1��4λ�ֻ��� �磺12345678901��1234-12345678-1234
        /// Ref:http://www.cnblogs.com/flyker/archive/2009/02/12/1389435.html
        /// </summary>
        public const string PHONE = @"((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)";

        /// <summary>
        /// ʹ�� , �ָ�� Id �ַ���
        /// </summary>
        public const string ID_STRINGS = @"([\d+][,]?)+";

        /// <summary>
        /// ���������� 2~8
        /// <para>Note:���ܷ�Ҏ�����Ї����������L������3�������֣������L������4�������֣�����7���֡�</para>
        /// </summary>
        public const string CHINESE_NAME = @"^[\u4E00-\u9FA5\uF900-\uFA2D]{2,8}$";

        /// <summary>
        /// URL����
        /// <para>http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)? </para>
        /// </summary>
        public const string URL = @"(http|ftp|https):\/\/[\w]+(.[\w]+)([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])";

        /// <summary>
        /// IPV4��֤
        /// </summary>
        public const string IPV4 = @"^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$";

    }
}